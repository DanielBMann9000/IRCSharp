using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class InviteMessage : ISendableMessage, IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string Nick { get; private set; }
        public string Channel { get; private set; }

        internal InviteMessage(IrcUserInfo userInfo, string nick, string channel) : this(nick, channel)
        {
            this.UserInfo = userInfo;
        }

        public InviteMessage(string nick, string channel)
        {
            this.Nick = nick;
            this.Channel = channel;
        }

        string ISendableMessage.ToMessage()
        {
            return string.Format("INVITE {0} {1}\r\n", this.Nick, this.Channel);
        }
    }
}