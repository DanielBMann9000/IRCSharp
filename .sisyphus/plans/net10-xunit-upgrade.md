# Plan: Upgrade to .NET 10 and xUnit

## TL;DR

> **Quick Summary**: Upgrade IrcSharp from .NET Framework 4.5 to .NET 10 with SDK-style project files, and convert all tests from MSTest to xUnit framework
> 
> **Deliverables**:
> - IrcSharp.Core.csproj converted to SDK-style .NET 10
> - IrcSharp.Core.Tests.Unit.csproj converted to SDK-style .NET 10 with xUnit
> - IrcSharp.Core.Tests.Integration.csproj converted to SDK-style .NET 10 with xUnit
> - All test files: MSTest attributes → xUnit attributes, MSTest assertions → xUnit assertions
> - Solution file updated for SDK-style projects
> - All tests passing after migration
> 
> **Estimated Effort**: Medium (6-8 hours)
> **Parallel Execution**: YES - 3 waves (8 tasks total)
> **Critical Path**: Core project → Unit tests → Integration tests → Final verification

---

## Context

### Original Request
Upgrade this .NET Framework project to a .NET 10 project. Convert test framework from MSTest to xUnit.

### Interview Summary
**Key Discussions**:
- Target framework: net10.0 for all projects
- Test framework: xUnit (modern, open-source, widely adopted)
- Project file format: SDK-style (modern, simpler, better tooling support)
- Migration should be behavior-preserving - no logic changes
- All three projects must be migrated (Core + Unit tests + Integration tests)

**Research Findings**:
- xUnit is the recommended modern test framework for .NET
- .NET 10 SDK projects use implicit usings and simplified syntax
- xUnit creates new test class instance per test (no shared state between tests)
- xUnit runs tests in parallel by default
- Core library has no external NuGet packages (only System references)
- FakeSocketConnection is custom test infrastructure, no MSTest-specific types used
- No strong name signing or complex assembly signing detected

### Metis Review
**Identified Gaps** (addressed):
- **xUnit compatibility with FakeSocketConnection**: CONFIRMED - custom infrastructure, no MSTest-specific types
- **No MSTest-specific assertion extensions**: CONFIRMED - only basic Assert methods used
- **Embedded bircd.exe compatibility**: ASSUMED - standalone binary, runtime should work
- **Async/await changes**: VERIFIED - no conditional compilation directives found
- **Assembly signing**: No strong name signing detected
- **Test class inheritance**: No base test classes found

---

## Work Objectives

### Core Objective
Upgrade IrcSharp from .NET Framework 4.5 to .NET 10 with SDK-style project files, and convert all tests from MSTest to xUnit framework while preserving all existing test logic and behavior.

### Concrete Deliverables
- IrcSharp.Core.csproj: SDK-style .NET 10 project
- IrcSharp.Core.Tests.Unit.csproj: SDK-style .NET 10 with xUnit packages
- IrcSharp.Core.Tests.Integration.csproj: SDK-style .NET 10 with xUnit packages
- All test files: MSTest → xUnit conversion
- Solution file: Updated for SDK-style projects
- All tests passing with `dotnet test`

### Definition of Done
- [x] All three projects build successfully with `dotnet build IrcSharp.sln`
- [x] All unit tests pass with `dotnet test IrcSharp.Core.Tests.Unit`
- [x] All integration tests pass with `dotnet test IrcSharp.Core.Tests.Integration`
- [x] No MSTest references remain in any project file
- [x] All projects target net10.0 framework

- [x] SDK-style project files with net10.0 target framework
- [x] xUnit packages replacing MSTest packages
- [x] All test attributes converted from MSTest to xUnit
- [x] All assertions converted from MSTest to xUnit
- [x] All tests passing after migration
- [x] No behavior changes - test logic preserved exactly

