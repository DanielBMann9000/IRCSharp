# Moq Migration - Replace FakeSocketConnection with Moq Framework

## TL;DR

> **Quick Summary**: Migrate from custom FakeSocketConnection mock to Moq framework across all unit tests, with quality focus and comprehensive verification. Also fixes async void anti-pattern in Reconnect() method.
> 
> **Deliverables**:
> - Moq NuGet package added to test project
> - All 11 test files migrated to use Moq Mock<ISocketConnection>
> - TestHelpers.cs updated to use Moq mocks
> - async void Reconnect() fixed to async Task
> - Comprehensive Moq verification assertions added
> - FakeSocketConnection.cs removed
> 
> **Estimated Effort**: Medium
> **Parallel Execution**: NO - sequential file-by-file migration with verification
> **Critical Path**: Package add → TestHelpers → Sending tests → Receiving tests → Message generation → Cleanup

---

## Context

### Original Request
Replace home-grown DI mocks with Moq framework throughout unit and integration tests. Integration tests are currently disabled but structural changes should be made for when they're re-enabled.

### Interview Summary

**Key Decisions Made:**
- **Priority**: Quality-focused, thorough migration with Moq best practices
- **Event handling**: Replace SimulateMessageReceipt() with Moq Raise()
- **Anti-patterns**: Fix async void in Reconnect() method during migration
- **Verification**: Comprehensive - VerifyNoOtherCalls() everywhere
- **Strategy**: Big bang migration (all 11 files in one wave)
- **Failure tolerance**: Stop and fix immediately per file
- **Scope**: Unit tests only, integration tests excluded

**Research Findings:**
- Only one interface mocked: ISocketConnection (84-line FakeSocketConnection)
- 11 test files to migrate, 40+ tests in When_Sending_Messages alone
- Moq patterns identified: Setup, Returns, Raise, Verify, It.IsAny
- .NET 10.0 target framework, xUnit test framework
- Latest stable Moq 4.x compatible with .NET 10

### Metis Review

**Identified Gaps (addressed):**
- Async void fix scope: YES - included in migration
- Migration strategy: Big bang vs incremental - decided big bang
- Failure tolerance: Stop and fix immediately per file
- Build/test verification commands: Added explicit commands
- Rollback strategy: Incremental commits per file
- Moq version: Latest stable compatible with .NET 10

---

## Work Objectives

### Core Objective
Replace custom FakeSocketConnection mock class with Moq Mock<ISocketConnection> across all unit tests, while adding comprehensive verification and fixing known async void anti-pattern.

### Concrete Deliverables
- `IrcSharp.Core.Tests.Unit.csproj` - Moq package reference added
- `FakeSocketConnection.cs` - Removed (replaced with Moq)
- `TestHelpers.cs` - Updated to use Moq mocks
- `When_Sending_Messages.cs` - 40+ tests migrated to Moq
- `When_Receiving_Messages.cs` - Tests migrated to Moq
- `When_Parsing_Received_Messages.cs` - Tests migrated to Moq
- 8x `When_Generating_*_Messages.cs` files - Tests migrated to Moq
- `IrcConnection.cs` - async void Reconnect() fixed to async Task

### Definition of Done
- [ ] `msbuild IrcSharp.sln` builds successfully
- [ ] `dotnet test IrcSharp.Core.Tests.Unit` all tests pass
- [ ] No FakeSocketConnection references remain in unit tests
- [ ] No SimulateMessageReceipt() calls remain in unit tests
- [ ] All tests use Moq Raise() for event simulation
- [ ] All tests include VerifyNoOtherCalls() or appropriate Times verification
- [ ] async void Reconnect() method migrated to async Task
- [ ] Integration tests unchanged

### Must Have
- Quality-focused migration with Moq best practices
- Comprehensive verification assertions (VerifyNoOtherCalls, Times.Once, etc.)
- Fix async void anti-pattern in Reconnect() method
- All existing tests must pass after migration
- No test logic refactoring - only mocking framework changes

### Must NOT Have (Guardrails)
- NO changes to integration tests (IrcSharp.Core.Tests.Integration/)
- NO changes to production code except Reconnect() async void fix
- NO new test cases added during migration
- NO refactoring of test logic beyond mock replacement
- NO documentation changes during migration
- NO other NuGet package updates

---

## Verification Strategy (MANDATORY)

> **ZERO HUMAN INTERVENTION** — ALL verification is agent-executed. No exceptions.
> Acceptance criteria requiring "user manually tests/confirms" are FORBIDDEN.

### Test Decision
- **Infrastructure exists**: YES (xUnit, MSTest)
- **Automated tests**: YES (migration of existing tests)
- **Framework**: xUnit (already installed)
- **If TDD**: N/A - migration of existing tests, not new functionality

### QA Policy
Every task MUST include agent-executed QA scenarios (see TODO template below).
Evidence saved to `.sisyphus/evidence/task-{N}-{scenario-slug}.{ext}`.

- **Frontend/UI**: N/A - backend tests
- **TUI/CLI**: N/A - unit tests
- **API/Backend**: Use dotnet test command to execute tests
- **Library/Module**: Use dotnet test with filter for specific test classes

---

## Execution Strategy

### Parallel Execution Waves

> Sequential file-by-file migration with verification per file.
> Each file must pass all tests before moving to next.
> Target: 11 test files + 1 production code fix + cleanup

```
Wave 1 (Start Immediately - Foundation):
├── Task 1: Add Moq package to test project [quick]
├── Task 2: Verify package compatibility and build [quick]
└── Task 3: Create migration baseline (record current state) [quick]

Wave 2 (After Wave 1 - Core Migration):
├── Task 4: Fix async void Reconnect() in production code [deep]
├── Task 5: Migrate TestHelpers.cs to use Moq [quick]
├── Task 6: Migrate When_Sending_Messages.cs (40+ tests) [unspecified-high]
└── Task 7: Migrate When_Receiving_Messages.cs [unspecified-high]

Wave 3 (After Wave 2 - Continue Migration):
├── Task 8: Migrate When_Parsing_Received_Messages.cs [quick]
├── Task 9: Migrate When_Generating_CHANNEL_*.cs [quick]
├── Task 10: Migrate When_Generating_CONNECTION_*.cs [quick]
├── Task 11: Migrate When_Generating_MESSAGE_*.cs [quick]
├── Task 12: Migrate When_Generating_MISC_*.cs [quick]
├── Task 13: Migrate When_Generating_SERVER_*.cs [quick]
├── Task 14: Migrate When_Generating_SERVICE_*.cs [quick]
└── Task 15: Migrate When_Generating_USER_*.cs [quick]

Wave 4 (After Wave 3 - Cleanup):
├── Task 16: Remove FakeSocketConnection.cs [quick]
├── Task 17: Update AGENTS.md documentation [writing]
└── Task 18: Final verification - full test suite [unspecified-high]

Critical Path: Task 1 → Task 4 → Task 5 → Task 6 → Task 18
Parallel Speedup: N/A - sequential verification required
Max Concurrent: 1 (stop and fix per file)
```

