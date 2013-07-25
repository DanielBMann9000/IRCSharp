using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages.Propagation
{
    public class MessagePropagator
    {
        #region Events
        public event EventHandler<UnknownMessage> OnUnknownMessage;
        #region Message Details (3)
        #region Connection Registration  (3.1)

        public event EventHandler<PassMessage> OnPassMessageSending;
        public event EventHandler<PassMessage> OnPassMessageSent;

        public event EventHandler<NickMessage> OnNickMessageReceived;
        public event EventHandler<NickMessage> OnNickMessageSending;
        public event EventHandler<NickMessage> OnNickMessageSent;

        public event EventHandler<UserMessage> OnUserMessageSending;
        public event EventHandler<UserMessage> OnUserMessageSent;

        public event EventHandler<OperMessage> OnOperMessageSending;
        public event EventHandler<OperMessage> OnOperMessageSent;

        public event EventHandler<UserModeMessage> OnUserModeMessageSending;
        public event EventHandler<UserModeMessage> OnUserModeMessageSent;

        public event EventHandler<ServiceMessage> OnServiceMessageSending;
        public event EventHandler<ServiceMessage> OnServiceMessageSent;

        public event EventHandler<QuitMessage> OnQuitMessageSending;
        public event EventHandler<QuitMessage> OnQuitMessageSent;
        public event EventHandler<QuitMessage> OnQuitMessageReceived;

        public event EventHandler<SquitMessage> OnSquitMessageSending;
        public event EventHandler<SquitMessage> OnSquitMessageSent;
        public event EventHandler<SquitMessage> OnSquitMessageReceived;

        #endregion Connection Registration (3.1)

        #region Channel Operations (3.2)
        public event EventHandler<JoinMessage> OnJoinMessageSending;
        public event EventHandler<JoinMessage> OnJoinMessageSent;
        public event EventHandler<JoinMessage> OnJoinMessageReceived;

        public event EventHandler<PartMessage> OnPartMessageSending;
        public event EventHandler<PartMessage> OnPartMessageSent;
        public event EventHandler<PartMessage> OnPartMessageReceived;

        public event EventHandler<ChannelModeMessage> OnChannelModeMessageSending;
        public event EventHandler<ChannelModeMessage> OnChannelModeMessageSent;
        public event EventHandler<ChannelModeMessage> OnChannelModeMessageReceived;

        public event EventHandler<TopicMessage> OnTopicMessageSending;
        public event EventHandler<TopicMessage> OnTopicMessageSent;
        public event EventHandler<TopicMessage> OnTopicMessageReceived;

        public event EventHandler<NamesMessage> OnNamesMessageSending;
        public event EventHandler<NamesMessage> OnNamesMessageSent;

        public event EventHandler<ListMessage> OnListMessageSending;
        public event EventHandler<ListMessage> OnListMessageSent;

        public event EventHandler<InviteMessage> OnInviteMessageSending;
        public event EventHandler<InviteMessage> OnInviteMessageSent;
        public event EventHandler<InviteMessage> OnInviteMessageReceived;

        public event EventHandler<KickMessage> OnKickMessageSending;
        public event EventHandler<KickMessage> OnKickMessageSent;
        public event EventHandler<KickMessage> OnKickMessageReceived;
        
        #endregion Channel Operations (3.2)

        #region Sending messages (3.3)
        public event EventHandler<PrivMsgMessage> OnPrivMsgMessageSending;
        public event EventHandler<PrivMsgMessage> OnPrivMsgMessageSent;
        public event EventHandler<PrivMsgMessage> OnPrivMsgMessageReceived;

        public event EventHandler<NoticeMessage> OnNoticeMessageSending;
        public event EventHandler<NoticeMessage> OnNoticeMessageSent;
        public event EventHandler<NoticeMessage> OnNoticeMessageReceived;
        #endregion Sending messages (3.3)

        #region Server queries and commands (3.4)
        #endregion Server queries and commands (3.4)

        #region Service queries and commands (3.5)
        #endregion Service queries and commands (3.5)

        #region User based queries (3.6)
        #endregion User based queries (3.6)

        #region Miscellaneous messages (3.7)
        public event EventHandler<PingMessage> OnPingMessageReceived;
        public event EventHandler<PongMessage> OnPongMessageSending;
        public event EventHandler<PongMessage> OnPongMessageSent;
        #endregion Miscellaneous messages (3.7)
        #endregion Message Details (3)

        #region Replies (5)
        #region Command responses (5.1)
        public event EventHandler<GenericNumericResponseMessage> OnWelcomeResponseMessageReceived;
        #endregion Command responses (5.1)

        #region Error replies (5.2)
        public event EventHandler<NotRegisteredNumericResponseMessage> OnNotRegisteredResponseMessageReceived;
        #endregion Error replies (5.2)

        #endregion Replies (5)
        #endregion Events

        internal delegate void ReceivedPropagator(IrcUserInfo identity, string arguments);
        
        private readonly IEnumerable<Tuple<string, ReceivedPropagator>> receivedPropagators;
        private readonly Dictionary<Type, Action<ISendableMessage>> sendingPropagators;
        private readonly Dictionary<Type, Action<ISendableMessage>> sentPropagators;
        
        internal MessagePropagator()
        {
            this.receivedPropagators = this.GetReceivedMessagePropagators<ReceivedMessagePropagatorAttribute, ReceivedPropagator>();

            //not a fan of this, but i want sending/sent message propagation in place even if it's not great. 
            this.sendingPropagators = new Dictionary<Type, Action<ISendableMessage>>
                                  {
                                      { typeof(PassMessage), msg => this.RouteSendableMessage(msg, this.OnPassMessageSending) },
                                      { typeof(NickMessage), msg => this.RouteSendableMessage(msg, this.OnNickMessageSending) },
                                      { typeof(UserMessage), msg => this.RouteSendableMessage(msg, this.OnUserMessageSending) },
                                      { typeof(JoinMessage), msg => this.RouteSendableMessage(msg, this.OnJoinMessageSending) },
                                      { typeof(PartMessage), msg => this.RouteSendableMessage(msg, this.OnPartMessageSending) },
                                      { typeof(ChannelModeMessage), msg => this.RouteSendableMessage(msg, this.OnChannelModeMessageSending) },
                                      { typeof(TopicMessage), msg => this.RouteSendableMessage(msg, this.OnTopicMessageSending) },
                                      { typeof(NamesMessage), msg => this.RouteSendableMessage(msg, this.OnNamesMessageSending) },
                                      { typeof(ListMessage), msg => this.RouteSendableMessage(msg, this.OnListMessageSending) },
                                      { typeof(InviteMessage), msg => this.RouteSendableMessage(msg, this.OnInviteMessageSending) },
                                      { typeof(KickMessage), msg => this.RouteSendableMessage(msg, this.OnKickMessageSending) },
                                      { typeof(PrivMsgMessage), msg => this.RouteSendableMessage(msg, this.OnPrivMsgMessageSending) },
                                      { typeof(NoticeMessage), msg => this.RouteSendableMessage(msg, this.OnNoticeMessageSending) },
                                      { typeof(PongMessage), msg => this.RouteSendableMessage(msg, this.OnPongMessageSending) },
                                      { typeof(OperMessage), msg => this.RouteSendableMessage(msg, this.OnOperMessageSending) },
                                      { typeof(UserModeMessage), msg => this.RouteSendableMessage(msg, this.OnUserModeMessageSending) },
                                      { typeof(ServiceMessage), msg => this.RouteSendableMessage(msg, this.OnServiceMessageSending) },
                                      { typeof(QuitMessage), msg => this.RouteSendableMessage(msg, this.OnQuitMessageSending) },
                                      { typeof(SquitMessage), msg => this.RouteSendableMessage(msg, this.OnSquitMessageSending) }
                                  };

            this.sentPropagators = new Dictionary<Type, Action<ISendableMessage>>
                                  {
                                      { typeof(PassMessage), msg => this.RouteSendableMessage(msg, this.OnPassMessageSent) },
                                      { typeof(NickMessage), msg => this.RouteSendableMessage(msg, this.OnNickMessageSent) },
                                      { typeof(UserMessage), msg => this.RouteSendableMessage(msg, this.OnUserMessageSent) },
                                      { typeof(JoinMessage), msg => this.RouteSendableMessage(msg, this.OnJoinMessageSent) },
                                      { typeof(PartMessage), msg => this.RouteSendableMessage(msg, this.OnPartMessageSent) },
                                      { typeof(ChannelModeMessage), msg => this.RouteSendableMessage(msg, this.OnChannelModeMessageSent) },
                                      { typeof(TopicMessage), msg => this.RouteSendableMessage(msg, this.OnTopicMessageSent) },
                                      { typeof(NamesMessage), msg => this.RouteSendableMessage(msg, this.OnNamesMessageSent) },
                                      { typeof(ListMessage), msg => this.RouteSendableMessage(msg, this.OnListMessageSent) },
                                      { typeof(InviteMessage), msg => this.RouteSendableMessage(msg, this.OnInviteMessageSent) },
                                      { typeof(KickMessage), msg => this.RouteSendableMessage(msg, this.OnKickMessageSent) },
                                      { typeof(PrivMsgMessage), msg => this.RouteSendableMessage(msg, this.OnPrivMsgMessageSent) },
                                      { typeof(NoticeMessage), msg => this.RouteSendableMessage(msg, this.OnNoticeMessageSent) },
                                      { typeof(PongMessage), msg => this.RouteSendableMessage(msg, this.OnPongMessageSent) },
                                      { typeof(OperMessage), msg => this.RouteSendableMessage(msg, this.OnOperMessageSent) },
                                      { typeof(UserModeMessage), msg => this.RouteSendableMessage(msg, this.OnUserModeMessageSent) },
                                      { typeof(ServiceMessage), msg => this.RouteSendableMessage(msg, this.OnServiceMessageSent) },
                                      { typeof(QuitMessage), msg => this.RouteSendableMessage(msg, this.OnQuitMessageSent) },
                                      { typeof(SquitMessage), msg => this.RouteSendableMessage(msg, this.OnSquitMessageSent) }
                                  };

        }

        internal void RouteReceivedMessage(string message)
        {
            string command;
            var commandArguments = string.Empty;
            IrcUserInfo parsedIdentity = null;

            if (message.StartsWith(":"))
            {
                var originator = message.Substring(1, message.IndexOf(" ", StringComparison.Ordinal) - 1);
                command = message.Split(' ').Skip(1).Take(1).First();
                var commandStart = message.IndexOf(command, StringComparison.Ordinal);

                if (commandStart + command.Length + 1 < message.Length)
                {
                    commandArguments = message.Substring(commandStart + command.Length + 1);
                }
                if (originator.Contains("!") && originator.Contains("@"))
                {
                    var exclaimationLocation = originator.IndexOf("!", StringComparison.Ordinal);
                    var atSignLocation = originator.IndexOf("@", StringComparison.Ordinal);
                    var nick = originator.Substring(0, exclaimationLocation);
                    var identity = originator.Substring(exclaimationLocation + 1, atSignLocation - exclaimationLocation - 1);
                    var host = originator.Substring(atSignLocation + 1, originator.Length - atSignLocation - 1);
                    parsedIdentity = new IrcUserInfo(nick, identity, host);
                }
                else
                {
                    parsedIdentity = new IrcUserInfo(originator);
                }
            }
            else
            {
                command = message.Split(' ').Take(1).First();
                if (message.Length != command.Length)
                {
                    commandArguments = message.Substring(message.IndexOf(command, StringComparison.Ordinal) + command.Length + 1);
                }
            }

            var messagePropagators = this.receivedPropagators.Where(p => p.Item1 == command).ToList();

            if (messagePropagators.Count == 0)
            {
                if (this.OnUnknownMessage != null)
                {
                    this.OnUnknownMessage(this, new UnknownMessage(message));
                }
            }
            else
            {
                // It's unlikely that one message will have multiple propagators, but I support it anyway.
                foreach (var messagePropagator in messagePropagators)
                {
                    messagePropagator.Item2(parsedIdentity, commandArguments);
                }
            }
        }

        internal void RouteSendingMessage(ISendableMessage message)
        {
            sendingPropagators[message.GetType()](message);
        }

        internal void RouteSentMessage(ISendableMessage message)
        {
            sentPropagators[message.GetType()](message);
        }

        private void RouteSendableMessage<T>(ISendableMessage message, EventHandler<T> eh)
        {
            if (eh != null)
            {
                eh(this, (T)message);
            }
        }

        #region Received Message Propagation
        [ReceivedMessagePropagator("001")]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private void PropagateWelcomeNumericResponseMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnWelcomeResponseMessageReceived != null)
            {
                var startIndex = arguments.IndexOf(":", StringComparison.Ordinal);
                this.OnWelcomeResponseMessageReceived(this, new GenericNumericResponseMessage("001", arguments.Substring(startIndex + 1, arguments.Length - startIndex - 1)));
            }
        }

        [ReceivedMessagePropagator("451")]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private void PropagateNotRegisteredNumericResponseMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnNotRegisteredResponseMessageReceived != null)
            {
                var args = TokenizeArguments(arguments);
                this.OnNotRegisteredResponseMessageReceived(this, new NotRegisteredNumericResponseMessage(args[1], args[2]));
            }
        }

        [ReceivedMessagePropagator("PING")]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private void PropagatePingMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnPingMessageReceived != null)
            {
                this.OnPingMessageReceived(this, new PingMessage(arguments.Remove(0, 1)));
            }
        }

        [ReceivedMessagePropagator("NICK")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateNickMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnNickMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                this.OnNickMessageReceived(this, new NickMessage(identity, tokenizedArguments[0]));
            }
        }

        [ReceivedMessagePropagator("JOIN")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateJoinMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnJoinMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                this.OnJoinMessageReceived(this, new JoinMessage(identity, tokenizedArguments[0]));
            }
        }

        [ReceivedMessagePropagator("PART")]
        // ReSharper disable once UnusedMember.Local
        private void PropagatePartMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnPartMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                if (tokenizedArguments.Count > 1)
                {
                    this.OnPartMessageReceived(this, new PartMessage(identity, tokenizedArguments[0], tokenizedArguments[1]));
                }
                else
                {
                    this.OnPartMessageReceived(this, new PartMessage(identity, tokenizedArguments[0]));
                }
                
            }
        }

        [ReceivedMessagePropagator("MODE")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateChannelModeMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnChannelModeMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                this.OnChannelModeMessageReceived(this, new ChannelModeMessage(identity, tokenizedArguments[0], tokenizedArguments[1]));
            }
        }

        [ReceivedMessagePropagator("TOPIC")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateTopicMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnTopicMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                if (tokenizedArguments.Count > 1)
                {
                    this.OnTopicMessageReceived(this, new TopicMessage(identity, tokenizedArguments[0], tokenizedArguments[1]));
                }
                else
                {
                    this.OnTopicMessageReceived(this, new TopicMessage(identity, tokenizedArguments[0], null));
                }
            }
        }

        [ReceivedMessagePropagator("KICK")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateKickMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnKickMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                if (tokenizedArguments.Count > 2)
                {
                    this.OnKickMessageReceived(this, new KickMessage(identity, tokenizedArguments[0], tokenizedArguments[1], tokenizedArguments[2]));
                }
                else
                {
                    this.OnKickMessageReceived(this, new KickMessage(identity, tokenizedArguments[0], tokenizedArguments[1]));
                }
            }
        }

        [ReceivedMessagePropagator("QUIT")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateQuitMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnQuitMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                if (tokenizedArguments.Count > 0)
                {
                    this.OnQuitMessageReceived(this, new QuitMessage(identity, tokenizedArguments[0]));
                }
                else
                {
                    this.OnQuitMessageReceived(this, new QuitMessage(identity));
                }
            }
        }

        [ReceivedMessagePropagator("SQUIT")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateSquitMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnSquitMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                this.OnSquitMessageReceived(this, new SquitMessage(identity, tokenizedArguments[0], tokenizedArguments[1]));
            }
        }

        [ReceivedMessagePropagator("INVITE")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateInviteMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnInviteMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                this.OnInviteMessageReceived(this, new InviteMessage(identity, tokenizedArguments[0], tokenizedArguments[1]));
            }
        }

        [ReceivedMessagePropagator("NOTICE")]
        // ReSharper disable once UnusedMember.Local
        private void PropagateNoticeMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnNoticeMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                this.OnNoticeMessageReceived(this, new NoticeMessage(identity, tokenizedArguments[0], tokenizedArguments[1]));
            }
        }

        [ReceivedMessagePropagator("PRIVMSG")]
        // ReSharper disable once UnusedMember.Local
        private void PropagatePrivMsgMessage(IrcUserInfo identity, string arguments)
        {
            if (this.OnPrivMsgMessageReceived != null)
            {
                var tokenizedArguments = TokenizeArguments(arguments);
                this.OnPrivMsgMessageReceived(this, new PrivMsgMessage(identity, tokenizedArguments[0], tokenizedArguments[1]));
            }
        }
        
        #endregion Message Propagation

        // this method belongs somewhere else, and I'm not 100% sure it will tokenize every IRC message properly
        private static List<string> TokenizeArguments(string arguments)
        {
            var tokenized = new List<string>();
            var everythingToTheColon = new string(arguments.TakeWhile(e => e != ':').ToArray());
            var everythingAfterTheColon = new string(arguments.Skip(everythingToTheColon.Length + 1).ToArray());
            tokenized.AddRange(everythingToTheColon.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            if (!string.IsNullOrEmpty(everythingAfterTheColon))
            {
                tokenized.Add(everythingAfterTheColon);
            }
            return tokenized;
        }
    }
}
