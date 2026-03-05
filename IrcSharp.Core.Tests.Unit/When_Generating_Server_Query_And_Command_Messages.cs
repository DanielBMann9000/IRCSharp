using Xunit;
using System.Diagnostics.CodeAnalysis;

using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Interfaces;


namespace IrcSharp.Core.Tests.Unit;
// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToConstant.Local


public class When_Generating_Server_Query_And_Command_Messages
{
    [Fact]
    public void A_Motd_Message_With_No_Target_Generates_Correctly()
    {
        var expected = "MOTD\r\n";
        ISendableMessage testMessage = new MotdMessage();
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Motd_Message_With_A_Target_Generates_Correctly()
    {
        var expected = "MOTD someserver\r\n";
        ISendableMessage testMessage = new MotdMessage("someserver");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Lusers_Message_With_No_Mask_Or_Target_Generates_Correctly()
    {
        var expected = "LUSERS\r\n";
        ISendableMessage testMessage = new LusersMessage();
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Lusers_Message_With_A_Mask_And_No_Target_Generates_Correctly()
    {
        var expected = "LUSERS someMask\r\n";
        ISendableMessage testMessage = new LusersMessage("someMask");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Lusers_Message_With_A_Mask_And_A_Target_Generates_Correctly()
    {
        var expected = "LUSERS someMask someTarget\r\n";
        ISendableMessage testMessage = new LusersMessage("someMask", "someTarget");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Version_Message_With_No_Target_Generates_Correctly()
    {
        var expected = "VERSION\r\n";
        ISendableMessage testMessage = new VersionMessage();
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Version_Message_With_A_Target_Generates_Correctly()
    {
        var expected = "VERSION someTarget\r\n";
        ISendableMessage testMessage = new VersionMessage("someTarget");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Stats_Message_With_No_Query_Or_Target_Generates_Correctly()
    {
        var expected = "STATS\r\n";
        ISendableMessage testMessage = new StatsMessage();
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Stats_Message_With_A_Query_And_No_Target_Generates_Correctly()
    {
        var expected = "STATS someQuery\r\n";
        ISendableMessage testMessage = new StatsMessage("someQuery");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Stats_Message_With_A_Query_And_A_Target_Generates_Correctly()
    {
        var expected = "STATS someQuery someTarget\r\n";
        ISendableMessage testMessage = new StatsMessage("someTarget", "someQuery");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Links_Message_With_No_Remote_Server_Or_Server_Mask_Generates_Correctly()
    {
        var expected = "LINKS\r\n";
        ISendableMessage testMessage = new LinksMessage();
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Stats_Message_With_A_Server_Mask_And_No_Remote_Server_Generates_Correctly()
    {
        var expected = "LINKS someMask\r\n";
        ISendableMessage testMessage = new LinksMessage("someMask");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Stats_Message_With_A_Server_Mask_And_A_Remote_Server_Generates_Correctly()
    {
        var expected = "LINKS someRemoteServer someMask\r\n";
        ISendableMessage testMessage = new LinksMessage("someMask", "someRemoteServer");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Time_Message_With_No_Target_Generates_Correctly()
    {
        var expected = "TIME\r\n";
        ISendableMessage testMessage = new TimeMessage();
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Time_Message_With_A_Target_Generates_Correctly()
    {
        var expected = "TIME foo.bar.com\r\n";
        ISendableMessage testMessage = new TimeMessage("foo.bar.com");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Connect_Message_With_No_Remote_Server_Generates_Correctly()
    {
        var expected = "CONNECT targetServer.com 6667\r\n";
        ISendableMessage testMessage = new ConnectMessage("targetServer.com", 6667);
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Connect_Message_With_A_Remote_Server_Generates_Correctly()
    {
        var expected = "CONNECT targetServer.com 6667 remoteServer\r\n";
        ISendableMessage testMessage = new ConnectMessage("targetServer.com", 6667, "remoteServer");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Trace_Message_With_No_Target_Generates_Correctly()
    {
        var expected = "TRACE\r\n";
        ISendableMessage testMessage = new TraceMessage();
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Trace_Message_With_A_Target_Generates_Correctly()
    {
        var expected = "TRACE someTarget\r\n";
        ISendableMessage testMessage = new TraceMessage("someTarget");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void An_Admin_Message_With_No_Target_Generates_Correctly()
    {
        var expected = "ADMIN\r\n";
        ISendableMessage testMessage = new AdminMessage();
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void An_Admin_Message_With_A_Target_Generates_Correctly()
    {
        var expected = "ADMIN someTarget\r\n";
        ISendableMessage testMessage = new AdminMessage("someTarget");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void An_Info_Message_With_No_Target_Generates_Correctly()
    {
        var expected = "INFO\r\n";
        ISendableMessage testMessage = new InfoMessage();
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void An_Info_Message_With_A_Target_Generates_Correctly()
    {
        var expected = "INFO someTarget\r\n";
        ISendableMessage testMessage = new InfoMessage("someTarget");
        Assert.Equal(expected, testMessage.ToMessage());
    }
}
