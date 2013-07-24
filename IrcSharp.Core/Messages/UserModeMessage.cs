using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class UserModeMessage : ISendableMessage
    {
        public string User { get; private set; }
        public string Arguments { get; private set; }
        public UserModeMessage(string user, string arguments)
        {
            this.User = user;
            this.Arguments = arguments;
        }

        string ISendableMessage.ToMessage()
        {
            return string.Format("MODE {0} {1}\r\n", this.User, this.Arguments);
        }
    }
}