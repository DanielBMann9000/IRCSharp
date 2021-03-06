﻿using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class ServiceMessage : ISendableMessage
    {
        public string ServiceNickname { get; private set; }
        public string Distribution { get; private set; }
        public string ServiceInfo { get; private set; }
        public ServiceMessage(string serviceNickname, string distribution, string info)
        {
            this.ServiceNickname = serviceNickname;
            this.Distribution = distribution;
            this.ServiceInfo = info;
        }

        string ISendableMessage.ToMessage()
        {
            return string.Format("SERVICE {0} * {1} 0 0 :{2}\r\n", this.ServiceNickname, this.Distribution, this.ServiceInfo);
        }
    }
}