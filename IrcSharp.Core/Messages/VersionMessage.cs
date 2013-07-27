using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class VersionMessage : BaseMessageWithOptionalTarget, ISendableMessage
    {
        public VersionMessage()
        {
        }

        public VersionMessage(string target) : base(target)
        {
        }

        public string ToMessage()
        {
            return base.ToMessage("VERSION");
        }

    }
}