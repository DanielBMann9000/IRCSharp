﻿using System;
using System.Net;
using System.Threading.Tasks;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Propagation;
using IrcSharp.Core.Messages.Receivable;
using IrcSharp.Core.Messages.Sendable;

namespace IrcSharp.Core.Connectivity
{
    public class IrcConnection : IDisposable
    {
        public event EventHandler<UnknownMessage> OnRawMessageReceived;
        public event EventHandler<UnknownMessage> OnRawMessageSent;

        private readonly ISocketConnection connectionManager;
        private bool canSend;

        public MessagePropagator MessagePropagator { get; private set; }
        public bool Connected 
        {
            get
            {
                return connectionManager.Connected;
            }
        }
        public IrcConnection(ISocketConnection connectionManager)
        {
            this.connectionManager = connectionManager;
            this.connectionManager.OnMessageReceived += this.ParseMessage;
            this.MessagePropagator = new MessagePropagator();

            this.MessagePropagator.OnWelcomeResponseMessage += this.ReadyToSendCommands;
            this.MessagePropagator.OnPingMessage += SendPongResponse;
        }

        public IrcConnection() : this(new SocketConnection())
        {
            
        }

        public async Task ConnectAsync(string nick, string realName, string server, int port)
        {
            await this.connectionManager.ConnectAsync(server, port);
            await InitializeConnection(nick, realName);
        }

        public async Task ConnectAsync(string nick, string realName, IPAddress server, int port)
        {
            await this.connectionManager.ConnectAsync(server, port);
            await InitializeConnection(nick, realName);
        }

        public async Task DisconnectAsync()
        {
            await this.connectionManager.DisconnectAsync();
        }

        private async Task InitializeConnection(string nick, string realName)
        {
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

            if (this.OnRawMessageSent != null)
            {
                this.OnRawMessageSent(this, new UnknownMessage(messageToSend.ToString()));
            }
            await this.connectionManager.SendMessageAsync(messageToSend);
        }

        // i hate this
        private async Task SendMessageInternalAsync(ISendableMessage messageToSend)
        {
            if (this.OnRawMessageSent != null)
            {
                this.OnRawMessageSent(this, new UnknownMessage(messageToSend.ToString()));
            }
            await this.connectionManager.SendMessageAsync(messageToSend);
        }

        private async void SendPongResponse(object sender, PingMessage e)
        {
            await this.SendMessageInternalAsync(new PongMessage(e.Value));
        }

        private void ParseMessage(object sender, MessageEventArgs e)
        {
            if (this.OnRawMessageReceived != null)
            {
                this.OnRawMessageReceived(this, new UnknownMessage(e.Message));
            }
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
