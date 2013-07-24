using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages.Receivable
{
    public class NickMessage : IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string New { get; private set; }

        public NickMessage(IrcUserInfo userIdentity, string newNick)
        {
            this.UserInfo = userIdentity;
            this.New = newNick;
        }
    }
}
