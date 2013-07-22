﻿using System.Linq;
using System.Threading.Tasks;

using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Sendable;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local
    [TestClass]
    public class When_Generating_Connection_Registration_Messages
    {
        /*
         * todo:
         * oper
         * user mode
         * service message
         * quit
         * squit
         */
        [TestMethod]
        public void A_Nick_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "NICK TestUser\r\n";
            ISendableMessage testMessage = new NickMessage("TestUser");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Pass_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "PASS foobar\r\n";
            ISendableMessage testMessage = new PassMessage("foobar");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_User_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "USER UserName 3 * :Test User\r\n";
            ISendableMessage testMessage = new UserMessage("UserName", UserMessage.Mode.Invisible | UserMessage.Mode.Wallops, "Test User");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public async Task A_Ping_Message_Is_Automatically_Responded_To_With_An_Appropriate_Pong()
        {
            var cm = new FakeSocketConnection();
            var con = new IrcConnection(cm);
            await con.ConnectAsync("foo", "bar", "baz", 0);
            var expected = "PONG 12345678\r\n";
            cm.SimulateMessageReceipt("PING :12345678");
            Assert.IsTrue(cm.Messages.Any(m => m == expected));
        }
    }
}