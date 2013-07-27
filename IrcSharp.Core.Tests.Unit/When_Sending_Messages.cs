using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using IrcSharp.Core.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class When_Sending_Messages
    {
        [TestMethod]
        public async Task Sending_Any_Message_Fires_The_RawMessageSent_Event()
        {
            await TestHelpers.RunSendableEventFiringTest(new PongMessage("foo"), (con, mre) => con.OnRawMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPassMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new PassMessage("foo"), (con, mre) => con.MessagePropagator.OnPassMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPassMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new PassMessage("foo"), (con, mre) => con.MessagePropagator.OnPassMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNickMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new NickMessage("foo"), (con, mre) => con.MessagePropagator.OnNickMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNickMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new NickMessage("foo"), (con, mre) => con.MessagePropagator.OnNickMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnUserMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new UserMessage("foo", UserMessage.Mode.None, "dbm"), (con, mre) => con.MessagePropagator.OnUserMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnUserMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new UserMessage("foo", UserMessage.Mode.None, "dbm"), (con, mre) => con.MessagePropagator.OnUserMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnJoinMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new JoinMessage("foo"), (con, mre) => con.MessagePropagator.OnJoinMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnJoinMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new JoinMessage("foo"), (con, mre) => con.MessagePropagator.OnJoinMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPartMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new PartMessage("foo"), (con, mre) => con.MessagePropagator.OnPartMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPartMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new PartMessage("foo"), (con, mre) => con.MessagePropagator.OnPartMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnChannelModeMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new ChannelModeMessage("foo"), (con, mre) => con.MessagePropagator.OnChannelModeMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnChannelModeMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new ChannelModeMessage("foo"), (con, mre) => con.MessagePropagator.OnChannelModeMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNamesMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new NamesMessage(), (con, mre) => con.MessagePropagator.OnNamesMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNamesMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new NamesMessage(), (con, mre) => con.MessagePropagator.OnNamesMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnListMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new ListMessage(), (con, mre) => con.MessagePropagator.OnListMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnListMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new ListMessage(), (con, mre) => con.MessagePropagator.OnListMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnInviteMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new InviteMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnInviteMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnInviteMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new InviteMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnInviteMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnKickMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new KickMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnKickMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnKickMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new KickMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnKickMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPrivMsgMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new PrivMsgMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnPrivMsgMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPrivMsgMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new PrivMsgMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnPrivMsgMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNoticeMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new NoticeMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnNoticeMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNoticeMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new NoticeMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnNoticeMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPongMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new PongMessage("bar"), (con, mre) => con.MessagePropagator.OnPongMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPongMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new PongMessage("bar"), (con, mre) => con.MessagePropagator.OnPongMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnTopicMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new TopicMessage("bar"), (con, mre) => con.MessagePropagator.OnTopicMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnTopicMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new TopicMessage("bar"), (con, mre) => con.MessagePropagator.OnTopicMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnOperMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new OperMessage("user", "pass"), (con, mre) => con.MessagePropagator.OnOperMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnOperMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new OperMessage("user", "pass"), (con, mre) => con.MessagePropagator.OnOperMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnUserModeMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new UserModeMessage("user", "pass"), (con, mre) => con.MessagePropagator.OnUserModeMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnUserModeMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new UserModeMessage("user", "pass"), (con, mre) => con.MessagePropagator.OnUserModeMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnServiceMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new ServiceMessage("nickname", "distribution", "info"), (con, mre) => con.MessagePropagator.OnServiceMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnServiceMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new ServiceMessage("nickname", "distribution", "info"), (con, mre) => con.MessagePropagator.OnServiceMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnQuitMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new QuitMessage(), (con, mre) => con.MessagePropagator.OnQuitMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnQuitMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new QuitMessage(), (con, mre) => con.MessagePropagator.OnQuitMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnSquitMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new SquitMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnSquitMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnSquitMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new SquitMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnSquitMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnMotdMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new MotdMessage(), (con, mre) => con.MessagePropagator.OnMotdMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnMotdMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new MotdMessage(), (con, mre) => con.MessagePropagator.OnMotdMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnLusersMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new LusersMessage(), (con, mre) => con.MessagePropagator.OnLusersMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnLusersMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new LusersMessage(), (con, mre) => con.MessagePropagator.OnLusersMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnVersionMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new VersionMessage(), (con, mre) => con.MessagePropagator.OnVersionMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnVersionMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new VersionMessage(), (con, mre) => con.MessagePropagator.OnVersionMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnStatsMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new StatsMessage(), (con, mre) => con.MessagePropagator.OnStatsMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnStatsMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new StatsMessage(), (con, mre) => con.MessagePropagator.OnStatsMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnLinksMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new LinksMessage(), (con, mre) => con.MessagePropagator.OnLinksMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnLinksMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new LinksMessage(), (con, mre) => con.MessagePropagator.OnLinksMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnTimeMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new TimeMessage(), (con, mre) => con.MessagePropagator.OnTimeMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnTimeMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new TimeMessage(), (con, mre) => con.MessagePropagator.OnTimeMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnConnectMessageSending_Event_Fires() { await TestHelpers.RunSendableEventFiringTest(new ConnectMessage("foo", 6667), (con, mre) => con.MessagePropagator.OnConnectMessageSending += (sender, args) => mre.Set()); }

        [TestMethod]
        public async Task OnConnectMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new ConnectMessage("foo", 6667), (con, mre) => con.MessagePropagator.OnConnectMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnTraceMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new TraceMessage(), (con, mre) => con.MessagePropagator.OnTraceMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnTraceMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new TraceMessage(), (con, mre) => con.MessagePropagator.OnTraceMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnAdminMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new AdminMessage(), (con, mre) => con.MessagePropagator.OnAdminMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnAdminMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new AdminMessage(), (con, mre) => con.MessagePropagator.OnAdminMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnInfoMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new InfoMessage(), (con, mre) => con.MessagePropagator.OnInfoMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnInfoMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new InfoMessage(), (con, mre) => con.MessagePropagator.OnInfoMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnServlistMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new ServlistMessage(), (con, mre) => con.MessagePropagator.OnServlistMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnServlistMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new ServlistMessage(), (con, mre) => con.MessagePropagator.OnServlistMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnSqueryMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new SqueryMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnSqueryMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnSqueryMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new SqueryMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnSqueryMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnWhoMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new WhoMessage(), (con, mre) => con.MessagePropagator.OnWhoMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnWhoMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new WhoMessage(), (con, mre) => con.MessagePropagator.OnWhoMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnWhoisMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new WhoisMessage("foo"), (con, mre) => con.MessagePropagator.OnWhoisMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnWhoisMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new WhoisMessage("foo"), (con, mre) => con.MessagePropagator.OnWhoisMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnWhowasMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new WhowasMessage("foo"), (con, mre) => con.MessagePropagator.OnWhowasMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnWhowasMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new WhowasMessage("foo"), (con, mre) => con.MessagePropagator.OnWhowasMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnKillMessageSending_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new KillMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnKillMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnKillMessageSent_Event_Fires()
        {
            await TestHelpers.RunSendableEventFiringTest(new KillMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnKillMessageSent += (sender, args) => mre.Set());
        }


    }
}