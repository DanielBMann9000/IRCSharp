using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class WhoMessage : ISendableMessage 
    {
        public string Mask { get; private set; }
        public bool OperatorsOnly { get; private set; }

        public WhoMessage() {}

        public WhoMessage(string mask, bool operatorsOnly = false)
        {
            this.Mask = mask;
            this.OperatorsOnly = operatorsOnly;
        }

        public string ToMessage()
        {
            var message = new StringBuilder();
            message.Append("WHO");
            if (!string.IsNullOrWhiteSpace(this.Mask))
            {
                message.AppendFormat(" {0}", this.Mask);

                if (this.OperatorsOnly)
                {
                    message.Append(" o");
                }
            }
            message.Append("\r\n");
            return message.ToString();
        }
    }
}