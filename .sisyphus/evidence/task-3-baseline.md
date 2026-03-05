# Moq Migration Baseline

**Date**: 2026-03-04  
**Session ID**: ses_345112554ffeRudVUXm5JO34Zi

## Current State (Pre-Migration)

### Test Results
- **Total Tests**: 196
- **Passed**: 196
- **Failed**: 0
- **Execution Time**: 1.0727 seconds

### FakeSocketConnection Usage
- **Total References**: 38
- **Files Using FakeSocketConnection**:
  - `FakeSocketConnection.cs` (definition)
  - `TestHelpers.cs` (1 usage)
  - `When_Parsing_Received_Messages.cs` (multiple usages)
  - `When_Generating_Miscellaneous_Messages.cs` (1 usage)
  - Other test files (implied by 38 total refs)

### SimulateMessageReceipt() Usage
- **Total References**: TBD (will document during migration)

### Git Status
- Clean working directory (after Task 1 commit)
- Only changes: Moq package added to csproj

## Migration Goals

After migration:
- Replace all `FakeSocketConnection` instantiations with `Mock<ISocketConnection>`
- Replace `SimulateMessageReceipt()` calls with `mockSocket.Raise()`
- Add comprehensive Moq verification assertions (VerifyNoOtherCalls, Times.*)
- Remove `FakeSocketConnection.cs` file
- Fix `async void Reconnect()` to `async Task`

## Baseline Metrics for Comparison

- Test Count: 196 (should remain identical)
- Pass Rate: 100% (must remain 100%)
- Execution Time: 1.0727 seconds (should be within 10%: 0.965 - 1.180 seconds)

## Next Steps

1. Fix `async void Reconnect()` anti-pattern
2. Migrate TestHelpers.cs to use Moq
3. Migrate When_Sending_Messages.cs (40+ tests)
4. Migrate all other test files
5. Remove FakeSocketConnection.cs
6. Final verification