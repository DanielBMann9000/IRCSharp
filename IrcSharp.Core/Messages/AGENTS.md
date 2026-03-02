# Messages

**Generated:** 2026-03-02
**Context:** Core library submodule

## OVERVIEW

IRC message types (RFC 2812 compliant) with typed event propagation. 42 message classes handling all IRC commands.

## WHERE TO LOOK

| Message Category | Files | Example |
|------------------|-------|---------|
| Connection registration | 3 | PassMessage, NickMessage, UserMessage |
| Channel operations | 8 | JoinMessage, PartMessage, ModeMessage, TopicMessage, NamesMessage, ListMessage, InviteMessage, KickMessage |
| Sending | 4 | PrivMsgMessage, NoticeMessage, PongMessage |
| Server queries | 12 | MotdMessage, LusersMessage, VersionMessage, StatsMessage, LinksMessage, TimeMessage, ConnectMessage, TraceMessage, AdminMessage, InfoMessage, ServlistMessage, SqueryMessage |
| User queries | 4 | WhoMessage, WhoisMessage, WhowasMessage |
| Miscellaneous | 4 | KillMessage, PingMessage, PongMessage, QuitMessage |

## CONVENTIONS

- Each message type has `ToMessage()` method returning raw IRC string
- Propagation events: `On*MessageSending`, `On*MessageSent`, `On*MessageReceived`
- Numeric responses: GenericNumericResponseMessage, NotRegisteredNumericResponseMessage
- MessagePropagator routes by command string (e.g., "PRIVMSG", "JOIN")

## ANTI-PATTERNS

- `TokenizeArguments` method (line 534 MessagePropagator.cs) — may not tokenize all IRC messages correctly
- Large MessagePropagator class (547 lines) — consider splitting by category