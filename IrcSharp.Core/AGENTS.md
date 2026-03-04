# IRCSharp Core

**Generated:** 2026-03-04
**Target Framework:** .NET 10.0

## OVERVIEW

Event-driven, asynchronous IRC library. Core message handling, connection management, and propagation system. Still in heavy refactoring cycle.

## STRUCTURE

```
IrcSharp.Core/
├── Connectivity/      # Socket I/O, connection lifecycle
├── Messages/          # IRC message types and propagation
├── Model/             # Data models (IrcUserInfo)
└── Properties/        # Assembly info
```

## WHERE TO LOOK

| Task | Location | Notes |
|------|----------|-------|
| IRC connection | `Connectivity/IrcConnection.cs` | Main entry point, async |
| Socket I/O | `Connectivity/SocketConnection.cs` | TCP wrapper |
| Message types | `Messages/*.cs` | 42 message classes |
| Message propagation | `Messages/Propagation/` | Event-based routing |

## CODE MAP

| Symbol | Type | Location | Role |
|--------|------|----------|------|
| `IrcConnection` | class | `Connectivity/` | Main API, event-driven |
| `MessagePropagator` | class | `Messages/Propagation/` | Event routing system |
| `ISocketConnection` | interface | `Connectivity/` | Socket abstraction |
| `ISendableMessage` | interface | `Messages/Interfaces/` | Message contract |
| `IReceivableMessage` | interface | `Messages/Interfaces/` | Incoming message contract |

## CONVENTIONS

- Async/await throughout (Task-based)
- Event-driven with `EventHandler<T>` patterns
- Interface-based design for testability
- BDD-style test naming: `When_<Scenario>_<Expected>`

## ANTI-PATTERNS (THIS PROJECT)

- `async void` in `Reconnect()` method, error handling anti-pattern
- Busy waiting with `while (!canSend)` + `Task.Delay` — polling vs event-based
- Legacy MSBuild .csproj format (pre-SDK style)
- Microsoft.VisualStudio.QualityTools MSTest framework (not open-source)

## UNIQUE STYLES

- Typed event handlers per IRC message type (OnPrivMsgMessageSending, OnNickMessageSent, etc.)
- Central MessagePropagator with regex-like command routing
- Separate sending/sent event phases for observability

## COMMANDS

```bash
# Build
msbuild IrcSharp.sln

# Run tests (Visual Studio)
Test → Run → All Tests
```

## NOTES

- No CI/CD configured
- Integration tests require real IRC server (bircd.exe embedded)
- Async void for reconnect handlers is known issue (see line 136 IrcConnection.cs)