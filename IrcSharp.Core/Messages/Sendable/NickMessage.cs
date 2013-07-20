namespace IrcSharp.Core.Messages.Sendable
{
    public class NickMessage : ISendableMessage
    {
        public string Nick { get; set; }

        public override string ToString()
        {
            return string.Format("NICK {0}\r\n", this.Nick);
        }
    }
}
