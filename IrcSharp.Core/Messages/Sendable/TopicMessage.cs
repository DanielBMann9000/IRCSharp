﻿using System.Text;

namespace IrcSharp.Core.Messages.Sendable
{
    public class TopicMessage : ISendableMessage
    {
        public string Channel { get; private set; }
        public string Topic { get; private set; }
        public bool RemoveTopic { get; private set; }
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

        public string ToMessage()
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