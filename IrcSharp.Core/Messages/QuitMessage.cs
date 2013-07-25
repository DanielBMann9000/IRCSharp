using System.Text;

using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class QuitMessage : ISendableMessage, IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string Reason { get; private set; }

        internal QuitMessage(IrcUserInfo userInfo, string reason = null) : this(reason)
        {
            this.UserInfo = userInfo;
        }

        public QuitMessage()
        {
        }

        public QuitMessage(string reason)
        {
            this.Reason = reason;
        }

        string ISendableMessage.ToMessage()
        {
            var message = new StringBuilder();
            message.Append("QUIT");
            if (!string.IsNullOrWhiteSpace(this.Reason))
            {
                message.AppendFormat(" :{0}", this.Reason);
            }
            message.Append("\r\n");
            return message.ToString();
        }
    }
}