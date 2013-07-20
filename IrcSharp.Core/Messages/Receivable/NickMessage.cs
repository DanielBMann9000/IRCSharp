using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages.Receivable
{
    public class NickMessage : IReceivableMessage
    {
        public string Original { get; private set; }
        public string New { get; private set; }
        public string Identity { get; private set; }
        public string Host { get; private set; }

        public NickMessage(IrcUserInfo userIdentity, string newNick)
        {
            this.Original = userIdentity.Nick;
            this.New = newNick;
            this.Identity = userIdentity.Identity;
            this.Host = userIdentity.Host;
        }
    }
}
