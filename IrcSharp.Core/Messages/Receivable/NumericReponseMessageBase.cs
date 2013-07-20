namespace IrcSharp.Core.Messages.Receivable
{
    public abstract class NumericReponseMessageBase : IReceivableMessage
    {        
        public string ResponseCode { get; private set; }
        protected NumericReponseMessageBase(string responseCode)
        {
            this.ResponseCode = responseCode;
        }   
    }
}