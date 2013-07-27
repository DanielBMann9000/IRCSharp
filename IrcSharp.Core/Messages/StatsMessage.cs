using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class StatsMessage : ISendableMessage
    {
        public string Query { get; private set; }
        public string Target { get; private set; }
        public StatsMessage() { }

        public StatsMessage(string target)
        {
            this.Target = target;
            
        }

        public StatsMessage(string target, string query) : this(target)
        {
            this.Query = query;
        }

        public string ToMessage()
        {
            var message = new StringBuilder();
            message.Append("STATS");
            if (!string.IsNullOrWhiteSpace(this.Query))
            {
                message.AppendFormat(" {0}", this.Query);
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