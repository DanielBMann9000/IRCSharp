using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Propagation;
using IrcSharp.Core.Messages.Receivable;
using IrcSharp.Core.Messages.Sendable;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Connectivity
{
    public class IrcConnection : IDisposable
    {
        #region Events
        public event EventHandler<UnknownMessage> OnRawMessage;
        public event EventHandler<UnknownMessage> OnUnknownMessage;
        #region Message Details (3)
        #region Connection Registration  (3.1)
        public event EventHandler<Messages.Receivable.NickMessage> OnNickMessage;
        #endregion Connection Registration (3.1)

        #region Channel Operations (3.2)
        #endregion Channel Operations (3.2)

        #region Sending messages (3.3)
        #endregion Sending messages (3.3)

        #region Server queries and commands (3.4)
        #endregion Server queries and commands (3.4)

        #region Service queries and commands (3.5)
        #endregion Service queries and commands (3.5)

        #region User based queries (3.6)
        #endregion User based queries (3.6)

        #region Miscellaneous messages (3.7)
        public event EventHandler<PingMessage> OnPingMessage;
        #endregion Miscellaneous messages (3.7)
        #endregion Message Details (3)

        #region Replies (5)
        #region Command responses (5.1)
        public event EventHandler<GenericNumericResponseMessage> OnWelcomeResponseMessage;
        #endregion Command responses (5.1)

        #region Error replies (5.2)
        public event EventHandler<NotRegisteredNumericResponseMessage> OnNotRegisteredResponseMessage;
        #endregion Error replies (5.2)
        
        #endregion Replies (5)
        #endregion Events

        private readonly IConnectionManager connectionManager;
        private bool canSend;

        internal delegate void MessagePropagator(IrcUserInfo identity, string arguments);

        private readonly IEnumerable<Tuple<MessagePropagatorAttribute, MessagePropagator>> propagators;

        public IrcConnection(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            this.connectionManager.OnMessageReceived += this.ParseMessage;
            this.OnWelcomeResponseMessage += this.ReadyToSendCommands;
            this.OnPingMessage += SendPongResponse;
            propagators = this.GetMessagePropagators<MessagePropagatorAttribute, MessagePropagator>();
        }

        public async Task ConnectAsync(string nick, string realName, string server, int port)
        {
            await this.connectionManager.ConnectAsync(server, port);
            if (this.connectionManager.Connected)
            {
                await this.SendMessageInternalAsync(new Messages.Sendable.NickMessage(nick));
                await this.SendMessageInternalAsync(new UserMessage(nick, UserMessage.Mode.None, realName));
            }
        }

        private void ReadyToSendCommands(object sender, GenericNumericResponseMessage e)
        {
            // ugh
            this.canSend = true;
        }

        public async Task SendMessageAsync(ISendableMessage messageToSend)
        {
            // this is horrible. Need to implement a better method of polling for "connectedness" and allow timeouts to occur.
            while (!this.canSend)
            {
                await Task.Delay(500);
            }

            await this.connectionManager.SendMessageAsync(messageToSend);
        }

        // i hate this
        private async Task SendMessageInternalAsync(ISendableMessage messageToSend)
        {
            await this.connectionManager.SendMessageAsync(messageToSend);
        }

        private async void SendPongResponse(object sender, PingMessage e)
        {
            await this.SendMessageInternalAsync(new PongMessage(e.Value));
        }

        private void ParseMessage(object sender, MessageReceivedEventArgs e)
        {
            if (this.OnRawMessage != null)
            {
                this.OnRawMessage(this, new UnknownMessage(e.Message));
            }
            
            this.PropagateMessage(e.Message);
        }

        // todo: this method is horrible. refactor with great enthusiasm.
        public void PropagateMessage(string message)
        {
            string command;
            var commandArguments = string.Empty;
            IrcUserInfo parsedIdentity = null;

            if (message.StartsWith(":"))
            {
                var originator = message.Substring(1, message.IndexOf(" ", StringComparison.Ordinal) - 1);
                command = message.Split(' ').Skip(1).Take(1).First();
                commandArguments = message.Substring(message.IndexOf(command, StringComparison.Ordinal) + command.Length + 1);
                if (originator.Contains("!") && originator.Contains("@"))
                {
                    var exclaimationLocation = originator.IndexOf("!", StringComparison.Ordinal);
                    var atSignLocation = originator.IndexOf("@", StringComparison.Ordinal);
                    var nick = originator.Substring(0, exclaimationLocation);
                    var identity = originator.Substring(exclaimationLocation + 1, atSignLocation - exclaimationLocation - 1);
                    var host = originator.Substring(atSignLocation + 1, originator.Length - atSignLocation - 1);
                    parsedIdentity = new IrcUserInfo(nick, identity, host);
                }
                else
                {
                    parsedIdentity = new IrcUserInfo(originator);
                }
            }
            else
            {
                command = message.Split(' ').Take(1).First();
                if (message.Length != command.Length)
                {
                    commandArguments = message.Substring(message.IndexOf(command, StringComparison.Ordinal) + command.Length + 1);
                }
            }

            var propagator = this.propagators.Where(p => p.Item1.CommandName == command);
            if (propagator.Any())
            {
                propagator.First().Item2(parsedIdentity, commandArguments);
            }
            else
            {
                if (this.OnUnknownMessage != null)
                {
                    this.OnUnknownMessage(this, new UnknownMessage(message));
                }
            }
        }

        //TODO: need to start splitting this class up, it's getting huge
        #region Message Propagation
        [MessagePropagator("001")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateWelcomeNumericResponseMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnWelcomeResponseMessage != null)
            {
                var startIndex = arguments.IndexOf(":", StringComparison.Ordinal);
                this.OnWelcomeResponseMessage(this, new GenericNumericResponseMessage("001", arguments.Substring(startIndex+1, arguments.Length - startIndex-1)));
            }
        }

        [MessagePropagator("451")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateNotRegisteredNumericResponseMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnNotRegisteredResponseMessage != null)
            {
                var args = TokenizeArguments(arguments);
                this.OnNotRegisteredResponseMessage(this, new NotRegisteredNumericResponseMessage(args[1], args[2]));
            }
        }

        [MessagePropagator("PING")]
        // ReSharper disable once UnusedMember.Local
        private void PropagatePingMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnPingMessage != null)
            {
                this.OnPingMessage(this, new PingMessage(arguments.Remove(0,1)));
            }
        }

        [MessagePropagator("NICK")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateNickMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnNickMessage != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                this.OnNickMessage(this, new Messages.Receivable.NickMessage(identity, tokenizedArguments[0]));
            }
        }
        #endregion Message Propagation

        // this method belongs somewhere else, and I'm not 100% sure it will tokenize every IRC message properly
        private static List<string> TokenizeArguments(string arguments)
        {
            var tokenized = new List<string>();
            var everythingToTheColon = new string(arguments.TakeWhile(e => e != ':').ToArray());
            var everythingAfterTheColon = new string(arguments.Skip(everythingToTheColon.Length+1).ToArray());
            tokenized.AddRange(everythingToTheColon.Split(new []{" "}, StringSplitOptions.RemoveEmptyEntries));
            if (!string.IsNullOrEmpty(everythingAfterTheColon))
            {
                tokenized.Add(everythingAfterTheColon);
            }
            return tokenized;
        }

        #region IDisposable implementation
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.connectionManager.OnMessageReceived -= this.ParseMessage;
                this.OnPingMessage -= this.SendPongResponse;
                this.connectionManager.Dispose();
            }
        }
        #endregion
    }
}
