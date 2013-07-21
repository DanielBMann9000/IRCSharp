using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;

namespace IrcSharp.Core.Tests.Unit
{
    public class FakeSocketConnection : ISocketConnection
    {
        public readonly List<string> messages = new List<string>();

        public ReadOnlyCollection<string> Messages
        {
            get
            {
                return new ReadOnlyCollection<string>(messages);
            }
        }

        public event EventHandler<MessageEventArgs> OnMessageReceived;
        public event EventHandler<MessageEventArgs> OnMessageSent;
        public bool Connected { get; private set; }

        
#pragma warning disable 1998
        public async Task ConnectAsync(IPAddress ipAddress, int port)
#pragma warning restore 1998
        {
            //Fire a fake "welcome" event so that the client starts sending commands normally
            this.SendFakeMessage(":localhost.com 001 DBM :Welcome to the Internet Relay Network DBM");
            this.Connected = true;
        }

        
#pragma warning disable 1998
        public async Task ConnectAsync(string hostName, int port)
#pragma warning restore 1998
        {
            //Fire a fake "welcome" event so that the client starts sending commands normally
            this.SendFakeMessage(":localhost.com 001 DBM :Welcome to the Internet Relay Network DBM");
            this.Connected = true;
        }

#pragma warning disable 1998
        public async Task SendMessageAsync(ISendableMessage message)
#pragma warning restore 1998
        {
            if (!this.Connected)
            {
                throw new ConnectionFailedException(null, null);
            }
            if (this.OnMessageSent != null)
            {
                this.OnMessageSent(this, new MessageEventArgs { Message = message.ToString() });
            }
            messages.Add(message.ToString());
        }

#pragma warning disable 1998
        public async Task DisconnectAsync()
#pragma warning restore 1998
        {
            this.Connected = false;
        }

        public void Dispose()
        {
        }

        public void SendFakeMessage(string fakeMessage)
        {
            if (OnMessageReceived != null)
            {
                OnMessageReceived(this, new MessageEventArgs { Message = fakeMessage });
            }   
        }
    }
}
