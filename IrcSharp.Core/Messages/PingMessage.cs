using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class PingMessage : IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string Value { get; set; }

        public PingMessage(string value)
        {
            this.Value = value;
        }

        
    }
}
