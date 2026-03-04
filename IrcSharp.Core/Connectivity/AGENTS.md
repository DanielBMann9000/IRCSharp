# Connectivity

**Generated:** 2026-03-02
**Context:** Core library submodule

## OVERVIEW

Network connection management. Async socket I/O with connection lifecycle and reconnect logic.

## STRUCTURE

```
Connectivity/
├── IrcConnection.cs          # Main API (189 lines)
├── SocketConnection.cs       # Socket wrapper (144 lines)
├── ISocketConnection.cs      # Interface
├── ConnectionFailedException.cs
└── MessageEventArgs.cs
```

## WHERE TO LOOK

| Component | File | Role |
|-----------|------|------|
| IrcConnection | `IrcConnection.cs` | Main API, event aggregation |
| SocketConnection | `SocketConnection.cs` | TCP socket wrapper |
| ISocketConnection | `ISocketConnection.cs` | Interface |
| ConnectionFailedException | `ConnectionFailedException.cs` | Exception type |
| MessageEventArgs | `MessageEventArgs.cs` | Event args |

## CONVENTIONS

- Async/await for all network operations
- Event-driven: `OnDisconnected`, `OnUnexpectedDisconnection`
- Interface-based design for testability

## ANTI-PATTERNS

- `async void Reconnect()` — line 135 IrcConnection.cs (error handling issue)
- Busy waiting with `while (!canSend)` + `Task.Delay(500)` — lines 98-101

## NOTES

- Reconnect loop uses 5-second delay
- Event-driven disconnection handling
- Socket connection supports graceful shutdown