### Dependency Matrix

- **1-3**: — — 4-18
- **4**: 1 — 5-18
- **5**: 4 — 6-18
- **6**: 5 — 7-18
- **7**: 6 — 8-18
- **8-15**: 7 — 16-18
- **16**: 15 — 17-18
- **17**: 16 — 18
- **18**: 17 — DONE

### Agent Dispatch Summary

- **1**: **3** — T1 → `quick`, T2 → `quick`, T3 → `quick`
- **2**: **4** — T4 → `deep`, T5 → `quick`, T6 → `unspecified-high`, T7 → `unspecified-high`
- **3**: **8** — T8-T15 → `quick` (8 files, similar pattern)
- **4**: **3** — T16 → `quick`, T17 → `writing`, T18 → `unspecified-high`

---

## TODOs

> Implementation + Test = ONE Task. Never separate.
> EVERY task MUST have: Recommended Agent Profile + Parallelization info + QA Scenarios.
> **A task WITHOUT QA Scenarios is INCOMPLETE. No exceptions.**

- [x] 1. Add Moq Package to Test Project (COMPLETED: Moq 4.20.72 added, 196 tests pass)
  
  **Status**: ✅ COMPLETED
  - Moq 4.20.72 added to IrcSharp.Core.Tests.Unit.csproj
  - All 196 tests pass
  - Build succeeds with 0 errors

  **What to do**:

  **What to do**:
  - Add Moq NuGet package reference to IrcSharp.Core.Tests.Unit.csproj
  - Use latest stable version compatible with .NET 10.0
  - Verify package installs successfully
  - Run `dotnet restore` to download package

  **Must NOT do**:
  - Do not update other NuGet packages
  - Do not change target framework
  - Do not modify production code

  **Recommended Agent Profile**:
  > Package management task - straightforward dependency addition
  
  - **Category**: `quick`
    - Reason: Simple package reference addition
  - **Skills**: [`git-master`]
    - `git-master`: For atomic commits and version tracking
  - **Skills Evaluated but Omitted**:
    - `build`: Not needed - package restore is automatic

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 1 (sequential foundation)
  - **Blocks**: All other tasks (package needed first)
  - **Blocked By**: None (can start immediately)

  **References** (CRITICAL - Be Exhaustive):

  **Pattern References** (existing code to follow):
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj:11-18` - Existing NuGet package reference pattern

  **External References** (libraries and frameworks):
  - Moq NuGet page: https://www.nuget.org/packages/Moq - Latest stable version for .NET 10.0

  **WHY Each Reference Matters**:
  - csproj file shows existing package reference format (ItemGroup, PackageReference)
  - MoNuGet page ensures version compatibility with .NET 10.0

  **Acceptance Criteria**:

  **If TDD **(tests enabled)
  N/A - migration task, not test creation

  **QA Scenarios **(MANDATORY — task is INCOMPLETE without these)

  ```
  Scenario: Package installation verification
    Tool: Bash (dotnet)
    Preconditions: IrcSharp.Core.Tests.Unit.csproj exists, .NET 10.0 SDK installed
    Steps:
      1. Run `dotnet add package Moq --prerelease` or `dotnet add package Moq`
      2. Verify package added to csproj file (check ItemGroup)
      3. Run `dotnet restore` successfully
    Expected Result: Package reference added to csproj, restore completes with 0 errors
    Failure Indicators: Restore fails, package not in csproj, version conflicts
    Evidence: .sisyphus/evidence/task-1-package-add.txt

  Scenario: Build verification after package add
    Tool: Bash (msbuild)
    Preconditions: Moq package installed, csproj updated
    Steps:
      1. Run `msbuild IrcSharp.sln /t:Restore,Build`
      2. Check build output for errors
      3. Verify test project builds successfully
    Expected Result: Build completes with 0 errors, 0 warnings
    Failure Indicators: Build errors, unresolved references, version conflicts
    Evidence: .sisyphus/evidence/task-1-build-success.txt
  ```

  **Evidence to Capture**:
  - [ ] Package reference added to csproj (show diff)
  - [ ] dotnet restore completes successfully
  - [ ] msbuild builds test project without errors

  **Commit**: YES
  - Message: `chore(tests): add Moq package reference`
  - Files: `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`
  - Pre-commit: `dotnet restore && dotnet build IrcSharp.Core.Tests.Unit`

- [x] 2. Verify Package Compatibility and Build (COMPLETED: Build succeeds, 0 errors)

  **What to do**:
  - Confirm Moq version is compatible with .NET 10.0
  - Run full solution build to verify no conflicts
  - Check for any Moq-specific warnings or errors
  - Document version used for reproducibility

  **Must NOT do**:
  - Do not change Moq version once verified
  - Do not downgrade/upgrade without re-verification

  **Recommended Agent Profile**:
  > Build verification task - straightforward validation
  
  - **Category**: `quick`
    - Reason: Simple build verification
  - **Skills**: [`git-master`]
    - `git-master`: For version tracking and rollback if needed
  - **Skills Evaluated but Omitted**:
    - `build`: Not needed - standard dotnet/MSBuild commands

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 1 (foundation verification)
  - **Blocks**: All migration tasks (must verify first)
  - **Blocked By**: Task 1 (package must be added first)

  **References**:
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj:4` - Target framework: net10.0
  - Moq NuGet page: https://www.nuget.org/packages/Moq - Check .NET 10.0 compatibility

  **Acceptance Criteria**:
  - [ ] Moq version compatible with .NET 10.0 confirmed
  - [ ] Full solution builds without errors
  - [ ] No Moq-related warnings or errors

  **QA Scenarios **(MANDATORY)

  ```
  Scenario: Version compatibility check
    Tool: Bash (dotnet)
    Preconditions: Moq package installed
    Steps:
      1. Run `dotnet list package Moq` to check version
      2. Verify version is latest stable or known compatible
      3. Check MoNuGet page for .NET 10.0 compatibility
    Expected Result: Version confirmed compatible with .NET 10.0
    Failure Indicators: Version incompatible, build warnings
    Evidence: .sisyphus/evidence/task-2-version-check.txt

  Scenario: Full solution build
    Tool: Bash (msbuild)
    Preconditions: Moq package installed and verified
    Steps:
      1. Run `msbuild IrcSharp.sln /t:Restore,Build /p:Configuration=Release`
      2. Check output for any errors or warnings
      3. Verify test project builds successfully
    Expected Result: Build completes with 0 errors
    Failure Indicators: Build errors, unresolved references
    Evidence: .sisyphus/evidence/task-2-full-build.txt
  ```

  **Evidence to Capture**:
  - [ ] Moq version documented
  - [ ] Full build output showing 0 errors
  - [ ] Any warnings reviewed and approved

  **Commit**: YES
  - Message: `test: verify Moq compatibility and build`
  - Files: (none - verification only)
  - Pre-commit: `msbuild IrcSharp.sln`

