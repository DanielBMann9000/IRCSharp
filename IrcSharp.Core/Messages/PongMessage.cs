using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class PongMessage : ISendableMessage
    {
        public string ResponseValue { get; private set; }
        public PongMessage(string responseValue)
        {
            this.ResponseValue = responseValue;
        }

        string ISendableMessage.ToMessage()
        {
            return string.Format("PONG {0}\r\n", this.ResponseValue);
        }
    }
}