using System.Threading.Tasks;

using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Sendable;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    [TestClass]
    public class When_Sending_Channel_Operation_Messages
    {
        /*
         * todo: 
         * Channel Mode
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
            ISendableMessage testMessage = new JoinMessage(
                new[] { "#helloworld", "#goodbyeworld" },
                new[] { "hi", "bye" });
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
            ISendableMessage testMessage = new PartMessage(new[] { "#helloworld" });
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Part_Message_With_One_Channel_A_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld :Goodbye, cruel world!\r\n";
            ISendableMessage testMessage = new PartMessage(new[] { "#helloworld" }, "Goodbye, cruel world!");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Part_Message_With_Multiple_Channels_And_No_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld,#goodbyeworld\r\n";
            ISendableMessage testMessage = new PartMessage(new[] { "#helloworld", "#goodbyeworld" });
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Part_Message_With_Multiple_Channels_And_A_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld,#goodbyeworld :Now is the winter of our discount tent\r\n";
            ISendableMessage testMessage = new PartMessage(
                new[] { "#helloworld", "#goodbyeworld" },
                "Now is the winter of our discount tent");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Topic_Message_For_A_Channel_With_No_New_Topic_Is_Successfully_Generated()
        {
            var expected = "TOPIC #helloworld\r\n";
            ISendableMessage testMessage = new TopicMessage("#helloworld");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Topic_Message_For_A_Channel_With_A_New_Topic_Is_Successfully_Generated()
        {
            var expected = "TOPIC #helloworld :Welcome to the internet, bozo!\r\n";
            ISendableMessage testMessage = new TopicMessage("#helloworld", "Welcome to the internet, bozo!");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Topic_Message_For_A_Channel_With_Topic_Removal_Is_Successfully_Generated()
        {
            var expected = "TOPIC #helloworld :\r\n";
            ISendableMessage testMessage = new TopicMessage("#helloworld", true);
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Names_Message_With_No_Parameters_Is_Successfully_Generated()
        {
            var expected = "NAMES\r\n";
            ISendableMessage testMessage = new NamesMessage();
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Names_Message_With_A_Single_Channel_Parameter_Is_Successfully_Generated()
        {
            var expected = "NAMES #helloworld\r\n";
            ISendableMessage testMessage = new NamesMessage("#helloworld");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Names_Message_With_A_Multiple_Channel_Parameter_Is_Successfully_Generated()
        {
            var expected = "NAMES #helloworld,#goodbyeworld,#worldworld\r\n";
            ISendableMessage testMessage = new NamesMessage(new[] { "#helloworld", "#goodbyeworld", "#worldworld"});
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Names_Message_With_A_Single_Channel_Parameter_And_A_Target_Parameter_Is_Successfully_Generated()
        {
            var expected = "NAMES #helloworld target\r\n";
            ISendableMessage testMessage = new NamesMessage("#helloworld", "target");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Names_Message_With_A_Multiple_Channel_Parameter_And_A_Target_Parameter_IsSuccessfully_Generated()
        {
            var expected = "NAMES #helloworld,#goodbyeworld,#worldworld target\r\n";
            ISendableMessage testMessage = new NamesMessage(new[] { "#helloworld", "#goodbyeworld", "#worldworld" }, "target");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_List_Message_With_No_Parameters_Is_Successfully_Generated()
        {
            var expected = "LIST\r\n";
            ISendableMessage testMessage = new ListMessage();
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_List_Message_With_A_Single_Channel_Parameter_Is_Successfully_Generated()
        {
            var expected = "LIST #helloworld\r\n";
            ISendableMessage testMessage = new ListMessage("#helloworld");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_List_Message_With_A_Multiple_Channel_Parameter_Is_Successfully_Generated()
        {
            var expected = "LIST #helloworld,#goodbyeworld,#worldworld\r\n";
            ISendableMessage testMessage = new ListMessage(new[] { "#helloworld", "#goodbyeworld", "#worldworld" });
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_List_Message_With_A_Single_Channel_Parameter_And_A_Target_Parameter_Is_Successfully_Generated()
        {
            var expected = "LIST #helloworld target\r\n";
            ISendableMessage testMessage = new ListMessage("#helloworld", "target");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_List_Message_With_A_Multiple_Channel_Parameter_And_A_Target_Parameter_IsSuccessfully_Generated()
        {
            var expected = "LIST #helloworld,#goodbyeworld,#worldworld target\r\n";
            ISendableMessage testMessage = new ListMessage(new[] { "#helloworld", "#goodbyeworld", "#worldworld" }, "target");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task An_Invite_Message_With_A_Channel_And_A_Nick_Is_Successfully_Generated()
        {
            var expected = "INVITE DBM #helloworld\r\n";
            ISendableMessage testMessage = new InviteMessage("DBM", "#helloworld");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Kick_Message_With_A_Single_Channel_And_A_Single_Nick_And_No_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld DBM\r\n";
            ISendableMessage testMessage = new KickMessage("#helloworld", "DBM");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Kick_Message_With_A_Single_Channel_And_A_Single_Nick_And_A_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld DBM :You are bad at IRC, sir.\r\n";
            ISendableMessage testMessage = new KickMessage("#helloworld", "DBM", "You are bad at IRC, sir.");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Kick_Message_With_Multiple_Channels_And_A_Single_Nick_And_No_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld,#goodbyeworld DBM\r\n";
            ISendableMessage testMessage = new KickMessage(new[] { "#helloworld", "#goodbyeworld" }, "DBM");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Kick_Message_With_Multiple_Channels_And_A_Single_Nick_And_A_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld,#goodbyeworld DBM :Bye, bye!\r\n";
            ISendableMessage testMessage = new KickMessage(new[] { "#helloworld", "#goodbyeworld" }, "DBM", "Bye, bye!");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Kick_Message_With_A_Single_Channel_And_Multiple_Nicks_And_No_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld DBM,Gilligan,TheSkipper\r\n";
            ISendableMessage testMessage = new KickMessage("#helloworld", new [] {"DBM", "Gilligan", "TheSkipper"});
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Kick_Message_With_A_Single_Channel_And_Multiple_Nicks_And_A_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld DBM,Gilligan,TheSkipper :A three minute tour\r\n";
            ISendableMessage testMessage = new KickMessage("#helloworld", new[] { "DBM", "Gilligan", "TheSkipper" }, "A three minute tour");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Kick_Message_With_Multiple_Channels_And_Multiple_Nicks_And_No_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld,#theisland DBM,Gilligan,TheSkipper\r\n";
            ISendableMessage testMessage = new KickMessage(new [] { "#helloworld", "#theisland" }, new[] { "DBM", "Gilligan", "TheSkipper" });
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Kick_Message_With_Multiple_Channels_And_Multiple_Nicks_And_A_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld,#theisland DBM,Gilligan,TheSkipper :Where's Ginger?\r\n";
            ISendableMessage testMessage = new KickMessage(new[] { "#helloworld", "#theisland" }, new[] { "DBM", "Gilligan", "TheSkipper" }, "Where's Ginger?");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        [TestMethod]
        public async Task A_Mode_Message_With_A_Channel_Parameter_Is_Successfully_Generated()
        {
            var expected = "MODE #helloworld\r\n";
            ISendableMessage testMessage = new ModeMessage("#helloworld");
            await TestHelpers.RunSendMessageTest(expected, testMessage);
        }

        //todo: lots of mode tests
    }
}