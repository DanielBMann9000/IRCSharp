namespace IrcSharp.Core.Messages.Sendable
{
    public class PassMessage : ISendableMessage
    {
        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("PASS {0}\r\n", this.Password);
        }
    }
}