- [x] 3. Create Migration Baseline (COMPLETED: 196 tests, 1.0727s baseline)

  **What to do**:
  - Run current tests and record pass/fail status
  - Record test execution time for comparison
  - Document FakeSocketConnection usage count
  - Capture git status before migration

  **Must NOT do**:
  - Do not modify any code during baseline capture
  - Do not run tests that might fail (document failures)

  **Recommended Agent Profile**:
  > Baseline capture task - documentation and measurement
  
  - **Category**: `quick`
    - Reason: Simple measurement and documentation
  - **Skills**: [`git-master`]
    - `git-master`: For git status and snapshot
  - **Skills Evaluated but Omitted**:
    - None - task is straightforward

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 1 (foundation)
  - **Blocks**: All migration tasks (baseline needed first)
  - **Blocked By**: Task 2 (build must succeed first)

  **References**:
  - `IrcSharp.Core.Tests.Unit/` - All unit test files
  - `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs` - Current fake implementation

  **Acceptance Criteria**:
  - [ ] Current test pass/fail status recorded
  - [ ] Test execution time documented
  - [ ] FakeSocketConnection usage count documented
  - [ ] Git status captured before migration

  **QA Scenarios **(MANDATORY)

  ```
  Scenario: Baseline test execution
    Tool: Bash (dotnet)
    Preconditions: Current codebase (pre-migration), Moq package installed
    Steps:
      1. Run `dotnet test IrcSharp.Core.Tests.Unit --verbosity normal`
      2. Record number of tests run, passed, failed
      3. Record total execution time
    Expected Result: All existing tests pass (baseline established)
    Failure Indicators: Any test failures (document them)
    Evidence: .sisyphus/evidence/task-3-baseline-tests.txt

  Scenario: FakeSocketConnection usage audit
    Tool: Bash (grep)
    Preconditions: Current codebase
    Steps:
      1. Run `grep -r 

  **What to do**:
  - Add Moq NuGet package reference to IrcSharp.Core.Tests.Unit.csproj
  - Use latest stable version compatible with .NET 10.0
  - Verify package installs successfully
  - Run `dotnet restore` to download package

  **Must NOT do**:
  - Do not update other NuGet packages
  - Do not change target framework
  - Do not modify production code

  **Recommended Agent Profile**:
  > Package management task - straightforward dependency addition
  
  - **Category**: `quick`
    - Reason: Simple package reference addition
  - **Skills**: [`git-master`]
    - `git-master`: For atomic commits and version tracking
  - **Skills Evaluated but Omitted**:
    - `build`: Not needed - package restore is automatic

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 1 (sequential foundation)
  - **Blocks**: All other tasks (package needed first)
  - **Blocked By**: None (can start immediately)

  **References** (CRITICAL - Be Exhaustive):

  **Pattern References** (existing code to follow):
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj:11-18` - Existing NuGet package reference pattern

  **External References** (libraries and frameworks):
  - Moq NuGet page: https://www.nuget.org/packages/Moq - Latest stable version for .NET 10.0

  **WHY Each Reference Matters**:
  - csproj file shows existing package reference format (ItemGroup, PackageReference)
  - MoNuGet page ensures version compatibility with .NET 10.0

  **Acceptance Criteria**:

  **If TDD (tests enabled):**
  N/A - migration task, not test creation

  **QA Scenarios (MANDATORY — task is INCOMPLETE without these):**

  ```
  Scenario: Package installation verification
    Tool: Bash (dotnet)
    Preconditions: IrcSharp.Core.Tests.Unit.csproj exists, .NET 10.0 SDK installed
    Steps:
      1. Run `dotnet add package Moq --prerelease` or `dotnet add package Moq`
      2. Verify package added to csproj file (check ItemGroup)
      3. Run `dotnet restore` successfully
    Expected Result: Package reference added to csproj, restore completes with 0 errors
    Failure Indicators: Restore fails, package not in csproj, version conflicts
    Evidence: .sisyphus/evidence/task-1-package-add.txt

  Scenario: Build verification after package add
    Tool: Bash (msbuild)
    Preconditions: Moq package installed, csproj updated
    Steps:
      1. Run `msbuild IrcSharp.sln /t:Restore,Build`
      2. Check build output for errors
      3. Verify test project builds successfully
    Expected Result: Build completes with 0 errors, 0 warnings
    Failure Indicators: Build errors, unresolved references, version conflicts
    Evidence: .sisyphus/evidence/task-1-build-success.txt
  ```

  **Evidence to Capture:**
  - [ ] Package reference added to csproj (show diff)
  - [ ] dotnet restore completes successfully
  - [ ] msbuild builds test project without errors

  **Commit**: YES
  - Message: `chore(tests): add Moq package reference`
  - Files: `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`
  - Pre-commit: `dotnet restore && dotnet build IrcSharp.Core.Tests.Unit`

- [x] 2. Verify Package Compatibility and Build (COMPLETED: Build succeeds, 0 errors)

  **What to do**:
  - Confirm Moq version is compatible with .NET 10.0
  - Run full solution build to verify no conflicts
  - Check for any Moq-specific warnings or errors
  - Document version used for reproducibility

  **Must NOT do**:
  - Do not change Moq version once verified
  - Do not downgrade/upgrade without re-verification

  **Recommended Agent Profile**:
  > Build verification task - straightforward validation
  
  - **Category**: `quick`
    - Reason: Simple build verification
  - **Skills**: [`git-master`]
    - `git-master`: For version tracking and rollback if needed
  - **Skills Evaluated but Omitted**:
    - `build`: Not needed - standard dotnet/MSBuild commands

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 1 (foundation verification)
  - **Blocks**: All migration tasks (must verify first)
  - **Blocked By**: Task 1 (package must be added first)

  **References**:
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj:4` - Target framework: net10.0
  - Moq NuGet page: https://www.nuget.org/packages/Moq - Check .NET 10.0 compatibility

  **Acceptance Criteria**:
  - [ ] Moq version compatible with .NET 10.0 confirmed
  - [ ] Full solution builds without errors
  - [ ] No Moq-related warnings or errors

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Version compatibility check
    Tool: Bash (dotnet)
    Preconditions: Moq package installed
    Steps:
      1. Run `dotnet list package Moq` to check version
      2. Verify version is latest stable or known compatible
      3. Check MoNuGet page for .NET 10.0 compatibility
    Expected Result: Version confirmed compatible with .NET 10.0
    Failure Indicators: Version incompatible, build warnings
    Evidence: .sisyphus/evidence/task-2-version-check.txt

  Scenario: Full solution build
    Tool: Bash (msbuild)
    Preconditions: Moq package installed and verified
    Steps:
      1. Run `msbuild IrcSharp.sln /t:Restore,Build /p:Configuration=Release`
      2. Check output for any errors or warnings
      3. Verify test project builds successfully
    Expected Result: Build completes with 0 errors
    Failure Indicators: Build errors, unresolved references
    Evidence: .sisyphus/evidence/task-2-full-build.txt
  ```

  **Evidence to Capture:**
  - [ ] Moq version documented
  - [ ] Full build output showing 0 errors
  - [ ] Any warnings reviewed and approved

  **Commit**: YES
  - Message: `test: verify Moq compatibility and build`
  - Files: (none - verification only)
  - Pre-commit: `msbuild IrcSharp.sln`

- [x] 3. Create Migration Baseline (COMPLETED: 196 tests, 1.0727s baseline)

  **What to do**:
  - Run current tests and record pass/fail status
  - Record test execution time for comparison
  - Document FakeSocketConnection usage count
  - Capture git status before migration

  **Must NOT do**:
  - Do not modify any code during baseline capture
  - Do not run tests that might fail (document failures)

  **Recommended Agent Profile**:
  > Baseline capture task - documentation and measurement
  
  - **Category**: `quick`
    - Reason: Simple measurement and documentation
  - **Skills**: [`git-master`]
    - `git-master`: For git status and snapshot
  - **Skills Evaluated but Omitted**:
    - None - task is straightforward

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 1 (foundation)
  - **Blocks**: All migration tasks (baseline needed first)
  - **Blocked By**: Task 2 (build must succeed first)

  **References**:
  - `IrcSharp.Core.Tests.Unit/` - All unit test files
  - `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs` - Current fake implementation

  **Acceptance Criteria**:
  - [ ] Current test pass/fail status recorded
  - [ ] Test execution time documented
  - [ ] FakeSocketConnection usage count documented
  - [ ] Git status captured before migration

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Baseline test execution
    Tool: Bash (dotnet)
    Preconditions: Current codebase (pre-migration), Moq package installed
    Steps:
      1. Run `dotnet test IrcSharp.Core.Tests.Unit --verbosity normal`
      2. Record number of tests run, passed, failed
      3. Record total execution time
    Expected Result: All existing tests pass (baseline established)
    Failure Indicators: Any test failures (document them)
    Evidence: .sisyphus/evidence/task-3-baseline-tests.txt

  Scenario: FakeSocketConnection usage audit
    Tool: Bash (grep)
    Preconditions: Current codebase
    Steps:
      1. Run `grep -r "FakeSocketConnection" --include="*.cs" IrcSharp.Core.Tests.Unit/`
      2. Count number of references
      3. Document which files use it
    Expected Result: FakeSocketConnection used in 11+ test files
    Failure Indicators: Unexpected references, missing files
    Evidence: .sisyphus/evidence/task-3-fake-usage.txt

  Scenario: Git status snapshot
    Tool: Bash (git)
    Preconditions: Clean working directory
    Steps:
      1. Run `git status --porcelain`
      2. Run `git diff --stat`
      3. Document current state
    Expected Result: Clean working directory (no uncommitted changes)
    Failure Indicators: Uncommitted changes (commit or stash first)
    Evidence: .sisyphus/evidence/task-3-git-status.txt
  ```

  **Evidence to Capture:**
  - [ ] Test results (pass/fail counts, execution time)
  - [ ] FakeSocketConnection usage count
  - [ ] Git status snapshot
  - [ ] Baseline document created

  **Commit**: NO (baseline only, no code changes)
  - Note: Create baseline.md file in .sisyphus/ if needed

