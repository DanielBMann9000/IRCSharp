using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class ConnectMessage : ISendableMessage
    {
        public string TargetServer { get; private set; }
        public int Port { get; private set; }
        public string RemoteServer { get; private set; }

        public ConnectMessage(string targetServer, int port)
        {
            this.TargetServer = targetServer;
            this.Port = port;
        }

        public ConnectMessage(string targetServer, int port, string remoteServer) : this(targetServer, port)
        {
            this.RemoteServer = remoteServer;
        }

        public string ToMessage()
        {
            var message = new StringBuilder();
            message.AppendFormat("CONNECT {0} {1}", this.TargetServer, this.Port);
            if (!string.IsNullOrWhiteSpace(this.RemoteServer))
            {
                message.AppendFormat(" {0}", this.RemoteServer);
            }

            message.Append("\r\n");

            return message.ToString();
        }
    }
}