using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class When_Parsing_Received_Messages
    {
        [TestMethod]
        public async Task A_Parsed_Ping_Message_Has_The_Value_Property_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                PingMessage actual = null;
                con.MessagePropagator.OnPingMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt("PING :12345678");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }
                Assert.AreEqual("12345678", actual.Value);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Nick_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                NickMessage actual = null;
                con.MessagePropagator.OnNickMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com NICK NewNick");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }
                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("NewNick", actual.Nick);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Join_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                JoinMessage actual = null;
                con.MessagePropagator.OnJoinMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com JOIN #helloworld");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }
                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("#helloworld", actual.Channels[0]);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Part_Message_With_A_Parting_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                PartMessage actual = null;
                con.MessagePropagator.OnPartMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com PART #helloworld :byebye");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("#helloworld", actual.Channels[0]);
                Assert.AreEqual("byebye", actual.PartingMessage);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Part_Message_With_No_Parting_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                PartMessage actual = null;
                con.MessagePropagator.OnPartMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com PART #helloworld");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("#helloworld", actual.Channels[0]);
                Assert.IsNull(actual.PartingMessage);
            }
        }

        [TestMethod]
        public async Task A_Parsed_ChannelMode_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                ChannelModeMessage actual = null;
                con.MessagePropagator.OnChannelModeMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com MODE #helloworld +s");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("#helloworld", actual.Channel);
                Assert.AreEqual("+s", actual.RawCommand);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Topic_Message_With_A_New_Topic_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                TopicMessage actual = null;
                con.MessagePropagator.OnTopicMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com TOPIC #helloworld :New Topic!");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("#helloworld", actual.Channel);
                Assert.AreEqual("New Topic!", actual.Topic);
                Assert.IsFalse(actual.RemoveTopic);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Topic_Message_For_Removing_The_Current_TopicHas_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                TopicMessage actual = null;
                con.MessagePropagator.OnTopicMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com TOPIC #helloworld :");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("#helloworld", actual.Channel);
                Assert.IsNull(actual.Topic);
                Assert.IsTrue(actual.RemoveTopic);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Kick_Message_With_A_Reason_Provided_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                KickMessage actual = null;
                con.MessagePropagator.OnKickMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com KICK #helloworld Daniel :get out!");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("#helloworld", actual.Channels[0]);
                Assert.AreEqual("Daniel", actual.Nicks[0]);
                Assert.AreEqual("get out!", actual.Message);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Kick_Message_With_No_Reason_Provided_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                KickMessage actual = null;
                con.MessagePropagator.OnKickMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com KICK #helloworld Daniel");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("#helloworld", actual.Channels[0]);
                Assert.AreEqual("Daniel", actual.Nicks[0]);
                Assert.IsNull(actual.Message);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Quit_Message_With_No_Reason_Provided_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                QuitMessage actual = null;
                con.MessagePropagator.OnQuitMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com QUIT");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.IsNull(actual.Reason);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Quit_Message_With_A_Reason_Provided_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                QuitMessage actual = null;
                con.MessagePropagator.OnQuitMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com QUIT :bye bye!");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("bye bye!", actual.Reason);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Squit_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                SquitMessage actual = null;
                con.MessagePropagator.OnSquitMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com SQUIT whatever.com :bye bye!");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("whatever.com", actual.Server);
                Assert.AreEqual("bye bye!", actual.Reason);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Invite_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                InviteMessage actual = null;
                con.MessagePropagator.OnInviteMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com INVITE Daniel #helloworld");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("#helloworld", actual.Channel);
                Assert.AreEqual("Daniel", actual.Nick);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Notice_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                NoticeMessage actual = null;
                con.MessagePropagator.OnNoticeMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com NOTICE #helloworld :oh god what's happening");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("#helloworld", actual.MessageDestination);
                Assert.AreEqual("oh god what's happening", actual.Message);
            }
        }

        [TestMethod]
        public async Task A_Parsed_PrivMsg_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                PrivMsgMessage actual = null;
                con.MessagePropagator.OnPrivMsgMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com PRIVMSG Daniel :oh god what's happening");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("Daniel", actual.MessageDestination);
                Assert.AreEqual("oh god what's happening", actual.Message);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Generic_Numeric_Response_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                GenericNumericResponseMessage actual = null;
                con.MessagePropagator.OnWelcomeResponseMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":localhost.com 001 DBM :Welcome to the Internet Relay Network DBM");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }
                Assert.AreEqual("001", actual.ResponseCode);
                Assert.AreEqual("Welcome to the Internet Relay Network DBM", actual.ResponseText);
            }

        }

        [TestMethod]
        public async Task A_Parsed_NotRegisteredResponse_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                NotRegisteredNumericResponseMessage actual = null;
                con.MessagePropagator.OnNotRegisteredResponseMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":localhost.com 451 DBM JOIN :Register first.");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }
                Assert.AreEqual("JOIN", actual.Command);
                Assert.AreEqual("Register first.", actual.Message);
            }
        }

        [TestMethod]
        public async Task A_Parsed_Kill_Message_Has_The_Appropriate_Properties_Filled()
        {
            var mre = new ManualResetEvent(false);
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                KillMessage actual = null;
                con.MessagePropagator.OnKillMessageReceived += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };

                await con.ConnectAsync("foo", "bar", "baz", 0);
                cm.SimulateMessageReceipt(":Test!daniel@foo.bar.com KILL daniel :byebye");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }

                Assert.AreEqual("Test", actual.UserInfo.Nick);
                Assert.AreEqual("daniel", actual.UserInfo.Identity);
                Assert.AreEqual("foo.bar.com", actual.UserInfo.Host);
                Assert.AreEqual("daniel", actual.Nickname);
                Assert.AreEqual("byebye", actual.Comment);
            }
        }

    }
}