using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class TimeMessage: BaseMessageWithOptionalTarget, ISendableMessage 
    {
        public TimeMessage(){}
        public TimeMessage(string target) : base(target)
        {
        }

        public string ToMessage()
        {
            return base.ToMessage("TIME");
        }
    }
}