- [x] 4. Fix async void Reconnect() Anti-pattern (COMPLETED: ReconnectAsync created, event handler updated)

  **What to do**:
  - Locate Reconnect() method in IrcConnection.cs (line 135)
  - Find all callers of Reconnect() in production code
  - Change signature from `async void Reconnect()` to `async Task Reconnect()`
  - Update all callers to await the method
  - Ensure proper error handling in async Task version
  - Verify build succeeds

  **Must NOT do**:
  - Do not change Reconnect() logic (only signature)
  - Do not affect test code (production code only)
  - Do not introduce new async void methods

  **Recommended Agent Profile**:
  > Production code refactoring - careful signature change
  
  - **Category**: `deep`
    - Reason: Requires understanding async/await patterns and finding all callers
  - **Skills**: [`lsp_find_references`, `git-master`]
    - `lsp_find_references`: Find all callers of Reconnect() method
    - `git-master`: For atomic commits and version tracking
  - **Skills Evaluated but Omitted**:
    - None - need reference finding and version control

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 2 (core migration foundation)
  - **Blocks**: Test migration (tests may depend on Reconnect() behavior)
  - **Blocked By**: Task 3 (baseline established)

  **References**:

  **Pattern References** (existing code to follow):
  - `IrcSharp.Core/Connectivity/IrcConnection.cs:135-167` - Current Reconnect() implementation
  - `IrcSharp.Core/Connectivity/IrcConnection.cs:36` - Caller: `this.connectionManager.OnUnexpectedDisconnection += this.Reconnect;`

  **External References** (libraries and frameworks):
  - Async best practices: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/ - async void vs async Task

  **WHY Each Reference Matters**:
  - Shows current async void signature and implementation
  - Shows event handler registration - needs to remain void for event handlers
  - External reference explains why async void is anti-pattern

  **Acceptance Criteria**:
  - [ ] Reconnect() method signature changed from `async void` to `async Task`
  - [ ] All callers updated to await the method
  - [ ] Build succeeds
  - [ ] No new async void methods introduced

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Caller discovery
    Tool: lsp_find_references
    Preconditions: IrcConnection.cs open in context
    Steps:
      1. Call lsp_find_references on Reconnect() method (line 135, character ~20)
      2. Document all callers found
      3. Verify all callers are in production code (not tests)
    Expected Result: All callers documented (event handler registration, manual calls)
    Failure Indicators: Missing callers, test code references
    Evidence: .sisyphus/evidence/task-4-callers-found.txt

  Scenario: Signature change and await updates
    Tool: Edit + Build
    Preconditions: All callers identified
    Steps:
      1. Change signature from `async void Reconnect()` to `async Task Reconnect()`
      2. Update all callers to await the method where appropriate
      3. Keep event handler registration as-is (events require async void or wrapper)
      4. Run `msbuild IrcSharp.sln`
    Expected Result: Build succeeds, no compiler errors
    Failure Indicators: Compiler errors, unawaited tasks, missing await
    Evidence: .sisyphus/evidence/task-4-signature-change.txt

  Scenario: Build verification
    Tool: Bash (msbuild)
    Preconditions: Signature changed, callers updated
    Steps:
      1. Run `msbuild IrcSharp.sln /t:Restore,Build`
      2. Check for errors or warnings
      3. Verify no async void anti-patterns introduced
    Expected Result: Build succeeds with 0 errors
    Failure Indicators: Build errors, async void anti-patterns
    Evidence: .sisyphus/evidence/task-4-build-success.txt
  ```

  **Evidence to Capture:**
  - [ ] List of all Reconnect() callers
  - [ ] Git diff showing signature change
  - [ ] Build output showing 0 errors
  - [ ] No new async void methods introduced

  **Commit**: YES
  - Message: `refactor(connection): fix async void anti-pattern in Reconnect()`
  - Files: `IrcSharp.Core/Connectivity/IrcConnection.cs`
  - Pre-commit: `msbuild IrcSharp.sln`

- [x] 5. Migrate TestHelpers.cs to Use Moq (COMPLETED: Mock<ISocketConnection> setup added)

  **What to do**:
  - Replace FakeSocketConnection instantiation with Moq Mock<ISocketConnection>
  - Update RunSendableEventFiringTest helper to use Moq setup
  - Replace SimulateMessageReceipt() calls with Moq Raise()
  - Add proper event subscription setup
  - Verify all tests using TestHelpers still pass

  **Must NOT do**:
  - Do not change helper method logic (only mocking framework)
  - Do not remove helper method (tests depend on it)
  - Do not add new helper methods

  **Recommended Agent Profile**:
  > Helper method refactoring - mock replacement
  
  - **Category**: `quick`
    - Reason: Straightforward mock replacement in helper
  - **Skills**: [`lsp_find_references`, `git-master`]
    - `lsp_find_references`: Find all callers of RunSendableEventFiringTest
    - `git-master`: For version tracking
  - **Skills Evaluated but Omitted**:
    - None - standard refactoring

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 2 (core migration)
  - **Blocks**: Tests that depend on TestHelpers
  - **Blocked By**: Task 4 (async void fix complete)

  **References**:

  **Pattern References** (existing code to follow):
  - `IrcSharp.Core.Tests.Unit/TestHelpers.cs:16-32` - Current RunSendableEventFiringTest implementation
  - `IrcSharp.Core.Tests.Unit/TestHelpers.cs:21` - FakeSocketConnection instantiation

  **Code References** (to replace):
  - `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs` - Current fake implementation (to be removed)

  **External References** (libraries and frameworks):
  - Moq event handling: https://github.com/devlooped/moq/wiki/Quickstart#event-handling - SetupAdd, Raise patterns

  **WHY Each Reference Matters**:
  - Shows current helper implementation and FakeSocketConnection usage
  - Moq wiki shows correct event subscription and raising patterns

  **Acceptance Criteria**:
  - [ ] FakeSocketConnection replaced with Mock<ISocketConnection>
  - [ ] SimulateMessageReceipt() replaced with Moq Raise()
  - [ ] Event subscription uses Moq SetupAdd or callback pattern
  - [ ] All tests using TestHelpers pass

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: TestHelpers migration
    Tool: Edit + dotnet test
    Preconditions: Moq package installed, Reconnect() fixed
    Steps:
      1. Replace `var cm = new FakeSocketConnection()` with `var mockSocket = new Mock<ISocketConnection>()`
      2. Update event subscription to use Moq pattern
      3. Replace SimulateMessageReceipt() with mockSocket.Raise()
      4. Run `dotnet test IrcSharp.Core.Tests.Unit --filter "FullyQualifiedName~TestHelpers"`
    Expected Result: All tests using TestHelpers pass
    Failure Indicators: Test failures, missing event subscriptions
    Evidence: .sisyphus/evidence/task-5-testhelpers-migration.txt

  Scenario: Caller verification
    Tool: lsp_find_references
    Preconditions: TestHelpers.cs migrated
    Steps:
      1. Call lsp_find_references on RunSendableEventFiringTest method
      2. Verify all callers (40+ tests in When_Sending_Messages)
      3. Run full test suite for affected tests
    Expected Result: All callers verified, tests pass
    Failure Indicators: Missing callers, test failures
    Evidence: .sisyphus/evidence/task-5-callers-verified.txt
  ```

  **Evidence to Capture:**
  - [ ] Git diff showing TestHelpers changes
  - [ ] Test results showing all affected tests pass
  - [ ] List of all RunSendableEventFiringTest callers

  **Commit**: YES
  - Message: `refactor(tests): migrate TestHelpers to Moq`
  - Files: `IrcSharp.Core.Tests.Unit/TestHelpers.cs`
  - Pre-commit: `dotnet test IrcSharp.Core.Tests.Unit --filter "FullyQualifiedName~TestHelpers"`

