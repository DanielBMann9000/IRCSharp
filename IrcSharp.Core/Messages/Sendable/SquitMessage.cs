namespace IrcSharp.Core.Messages.Sendable
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

        public string ToMessage()
        {
            return string.Format("SQUIT {0} :{1}\r\n", this.Server, this.Reason);
        }
    }
}