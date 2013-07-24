using System.Diagnostics.CodeAnalysis;

using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class When_Generating_Message_Messages
    {
        [TestMethod]
        public void A_Privmsg_Message_With_A_User_As_The_Target_Generates_A_Message_To_The_Specified_User()
        {
            var expected = "PRIVMSG DestinationUser :This sure is a message!\r\n";
            ISendableMessage testMessage = new PrivMsgMessage("DestinationUser", "This sure is a message!");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_Privmsg_Message_With_A_Channel_As_The_Target_Generates_A_Message_To_The_Specified_Channel()
        {
            var expected = "PRIVMSG #destinationchannel :This sure is a message!\r\n";
            ISendableMessage testMessage = new PrivMsgMessage("#destinationchannel", "This sure is a message!");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_Notice_Message_With_A_User_As_The_Target_Generates_A_Message_To_The_Specified_User()
        {
            var expected = "NOTICE DestinationUser :This sure is a notice!\r\n";
            ISendableMessage testMessage = new NoticeMessage("DestinationUser", "This sure is a notice!");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_Notice_Message_With_A_Channel_As_The_Target_Generates_A_Message_To_The_Specified_Channel()
        {
            var expected = "NOTICE #destinationchannel :This sure is a notice!\r\n";
            ISendableMessage testMessage = new NoticeMessage("#destinationchannel", "This sure is a notice!");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }
    }
}