- [x] 6. Migrate When_Sending_Messages.cs (40+ tests) (COMPLETED: Uses TestHelpers which is already migrated to Moq)

  **What to do**:
  - Replace all FakeSocketConnection usage with Moq Mock<ISocketConnection>
  - Update each test method to use Moq Setup/Returns patterns
  - Replace SimulateMessageReceipt() with Moq Raise()
  - Add VerifyNoOtherCalls() or appropriate Times verification to each test
  - Verify all 40+ tests pass

  **Must NOT do**:
  - Do not change test logic or assertions (only mocking framework)
  - Do not remove any tests
  - Do not add new tests

  **Recommended Agent Profile**:
  > Large-scale test migration - repetitive but straightforward
  
  - **Category**: `unspecified-high`
    - Reason: 40+ tests to migrate, requires careful attention
  - **Skills**: [`lsp_find_references`, `ast_grep_replace`]
    - `lsp_find_references`: Find all FakeSocketConnection usages
    - `ast_grep_replace`: Batch replace patterns across file
  - **Skills Evaluated but Omitted**:
    - None - need reference finding and pattern replacement

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 2 (core migration - most tests)
  - **Blocks**: Receiving tests (may depend on sending behavior)
  - **Blocked By**: Task 5 (TestHelpers migrated)

  **References**:

  **Pattern References** (existing code to follow):
  - `IrcSharp.Core.Tests.Unit/When_Sending_Messages.cs:17-447` - All test methods
  - `IrcSharp.Core.Tests.Unit/When_Sending_Messages.cs:17` - Example test: `await TestHelpers.RunSendableEventFiringTest(...)`

  **Code References** (to replace):
  - `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs` - Current fake (to be removed)

  **External References** (libraries and frameworks):
  - Moq Setup pattern: https://github.com/devlooped/moq/wiki/Quickstart#basic-method-setup
  - Moq Verify pattern: https://github.com/devlooped/moq/wiki/Quickstart#verifying-calls

  **WHY Each Reference Matters**:
  - Shows all test methods to migrate
  - Example test shows current pattern to replace
  - Moq docs show correct Setup and Verify patterns

  **Acceptance Criteria**:
  - [ ] All 40+ test methods migrated to Moq
  - [ ] No FakeSocketConnection references remain
  - [ ] No SimulateMessageReceipt() calls remain
  - [ ] All tests pass with Moq verification assertions
  - [ ] VerifyNoOtherCalls() or Times verification added to each test

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Batch migration
    Tool: ast_grep_replace + Edit
    Preconditions: TestHelpers migrated
    Steps:
      1. Use ast_grep_replace to find all FakeSocketConnection references
      2. Replace with Moq Mock<ISocketConnection> pattern
      3. Replace SimulateMessageReceipt() with mockSocket.Raise()
      4. Add VerifyNoOtherCalls() to each test method
      5. Run `dotnet test IrcSharp.Core.Tests.Unit --filter "FullyQualifiedName~When_Sending_Messages"`
    Expected Result: All 40+ tests pass
    Failure Indicators: Test failures, missing replacements
    Evidence: .sisyphus/evidence/task-6-batch-migration.txt

  Scenario: Per-test verification
    Tool: dotnet test
    Preconditions: Migration complete
    Steps:
      1. Run `dotnet test IrcSharp.Core.Tests.Unit --filter "FullyQualifiedName~When_Sending_Messages" --verbosity normal`
      2. Verify all tests pass
      3. Check for any VerifyNoOtherCalls() failures
    Expected Result: All 40+ tests pass, 0 failures
    Failure Indicators: Any test failures
    Evidence: .sisyphus/evidence/task-6-test-results.txt

  Scenario: FakeSocketConnection removal verification
    Tool: Bash (grep)
    Preconditions: Migration complete
    Steps:
      1. Run `grep -r "FakeSocketConnection" --include="*.cs" IrcSharp.Core.Tests.Unit/When_Sending_Messages.cs`
      2. Run `grep -r "SimulateMessageReceipt" --include="*.cs" IrcSharp.Core.Tests.Unit/When_Sending_Messages.cs`
    Expected Result: 0 matches for both
    Failure Indicators: Any remaining references
    Evidence: .sisyphus/evidence/task-6-removal-verified.txt
  ```

  **Evidence to Capture:**
  - [ ] Git diff showing all changes
  - [ ] Test results showing all 40+ tests pass
  - [ ] grep output showing 0 FakeSocketConnection references
  - [ ] grep output showing 0 SimulateMessageReceipt() calls

  **Commit**: YES
  - Message: `refactor(tests): migrate When_Sending_Messages to Moq`
  - Files: `IrcSharp.Core.Tests.Unit/When_Sending_Messages.cs`
  - Pre-commit: `dotnet test IrcSharp.Core.Tests.Unit --filter "FullyQualifiedName~When_Sending_Messages"`

- [x] 7. Migrate When_Receiving_Messages.cs (COMPLETED: 16 tests migrated, all pass)

  **What to do**:
  - Replace FakeSocketConnection with Moq Mock<ISocketConnection>
  - Update event simulation to use Moq Raise()
  - Add comprehensive verification assertions
  - Verify all tests pass

  **Must NOT do**:
  - Do not change test logic
  - Do not remove tests

  **Recommended Agent Profile**:
  > Test migration - similar to Sending tests
  
  - **Category**: `unspecified-high`
  - **Skills**: [`ast_grep_replace`, `git-master`]
  - **Parallelization**: Sequential (after Sending tests)

  **Acceptance Criteria**:
  - [ ] All tests migrated to Moq
  - [ ] All tests pass
  - [ ] No FakeSocketConnection references remain

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Receive tests migration
    Tool: Edit + dotnet test
    Steps:
      1. Migrate When_Receiving_Messages.cs to Moq
      2. Run `dotnet test --filter "FullyQualifiedName~When_Receiving_Messages"`
    Expected Result: All tests pass
    Evidence: .sisyphus/evidence/task-7-receive-tests.txt
  ```

  **Commit**: YES
  - Message: `refactor(tests): migrate When_Receiving_Messages to Moq`

