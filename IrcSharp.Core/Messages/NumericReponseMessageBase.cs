using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public abstract class NumericReponseMessageBase : IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string ResponseCode { get; private set; }
        protected NumericReponseMessageBase(string responseCode)
        {
            this.ResponseCode = responseCode;
        }
    }
}