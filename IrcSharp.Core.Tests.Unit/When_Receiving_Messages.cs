using System.Threading;
using System.Threading.Tasks;
using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages.Receivable;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    [TestClass]
    public class When_Receiving_Messages
    {
        [TestMethod]
        public async Task A_Message_That_Falls_Into_No_Other_Categories_Results_In_An_Unknown_Message_Event()
        {
            var mre = new ManualResetEvent(false);
            var cm = new FakeConnectionManager();
            using (var con = new IrcConnection(cm))
            {
                await con.ConnectAsync(null, null, null, 0);
                UnknownMessage actual = null;
                con.OnUnknownMessage += (sender, args) =>
                {
                    actual = args;
                    mre.Set();
                };
                cm.SendFakeMessage("garbage");
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }
                Assert.IsNotNull(actual);
                Assert.AreEqual("garbage", actual.UnparsedMessage);
            }
        }

        [TestMethod]
        public async Task A_Ping_Message_Results_In_A_Ping_Event()
        {
            var cm = new FakeConnectionManager();
            var mre = new ManualResetEvent(false);
            var con = new IrcConnection(cm);
            PingMessage actual = null;
            con.OnPingMessage += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync(null, null, null, 0);
            cm.SendFakeMessage("PING :12345678");
            if (!mre.WaitOne(1000))
            {
                Assert.Fail("The event was never received.");
            }
            Assert.AreEqual("12345678", actual.Value);
        }

        [TestMethod]
        public async Task A_Nick_Message_Results_In_A_Nick_Event()
        {
            var cm = new FakeConnectionManager();
            var mre = new ManualResetEvent(false);
            var con = new IrcConnection(cm);
            NickMessage actual = null;
            con.OnNickMessage += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync(null, null, null, 0);
            cm.SendFakeMessage(":Test!daniel@foo.bar.com NICK NewNick");
            if (!mre.WaitOne(1000))
            {
                Assert.Fail("The event was never received.");
            }
            Assert.AreEqual("Test", actual.Original);
            Assert.AreEqual("daniel", actual.Identity);
            Assert.AreEqual("foo.bar.com", actual.Host);
            Assert.AreEqual("NewNick", actual.New);
        }

        [TestMethod]
        public async Task A_Numeric_Response_Message_With_A_Code_Of_001_Results_In_A_GenericNumericResponse_Message()
        {
            var cm = new FakeConnectionManager();
            var mre = new ManualResetEvent(false);
            var con = new IrcConnection(cm);
            GenericNumericResponseMessage actual = null;
            con.OnWelcomeResponseMessage += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync(null, null, null, 0);
            cm.SendFakeMessage(":localhost.com 001 DBM :Welcome to the Internet Relay Network DBM");
            if (!mre.WaitOne(1000))
            {
                Assert.Fail("The event was never received.");
            }
            Assert.AreEqual("001", actual.ResponseCode);
            Assert.AreEqual("Welcome to the Internet Relay Network DBM", actual.ResponseText);
        }

        [TestMethod]
        public async Task A_Numeric_Response_Message_With_A_Code_Of_451_Results_In_A_NotRegisteredResponse_Message()
        {
            var cm = new FakeConnectionManager();
            var mre = new ManualResetEvent(false);
            var con = new IrcConnection(cm);
            NotRegisteredNumericResponseMessage actual = null;
            con.OnNotRegisteredResponseMessage += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync(null, null, null, 0);
            cm.SendFakeMessage(":localhost.com 451 DBM JOIN :Register first.");
            if (!mre.WaitOne(1000))
            {
                Assert.Fail("The event was never received.");
            }
            Assert.AreEqual("JOIN", actual.Command);
            Assert.AreEqual("Register first.", actual.Message);
        }
    }
}
