using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class TraceMessage : BaseMessageWithOptionalTarget, ISendableMessage 
    {
        public TraceMessage(){}

        public TraceMessage(string target) : base(target)
        {
            this.Target = target;
        }

        public string ToMessage()
        {
            return base.ToMessage("TRACE");
        }
    }
}