# Unit Tests

**Generated:** 2026-03-02
**Context:** Test project

## OVERVIEW

MSTest unit tests for core library. 13 test files covering message parsing, generation, and event firing.

## WHERE TO LOOK

| Test File | Focus |
|-----------|-------|
| When_Parsing_Received_Messages.cs | Message parsing logic |
| When_Generating_*_Messages.cs (8 files) | Message ToMessage() output |
| When_Sending_Messages.cs | Event firing verification |
| When_Receiving_Messages.cs | Event firing verification |
| TestHelpers.cs | Shared async test runner |
| FakeSocketConnection.cs | Mock socket implementation |

## CONVENTIONS

- Test naming: `When_<Scenario>_<ExpectedResult>`
- Async tests use ManualResetEvent with 1-second timeout
- TestHelpers.RunSendableEventFiringTest for event-based testing
- FakeSocketConnection simulates IRC traffic
- `[ExcludeFromCodeCoverage]` on test classes

## ANTI-PATTERNS

- MSTest framework (Microsoft.VisualStudio.QualityTools) — consider xUnit/NUnit
- Pragma warning disable 1998 on async void test methods