### Must NOT Have (Guardrails)
- No MSTest references in any project files
- No behavior changes during migration (async void, busy-wait fixes are separate tasks)
- No code modernization (records, top-level statements, etc.)
- No test parallelization changes unless explicitly requested
- [x] No MSTest references in any project files
- [x] No behavior changes during migration (async void, busy-wait fixes are separate tasks)
- [x] No code modernization (records, top-level statements, etc.)
- [x] No test parallelization changes unless explicitly requested
- [x] No package updates beyond test framework

---

## Verification Strategy (MANDATORY)

> **ZERO HUMAN INTERVENTION** — ALL verification is agent-executed. No exceptions.
> Acceptance criteria requiring "user manually tests/confirms" are FORBIDDEN.

### Test Decision
- **Infrastructure exists**: YES - MSTest is currently set up
- **Automated tests**: TDD-style - Each task includes test verification
- **Framework**: xUnit (2.9.0 - latest stable)
- **If TDD**: Each task follows RED (failing test) → GREEN (minimal impl) → REFACTOR

### QA Policy
Every task MUST include agent-executed QA scenarios (see TODO template below).
Evidence saved to `.sisyphus/evidence/task-{N}-{scenario-slug}.{ext}`.

- **Build verification**: Use Bash (dotnet build) — Assert exit code 0, no warnings
- **Unit test execution**: Use Bash (dotnet test) — Assert all tests pass
- **Integration test execution**: Use Bash (dotnet test) — Assert all tests pass
- **Test framework verification**: Use Bash (dotnet test --listtests) — Assert xUnit references only

---

## Execution Strategy

### Parallel Execution Waves

> Maximize throughput by grouping independent tasks into parallel waves.
> Each wave completes before the next begins.
> Target: 5-8 tasks per wave. Fewer than 3 per wave (except final) = under-splitting.

```
Wave 1 (Start Immediately — foundation):
├── Task 1: Backup and analyze current project structure [quick]
├── Task 2: Convert IrcSharp.Core project to .NET 10 SDK-style [quick]
├── Task 3: Add xUnit packages to Unit tests, remove MSTest [quick]
├── Task 4: Add xUnit packages to Integration tests, remove MSTest [quick]

Wave 2 (After Wave 1 — test conversions):
├── Task 5: Convert Unit test attributes and assertions to xUnit [quick]
├── Task 6: Convert Integration test attributes and assertions to xUnit [quick]

Wave 3 (After Wave 2 — verification):
├── Task 7: Final verification and cleanup [quick]
├── Task 8: Update documentation files [quick]

Critical Path: Task 1 → Task 2 → Task 5 → Task 7 → Task 8
Parallel Speedup: ~60% faster than sequential
Max Concurrent: 4 (Waves 1 & 2)
```

### Dependency Matrix

- **1**: — — 2, 3, 4 (analysis required before changes)
- **2**: 1 — 5 (Core project must be converted before test conversions)
- **3**: 1 — 5 (Unit test packages must be set up before conversion)
- **4**: 1 — 6 (Integration test packages must be set up before conversion)
- **5**: 2, 3 — 7 (Unit tests converted before final verification)
- **6**: 4 — 7 (Integration tests converted before final verification)
- **7**: 5, 6 — 8 (All tests must pass before documentation updates)
- **8**: 7 — (final cleanup after all code is converted)

### Agent Dispatch Summary

- **Wave 1**: 4 tasks — `quick` category (project file updates)
- **Wave 2**: 2 tasks — `quick` category (find/replace conversion)
- **Wave 3**: 2 tasks — `quick` category (verification and cleanup)

---

## TODOs

> Implementation + Test = ONE Task. Never separate.
> EVERY task MUST have: Recommended Agent Profile + Parallelization info + QA Scenarios.
> **A task WITHOUT QA Scenarios is INCOMPLETE. No exceptions.**

