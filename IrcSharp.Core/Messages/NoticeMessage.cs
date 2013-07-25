using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class NoticeMessage : ISendableMessage, IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string MessageDestination { get; private set; }
        public string Message { get; private set; }

        internal NoticeMessage(IrcUserInfo userInfo, string destination, string message) : this(destination, message)
        {
            this.UserInfo = userInfo;
        }

        public NoticeMessage(string destination, string message)
        {
            this.MessageDestination = destination;
            this.Message = message;
        }

        string ISendableMessage.ToMessage()
        {
            return string.Format("NOTICE {0} :{1}\r\n", this.MessageDestination, this.Message);
        }
    }
}