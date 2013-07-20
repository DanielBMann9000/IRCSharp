using System;

namespace IrcSharp.Core.Messages.Sendable
{
    public class UserMessage : ISendableMessage
    {
        public string UserName { get; set; }
        public Mode UserMode { get; set; }
        public string RealName { get; set; }

        public override string ToString()
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
