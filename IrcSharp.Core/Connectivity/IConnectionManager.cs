﻿using System;
using System.Net;
using System.Threading.Tasks;
using IrcSharp.Core.Messages;

namespace IrcSharp.Core.Connectivity
{
    public interface IConnectionManager : IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> OnMessageReceived;
        bool Connected { get; }
        Task ConnectAsync(IPAddress ipAddress, int port);
        Task ConnectAsync(string hostName, int port);
        Task SendMessageAsync(ISendableMessage message);
        Task DisconnectAsync();
    }
}