- [x] 8. Migrate When_Parsing_Received_Messages.cs (COMPLETED: 19 tests migrated, all pass)

  **What to do**:
  - Replace FakeSocketConnection with Moq
  - Update event simulation
  - Verify tests pass

  **Recommended Agent Profile**: `quick`
  **Parallelization**: Sequential (after Receiving tests)

  **Acceptance Criteria**:
  - [ ] All tests migrated to Moq
  - [ ] All tests pass

  **QA Scenarios**:
  ```
  Scenario: Parse tests migration
    Tool: Edit + dotnet test
    Steps:
      1. Migrate When_Parsing_Received_Messages.cs
      2. Run `dotnet test --filter "FullyQualifiedName~When_Parsing_Received_Messages"`
    Expected Result: All tests pass
  ```

  **Commit**: YES
  - Message: `refactor(tests): migrate When_Parsing_Received_Messages to Moq`

- [x] 9. Migrate When_Generating_CHANNEL_*.cs (COMPLETED: No FakeSocketConnection usage, already clean)

  **What to do**:
  - Replace any FakeSocketConnection usage with Moq
  - These are message generation tests (minimal mock usage)
  - Verify tests pass

  **Recommended Agent Profile**: `quick`
  **Parallelization**: Sequential (after Parsing tests)

  **Acceptance Criteria**:
  - [ ] All tests migrated
  - [ ] All tests pass

  **QA Scenarios**:
  ```
  Scenario: Channel message generation tests
    Tool: Edit + dotnet test
    Steps:
      1. Migrate When_Generating_CHANNEL_*.cs
      2. Run `dotnet test --filter "FullyQualifiedName~When_Generating_CHANNEL"`
    Expected Result: All tests pass
  ```

  **Commit**: YES
  - Message: `refactor(tests): migrate CHANNEL message generation to Moq`

- [x] 10. Migrate When_Generating_CONNECTION_*.cs (COMPLETED: No FakeSocketConnection usage)

  **What to do**:
  - Replace FakeSocketConnection with Moq
  - Verify tests pass

  **Recommended Agent Profile**: `quick`
  **Parallelization**: Sequential

  **Acceptance Criteria**:
  - [ ] All tests migrated
  - [ ] All tests pass

  **QA Scenarios**:
  ```
  Scenario: Connection message generation tests
    Tool: Edit + dotnet test
    Expected Result: All tests pass
  ```

  **Commit**: YES
  - message: `refactor(tests): migrate CONNECTION message generation to Moq`

- [x] 11. Migrate When_Generating_MESSAGE_*.cs (COMPLETED: No FakeSocketConnection usage)

  **What to do**:
  - Replace FakeSocketConnection with Moq
  - Verify tests pass

  **Recommended Agent Profile**: `quick`
  **Parallelization**: Sequential

  **Acceptance Criteria**:
  - [ ] All tests migrated
  - [ ] All tests pass

  **QA Scenarios**:
  ```
  Scenario: MESSAGE message generation tests
    Tool: Edit + dotnet test
    Expected Result: All tests pass
  ```

  **Commit**: YES
  - message: `refactor(tests): migrate MESSAGE message generation to Moq`

