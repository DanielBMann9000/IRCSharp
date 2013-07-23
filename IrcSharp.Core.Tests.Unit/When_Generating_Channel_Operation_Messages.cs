using System.Diagnostics.CodeAnalysis;

using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Sendable;
using IrcSharp.Core.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class When_Generating_Channel_Operation_Messages
    {
        [TestMethod]
        public void A_Join_Message_For_A_Single_Keyless_Channel_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld\r\n";
            ISendableMessage testMessage = new JoinMessage("#helloworld");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Join_Message_For_A_Single_Keyed_Channel_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld thisisakey\r\n";
            ISendableMessage testMessage = new JoinMessage("#helloworld", "thisisakey");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Join_Message_For_A_Multiple_Channels_And_No_Keys_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld,#goodbyeworld\r\n";
            ISendableMessage testMessage = new JoinMessage(new[] { "#helloworld", "#goodbyeworld" });
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Join_Message_For_A_Multiple_Channels_With_Keys_Is_Successfully_Generated()
        {
            var expected = "JOIN #helloworld,#goodbyeworld hi,bye\r\n";
            ISendableMessage testMessage = new JoinMessage(
                new[] { "#helloworld", "#goodbyeworld" },
                new[] { "hi", "bye" });
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Join_Message_With_No_Channels_Provided_Sends_A_Part_Message()
        {
            var expected = "JOIN 0\r\n";
            ISendableMessage testMessage = new JoinMessage();
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Part_Message_With_One_Channel_And_No_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld\r\n";
            ISendableMessage testMessage = new PartMessage(new[] { "#helloworld" });
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Part_Message_With_One_Channel_A_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld :Goodbye, cruel world!\r\n";
            ISendableMessage testMessage = new PartMessage(new[] { "#helloworld" }, "Goodbye, cruel world!");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Part_Message_With_Multiple_Channels_And_No_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld,#goodbyeworld\r\n";
            ISendableMessage testMessage = new PartMessage(new[] { "#helloworld", "#goodbyeworld" });
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Part_Message_With_Multiple_Channels_And_A_Parting_Message_Is_Successfully_Generated()
        {
            var expected = "PART #helloworld,#goodbyeworld :Now is the winter of our discount tent\r\n";
            ISendableMessage testMessage = new PartMessage(
                new[] { "#helloworld", "#goodbyeworld" },
                "Now is the winter of our discount tent");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Topic_Message_For_A_Channel_With_No_New_Topic_Is_Successfully_Generated()
        {
            var expected = "TOPIC #helloworld\r\n";
            ISendableMessage testMessage = new TopicMessage("#helloworld");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Topic_Message_For_A_Channel_With_A_New_Topic_Is_Successfully_Generated()
        {
            var expected = "TOPIC #helloworld :Welcome to the internet, bozo!\r\n";
            ISendableMessage testMessage = new TopicMessage("#helloworld", "Welcome to the internet, bozo!");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Topic_Message_For_A_Channel_With_Topic_Removal_Is_Successfully_Generated()
        {
            var expected = "TOPIC #helloworld :\r\n";
            ISendableMessage testMessage = new TopicMessage("#helloworld", true);
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Names_Message_With_No_Parameters_Is_Successfully_Generated()
        {
            var expected = "NAMES\r\n";
            ISendableMessage testMessage = new NamesMessage();
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Names_Message_With_A_Single_Channel_Parameter_Is_Successfully_Generated()
        {
            var expected = "NAMES #helloworld\r\n";
            ISendableMessage testMessage = new NamesMessage("#helloworld");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Names_Message_With_A_Multiple_Channel_Parameter_Is_Successfully_Generated()
        {
            var expected = "NAMES #helloworld,#goodbyeworld,#worldworld\r\n";
            ISendableMessage testMessage = new NamesMessage(new[] { "#helloworld", "#goodbyeworld", "#worldworld"});
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Names_Message_With_A_Single_Channel_Parameter_And_A_Target_Parameter_Is_Successfully_Generated()
        {
            var expected = "NAMES #helloworld target\r\n";
            ISendableMessage testMessage = new NamesMessage("#helloworld", "target");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Names_Message_With_A_Multiple_Channel_Parameter_And_A_Target_Parameter_IsSuccessfully_Generated()
        {
            var expected = "NAMES #helloworld,#goodbyeworld,#worldworld target\r\n";
            ISendableMessage testMessage = new NamesMessage(new[] { "#helloworld", "#goodbyeworld", "#worldworld" }, "target");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_List_Message_With_No_Parameters_Is_Successfully_Generated()
        {
            var expected = "LIST\r\n";
            ISendableMessage testMessage = new ListMessage();
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_List_Message_With_A_Single_Channel_Parameter_Is_Successfully_Generated()
        {
            var expected = "LIST #helloworld\r\n";
            ISendableMessage testMessage = new ListMessage("#helloworld");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_List_Message_With_A_Multiple_Channel_Parameter_Is_Successfully_Generated()
        {
            var expected = "LIST #helloworld,#goodbyeworld,#worldworld\r\n";
            ISendableMessage testMessage = new ListMessage(new[] { "#helloworld", "#goodbyeworld", "#worldworld" });
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_List_Message_With_A_Single_Channel_Parameter_And_A_Target_Parameter_Is_Successfully_Generated()
        {
            var expected = "LIST #helloworld target\r\n";
            ISendableMessage testMessage = new ListMessage("#helloworld", "target");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_List_Message_With_A_Multiple_Channel_Parameter_And_A_Target_Parameter_IsSuccessfully_Generated()
        {
            var expected = "LIST #helloworld,#goodbyeworld,#worldworld target\r\n";
            ISendableMessage testMessage = new ListMessage(new[] { "#helloworld", "#goodbyeworld", "#worldworld" }, "target");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void An_Invite_Message_With_A_Channel_And_A_Nick_Is_Successfully_Generated()
        {
            var expected = "INVITE DBM #helloworld\r\n";
            ISendableMessage testMessage = new InviteMessage("DBM", "#helloworld");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Kick_Message_With_A_Single_Channel_And_A_Single_Nick_And_No_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld DBM\r\n";
            ISendableMessage testMessage = new KickMessage("#helloworld", "DBM");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Kick_Message_With_A_Single_Channel_And_A_Single_Nick_And_A_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld DBM :You are bad at IRC, sir.\r\n";
            ISendableMessage testMessage = new KickMessage("#helloworld", "DBM", "You are bad at IRC, sir.");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Kick_Message_With_Multiple_Channels_And_A_Single_Nick_And_No_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld,#goodbyeworld DBM\r\n";
            ISendableMessage testMessage = new KickMessage(new[] { "#helloworld", "#goodbyeworld" }, "DBM");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Kick_Message_With_Multiple_Channels_And_A_Single_Nick_And_A_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld,#goodbyeworld DBM :Bye, bye!\r\n";
            ISendableMessage testMessage = new KickMessage(new[] { "#helloworld", "#goodbyeworld" }, "DBM", "Bye, bye!");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Kick_Message_With_A_Single_Channel_And_Multiple_Nicks_And_No_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld DBM,Gilligan,TheSkipper\r\n";
            ISendableMessage testMessage = new KickMessage("#helloworld", new [] {"DBM", "Gilligan", "TheSkipper"});
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Kick_Message_With_A_Single_Channel_And_Multiple_Nicks_And_A_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld DBM,Gilligan,TheSkipper :A three minute tour\r\n";
            ISendableMessage testMessage = new KickMessage("#helloworld", new[] { "DBM", "Gilligan", "TheSkipper" }, "A three minute tour");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Kick_Message_With_Multiple_Channels_And_Multiple_Nicks_And_No_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld,#theisland DBM,Gilligan,TheSkipper\r\n";
            ISendableMessage testMessage = new KickMessage(new [] { "#helloworld", "#theisland" }, new[] { "DBM", "Gilligan", "TheSkipper" });
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Kick_Message_With_Multiple_Channels_And_Multiple_Nicks_And_A_Message_Is_Successfully_Generated()
        {
            var expected = "KICK #helloworld,#theisland DBM,Gilligan,TheSkipper :Where's Ginger?\r\n";
            ISendableMessage testMessage = new KickMessage(new[] { "#helloworld", "#theisland" }, new[] { "DBM", "Gilligan", "TheSkipper" }, "Where's Ginger?");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Mode_Message_With_A_Channel_Parameter_Is_Successfully_Generated()
        {
            var expected = "MODE #helloworld\r\n";
            ISendableMessage testMessage = new ChannelModeMessage("#helloworld");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Mode_Message_With_A_Channel_Parameter_And_A_Single_Mode_Change_With_No_Target_Is_Successfully_Generated()
        {
            var expected = "MODE #helloworld +s\r\n";
            ISendableMessage testMessage = new ChannelModeMessage("#helloworld", "+s");
            Assert.AreEqual(expected, testMessage.ToString());
        }

        [TestMethod]
        public void A_Mode_Message_With_A_Channel_Parameter_And_A_Single_Mode_Change_With_A_Target_Is_Successfully_Generated()
        {
            var expected = "MODE #helloworld +o Daniel\r\n";
            ISendableMessage testMessage = new ChannelModeMessage("#helloworld", "+o Daniel");
            Assert.AreEqual(expected, testMessage.ToString());
        }
        
    }
}