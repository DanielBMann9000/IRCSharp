using System.Threading.Tasks;

using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Sendable;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    [TestClass]
    public class When_Sending_Channel_Operation_Messages
    {
        /*
         * todo: 
         * Channel Mode
         * Topic
         * Names
         * List
         * Invite
         * Kick
         */
        [TestMethod]
        public async Task A_Join_Message_For_A_Single_Keyless_Channel_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld\r\n";
            ISendableMessage testMessage = new JoinMessage("#helloworld");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Join_Message_For_A_Single_Keyed_Channel_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld thisisakey\r\n";
            ISendableMessage testMessage = new JoinMessage("#helloworld", "thisisakey");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Join_Message_For_A_Multiple_Channels_And_No_Keys_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld,#goodbyeworld\r\n";
            ISendableMessage testMessage = new JoinMessage(new[] { "#helloworld", "#goodbyeworld" });
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Join_Message_For_A_Multiple_Channels_With_Keys_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld,#goodbyeworld hi,bye\r\n";
            ISendableMessage testMessage = new JoinMessage(new[] { "#helloworld", "#goodbyeworld" }, new[] { "hi", "bye" });
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Join_Message_With_No_Channels_Provided_Sends_A_Part_Message()
        {
            var expected = "JOIN 0\r\n";
            ISendableMessage testMessage = new JoinMessage();
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Part_Message_With_One_Channel_And_No_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld\r\n";
            ISendableMessage testMessage = new PartMessage(new []{"#helloworld"});
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Part_Message_With_One_Channel_A_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld :Goodbye, cruel world!\r\n";
            ISendableMessage testMessage = new PartMessage(new []{"#helloworld"}, "Goodbye, cruel world!");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Part_Message_With_Multiple_Channels_And_No_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld,#goodbyeworld\r\n";
            ISendableMessage testMessage = new PartMessage(new [] {"#helloworld", "#goodbyeworld"});
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Part_Message_With_Multiple_Channels_And_A_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld,#goodbyeworld :Now is the winter of our discount tent\r\n";
            ISendableMessage testMessage = new PartMessage(new[] { "#helloworld", "#goodbyeworld" }, "Now is the winter of our discount tent");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }
    }
}