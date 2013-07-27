using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class InfoMessage : BaseMessageWithOptionalTarget, ISendableMessage
    {
        public InfoMessage(){}
        public InfoMessage(string target) : base(target){}

        public string ToMessage()
        {
            return base.ToMessage("INFO");
        }
    }
}