﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using IrcSharp.Core.Messages;

namespace IrcSharp.Core.Connectivity
{
    public class ConnectionManager : IConnectionManager
    {
        public event EventHandler<MessageReceivedEventArgs> OnMessageReceived;

        private readonly TcpClient client = new TcpClient();
        private readonly CancellationTokenSource pollerCancellationTokenSource = new CancellationTokenSource();
        private StreamReader incomingMessageStream;
        private StreamWriter outgoingMessageStream;

        public bool Connected 
        { 
            get
            {
                return this.client.Connected;
            } 
        }

        public async Task ConnectAsync(IPAddress ipAddress, int port)
        {
            await this.InitiateConnection(() => this.client.ConnectAsync(ipAddress, port));
        }

        public async Task ConnectAsync(string hostName, int port)
        {
            await this.InitiateConnection(() => this.client.ConnectAsync(hostName, port));
        }

        public async Task DisconnectAsync()
        {
            this.pollerCancellationTokenSource.Cancel();
            await Task.Run(() => this.client.Close());
        }

        public async Task SendMessageAsync(ISendableMessage message)
        {
            await this.outgoingMessageStream.WriteAsync(message.ToString());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.incomingMessageStream != null)
                {
                    this.incomingMessageStream.Dispose();
                }
                if (this.outgoingMessageStream != null)
                {
                    this.outgoingMessageStream.Dispose();
                }
                pollerCancellationTokenSource.Cancel();
            }
        }

        private async Task InitiateConnection(Func<Task> connectionAction)
        {
            try
            {
                await connectionAction();
            }
            catch (SocketException ex)
            {
                throw new ConnectionFailedException("Could not connect to the server.", ex);
            }

            this.CreateStreams(this.client.GetStream());

            //i'm 99% sure i'm not using that cancellation token properly. 
            var token = this.pollerCancellationTokenSource.Token;
            
#pragma warning disable 4014
            Task.Run(() => this.Poll(), token);
#pragma warning restore 4014
        }

        private void CreateStreams(Stream ns)
        {
            this.incomingMessageStream = new StreamReader(ns);
            this.outgoingMessageStream = new StreamWriter(ns) {AutoFlush = true};
        }

        private async Task Poll()
        {
            while (this.Connected)
            {
                var result = await this.incomingMessageStream.ReadLineAsync();
                this.RaiseMessageReceivedEvent(result);
            }
        }

        private void RaiseMessageReceivedEvent(string message)
        {
            if (this.OnMessageReceived != null)
            {
                this.OnMessageReceived(this, new MessageReceivedEventArgs { Message = message });
            }
        }
    }
}