# Integration Tests

**Generated:** 2026-03-02
**Context:** Test project

## OVERVIEW

Integration tests against real IRC server (bircd.exe embedded).

## WHERE TO LOOK

| Component | File | Role |
|-----------|------|------|
| AssemblyInit.cs | Server lifecycle management |
| When_Connecting_To_A_Real_Server.cs | Connection/integration tests |

## STRUCTURE

```
IrcSharp.Core.Tests.Integration/
├── AssemblyInit.cs                    # Server start/stop
├── When_Connecting_To_A_Real_Server.cs
├── Properties/
├── bircd.exe (embedded)              # Test IRC server binary
└── App.config                        # Server host/port config
```

## CONVENTIONS

- `[AssemblyInitialize]` / `[AssemblyCleanup]` for server lifecycle
- Embedded bircd.exe copied to output directory
- Server configurable via App.config (default: localhost:5454)
- Tests verify connection, disconnection, reconnect scenarios

## ANTI-PATTERNS

- Embedded binary in test project — not typical separation
- Flaky tests due to real server dependency (as noted in README)