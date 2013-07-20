namespace IrcSharp.Core.Messages.Receivable
{
    public class NickMessage : IReceivableMessage
    {
        public string Original { get; private set; }
        public string New { get; private set; }
        public string Identity { get; private set; }
        public string Host { get; private set; }

        public NickMessage(string originalNick, string newNick, string identity, string host)
        {
            this.Original = originalNick;
            this.New = newNick;
            this.Identity = identity;
            this.Host = host;
        }
    }
}
