using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class ServlistMessage : ISendableMessage
    {
        public string Mask { get; private set; }
        public string Type { get; private set; }

        public ServlistMessage(){}

        public ServlistMessage(string mask)
        {
            this.Mask = mask;
        }

        public ServlistMessage(string mask, string type) : this(mask)
        {
            this.Type = type;
        }

        public string ToMessage()
        {
            var message = new StringBuilder();
            message.Append("SERVLIST");
            if (!string.IsNullOrWhiteSpace(this.Mask))
            {
                message.AppendFormat(" {0}", this.Mask);
            }

            if (!string.IsNullOrWhiteSpace(this.Type))
            {
                message.AppendFormat(" {0}", this.Type);
            }

            message.Append("\r\n");

            return message.ToString();
        }
    }
}