using System;
using System.Net;
using System.Threading.Tasks;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Connectivity
{
    public interface ISocketConnection : IDisposable
    {
        event EventHandler<MessageEventArgs> OnMessageReceived;
        bool Connected { get; }
        Task ConnectAsync(IPAddress ipAddress, int port);
        Task ConnectAsync(string hostName, int port);
        Task SendMessageAsync(ISendableMessage message);
        Task DisconnectAsync();
    }
}