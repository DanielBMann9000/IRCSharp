using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class UnknownMessage : IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string UnparsedMessage { get; private set; }

        public UnknownMessage(string message)
        {
            this.UnparsedMessage = message;
        }
    }
}