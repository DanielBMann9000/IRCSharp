using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class KillMessage : ISendableMessage, IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string Nickname { get; private set; }
        public string Comment { get; private set; }

        internal KillMessage(IrcUserInfo userInfo, string nickname, string comment) : this(nickname, comment)
        {
            this.UserInfo = userInfo;
        }

        public KillMessage(string nickname, string comment)
        {
            this.Nickname = nickname;
            this.Comment = comment;
        }

        public string ToMessage()
        {
            return string.Format("KILL {0} :{1}\r\n", this.Nickname, this.Comment);
        }
    }
}