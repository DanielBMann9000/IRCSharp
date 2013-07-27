using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class AdminMessage : BaseMessageWithOptionalTarget, ISendableMessage 
    {
        public AdminMessage() {}
        public AdminMessage(string target) : base(target){}

        public string ToMessage()
        {
            return base.ToMessage("ADMIN");
        }
    }
}