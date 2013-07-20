using System.Linq;
using System.Threading.Tasks;

using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Sendable;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    [TestClass]
    public class When_Sending_Connection_Registration_Messages
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
        public async Task A_Nick_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "NICK TestUser\r\n";
            ISendableMessage testMessage = new NickMessage("TestUser");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Pass_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "PASS foobar\r\n";
            ISendableMessage testMessage = new PassMessage("foobar");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_User_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "USER UserName 3 * :Test User\r\n";
            ISendableMessage testMessage = new UserMessage("UserName", UserMessage.Mode.Invisible | UserMessage.Mode.Wallops, "Test User");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Ping_Message_Is_Automatically_Responded_To_With_An_Appropriate_Pong()
        {
            var cm = new FakeSocketConnection();
            var con = new IrcConnection(cm);
            await con.ConnectAsync("foo", "bar", "baz", 0);
            var expected = "PONG 12345678\r\n";
            cm.SendFakeMessage("PING :12345678");
            Assert.IsTrue(cm.Messages.Any(m => m == expected));
        }
    }
}