namespace IrcSharp.Core.Messages.Receivable
{
    public class UnknownMessage : IReceivableMessage
    {
        public string UnparsedMessage { get; private set; }

        public UnknownMessage(string message)
        {
            this.UnparsedMessage = message;
        }
    }
}