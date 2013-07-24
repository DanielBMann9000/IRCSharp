using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class OperMessage : ISendableMessage
    {
        public string User { get; private set; }
        public string Password { get; private set; }
        public OperMessage(string user, string password)
        {
            this.User = user;
            this.Password = password;
        }

        string ISendableMessage.ToMessage()
        {
            return string.Format("OPER {0} {1}\r\n", this.User, this.Password);
        }
    }
}
