# Connectivity

**Generated:** 2026-03-02
**Context:** Core library submodule

## OVERVIEW

Network connection management. Async socket I/O with connection lifecycle and reconnect logic.

## WHERE TO LOOK

| Component | File | Role |
|-----------|------|------|
| IrcConnection | `IrcConnection.cs` | Main API, event aggregation |
| SocketConnection | `SocketConnection.cs` | TCP socket wrapper |
| ISocketConnection | `ISocketConnection.cs` | Interface |
| ConnectionFailedException | `ConnectionFailedException.cs` | Exception type |
| MessageEventArgs | `MessageEventArgs.cs` | Event args |

## STRUCTURE

```
Connectivity/
├── IrcConnection.cs          # Main API (189 lines)
├── SocketConnection.cs       # Socket wrapper (144 lines)
├── ISocketConnection.cs      # Interface
├── ConnectionFailedException.cs
└── MessageEventArgs.cs
```

## CONVENTIONS

- Async/await for all network operations
- Event-driven: `OnDisconnected`, `OnUnexpectedDisconnection`
- Reconnect loop with 5-second delay (async void anti-pattern)

## ANTI-PATTERNS

- `async void Reconnect()` — line 136 IrcConnection.cs (error handling issue)
- Busy waiting with `while (!canSend)` + `Task.Delay(500)` — line 98-101