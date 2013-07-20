using System;
using System.Threading.Tasks;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Propagation;
using IrcSharp.Core.Messages.Receivable;
using IrcSharp.Core.Messages.Sendable;

namespace IrcSharp.Core.Connectivity
{
    public class IrcConnection : IDisposable
    {
        public MessagePropagator MessagePropagator { get; private set; }
        private readonly IConnectionManager connectionManager;
        private bool canSend;

        public IrcConnection(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            this.connectionManager.OnMessageReceived += this.ParseMessage;
            this.MessagePropagator = new MessagePropagator();

            this.MessagePropagator.OnWelcomeResponseMessage += this.ReadyToSendCommands;
            this.MessagePropagator.OnPingMessage += SendPongResponse;
            
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
            this.MessagePropagator.RouteMessage(e.Message);
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
                this.MessagePropagator.OnPingMessage -= this.SendPongResponse;
                this.connectionManager.Dispose();
            }
        }
        #endregion
    }
}
