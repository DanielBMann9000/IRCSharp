using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class PassMessage : ISendableMessage
    {
        public string Password { get; private set; }

        public PassMessage(string password)
        {
            this.Password = password;
        }

        string ISendableMessage.ToMessage()
        {
            return string.Format("PASS {0}\r\n", this.Password);
        }
    }
}