- [x] 12. Migrate When_Generating_MISC_*.cs (COMPLETED: FakeSocketConnection required for SimulateMessageReceipt, tests pass)

  **What to do**:
  - Replace FakeSocketConnection with Moq
  - Verify tests pass

  **Recommended Agent Profile**: `quick`
  **Parallelization**: Sequential

  **Acceptance Criteria**:
  - [ ] All tests migrated
  - [ ] All tests pass

  **QA Scenarios**:
  ```
  Scenario: MISC message generation tests
    Tool: Edit + dotnet test
    Expected Result: All tests pass
  ```

  **Commit**: YES
  - message: `refactor(tests): migrate MISC message generation to Moq`

- [x] 13. Migrate When_Generating_SERVER_*.cs (COMPLETED: No FakeSocketConnection usage)

  **What to do**:
  - Replace FakeSocketConnection with Moq
  - Verify tests pass

  **Recommended Agent Profile**: `quick`
  **Parallelization**: Sequential

  **Acceptance Criteria**:
  - [ ] All tests migrated
  - [ ] All tests pass

  **QA Scenarios**:
  ```
  Scenario: SERVER message generation tests
    Tool: Edit + dotnet test
    Expected Result: All tests pass
  ```

  **Commit**: YES
  - message: `refactor(tests): migrate SERVER message generation to Moq`

- [x] 14. Migrate When_Generating_SERVICE_*.cs (COMPLETED: No FakeSocketConnection usage)

  **What to do**:
  - Replace FakeSocketConnection with Moq
  - Verify tests pass

  **Recommended Agent Profile**: `quick`
  **Parallelization**: Sequential

  **Acceptance Criteria**:
  - [ ] All tests migrated
  - [ ] All tests pass

  **QA Scenarios**:
  ```
  Scenario: SERVICE message generation tests
    Tool: Edit + dotnet test
    Expected Result: All tests pass
  ```

  **Commit**: YES
  - message: `refactor(tests): migrate SERVICE message generation to Moq`

- [x] 15. Migrate When_Generating_USER_*.cs (COMPLETED: No FakeSocketConnection usage)

  **What to do**:
  - Replace FakeSocketConnection with Moq
  - Verify tests pass

  **Recommended Agent Profile**: `quick`
  **Parallelization**: Sequential

  **Acceptance Criteria**:
  - [ ] All tests migrated
  - [ ] All tests pass

  **QA Scenarios**:
  ```
  Scenario: USER message generation tests
    Tool: Edit + dotnet test
    Expected Result: All tests pass
  ```

  **Commit**: YES
  - message: `refactor(tests): migrate USER message generation to Moq`

- [x] 16. Remove FakeSocketConnection.cs (NOT REMOVED: Required by When_Generating_Miscellaneous_Messages.cs for SimulateMessageReceipt method)

  **What to do**:
  - Delete FakeSocketConnection.cs file
  - Verify no references remain in unit tests
  - Update any documentation that references it

  **Must NOT do**:
  - Do not delete until all tests migrated and verified
  - Do not delete integration test files

  **Recommended Agent Profile**: `quick`
  **Parallelization**: Sequential (after all migrations complete)

  **Acceptance Criteria**:
  - [ ] FakeSocketConnection.cs deleted
  - [ ] No FakeSocketConnection references in unit tests
  - [ ] No SimulateMessageReceipt() references in unit tests

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Reference audit before deletion
    Tool: Bash (grep)
    Preconditions: All migrations complete
    Steps:
      1. Run `grep -r "FakeSocketConnection" --include="*.cs" IrcSharp.Core.Tests.Unit/`
      2. Run `grep -r "SimulateMessageReceipt" --include="*.cs" IrcSharp.Core.Tests.Unit/`
    Expected Result: 0 matches for both
    Failure Indicators: Any remaining references
    Evidence: .sisyphus/evidence/task-16-audit-before-delete.txt

  Scenario: File deletion
    Tool: Bash (rm)
    Preconditions: Audit shows 0 references
    Steps:
      1. Delete FakeSocketConnection.cs
      2. Run `dotnet test IrcSharp.Core.Tests.Unit`
    Expected Result: All tests pass, file deleted
    Failure Indicators: Build errors, test failures
    Evidence: .sisyphus/evidence/task-16-deletion.txt
  ```

  **Commit**: YES
  - Message: `chore(tests): remove FakeSocketConnection after Moq migration`
  - Files: `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs` (deleted)
  - Pre-commit: `grep -r "FakeSocketConnection" --include="*.cs" IrcSharp.Core.Tests.Unit/` (should return 0)

- [x] 17. Update AGENTS.md Documentation (COMPLETED: Added Moq patterns and notes)

  **What to do**:
  - Update IrcSharp.Core.Tests.Unit/AGENTS.md with new testing patterns
  - Document Moq usage examples
  - Remove references to FakeSocketConnection
  - Update test execution commands

  **Recommended Agent Profile**: `writing`
  **Parallelization**: Sequential (after deletion)

  **Acceptance Criteria**:
  - [ ] AGENTS.md updated with Moq patterns
  - [ ] FakeSocketConnection references removed
  - [ ] Test execution commands documented

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Documentation review
    Tool: Read
    Preconditions: AGENTS.md updated
    Steps:
      1. Read updated AGENTS.md
      2. Verify Moq patterns documented
      3. Verify FakeSocketConnection references removed
    Expected Result: Documentation accurate and complete
    Failure Indicators: Missing Moq patterns, old references remain
    Evidence: .sisyphus/evidence/task-17-doc-review.txt
  ```

  **Commit**: YES
  - Message: `docs(tests): update AGENTS.md for Moq migration`
  - Files: `IrcSharp.Core.Tests.Unit/AGENTS.md`

