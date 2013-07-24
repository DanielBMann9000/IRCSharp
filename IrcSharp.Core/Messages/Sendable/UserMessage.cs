using System;

namespace IrcSharp.Core.Messages.Sendable
{
    public class UserMessage : ISendableMessage
    {
        public string UserName { get; private set; }
        public Mode UserMode { get; private set; }
        public string RealName { get; private set; }

        public UserMessage(string userName, Mode userMode, string realName)
        {
            this.UserName = userName;
            this.UserMode = userMode;
            this.RealName = realName;
        }

        public string ToMessage()
        {
            return string.Format("USER {0} {1} * :{2}\r\n", this.UserName, (int)this.UserMode, this.RealName);
        }

        [Flags]
        public enum Mode
        {
            None = 0,
            Wallops = 1,
            Invisible = 2
        }
    }
}
