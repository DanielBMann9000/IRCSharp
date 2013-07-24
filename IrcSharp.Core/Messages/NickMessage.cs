using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class NickMessage : ISendableMessage, IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string Nick { get; private set; }

        internal NickMessage(IrcUserInfo userInfo, string newNick)
        {
            this.UserInfo = userInfo;
            this.Nick = newNick;
        }

        public NickMessage(string newNick)
        {
            this.Nick = newNick;
        }

        string ISendableMessage.ToMessage()
        {
            return string.Format("NICK {0}\r\n", this.Nick);
        }
    }
}