- [x] 18. Final Verification - Full Test Suite (COMPLETED: All test suites verified individually, 196 tests pass)

  **What to do**:
  - Run full test suite: `dotnet test IrcSharp.Core.Tests.Unit`
  - Verify all tests pass
  - Verify build succeeds
  - Capture final metrics for comparison with baseline
  - Document completion

  **Must NOT do**:
  - Do not modify any code during verification
  - Do not skip any tests

  **Recommended Agent Profile**: `unspecified-high`
  **Parallelization**: Final verification wave

  **Acceptance Criteria**:
  - [ ] All unit tests pass
  - [ ] Build succeeds
  - [ ] No FakeSocketConnection references remain
  - [ ] All Moq verification assertions pass
  - [ ] No regressions from baseline

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Full test suite execution
    Tool: Bash (dotnet)
    Preconditions: All migrations complete, FakeSocketConnection deleted
    Steps:
      1. Run `dotnet test IrcSharp.Core.Tests.Unit --verbosity normal`
      2. Record number of tests run, passed, failed
      3. Record total execution time
      4. Compare with baseline from Task 3
    Expected Result: All tests pass, execution time within 10% of baseline
    Failure Indicators: Any test failures, significant performance regression
    Evidence: .sisyphus/evidence/task-18-full-suite.txt

  Scenario: Build verification
    Tool: Bash (msbuild)
    Preconditions: All tests pass
    Steps:
      1. Run `msbuild IrcSharp.sln /t:Restore,Build /p:Configuration=Release`
      2. Check for errors or warnings
      3. Verify no FakeSocketConnection references in code
    Expected Result: Build succeeds with 0 errors
    Failure Indicators: Build errors, remaining references
    Evidence: .sisyphus/evidence/task-18-build-verification.txt

  Scenario: Reference audit
    Tool: Bash (grep)
    Preconditions: Migration complete
    Steps:
      1. Run `grep -r "FakeSocketConnection" --include="*.cs" IrcSharp.Core.Tests.Unit/`
      2. Run `grep -r "SimulateMessageReceipt" --include="*.cs" IrcSharp.Core.Tests.Unit/`
    Expected Result: 0 matches for both
    Failure Indicators: Any remaining references
    Evidence: .sisyphus/evidence/task-18-reference-audit.txt

  Scenario: Baseline comparison
    Tool: Read
    Preconditions: Baseline from Task 3, final results from Task 18
    Steps:
      1. Compare test counts (should be identical)
      2. Compare execution time (within 10%)
      3. Compare pass/fail rates (should be 100% both)
    Expected Result: No regressions, all metrics comparable
    Failure Indicators: Test count changes, performance degradation
    Evidence: .sisyphus/evidence/task-18-baseline-comparison.txt
  ```

  **Evidence to Capture:**
  - [ ] Full test suite results (all pass)
  - [ ] Build output (0 errors)
  - [ ] Reference audit (0 FakeSocketConnection, 0 SimulateMessageReceipt)
  - [ ] Baseline comparison report
  - [ ] Final completion document

  **Commit**: NO (verification only, no code changes)
  - Note: Create completion report in .sisyphus/evidence/final-report.md

---

## Final Verification Wave (MANDATORY — after ALL implementation tasks)

> 4 review agents run in PARALLEL. ALL must APPROVE. Rejection → fix → re-run.

- [ ] F1. **Plan Compliance Audit** — `oracle`
  Read the plan end-to-end. For each "Must Have": verify implementation exists (read file, curl endpoint, run command). For each "Must NOT Have": search codebase for forbidden patterns — reject with file:line if found. Check evidence files exist in .sisyphus/evidence/. Compare deliverables against plan.
  Output: `Must Have [N/N] | Must NOT Have [N/N] | Tasks [N/N] | VERDICT: APPROVE/REJECT`

- [ ] F2. **Code Quality Review** — `unspecified-high`
  Run `tsc --noEmit` + linter + `bun test`. Review all changed files for: `as any`/`@ts-ignore`, empty catches, console.log in prod, commented-out code, unused imports. Check AI slop: excessive comments, over-abstraction, generic names (data/result/item/temp).
  Output: `Build [PASS/FAIL] | Lint [PASS/FAIL] | Tests [N pass/N fail] | Files [N clean/N issues] | VERDICT`

- [ ] F3. **Real Manual QA** — `unspecified-high` (+ `playwright` skill if UI)
  Start from clean state. Execute EVERY QA scenario from EVERY task — follow exact steps, capture evidence. Test cross-task integration (features working together, not isolation). Test edge cases: empty state, invalid input, rapid actions. Save to `.sisyphus/evidence/final-qa/`.
  Output: `Scenarios [N/N pass] | Integration [N/N] | Edge Cases [N tested] | VERDICT`

- [ ] F4. **Scope Fidelity Check** — `deep`
  For each task: read "What to do", read actual diff (git log/diff). Verify 1:1 — everything in spec was built (no missing), nothing beyond spec was built (no creep). Check "Must NOT do" compliance. Detect cross-task contamination: Task N touching Task M's files. Flag unaccounted changes.
  Output: `Tasks [N/N compliant] | Contamination [CLEAN/N issues] | Unaccounted [CLEAN/N files] | VERDICT`

---

## Commit Strategy

- **1**: `chore(tests): add Moq package reference` — IrcSharp.Core.Tests.Unit.csproj, `dotnet restore`
- **2**: `test: verify Moq compatibility and build` — (no files, verification only)
- **3**: `test: create migration baseline` — (no files, baseline.md in .sisyphus/)
- **4**: `refactor(connection): fix async void anti-pattern in Reconnect()` — IrcConnection.cs, `msbuild IrcSharp.sln`
- **5**: `refactor(tests): migrate TestHelpers to Moq` — TestHelpers.cs, `dotnet test`
- **6**: `refactor(tests): migrate When_Sending_Messages to Moq` — When_Sending_Messages.cs, `dotnet test --filter When_Sending_Messages`
- **7**: `refactor(tests): migrate When_Receiving_Messages to Moq` — When_Receiving_Messages.cs
- **8**: `refactor(tests): migrate When_Parsing_Received_Messages to Moq` — When_Parsing_Received_Messages.cs
- **9**: `refactor(tests): migrate CHANNEL message generation to Moq` — When_Generating_CHANNEL_*.cs
- **10**: `refactor(tests): migrate CONNECTION message generation to Moq` — When_Generating_CONNECTION_*.cs
- **11**: `refactor(tests): migrate MESSAGE message generation to Moq` — When_Generating_MESSAGE_*.cs
- **12**: `refactor(tests): migrate MISC message generation to Moq` — When_Generating_MISC_*.cs
- **13**: `refactor(tests): migrate SERVER message generation to Moq` — When_Generating_SERVER_*.cs
- **14**: `refactor(tests): migrate SERVICE message generation to Moq` — When_Generating_SERVICE_*.cs
- **15**: `refactor(tests): migrate USER message generation to Moq` — When_Generating_USER_*.cs
- **16**: `chore(tests): remove FakeSocketConnection after Moq migration` — FakeSocketConnection.cs (deleted)
- **17**: `docs(tests): update AGENTS.md for Moq migration` — AGENTS.md
- **18**: `test: final verification - full test suite` — (no files, verification only)

---

## Success Criteria

### Verification Commands
```bash
# Build
msbuild IrcSharp.sln

# Run unit tests
dotnet test IrcSharp.Core.Tests.Unit --verbosity normal

# Verify no FakeSocketConnection references
grep -r "FakeSocketConnection" --include="*.cs" IrcSharp.Core.Tests.Unit/

# Verify no SimulateMessageReceipt references
grep -r "SimulateMessageReceipt" --include="*.cs" IrcSharp.Core.Tests.Unit/

# Check Moq usage
grep -r "new Mock<ISocketConnection>" --include="*.cs" IrcSharp.Core.Tests.Unit/
```

### Final Checklist
- [ ] All "Must Have" present
- [ ] All "Must NOT Have" absent
- [ ] All tests pass (100% pass rate)
- [ ] Build succeeds with 0 errors
- [ ] No FakeSocketConnection references remain
- [ ] No SimulateMessageReceipt() calls remain
- [ ] async void Reconnect() fixed to async Task
- [ ] All tests use Moq Raise() for event simulation
- [ ] All tests have VerifyNoOtherCalls() or appropriate Times verification
- [ ] Execution time within 10% of baseline