namespace IrcSharp.Core.Messages
{
    public class GenericNumericResponseMessage : NumericReponseMessageBase
    {
        public string ResponseText { get; private set; }
        public GenericNumericResponseMessage(string responseCode, string responseText) : base(responseCode)
        {
            this.ResponseText = responseText;
        }
    }
}
