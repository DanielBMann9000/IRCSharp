using System.Text;

namespace IrcSharp.Core.Messages.Sendable
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

        public string ToMessage()
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