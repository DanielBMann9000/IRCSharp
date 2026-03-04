# IRCSharp

**Generated:** 2026-03-04
**Commit:** (from git)
**Branch:** (current branch)

## OVERVIEW

Event-driven, asynchronous IRC library. Core functionality: connection management, message propagation, async socket I/O. Still in heavy refactoring cycle.

## STRUCTURE

```
IRCSharp/
├── IrcSharp.sln                          # Visual Studio solution
├── IrcSharp.Core/                        # Main library (.NET 10.0)
│   ├── Connectivity/                       # Socket I/O, connection lifecycle
│   ├── Messages/                           # 42 RFC 2812 message classes
│   │   └── Propagation/                    # Event-based message routing
│   └── Model/                              # Data models
├── IrcSharp.Core.Tests.Unit/             # Unit tests (MSTest)
├── IrcSharp.Core.Tests.Integration/      # Integration tests
└── README.md                             # Project overview
```

## WHERE TO LOOK

| Task | Location | Notes |
|------|----------|-------|
| Main API | `IrcSharp.Core/Connectivity/IrcConnection.cs` | Async connect, send, disconnect |
| Message types | `IrcSharp.Core/Messages/` | 42 RFC 2812 message classes |
| Message routing | `IrcSharp.Core/Messages/Propagation/` | Typed event propagation |
| Unit tests | `IrcSharp.Core.Tests.Unit/` | MSTest with FakeSocketConnection |
| Integration tests | `IrcSharp.Core.Tests.Integration/` | Real server (bircd.exe) |

## CODE MAP

| Symbol | Type | Location | Refs | Role |
|--------|------|----------|------|------|
| `IrcConnection` | class | `Connectivity/` | 189 lines | Main API, event aggregator |
| `MessagePropagator` | class | `Propagation/` | 547 lines | Event routing system |
| `SocketConnection` | class | `Connectivity/` | 144 lines | TCP socket wrapper |
| `ISocketConnection` | interface | `Connectivity/` | - | Socket abstraction |
| `ISendableMessage` | interface | `Messages/Interfaces/` | - | Message contract |

## CONVENTIONS

- Async/await throughout (Task-based)
- Event-driven with `EventHandler<T>` patterns
- Interface-based design for testability
- BDD-style test naming: `When_<Scenario>_<Expected>`
- MSTest framework (Microsoft.VisualStudio.QualityTools)
- Legacy MSBuild .csproj format (pre-SDK style)

## ANTI-PATTERNS (THIS PROJECT)

- `async void` in `Reconnect()` - error handling anti-pattern (line 135)
- Busy waiting with `while (!canSend)` + `Task.Delay(500)` - polling vs event-based (lines 98-101)
- Large MessagePropagator class (547 lines) - consider splitting
- Microsoft MSTest framework (not open-source)
- Embedded test server binary in integration tests

## COMMANDS

```
# Build
msbuild IrcSharp.sln

# Run tests (Visual Studio)
Test → Run → All Tests
```

## NOTES

- No CI/CD configured
- Integration tests require real IRC server (bircd.exe embedded)
- No external config files (.eslintrc, .editorconfig, etc.)
- Target framework: .NET 10.0