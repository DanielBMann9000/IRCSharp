using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class LinksMessage : ISendableMessage
    {
        public string RemoteServer { get; private set; }
        public string ServerMask { get; private set; }
        public LinksMessage() { }

        public LinksMessage(string serverMask)
        {
            this.ServerMask = serverMask;
            
        }

        public LinksMessage(string serverMask, string remoteServer) : this(serverMask)
        {
            this.RemoteServer = remoteServer;
        }

        public string ToMessage()
        {
            var message = new StringBuilder();
            message.Append("LINKS");
            if (!string.IsNullOrWhiteSpace(this.RemoteServer))
            {
                message.AppendFormat(" {0}", this.RemoteServer);
            }

            if (!string.IsNullOrWhiteSpace(this.ServerMask))
            {
                message.AppendFormat(" {0}", this.ServerMask);
            }

            message.Append("\r\n");

            return message.ToString();
        }
    }
}