namespace IrcSharp.Core.Messages.Receivable
{
    public class PingMessage : IReceivableMessage
    {
        public PingMessage(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }
    }
}
