using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class MotdMessage : BaseMessageWithOptionalTarget, ISendableMessage
    {
        public MotdMessage()
        {    
        }

        public MotdMessage(string target) : base(target)
        {
        }

        public string ToMessage()
        {
            return base.ToMessage("MOTD");
        }
    }
}