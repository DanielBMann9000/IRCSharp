namespace IrcSharp.Core.Messages.Receivable
{
    public class NotRegisteredNumericResponseMessage : NumericReponseMessageBase
    {
        public string Command { get; private set; }
        public string Message { get; private set; }
        public NotRegisteredNumericResponseMessage(string command, string message) : base("451")
        {
            this.Command = command;
            this.Message = message;
        }
    }
}
