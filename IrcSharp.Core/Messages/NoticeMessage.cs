using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class NoticeMessage : ISendableMessage
    {
        public string MessageDestination { get; private set; }
        public string Message { get; private set; }
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