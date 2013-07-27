using System.Text;

namespace IrcSharp.Core.Messages
{
    public abstract class BaseMessageWithOptionalTarget
    {
        public string Target { get; protected set; }

        protected BaseMessageWithOptionalTarget(){}
        protected BaseMessageWithOptionalTarget(string target)
        {
            this.Target = target;
        }

        protected string ToMessage(string type)
        {
            var message = new StringBuilder();
            message.Append(type);
            if (!string.IsNullOrWhiteSpace(this.Target))
            {
                message.AppendFormat(" {0}", this.Target);
            }
            message.Append("\r\n");
            return message.ToString();
        }
    }
}
