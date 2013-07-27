using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class SqueryMessage : ISendableMessage 
    {
        public string ServiceName { get; private set; }
        public string QueryText { get; private set; }
        public SqueryMessage(string serviceName, string queryText)
        {
            this.ServiceName = serviceName;
            this.QueryText = queryText;
        }

        public string ToMessage()
        {
            return string.Format("SQUERY {0} :{1}\r\n", this.ServiceName, this.QueryText);
        }
    }
}