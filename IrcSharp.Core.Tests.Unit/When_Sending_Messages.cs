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
    }
}