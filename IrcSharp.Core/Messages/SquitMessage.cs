using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class SquitMessage : ISendableMessage
    {
        public string Server { get; private set; }
        public string Reason { get; private set; }


        public SquitMessage(string server, string reason)
        {
            this.Server = server;
            this.Reason = reason;
        }

        string ISendableMessage.ToMessage()
        {
            return string.Format("SQUIT {0} :{1}\r\n", this.Server, this.Reason);
        }
    }
}