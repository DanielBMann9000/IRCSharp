using System;
using System.Threading;
using System.Threading.Tasks;

using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Sendable;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [TestClass]
    public class When_Sending_Messages
    {
        [TestMethod]
        public async Task OnPassMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new PassMessage("foo"), (con, mre) => con.MessagePropagator.OnPassMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPassMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new PassMessage("foo"), (con, mre) => con.MessagePropagator.OnPassMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNickMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new NickMessage("foo"), (con, mre) => con.MessagePropagator.OnNickMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNickMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new NickMessage("foo"), (con, mre) => con.MessagePropagator.OnNickMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnUserMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new UserMessage("foo", UserMessage.Mode.None, "dbm"), (con, mre) => con.MessagePropagator.OnUserMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnUserMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new UserMessage("foo", UserMessage.Mode.None, "dbm"), (con, mre) => con.MessagePropagator.OnUserMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnJoinMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new JoinMessage("foo"), (con, mre) => con.MessagePropagator.OnJoinMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnJoinMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new JoinMessage("foo"), (con, mre) => con.MessagePropagator.OnJoinMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPartMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new PartMessage("foo"), (con, mre) => con.MessagePropagator.OnPartMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPartMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new PartMessage("foo"), (con, mre) => con.MessagePropagator.OnPartMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnModeMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new ModeMessage("foo"), (con, mre) => con.MessagePropagator.OnModeMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnModeMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new ModeMessage("foo"), (con, mre) => con.MessagePropagator.OnModeMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNamesMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new NamesMessage(), (con, mre) => con.MessagePropagator.OnNamesMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNamesMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new NamesMessage(), (con, mre) => con.MessagePropagator.OnNamesMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnListMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new ListMessage(), (con, mre) => con.MessagePropagator.OnListMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnListMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new ListMessage(), (con, mre) => con.MessagePropagator.OnListMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnInviteMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new InviteMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnInviteMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnInviteMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new InviteMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnInviteMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnKickMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new KickMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnKickMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnKickMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new KickMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnKickMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPrivMsgMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new PrivMsgMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnPrivMsgMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPrivMsgMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new PrivMsgMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnPrivMsgMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNoticeMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new NoticeMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnNoticeMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnNoticeMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new NoticeMessage("foo", "bar"), (con, mre) => con.MessagePropagator.OnNoticeMessageSent += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPongMessageSending_Event_Fires()
        {
            await RunEventFiringTest(new PongMessage("bar"), (con, mre) => con.MessagePropagator.OnPongMessageSending += (sender, args) => mre.Set());
        }

        [TestMethod]
        public async Task OnPongMessageSent_Event_Fires()
        {
            await RunEventFiringTest(new PongMessage("bar"), (con, mre) => con.MessagePropagator.OnPongMessageSent += (sender, args) => mre.Set());
        }


        private static async Task RunEventFiringTest(ISendableMessage message, Action<IrcConnection, ManualResetEvent> registrationAction)
        {
            var mre = new ManualResetEvent(false);
            var cm = new FakeSocketConnection();
            using (var con = new IrcConnection(cm))
            {
                await con.ConnectAsync("foo", "bar", "baz", 0);
                registrationAction(con, mre);
                await con.SendMessageAsync(message);
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }
            }
        }
    }
}