﻿using System.Text;

using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class TopicMessage : ISendableMessage, IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public string Channel { get; private set; }
        public string Topic { get; private set; }
        public bool RemoveTopic { get; private set; }

        internal TopicMessage(IrcUserInfo userInfo, string channel, string newTopic)
        {
            this.UserInfo = userInfo;
            this.Channel = channel;
            if (string.IsNullOrWhiteSpace(newTopic))
            {
                this.RemoveTopic = true;
                this.Topic = null;
            }
            else
            {
                this.Topic = newTopic;
            }
        }

        public TopicMessage(string channel)
        {
            this.Channel = channel;
        }

        public TopicMessage(string channel, string topic) : this(channel)
        {
            this.Topic = topic;
        }

        public TopicMessage(string channel, bool removeTopic) : this(channel)
        {
            this.RemoveTopic = removeTopic;
        }

        string ISendableMessage.ToMessage()
        {
            var message = new StringBuilder();
            message.AppendFormat("TOPIC {0}", this.Channel);

            if (!string.IsNullOrWhiteSpace(this.Topic) && !this.RemoveTopic)
            {
                message.AppendFormat(" :{0}", this.Topic);
            }
            else if (this.RemoveTopic)
            {
                message.Append(" :");
            }

            message.Append("\r\n");

            return message.ToString();
        }
    }
}