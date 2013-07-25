using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class SquitMessage : ISendableMessage, IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string Server { get; private set; }
        public string Reason { get; private set; }

        internal SquitMessage(IrcUserInfo userInfo, string server, string reason) : this(server, reason)
        {
            this.UserInfo = userInfo;
        }

        public SquitMessage(string server, string reason)
        {
            this.Server = server;
            this.Reason = reason;
        }

        string ISendableMessage.ToMessage()
        {
            return string.Format("SQUIT {0} :{1}\r\n", this.Server, this.Reason);
        }
    }
}