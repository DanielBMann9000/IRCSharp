namespace IrcSharp.Core.Messages.Sendable
{
    public class PongMessage : ISendableMessage
    {
        public string ResponseValue { get; private set; }
        public PongMessage(string responseValue)
        {
            this.ResponseValue = responseValue;
        }

        public override string ToString()
        {
            return string.Format("PONG {0}\r\n", this.ResponseValue);
        }
    }
}