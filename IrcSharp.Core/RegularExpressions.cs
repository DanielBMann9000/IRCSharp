using System.Text.RegularExpressions;

namespace IrcSharp.Core
{
    internal static class RegularExpressions
    {
        internal static Regex ReplyCodeRegex = new Regex("^:[^ ]+? ([0-9]{3}) .+$", RegexOptions.Compiled);
        internal static Regex PingRegex = new Regex("^PING :.*", RegexOptions.Compiled);
        internal static Regex ErrorRegex = new Regex("^ERROR :.*", RegexOptions.Compiled);
        internal static Regex ActionRegex = new Regex("^:.*? PRIVMSG (.).* :" + "\x1" + "ACTION .*" + "\x1" + "$", RegexOptions.Compiled);
        internal static Regex CtcpRequestRegex = new Regex("^:.*? PRIVMSG .* :" + "\x1" + ".*" + "\x1" + "$", RegexOptions.Compiled);
        internal static Regex MessageRegex = new Regex("^:.*? PRIVMSG (.).* :.*$", RegexOptions.Compiled);
        internal static Regex CtcpReplyRegex = new Regex("^:.*? NOTICE .* :" + "\x1" + ".*" + "\x1" + "$", RegexOptions.Compiled);
        internal static Regex NoticeRegex = new Regex("^:.*? NOTICE (.).* :.*$", RegexOptions.Compiled);
        internal static Regex InviteRegex = new Regex("^:.*? INVITE .* .*$", RegexOptions.Compiled);
        internal static Regex JoinRegex = new Regex("^:.*? JOIN .*$", RegexOptions.Compiled);
        internal static Regex TopicRegex = new Regex("^:.*? TOPIC .* :.*$", RegexOptions.Compiled);
        internal static Regex NickRegex = new Regex("^:.*? NICK .*$", RegexOptions.Compiled);
        internal static Regex KickRegex = new Regex("^:.*? KICK .* .*$", RegexOptions.Compiled);
        internal static Regex PartRegex = new Regex("^:.*? PART .*$", RegexOptions.Compiled);
        internal static Regex ModeRegex = new Regex("^:.*? MODE (.*) .*$", RegexOptions.Compiled);
        internal static Regex QuitRegex = new Regex("^:.*? QUIT :.*$", RegexOptions.Compiled);
        internal static Regex NicknameRegex = new Regex(@"^[A-Za-z\[\]\\`_^{|}][A-Za-z0-9\[\]\\`_\-^{|}]+$", RegexOptions.Compiled);
    }
}
