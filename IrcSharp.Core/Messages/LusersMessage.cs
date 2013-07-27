using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class LusersMessage : ISendableMessage
    {
        public string Mask { get; private set; }
        public string Target { get; private set; }
        public LusersMessage() {}

        public LusersMessage(string mask)
        {
            this.Mask = mask;
        }

        public LusersMessage(string mask, string target) : this(mask)
        {
            this.Target = target;
        }
        
        public string ToMessage()
        {
            var message = new StringBuilder();
            message.Append("LUSERS");
            if (!string.IsNullOrWhiteSpace(this.Mask))
            {
                message.AppendFormat(" {0}", this.Mask);
            }

            if (!string.IsNullOrWhiteSpace(this.Target))
            {
                message.AppendFormat(" {0}", this.Target);
            }

            message.Append("\r\n");

            return message.ToString();
        }
    }
}