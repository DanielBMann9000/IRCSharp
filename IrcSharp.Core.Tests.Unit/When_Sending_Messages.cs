using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Sendable;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    [TestClass]
    public class When_Sending_Messages
    {
        [TestMethod]
        public async Task A_Nick_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "NICK TestUser\r\n";
            ISendableMessage testMessage = new NickMessage { Nick = "TestUser" };
            await this.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Pass_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "PASS foobar\r\n";
            ISendableMessage testMessage = new PassMessage { Password = "foobar" };
            await this.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_User_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "USER UserName 3 * :Test User\r\n";
            ISendableMessage testMessage = new UserMessage
                                           {
                                               UserName = "UserName",
                                               UserMode = UserMessage.Mode.Invisible | UserMessage.Mode.Wallops,
                                               RealName = "Test User"
                                           };
            await this.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Ping_Message_Is_Automatically_Responded_To_With_An_Appropriate_Pong()
        {
            var cm = new FakeConnectionManager();
            var con = new IrcConnection(cm);
            await con.ConnectAsync(null, null, null, 0);
            var expected = "PONG 12345678\r\n";
            cm.SendFakeMessage("PING :12345678");
            Assert.IsTrue(cm.Messages.Any(m => m == expected));
        }

        [TestMethod]
        public async Task A_Join_Message_For_A_Single_Keyless_Channel_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld\r\n";
            ISendableMessage testMessage = new JoinMessage(new List<string> { "#helloworld" });
            await this.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Join_Message_For_A_Single_Keyed_Channel_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld thisisakey\r\n";
            ISendableMessage testMessage = new JoinMessage(new [] { "#helloworld" }, new [] { "thisisakey" } );
            await this.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Join_Message_For_A_Multiple_Channels_And_No_Keys_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld,#goodbyeworld\r\n";
            ISendableMessage testMessage = new JoinMessage(new[] { "#helloworld", "#goodbyeworld" });
            await this.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Join_Message_For_A_Multiple_Channels_With_Keys_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld,#goodbyeworld hi,bye\r\n";
            ISendableMessage testMessage = new JoinMessage(new[] { "#helloworld", "#goodbyeworld" }, new [] {"hi", "bye"});
            await this.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Join_Message_With_No_Channels_Provided_Sends_A_Quit_Message()
        {
            var expected = "JOIN 0\r\n";
            ISendableMessage testMessage = new JoinMessage();
            await this.RunSendMessageTest(expected, testMessage);
        }

        #region Helpers
        private async Task RunSendMessageTest(string expected, ISendableMessage messageToTest)
        {
            var cm = new FakeConnectionManager();
            var con = new IrcConnection(cm);
            await con.ConnectAsync(null, null, null, 0);
            await con.SendMessageAsync(messageToTest);
            Assert.IsTrue(cm.Messages.Any(m => m == expected));
        }
        #endregion Helpers
    }
}
