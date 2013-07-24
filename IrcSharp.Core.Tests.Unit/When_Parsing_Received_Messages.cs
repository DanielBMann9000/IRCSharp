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
        public async Task A_Parsed_A_NotRegisteredResponse_Message_Has_The_Appropriate_Properties_Filled()
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
    }
}