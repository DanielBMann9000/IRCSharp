using System;
using System.Collections.Generic;
using System.Linq;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages.Propagation
{
    public class MessagePropagator
    {
        #region Events
        public event EventHandler<Receivable.UnknownMessage> OnUnknownMessage;
        #region Message Details (3)
        #region Connection Registration  (3.1)

        public event EventHandler<Sendable.PassMessage> OnPassMessageSending;
        public event EventHandler<Sendable.PassMessage> OnPassMessageSent;

        public event EventHandler<Receivable.NickMessage> OnNickMessageReceived;
        public event EventHandler<Sendable.NickMessage> OnNickMessageSending;
        public event EventHandler<Sendable.NickMessage> OnNickMessageSent;

        public event EventHandler<Sendable.UserMessage> OnUserMessageSending;
        public event EventHandler<Sendable.UserMessage> OnUserMessageSent;

        #endregion Connection Registration (3.1)

        #region Channel Operations (3.2)
        public event EventHandler<Sendable.JoinMessage> OnJoinMessageSending;
        public event EventHandler<Sendable.JoinMessage> OnJoinMessageSent;

        public event EventHandler<Sendable.PartMessage> OnPartMessageSending;
        public event EventHandler<Sendable.PartMessage> OnPartMessageSent;

        public event EventHandler<Sendable.ChannelModeMessage> OnChannelModeMessageSending;
        public event EventHandler<Sendable.ChannelModeMessage> OnChannelModeMessageSent;

        public event EventHandler<Sendable.TopicMessage> OnTopicMessageSending;
        public event EventHandler<Sendable.TopicMessage> OnTopicMessageSent;

        public event EventHandler<Sendable.NamesMessage> OnNamesMessageSending;
        public event EventHandler<Sendable.NamesMessage> OnNamesMessageSent;

        public event EventHandler<Sendable.ListMessage> OnListMessageSending;
        public event EventHandler<Sendable.ListMessage> OnListMessageSent;

        public event EventHandler<Sendable.InviteMessage> OnInviteMessageSending;
        public event EventHandler<Sendable.InviteMessage> OnInviteMessageSent;

        public event EventHandler<Sendable.KickMessage> OnKickMessageSending;
        public event EventHandler<Sendable.KickMessage> OnKickMessageSent;
        
        #endregion Channel Operations (3.2)

        #region Sending messages (3.3)
        public event EventHandler<Sendable.PrivMsgMessage> OnPrivMsgMessageSending;
        public event EventHandler<Sendable.PrivMsgMessage> OnPrivMsgMessageSent;

        public event EventHandler<Sendable.NoticeMessage> OnNoticeMessageSending;
        public event EventHandler<Sendable.NoticeMessage> OnNoticeMessageSent;
        #endregion Sending messages (3.3)

        #region Server queries and commands (3.4)
        #endregion Server queries and commands (3.4)

        #region Service queries and commands (3.5)
        #endregion Service queries and commands (3.5)

        #region User based queries (3.6)
        #endregion User based queries (3.6)

        #region Miscellaneous messages (3.7)
        public event EventHandler<Receivable.PingMessage> OnPingMessageReceived;
        public event EventHandler<Sendable.PongMessage> OnPongMessageSending;
        public event EventHandler<Sendable.PongMessage> OnPongMessageSent;
        #endregion Miscellaneous messages (3.7)
        #endregion Message Details (3)

        #region Replies (5)
        #region Command responses (5.1)
        public event EventHandler<Receivable.GenericNumericResponseMessage> OnWelcomeResponseMessageReceived;
        #endregion Command responses (5.1)

        #region Error replies (5.2)
        public event EventHandler<Receivable.NotRegisteredNumericResponseMessage> OnNotRegisteredResponseMessageReceived;
        #endregion Error replies (5.2)

        #endregion Replies (5)
        #endregion Events

        internal delegate void ReceivedPropagator(IrcUserInfo identity, string arguments);
        
        private readonly IEnumerable<Tuple<string, ReceivedPropagator>> receivedPropagators;
        private readonly Dictionary<Type, Action<ISendableMessage>> sendingPropagators;
        private readonly Dictionary<Type, Action<ISendableMessage>> sentPropagators;
        
        internal MessagePropagator()
        {
            this.receivedPropagators = this.GetReceivedMessagePropagators<ReceivedMessagePropagatorAttribute, ReceivedPropagator>();

            //not a fan of this, but i want sending/sent message propagation in place even if it's not great. 
            this.sendingPropagators = new Dictionary<Type, Action<ISendableMessage>>
                                  {
                                      { typeof(Sendable.PassMessage), msg => this.RouteSendableMessage(msg, this.OnPassMessageSending) },
                                      { typeof(Sendable.NickMessage), msg => this.RouteSendableMessage(msg, this.OnNickMessageSending) },
                                      { typeof(Sendable.UserMessage), msg => this.RouteSendableMessage(msg, this.OnUserMessageSending) },
                                      { typeof(Sendable.JoinMessage), msg => this.RouteSendableMessage(msg, this.OnJoinMessageSending) },
                                      { typeof(Sendable.PartMessage), msg => this.RouteSendableMessage(msg, this.OnPartMessageSending) },
                                      { typeof(Sendable.ChannelModeMessage), msg => this.RouteSendableMessage(msg, this.OnChannelModeMessageSending) },
                                      { typeof(Sendable.TopicMessage), msg => this.RouteSendableMessage(msg, this.OnTopicMessageSending) },
                                      { typeof(Sendable.NamesMessage), msg => this.RouteSendableMessage(msg, this.OnNamesMessageSending) },
                                      { typeof(Sendable.ListMessage), msg => this.RouteSendableMessage(msg, this.OnListMessageSending) },
                                      { typeof(Sendable.InviteMessage), msg => this.RouteSendableMessage(msg, this.OnInviteMessageSending) },
                                      { typeof(Sendable.KickMessage), msg => this.RouteSendableMessage(msg, this.OnKickMessageSending) },
                                      { typeof(Sendable.PrivMsgMessage), msg => this.RouteSendableMessage(msg, this.OnPrivMsgMessageSending) },
                                      { typeof(Sendable.NoticeMessage), msg => this.RouteSendableMessage(msg, this.OnNoticeMessageSending) },
                                      { typeof(Sendable.PongMessage), msg => this.RouteSendableMessage(msg, this.OnPongMessageSending) }
                                  };

            this.sentPropagators = new Dictionary<Type, Action<ISendableMessage>>
                                  {
                                      { typeof(Sendable.PassMessage), msg => this.RouteSendableMessage(msg, this.OnPassMessageSent) },
                                      { typeof(Sendable.NickMessage), msg => this.RouteSendableMessage(msg, this.OnNickMessageSent) },
                                      { typeof(Sendable.UserMessage), msg => this.RouteSendableMessage(msg, this.OnUserMessageSent) },
                                      { typeof(Sendable.JoinMessage), msg => this.RouteSendableMessage(msg, this.OnJoinMessageSent) },
                                      { typeof(Sendable.PartMessage), msg => this.RouteSendableMessage(msg, this.OnPartMessageSent) },
                                      { typeof(Sendable.ChannelModeMessage), msg => this.RouteSendableMessage(msg, this.OnChannelModeMessageSent) },
                                      { typeof(Sendable.TopicMessage), msg => this.RouteSendableMessage(msg, this.OnTopicMessageSent) },
                                      { typeof(Sendable.NamesMessage), msg => this.RouteSendableMessage(msg, this.OnNamesMessageSent) },
                                      { typeof(Sendable.ListMessage), msg => this.RouteSendableMessage(msg, this.OnListMessageSent) },
                                      { typeof(Sendable.InviteMessage), msg => this.RouteSendableMessage(msg, this.OnInviteMessageSent) },
                                      { typeof(Sendable.KickMessage), msg => this.RouteSendableMessage(msg, this.OnKickMessageSent) },
                                      { typeof(Sendable.PrivMsgMessage), msg => this.RouteSendableMessage(msg, this.OnPrivMsgMessageSent) },
                                      { typeof(Sendable.NoticeMessage), msg => this.RouteSendableMessage(msg, this.OnNoticeMessageSent) },
                                      { typeof(Sendable.PongMessage), msg => this.RouteSendableMessage(msg, this.OnPongMessageSent) }
                                  };

        }

        internal void RouteReceivedMessage(string message)
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

            var messagePropagators = this.receivedPropagators.Where(p => p.Item1 == command).ToList();

            if (messagePropagators.Count == 0)
            {
                if (this.OnUnknownMessage != null)
                {
                    this.OnUnknownMessage(this, new Receivable.UnknownMessage(message));
                }
            }
            else
            {
                // It's unlikely that one message will have multiple propagators, but I support it anyway.
                foreach (var messagePropagator in messagePropagators)
                {
                    messagePropagator.Item2(parsedIdentity, commandArguments);
                }
            }
        }

        internal void RouteSendingMessage(ISendableMessage message)
        {
            sendingPropagators[message.GetType()](message);
        }

        internal void RouteSentMessage(ISendableMessage message)
        {
            sentPropagators[message.GetType()](message);
        }

        private void RouteSendableMessage<T>(ISendableMessage message, EventHandler<T> eh)
        {
            if (eh != null)
            {
                eh(this, (T)message);
            }
        }

        #region Received Message Propagation
        [ReceivedMessagePropagator("001")]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private void PropagateWelcomeNumericResponseMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnWelcomeResponseMessageReceived != null)
            {
                var startIndex = arguments.IndexOf(":", StringComparison.Ordinal);
                this.OnWelcomeResponseMessageReceived(this, new Receivable.GenericNumericResponseMessage("001", arguments.Substring(startIndex + 1, arguments.Length - startIndex - 1)));
            }
        }

        [ReceivedMessagePropagator("451")]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private void PropagateNotRegisteredNumericResponseMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnNotRegisteredResponseMessageReceived != null)
            {
                var args = TokenizeArguments(arguments);
                this.OnNotRegisteredResponseMessageReceived(this, new Receivable.NotRegisteredNumericResponseMessage(args[1], args[2]));
            }
        }

        [ReceivedMessagePropagator("PING")]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private void PropagatePingMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnPingMessageReceived != null)
            {
                this.OnPingMessageReceived(this, new Receivable.PingMessage(arguments.Remove(0, 1)));
            }
        }

        [ReceivedMessagePropagator("NICK")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateNickMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnNickMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                this.OnNickMessageReceived(this, new Receivable.NickMessage(identity, tokenizedArguments[0]));
            }
        }
        #endregion Message Propagation

        // this method belongs somewhere else, and I'm not 100% sure it will tokenize every IRC message properly
        private static List<string> TokenizeArguments(string arguments)
        {
            var tokenized = new List<string>();
            var everythingToTheColon = new string(arguments.TakeWhile(e => e != ':').ToArray());
            var everythingAfterTheColon = new string(arguments.Skip(everythingToTheColon.Length + 1).ToArray());
            tokenized.AddRange(everythingToTheColon.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            if (!string.IsNullOrEmpty(everythingAfterTheColon))
            {
                tokenized.Add(everythingAfterTheColon);
            }
            return tokenized;
        }
    }
}
