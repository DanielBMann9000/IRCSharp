using Xunit;
using System.Diagnostics.CodeAnalysis;

using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Interfaces;


namespace IrcSharp.Core.Tests.Unit;
// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToConstant.Local


public class When_Generating_User_Based_Query_Messages
{
    [Fact]
    public void A_Who_Message_With_No_Mask_Or_Operator_Only_Flag_Generates_Correctly()
    {
        var expected = "WHO\r\n";
        ISendableMessage testMessage = new WhoMessage();
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Who_Message_With_A_Mask_And_No_Operator_Only_Flag_Generates_Correctly()
    {
        var expected = "WHO *.com\r\n";
        ISendableMessage testMessage = new WhoMessage("*.com");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Who_Message_With_A_Mask_And_The_Operator_Only_Flag_Generates_Correctly()
    {
        var expected = "WHO *.com o\r\n";
        ISendableMessage testMessage = new WhoMessage("*.com", true);
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Whois_Message_With_A_Single_Mask_And_No_Target_Generates_Correctly()
    {
        var expected = "WHOIS daniel\r\n";
        ISendableMessage testMessage = new WhoisMessage("daniel");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Whois_Message_With_Multiple_Masks_And_No_Target_Generates_Correctly()
    {
        var expected = "WHOIS daniel,dbm,skippy\r\n";
        ISendableMessage testMessage = new WhoisMessage(new[] { "daniel", "dbm", "skippy" });
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Whois_Message_With_A_Single_Mask_And_A_Target_Generates_Correctly()
    {
        var expected = "WHOIS someTarget daniel\r\n";
        ISendableMessage testMessage = new WhoisMessage("daniel", "someTarget");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Whois_Message_With_A_Multiple_Masks_And_A_Target_Generates_Correctly()
    {
        var expected = "WHOIS someTarget daniel,dbm,skippy\r\n";
        ISendableMessage testMessage = new WhoisMessage(new[] { "daniel", "dbm", "skippy" }, "someTarget");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Whowas_Message_With_A_Single_Nickname_And_No_Count_Or_Target_Generates_Correctly()
    {
        var expected = "WHOWAS daniel\r\n";
        ISendableMessage testMessage = new WhowasMessage("daniel");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Whowas_Message_With_Multiple_Nicknames_And_No_Count_Or_Target_Generates_Correctly()
    {
        var expected = "WHOWAS daniel,dbm,skippy\r\n";
        ISendableMessage testMessage = new WhowasMessage(new[] { "daniel", "dbm", "skippy" });
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Whowas_Message_With_A_Single_Nickname_And_A_Count_And_No_Target_Generates_Correctly()
    {
        var expected = "WHOWAS daniel 9\r\n";
        ISendableMessage testMessage = new WhowasMessage("daniel", 9);
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Whowas_Message_With_Multiple_Nicknames_And_A_Count_And_No_Target_Generates_Correctly()
    {
        var expected = "WHOWAS daniel,dbm,skippy 9\r\n";
        ISendableMessage testMessage = new WhowasMessage(new[] { "daniel", "dbm", "skippy" }, 9);
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Whowas_Message_With_A_Single_Nickname_And_A_Count_And_A_Target_Generates_Correctly()
    {
        var expected = "WHOWAS daniel 9 *.edu\r\n";
        ISendableMessage testMessage = new WhowasMessage("daniel", 9, "*.edu");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Whowas_Message_With_Multiple_Nicknames_And_A_Count_And_A_Target_Generates_Correctly()
    {
        var expected = "WHOWAS daniel,dbm,skippy 9 *.edu\r\n";
        ISendableMessage testMessage = new WhowasMessage(new[] { "daniel", "dbm", "skippy" }, 9, "*.edu");
        Assert.Equal(expected, testMessage.ToMessage());
    }
}
