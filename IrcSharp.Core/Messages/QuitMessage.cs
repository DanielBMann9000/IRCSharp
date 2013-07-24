using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class QuitMessage : ISendableMessage
    {
        public string Reason { get; private set; }

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