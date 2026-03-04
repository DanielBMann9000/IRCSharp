# Draft: Moq Migration Planning

## Original Request
Replace home-grown DI mocks with Moq framework throughout unit and integration tests. Integration tests are currently disabled but structural changes should be made for when they're re-enabled.

## Current State Analysis

### Mock Infrastructure
**FakeSocketConnection** (`IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs`)
- 84 lines implementing `ISocketConnection` interface
- Simulates connection lifecycle, message sending/receiving
- Manually tracks sent messages, fires events via `SimulateMessageReceipt()` method
- Used across 11 unit test files

### Dependency Injection Structure
- **Interface-based DI**: Already in place via `ISocketConnection` interface
- **Constructor injection**: `IrcConnection` accepts `ISocketConnection` via constructor
- **Manual DI**: No framework - tests explicitly construct dependencies
- **No production code changes needed**: Already testable design

### Affected Files
**Test Code (11 files)**:
- `FakeSocketConnection.cs` - entire file to be replaced
- `TestHelpers.cs` - `RunSendableEventFiringTest` method refactored
- `When_Sending_Messages.cs` - 40+ test methods
- `When_Receiving_Messages.cs` - 15+ test methods
- `When_Parsing_Received_Messages.cs`
- 8x `When_Generating_*_Messages.cs` files

**Production Code (0 files)**:
- Already interface-based, no changes needed

### Test Patterns Identified
1. **Event firing tests**: Subscribe to events, verify they fire
2. **Message sending**: Create message, send, verify event fires
3. **Message receiving**: Call `SimulateMessageReceipt()`, verify event handlers execute
4. **Manual tracking**: Check internal state (message lists, counters)

## Moq Framework Research

### Package Information
- **Package**: `Moq`
- **Version for .NET Framework 4.5**: `4.5.30` (compatible)
- **Install**: `Install-Package Moq -Version 4.5.30`

### Key API Patterns

**Mock Creation**:
```csharp
var mockSocket = new Mock<ISocketConnection>();
ISocketConnection socket = mockSocket.Object;
```

**Setup Expectations**:
```csharp
mockSocket.Setup(s => s.Connect()).Returns(true);
mockSocket.Setup(s => s.IsConnected()).Returns(true);
```

**Event Handling** (critical for IRCSharp):
```csharp
// Setup event subscription
mockSocket.SetupAdd(s => s.OnMessageReceived += It.IsAny<EventHandler>());

// Raise events
mockSocket.Raise(s => s.OnMessageReceived += null, this, new MessageEventArgs { ... });
```

**Verification**:
```csharp
mockSocket.Verify(s => s.Connect(), Times.Once());
mockSocket.VerifyNoOtherCalls();
```

### Migration Pattern Examples

**Before (Custom Mock)**:
```csharp
var fakeSocket = new FakeSocketConnection();
var connection = new IrcConnection(fakeSocket);
fakeSocket.SimulateMessageReceipt(":localhost 001 Welcome");
// ... verify event fired
```

**After (Moq)**:
```csharp
var mockSocket = new Mock<ISocketConnection>();
var eventFired = false;
mockSocket.SetupAdd(s => s.OnMessageReceived += It.IsAny<EventHandler>())
    .Callback((object sender, MessageEventArgs args) => eventFired = true);
mockSocket.Raise(s => s.OnMessageReceived += null, this, new MessageEventArgs { Message = ":localhost 001 Welcome" });
var connection = new IrcConnection(mockSocket.Object);
Assert.True(eventFired);
```

## Technical Decisions Needed

### 1. Event Handling Strategy
Current `FakeSocketConnection` uses public event fields. Moq requires setup for event subscription tracking.

**Question**: Should we:
- A) Keep events as public fields (current pattern) and use Moq's `SetupAdd`/`Raise`?
- B) Change events to properties with getters/setters for better Moq integration?
- C) Hybrid approach - keep events as-is, handle in test helpers?

### 2. Test Helper Refactoring
`TestHelpers.RunSendableEventFiringTest()` currently creates `FakeSocketConnection` and manages async test flow.

**Question**: Should this helper be:
- A) Completely rewritten to use Moq?
- B) Kept as-is for backward compatibility, new tests use Moq directly?
- C) Removed entirely, tests manage their own setup?

### 3. Integration Test Strategy
Integration tests currently disabled, but structural changes needed.

**Question**: Should we:
- A) Create Moq-based mocks for integration test dependencies too?
- B) Only migrate unit tests, leave integration tests for later?
- C) Create a separate branch for integration test migration?

### 4. Migration Scope
**Question**: Should this migration include:
- [ ] All 11 unit test files
- [ ] `TestHelpers.cs`
- [ ] `FakeSocketConnection.cs` deletion
- [ ] Integration test file updates (even if disabled)
- [ ] Update `AGENTS.md` documentation

## Scope Boundaries

### IN SCOPE
- Migrating unit tests from `FakeSocketConnection` to Moq
- Removing `FakeSocketConnection.cs`
- Updating `TestHelpers.cs` to use Moq
- Adding Moq NuGet dependency
- Ensuring all existing tests pass with Moq

### OUT OF SCOPE
- Changing production code (already interface-based)
- Enabling integration tests (they remain disabled)
- Major refactoring of test structure beyond mock replacement
- Adding new test coverage (migration only)

## Open Questions

1. **Moq version**: Stick with 4.5.30 (known .NET 4.5 compatible) or use latest 4.x that supports 4.5?
2. **Event handling**: How to best handle `EventHandler<T>` patterns with Moq?
3. **Async methods**: Current code uses `async void` anti-pattern - should Moq migration address this?
4. **Test cleanup**: Should we add `VerifyNoOtherCalls()` to all tests or be selective?
5. **Rollback strategy**: Keep old mocks in git, or delete immediately?

## Risk Assessment

### Low Risk
- Production code unchanged
- Interface-based design already in place
- Moq is well-established, stable library

### Medium Risk
- Event handling patterns may need experimentation
- Async method mocking could be tricky with `async void`
- Large number of test files (11+) to migrate

### Mitigation Strategy
- Pilot migration on 1-2 simple test files first
- Keep integration tests disabled until unit tests are stable
- Incremental commits per file group

## Next Steps

1. **Decision needed on**: Event handling strategy, test helper approach, integration test scope
2. **Pilot migration**: Select 2-3 simple test files
3. **Validate approach**: Ensure tests pass, event handling works
4. **Full migration**: Apply pattern to all test files
5. **Cleanup**: Remove `FakeSocketConnection.cs`, update docs

---

*Last updated: 2026-03-04*