- [x] 1. **Backup and analyze current project structure**

  **What to do**:
  - Read all .csproj files to identify current structure
  - Identify all MSTest references in project files
  - Identify all external NuGet packages (if any)
  - Identify test infrastructure dependencies
  - Create backup of solution file before making changes

  **Must NOT do**:
  - Do not modify any source files
  - Do not delete any existing files
  - Do not make any changes that would break the current build

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Simple analysis task, no code changes needed
  - **Skills**: [`git-master`]
    - `git-master`: For creating backup and checking git status
  - **Skills Evaluated but Omitted**:
    - None - simple analysis task

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (first task - no dependencies)
  - **Blocks**: Task 2, 3, 4
  - **Blocked By**: None (can start immediately)

  **References**:
  - `IrcSharp.sln` - Solution file to backup
  - `IrcSharp.Core/IrcSharp.Core.csproj` - Core library project
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj` - Unit test project
  - `IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj` - Integration test project

  **Acceptance Criteria**:
  - [ ] Solution file backed up to `IrcSharp.sln.bak`
  - [ ] MSTest references identified in all project files
  - [ ] External NuGet packages documented
  - [ ] Test infrastructure dependencies identified
  - [ ] Current build verified with `dotnet build IrcSharp.sln`

  **QA Scenarios**:

  ```
  Scenario: Project structure analysis
    Tool: Bash
    Preconditions: Current working directory is project root
    Steps:
      1. Run: dotnet sln IrcSharp.sln list
      2. Run: grep -r "MSTest" *.csproj
      3. Run: grep -r "Microsoft.VisualStudio.QualityTools" *.csproj
      4. Run: grep -r "PackageReference" IrcSharp.Core/*.csproj
    Expected Result: All project references listed, MSTest references found, package references identified
    Failure Indicators: Any command fails, unexpected output format
    Evidence: .sisyphus/evidence/task-1-analysis.txt
  ```

  **Evidence to Capture**:
  - [ ] task-1-analysis.txt - Analysis output
  - [ ] IrcSharp.sln.bak - Backup of solution file

  **Commit**: YES
  - Message: `chore: backup project structure before migration`
  - Files: `IrcSharp.sln.bak`
  - Pre-commit: `dotnet build IrcSharp.sln`

- [x] 2. **Convert IrcSharp.Core project to .NET 10 SDK-style**

  **What to do**:
  - Convert IrcSharp.Core.csproj to SDK-style format
  - Update TargetFrameworkVersion to net10.0
  - Remove old MSBuild properties (Configuration, Platform groups)
  - Remove old ItemGroup references (System, System.Core, etc.)
  - Keep all Compile Include entries

  **Must NOT do**:
  - Do not modify any source code files
  - Do not change any class names or method signatures
  - Do not add or remove any file references

  **Recommended Agent Profile**:
  - **Category**: `quick`
    - Reason: Simple project file conversion, no code changes
  - **Skills**: []
    - Standard file editing skills sufficient
  - **Skills Evaluated but Omitted**:
    - None - straightforward conversion

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (after task 1)
  - **Blocks**: Task 5, 6
  - **Blocked By**: Task 1

  **References**:
  - `IrcSharp.Core/IrcSharp.Core.csproj` - Target file for conversion

  **Acceptance Criteria**:
  - [ ] Project file converted to SDK-style format
  - [ ] TargetFramework set to net10.0
  - [ ] Build succeeds with `dotnet build IrcSharp.sln`
  - [ ] No warnings about target framework
  - [ ] No MSTest references in project file

  **QA Scenarios**:

  ```
  Scenario: Core library builds successfully
    Tool: Bash
    Preconditions: Project structure analyzed, backup created
    Steps:
      1. Run: dotnet build IrcSharp.sln
    Expected Result: Exit code 0, no warnings, IrcSharp.Core.dll created
    Failure Indicators: Build fails, target framework warnings, missing references
    Evidence: .sisyphus/evidence/task-2-build-output.txt
  ```

  **Evidence to Capture**:
  - [ ] task-2-build-output.txt - Build output
  - [ ] IrcSharp.Core/bin/Debug/net10.0/IrcSharp.Core.dll - Built assembly

  **Commit**: YES
  - Message: `refactor: convert Core project to .NET 10 SDK-style`
  - Files: `IrcSharp.Core/IrcSharp.Core.csproj`
  - Pre-commit: `dotnet build IrcSharp.sln`

- [x] 3. **Add xUnit packages to Unit tests, remove MSTest**

  **What to do**:
  - Update IrcSharp.Core.Tests.Unit.csproj to SDK-style format
  - Add xUnit packages (xunit, xunit.runner.visualstudio, Microsoft.NET.Test.Sdk)
  - Remove MSTest packages (Microsoft.VisualStudio.QualityTools)
  - Remove VisualStudioVersion and ProjectTypeGuids properties

  **Must NOT do**:
  - Do not modify any test source files
  - Do not change test logic or assertions
  - Do not add or remove any file references

  **Recommended Agent Profile**:
  - **Category**: `quick`
    - Reason: Simple project file updates
  - **Skills**: []
  - **Skills Evaluated but Omitted**:
    - None - straightforward conversion

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (after task 1)
  - **Blocks**: Task 5
  - **Blocked By**: Task 1

  **References**:
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj` - Target file for conversion

  **Acceptance Criteria**:
  - [ ] Project file converted to SDK-style format
  - [ ] xUnit packages added (xunit, xunit.runner.visualstudio, Microsoft.NET.Test.Sdk)
  - [ ] MSTest packages removed
  - [ ] Restore succeeds with `dotnet restore`
  - [ ] Project references Core library correctly

  **QA Scenarios**:

  ```
  Scenario: Unit test project restores packages
    Tool: Bash
    Preconditions: Core project converted to .NET 10
    Steps:
      1. Run: dotnet restore IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj
    Expected Result: Exit code 0, all packages restored
    Failure Indicators: Restore fails, missing package errors
    Evidence: .sisyphus/evidence/task-3-restore-output.txt
  ```

  **Evidence to Capture**:
  - [ ] task-3-restore-output.txt - Restore output

  **Commit**: YES
  - Message: `chore: add xUnit packages to Unit test project`
  - Files: `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`
  - Pre-commit: `dotnet restore IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`

- [x] 4. **Add xUnit packages to Integration tests, remove MSTest**

  **What to do**:
  - Update IrcSharp.Core.Tests.Integration.csproj to SDK-style format
  - Add xUnit packages (xunit, xunit.runner.visualstudio, Microsoft.NET.Test.Sdk)
  - Remove MSTest packages (Microsoft.VisualStudio.QualityTools)
  - Remove VisualStudioVersion and ProjectTypeGuids properties
  - Keep embedded bircd.exe content references

  **Must NOT do**:
  - Do not modify any test source files
  - Do not change test logic or assertions
  - Do not remove embedded bircd.exe references

  **Recommended Agent Profile**:
  - **Category**: `quick`
    - Reason: Simple project file updates
  - **Skills**: []
  - **Skills Evaluated but Omitted**:
    - None - straightforward conversion

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (after task 1)
  - **Blocks**: Task 6
  - **Blocked By**: Task 1

  **References**:
  - `IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj` - Target file for conversion

  **Acceptance Criteria**:
  - [ ] Project file converted to SDK-style format
  - [ ] xUnit packages added
  - [ ] MSTest packages removed
  - [ ] Embedded bircd.exe content references preserved
  - [ ] Restore succeeds with `dotnet restore`

  **QA Scenarios**:

  ```
  Scenario: Integration test project restores packages
    Tool: Bash
    Preconditions: Unit test project updated
    Steps:
      1. Run: dotnet restore IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj
    Expected Result: Exit code 0, all packages restored
    Failure Indicators: Restore fails, missing package errors
    Evidence: .sisyphus/evidence/task-4-restore-output.txt
  ```

  **Evidence to Capture**:
  - [ ] task-4-restore-output.txt - Restore output

  **Commit**: YES
  - Message: `chore: add xUnit packages to Integration test project`
  - Files: `IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj`
  - Pre-commit: `dotnet restore IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj`

- [x] 5. **Convert Unit test attributes and assertions to xUnit**

  **What to do**:
  - Convert [TestClass] → no attribute (just class declaration)
  - Convert [TestMethod] → [Fact] for synchronous tests
  - Convert [TestMethod] → [Theory] for parameterized tests
  - Convert Assert.AreEqual(x, y) → Assert.Equal(x, y)
  - Convert Assert.IsTrue(x) → Assert.True(x)
  - Convert Assert.IsFalse(x) → Assert.False(x)
  - Convert Assert.IsNull(x) → Assert.Null(x)
  - Convert Assert.IsNotNull(x) → Assert.NotNull(x)
  - Convert Assert.Fail(msg) → throw new Xunit.AssertException(msg)
  - Remove [ExcludeFromCodeCoverage] (xUnit doesn't use this)
  - Remove [TestInitialize], [ClassInitialize], [TestCleanup] attributes
  - Convert async void methods to async Task

  **Must NOT do**:
  - Do not change test logic or behavior
  - Do not rename test methods or classes
  - Do not add or remove test cases
  - Do not change assertion semantics

  **Recommended Agent Profile**:
  - **Category**: `quick`
    - Reason: Mechanical find/replace conversion
  - **Skills**: []
  - **Skills Evaluated but Omitted**:
    - None - straightforward conversion

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 2 (after tasks 2, 3)
  - **Blocks**: Task 7
  - **Blocked By**: Tasks 2, 3

  **References**:
  - `IrcSharp.Core.Tests.Unit/When_Generating_Miscellaneous_Messages.cs`
  - `IrcSharp.Core.Tests.Unit/When_Sending_Messages.cs`
  - `IrcSharp.Core.Tests.Unit/When_Parsing_Received_Messages.cs`
  - `IrcSharp.Core.Tests.Unit/When_Receiving_Messages.cs`
  - `IrcSharp.Core.Tests.Unit/When_Generating_*_Messages.cs` (8 files)
  - `IrcSharp.Core.Tests.Unit/TestHelpers.cs`
  - `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs`
  - `IrcSharp.Core.Tests.Unit/Properties/AssemblyInfo.cs`

  **Acceptance Criteria**:
  - [ ] All test files converted from MSTest to xUnit
  - [ ] No MSTest attributes remain in any test file
  - [ ] All assertions converted to xUnit equivalents
  - [ ] Build succeeds with `dotnet build`
  - [ ] Tests run with `dotnet test`

  **QA Scenarios**:

  ```
  Scenario: Unit tests build and run
    Tool: Bash
    Preconditions: xUnit packages added to Unit test project
    Steps:
      1. Run: dotnet build IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj
      2. Run: dotnet test IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj
    Expected Result: Exit code 0 for both, all tests pass
    Failure Indicators: Build fails, test failures, MSTest references found
    Evidence: .sisyphus/evidence/task-5-unit-tests.txt
  ```

  **Evidence to Capture**:
  - [ ] task-5-unit-tests.txt - Build and test output

  **Commit**: YES
  - Message: `refactor: convert Unit tests from MSTest to xUnit`
  - Files: `IrcSharp.Core.Tests.Unit/*.cs`
  - Pre-commit: `dotnet test IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`

- [x] 6. **Convert Integration test attributes and assertions to xUnit**

  **What to do**:
  - Convert [TestClass] → no attribute
  - Convert [TestMethod] → [Fact]
  - Convert [ClassInitialize] → static constructor or [Fact] setup
  - Convert Assert.AreEqual → Assert.Equal
  - Convert Assert.IsTrue → Assert.True
  - Remove [ExcludeFromCodeCoverage]
  - Convert async void methods to async Task

  **Must NOT do**:
  - Do not change test logic or behavior
  - Do not rename test methods or classes
  - Do not remove embedded bircd.exe references
  - Do not change server connection logic

  **Recommended Agent Profile**:
  - **Category**: `quick`
    - Reason: Mechanical find/replace conversion
  - **Skills**: []
  - **Skills Evaluated but Omitted**:
    - None - straightforward conversion

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 2 (after tasks 2, 4)
  - **Blocks**: Task 7
  - **Blocked By**: Tasks 2, 4

  **References**:
  - `IrcSharp.Core.Tests.Integration/When_Connecting_To_A_Real_Server.cs`
  - `IrcSharp.Core.Tests.Integration/AssemblyInit.cs`
  - `IrcSharp.Core.Tests.Integration/Properties/AssemblyInfo.cs`

  **Acceptance Criteria**:
  - [ ] All integration test files converted from MSTest to xUnit
  - [ ] No MSTest attributes remain in any test file
  - [ ] All assertions converted to xUnit equivalents
  - [ ] Embedded bircd.exe references preserved
  - [ ] Build succeeds with `dotnet build`
  - [ ] Tests run with `dotnet test`

  **QA Scenarios**:

  ```
  Scenario: Integration tests build and run
    Tool: Bash
    Preconditions: xUnit packages added to Integration test project
    Steps:
      1. Run: dotnet build IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj
      2. Run: dotnet test IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj
    Expected Result: Exit code 0 for both, all tests pass
    Failure Indicators: Build fails, test failures, MSTest references found
    Evidence: .sisyphus/evidence/task-6-integration-tests.txt
  ```

  **Evidence to Capture**:
  - [ ] task-6-integration-tests.txt - Build and test output

  **Commit**: YES
  - Message: `refactor: convert Integration tests from MSTest to xUnit`
  - Files: `IrcSharp.Core.Tests.Integration/*.cs`
  - Pre-commit: `dotnet test IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj`

- [x] 7. **Final verification and cleanup**

  **What to do**:
  - Full solution build with `dotnet build IrcSharp.sln`
  - Run all unit tests with `dotnet test IrcSharp.Core.Tests.Unit`
  - Run all integration tests with `dotnet test IrcSharp.Core.Tests.Integration`
  - Verify no MSTest references remain in any project file
  - Verify all projects target net10.0
  - Verify xUnit test framework references only

  **Must NOT do**:
  - Do not modify any source files
  - Do not change any project configurations
  - Do not add new packages

  **Recommended Agent Profile**:
  - **Category**: `quick`
    - Reason: Verification and cleanup task
  - **Skills**: [`git-master`]
    - `git-master`: For git cleanup and verification
  - **Skills Evaluated but Omitted**:
    - None - straightforward verification

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 3 (after tasks 5, 6)
  - **Blocks**: Task 8
  - **Blocked By**: Tasks 5, 6

  **References**:
  - `IrcSharp.sln` - Solution file
  - `IrcSharp.Core/IrcSharp.Core.csproj`
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`
  - `IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj`

  **Acceptance Criteria**:
  - [ ] Solution builds successfully with `dotnet build IrcSharp.sln`
  - [ ] All unit tests pass with `dotnet test IrcSharp.Core.Tests.Unit`
  - [ ] All integration tests pass with `dotnet test IrcSharp.Core.Tests.Integration`
  - [ ] No MSTest references remain in any project file
  - [ ] All projects target net10.0 framework
  - [ ] All tests use xUnit framework

  **QA Scenarios**:

  ```
  Scenario: Full solution verification
    Tool: Bash
    Preconditions: All test files converted to xUnit
    Steps:
      1. Run: dotnet build IrcSharp.sln
      2. Run: dotnet test IrcSharp.sln
      3. Run: grep -r "MSTest\|Microsoft.VisualStudio.QualityTools" *.csproj
      4. Run: dotnet test IrcSharp.sln --listtests
    Expected Result: All builds succeed, all tests pass, no MSTest references, xUnit framework listed
    Failure Indicators: Any build/test fails, MSTest references found, wrong framework
    Evidence: .sisyphus/evidence/task-7-verification.txt
  ```

  **Evidence to Capture**:
  - [ ] task-7-verification.txt - Full verification output
  - [ ] IrcSharp.sln - Solution file (updated)

  **Commit**: YES
  - Message: `build: final verification - all tests passing with .NET 10 and xUnit`
  - Files: `IrcSharp.sln`
  - Pre-commit: `dotnet test IrcSharp.sln`

- [x] 8. **Update documentation files**

  **What to do**:
  - Update AGENTS.md files to reflect .NET 10 target
  - Update README.md to reflect .NET 10 and xUnit
  - Update .gitignore if needed for SDK-style projects
  - Document migration changes

  **Must NOT do**:
  - Do not modify any source code
  - Do not modify any test code
  - Do not change any project configurations

  **Recommended Agent Profile**:
  - **Category**: `quick`
    - Reason: Documentation update task
  - **Skills**: []
  - **Skills Evaluated but Omitted**:
    - None - straightforward update

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 3 (after task 7)
  - **Blocks**: None
  - **Blocked By**: Task 7

  **References**:
  - `README.md` - Project overview
  - `AGENTS.md` - Agent documentation
  - `.gitignore` - Git ignore rules

  **Acceptance Criteria**:
  - [ ] README.md updated to reflect .NET 10 target
  - [ ] README.md updated to reference xUnit
  - [ ] AGENTS.md files updated to reflect migration
  - [ ] .gitignore updated for SDK-style projects if needed

  **QA Scenarios**:

  ```
  Scenario: Documentation verification
    Tool: Bash
    Preconditions: Solution verified, all tests passing
    Steps:
      1. Run: grep -r "NET Framework 4.5\|MSTest" README.md AGENTS.md
      2. Run: grep -r "net10\|xUnit" README.md AGENTS.md
    Expected Result: No old references found, new references present
    Failure Indicators: Old references still present, new references missing
    Evidence: .sisyphus/evidence/task-8-documentation.txt
  ```

  **Evidence to Capture**:
  - [ ] task-8-documentation.txt - Documentation verification output

  **Commit**: YES
  - Message: `docs: update documentation for .NET 10 and xUnit migration`
  - Files: `README.md`, `AGENTS.md`, `.gitignore`
  - Pre-commit: `grep -r "NET Framework 4.5\|MSTest" README.md AGENTS.md`

---

## Final Verification Wave (MANDATORY — after ALL implementation tasks)

> 4 review agents run in PARALLEL. ALL must APPROVE. Rejection → fix → re-run.

- [x] F1. **Plan Compliance Audit** — `quick`
- [x] F2. **Code Quality Review** — `quick`
- [x] F3. **Real Manual QA** — `quick`
- [x] F4. **Scope Fidelity Check** — `quick`
  For each task: read "What to do", read actual diff. Verify 1:1 — everything in spec was built (no missing), nothing beyond spec was built (no creep). Check "Must NOT do" compliance. Detect cross-task contamination.
  Output: `Tasks [N/N compliant] | Contamination [CLEAN/N issues] | Unaccounted [CLEAN/N files] | VERDICT`

---

## Commit Strategy

- **1**: `chore: backup project structure before migration` — IrcSharp.sln.bak
- **2**: `refactor: convert Core project to .NET 10 SDK-style` — IrcSharp.Core.csproj
- **3**: `chore: add xUnit packages to Unit test project` — Unit.csproj
- **4**: `chore: add xUnit packages to Integration test project` — Integration.csproj
- **5**: `refactor: convert Unit tests from MSTest to xUnit` — Unit/*.cs
- **6**: `refactor: convert Integration tests from MSTest to xUnit` — Integration/*.cs
- **7**: `build: final verification - all tests passing with .NET 10 and xUnit` — IrcSharp.sln
- **8**: `docs: update documentation for .NET 10 and xUnit migration` — README.md, AGENTS.md

---

## Success Criteria

### Verification Commands
```bash
dotnet build IrcSharp.sln  # Expected: Exit code 0, no warnings
dotnet test IrcSharp.sln   # Expected: All tests pass, exit code 0
```

### Final Checklist
- [x] All "Must Have" present
- [x] All "Must NOT Have" absent
- [x] All tests pass
- [x] All projects target net10.0
- [x] All projects use SDK-style format
- [x] All tests use xUnit framework
- [x] No MSTest references remain
- [x] Documentation updated