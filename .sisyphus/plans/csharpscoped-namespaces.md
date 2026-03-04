# C# File-Scoped Namespace Conversion

## TL;DR

> **Quick Summary**: Convert all 63 C# files in IRCSharp from traditional namespace blocks (`namespace X.Y { }`) to file-scoped namespaces (`namespace X.Y;`). Pure syntax refactoring with zero logic changes.
>
> **Deliverables**: 
> - 63 C# files with file-scoped namespace syntax
> - All projects build without errors
> - All 196 unit tests pass
> - Integration tests build successfully
>
> **Estimated Effort**: Quick (~15-20 minutes)
> **Parallel Execution**: NO - Sequential waves required
> **Critical Path**: Task 1 → Task 2 → Task 3 → Task 4 → Task 5 → Task 6 → Task 7

---

## Context

### Original Request
Convert the C# code in IRCSharp project to use file-scoped namespaces.

### Interview Summary
**Key Discussions**:
- **Target .NET Version**: .NET 10 (already upgraded, supports file-scoped namespaces)
- **Scope**: All 63 C# files across IrcSharp.Core, IrcSharp.Core.Tests.Unit, IrcSharp.Core.Tests.Integration
- **Test Strategy**: Batch conversion then test (convert all files in each phase, then verify)

**Research Findings**:
- **Current state**: 100% traditional namespace blocks (63 files)
- **Target state**: 100% file-scoped namespaces
- **No edge cases**: No nested namespaces, partial classes, or conditional compilation affecting namespaces

### Metis Review
**Identified Gaps** (addressed):
- **Order of operations**: Convert Core → Unit Tests → Integration Tests (progressive verification)
- **Tool recommendation**: Use `ast_grep_replace` with dry-run first for safety
- **Verification commands**: Build and test commands for each phase
- **Guardrails**: Exclude obj/ directories and AssemblyInfo.cs files

---

## Work Objectives

### Core Objective
Transform namespace syntax across 63 C# files from block-style to file-scoped, enabling cleaner code structure with zero behavior changes.

### Concrete Deliverables
- All 63 C# files using `namespace X.Y;` syntax (no opening brace)
- IrcSharp.Core builds successfully
- IrcSharp.Core.Tests.Unit builds and all 196 tests pass
- IrcSharp.Core.Tests.Integration builds successfully
- Zero traditional namespace blocks remaining (excluding obj/ and AssemblyInfo)

### Definition of Done
- [ ] `grep -r "^namespace.*{$" IrcSharp.Core IrcSharp.Core.Tests.Unit IrcSharp.Core.Tests.Integration --include="*.cs" | grep -v "obj/"` returns 0 matches
- [ ] `dotnet build IrcSharp.sln` returns "Build succeeded" with 0 errors
- [ ] `dotnet test IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj` shows 196 tests passed
- [ ] `dotnet test IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj` builds and runs successfully

### Must Have
- File-scoped namespace syntax on all 63 files
- No logic changes (pure syntax transformation)
- All tests pass (behavior preservation)
- Using directives remain at file top (before namespace declaration)

### Must NOT Have (Guardrails)
- NO changes to code logic, only namespace syntax
- NO modifications to obj/ directory files (auto-generated)
- NO changes to AssemblyInfo.cs files (no namespaces)
- NO combining all 63 files into single commit (use 3 commits by phase)
- NO skipping verification between phases

---

## Verification Strategy (MANDATORY)

> **ZERO HUMAN INTERVENTION** — ALL verification is agent-executed. No exceptions.
> Acceptance criteria requiring "user manually tests/confirms" are FORBIDDEN.

### Test Decision
- **Infrastructure exists**: YES (MSTest framework with dotnet test)
- **Automated tests**: Tests-after (convert all files in phase, then run tests)
- **Framework**: MSTest via `dotnet test`

### QA Policy
Every task includes agent-executed QA scenarios:
- **Build verification**: `dotnet build` commands with output parsing
- **Test execution**: `dotnet test` commands with result parsing
- **Syntax verification**: `grep` commands to count namespace patterns
- **Evidence saved to**: `.sisyphus/evidence/task-{N}-{scenario-slug}.txt`

---

## Execution Strategy

### Sequential Waves (Dependency Chain)

> This is a sequential refactoring task - Core must build before tests can be converted.
> Each wave completes (build + test) before the next begins.

```
Wave 1 (Start Immediately - Core Library):
├── Task 1: Convert IrcSharp.Core namespace blocks (54 files) [quick]
└── Task 2: Verify IrcSharp.Core build [quick]

Wave 2 (After Wave 1 - Unit Tests):
├── Task 3: Convert IrcSharp.Core.Tests.Unit namespace blocks (12 files) [quick]
└── Task 4: Verify IrcSharp.Core.Tests.Unit build and tests [quick]

Wave 3 (After Wave 2 - Integration Tests):
├── Task 5: Convert IrcSharp.Core.Tests.Integration namespace blocks (2 files) [quick]
└── Task 6: Verify IrcSharp.Core.Tests.Integration build [quick]

Wave FINAL (After ALL - Verification):
└── Task 7: Verify no traditional namespace blocks remain [quick]
```

Critical Path: Task 1 → Task 2 → Task 3 → Task 4 → Task 5 → Task 6 → Task 7

### Dependency Matrix

- **1**: — — 2
- **2**: 1 — 3
- **3**: 2 — 4
- **4**: 3 — 5
- **5**: 4 — 6
- **6**: 5 — 7
- **7**: 6 — None

### Agent Dispatch Summary

- **Wave 1**: 2 tasks → `quick` category
- **Wave 2**: 2 tasks → `quick` category
- **Wave 3**: 2 tasks → `quick` category
- **FINAL**: 1 task → `quick` category

---

## TODOs

- [ ] 1. **Convert IrcSharp.Core to file-scoped namespaces**

  **What to do**:
  - Transform all 54 C# files in IrcSharp.Core directory from `namespace X.Y {` to `namespace X.Y;`
  - Use `ast_grep_replace` with pattern: `namespace $NAMESPACE\n{` → `namespace $NAMESPACE;`
  - Language: csharp
  - Paths: IrcSharp.Core/**/*.cs (exclude obj/ directory)
  - Run dry-run first to verify pattern matches expected files

  **Must NOT do**:
  - DO NOT modify any code logic
  - DO NOT change using directives
  - DO NOT touch files in obj/ directory
  - DO NOT modify AssemblyInfo.cs files

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Pure syntax transformation, no logic changes, deterministic pattern
  - **Skills**: []
    - No additional skills needed - ast_grep_replace handles the transformation

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Sequential (Wave 1, first task)
  - **Blocks**: Task 2
  - **Blocked By**: None (can start immediately)

  **References** (CRITICAL - Be Exhaustive):

  **Pattern References** (existing code to transform):
  - `IrcSharp.Core/Connectivity/IrcConnection.cs:1` - Traditional namespace block pattern: `namespace IrcSharp.Core.Connectivity {`
  - `IrcSharp.Core/Messages/AdminMessage.cs:1` - Another example of traditional syntax
  - All 54 files in IrcSharp.Core follow the same pattern

  **Transformation Pattern**:
  - **Pattern**: `namespace $NAMESPACE\n{` (namespace declaration with opening brace on next line)
  - **Rewrite**: `namespace $NAMESPACE;` (semicolon, no brace)
  - **Tool**: `ast_grep_replace` with lang=csharp
  - **Dry-run**: YES (first to verify matches)

  **WHY Each Reference Matters**:
  - IrcConnection.cs shows the exact pattern to transform (namespace declaration + brace)
  - File-scoped namespace syntax reduces boilerplate and is standard in modern C#

  **Acceptance Criteria**:

  **If TDD (tests enabled)**:
  - [ ] N/A (no unit tests for library conversion itself)

  **QA Scenarios (MANDATORY — task is INCOMPLETE without these):**

  ```
  Scenario: Verify all 54 files converted to file-scoped syntax
    Tool: Bash (grep command)
    Preconditions: IrcSharp.Core directory exists with 54 C# files
    Steps:
      1. Run: `grep -r "^namespace.*;$" IrcSharp.Core --include="*.cs" | grep -v "obj/" | wc -l`
      2. Count returned number
      3. Run: `grep -r "^namespace.*{$" IrcSharp.Core --include="*.cs" | grep -v "obj/" | wc -l`
      4. Count returned number (should be 0)
    Expected Result: File-scoped count = 54, Traditional count = 0
    Failure Indicators: File-scoped count < 54 or Traditional count > 0 means incomplete conversion
    Evidence: .sisyphus/evidence/task-1-namespace-verification.txt

  Scenario: Verify no logic changes introduced
    Tool: Bash (git diff command)
    Preconditions: Git repository with current changes
    Steps:
      1. Run: `git diff --stat IrcSharp.Core -- "*.cs"`
      2. Review changed files list
      3. Spot-check 3 random files: `git diff IrcSharp.Core/<filename>`
      4. Confirm only namespace line changed (namespace X.Y { → namespace X.Y;)
    Expected Result: Only first line of each file changed, all other content identical
    Failure Indicators: Any file shows changes beyond namespace declaration
    Evidence: .sisyphus/evidence/task-1-logic-verification.txt
  ```

  **Evidence to Capture**:
  - [ ] task-1-namespace-verification.txt - grep counts showing 54 file-scoped, 0 traditional
  - [ ] task-1-logic-verification.txt - git diff showing only namespace line changed

  **Commit**: NO (group with Task 2)
  - Message: `refactor: Convert IrcSharp.Core to file-scoped namespaces`
  - Files: `IrcSharp.Core/**/*.cs`
  - Pre-commit: `dotnet build IrcSharp.Core/IrcSharp.Core.csproj`

---

- [ ] 2. **Verify IrcSharp.Core builds successfully**

  **What to do**:
  - Build the IrcSharp.Core library to confirm no compilation errors
  - Run: `dotnet build IrcSharp.Core/IrcSharp.Core.csproj`
  - Parse output for "Build succeeded" and error count = 0
  - Save build output to evidence file

  **Must NOT do**:
  - DO NOT proceed to next wave if build fails
  - DO NOT ignore compilation warnings about namespace syntax

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Simple build command execution and output parsing
  - **Skills**: []
    - No additional skills needed

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Sequential (Wave 1, second task)
  - **Blocks**: Task 3
  - **Blocked By**: Task 1

  **References** (CRITICAL - Be Exhaustive):

  **Build Configuration**:
  - `IrcSharp.Core/IrcSharp.Core.csproj` - Project configuration for core library
  - Target framework: net10.0 (verified supports file-scoped namespaces)

  **WHY Each Reference Matters**:
  - csproj file defines build configuration and target framework
  - Build verification ensures syntax is valid and compiles correctly

  **Acceptance Criteria**:

  **If TDD (tests enabled)**:
  - [ ] N/A (build verification task)

  **QA Scenarios (MANDATORY — task is INCOMPLETE without these):**

  ```
  Scenario: Build IrcSharp.Core and verify success
    Tool: Bash (dotnet build command)
    Preconditions: Task 1 completed (all 54 files converted)
    Steps:
      1. Run: `dotnet build IrcSharp.Core/IrcSharp.Core.csproj 2>&1 | tee .sisyphus/evidence/task-2-build-output.txt`
      2. Parse output for "Build succeeded" or "Build completed successfully"
      3. Check error count: `grep -i "error" .sisyphus/evidence/task-2-build-output.txt | wc -l`
      4. Verify exit code: echo $? (should be 0)
    Expected Result: Build succeeded, 0 errors, exit code 0
    Failure Indicators: Build failed, any errors, non-zero exit code
    Evidence: .sisyphus/evidence/task-2-build-output.txt

  Scenario: Verify no namespace-related warnings
    Tool: Bash (grep command)
    Preconditions: Build output saved to task-2-build-output.txt
    Steps:
      1. Run: `grep -i "namespace" .sisyphus/evidence/task-2-build-output.txt`
      2. Check for any namespace-related warnings or errors
    Expected Result: No namespace-related warnings (file-scoped syntax is valid)
    Failure Indicators: Warnings about namespace syntax or deprecated patterns
    Evidence: .sisyphus/evidence/task-2-namespace-warnings.txt
  ```

  **Evidence to Capture**:
  - [ ] task-2-build-output.txt - Full dotnet build output
  - [ ] task-2-namespace-warnings.txt - Verification no namespace warnings

  **Commit**: YES (with Task 1)
  - Message: `refactor: Convert IrcSharp.Core to file-scoped namespaces`
  - Files: `IrcSharp.Core/**/*.cs`
  - Pre-commit: `dotnet build IrcSharp.Core/IrcSharp.Core.csproj`

---

- [ ] 3. **Convert IrcSharp.Core.Tests.Unit to file-scoped namespaces**

  **What to do**:
  - Transform all 12 C# files in IrcSharp.Core.Tests.Unit directory
  - Use `ast_grep_replace` with pattern: `namespace $NAMESPACE\n{` → `namespace $NAMESPACE;`
  - Language: csharp
  - Paths: IrcSharp.Core.Tests.Unit/**/*.cs
  - Run dry-run first to verify pattern matches expected files

  **Must NOT do**:
  - DO NOT modify any test logic
  - DO NOT change using directives
  - DO NOT proceed if Wave 1 verification failed

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Same syntax transformation as Task 1, just different directory
  - **Skills**: []
    - No additional skills needed

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Sequential (Wave 2, first task)
  - **Blocks**: Task 4
  - **Blocked By**: Task 2

  **References** (CRITICAL - Be Exhaustive):

  **Pattern References** (existing code to transform):
  - `IrcSharp.Core.Tests.Unit/When_Sending_Messages.cs:1` - Traditional namespace block
  - `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs:1` - Another test file example
  - All 12 files in IrcSharp.Core.Tests.Unit follow the same pattern

  **Transformation Pattern**:
  - **Pattern**: `namespace $NAMESPACE\n{`
  - **Rewrite**: `namespace $NAMESPACE;`
  - **Tool**: `ast_grep_replace` with lang=csharp
  - **Dry-run**: YES (first to verify matches)

  **WHY Each Reference Matters**:
  - Test files follow same namespace pattern as library files
  - Conversion ensures consistency across entire codebase

  **Acceptance Criteria**:

  **If TDD (tests enabled)**:
  - [ ] N/A (no unit tests for test conversion itself)

  **QA Scenarios (MANDATORY — task is INCOMPLETE without these):**

  ```
  Scenario: Verify all 12 test files converted to file-scoped syntax
    Tool: Bash (grep command)
    Preconditions: IrcSharp.Core.Tests.Unit directory exists with 12 C# files
    Steps:
      1. Run: `grep -r "^namespace.*;$" IrcSharp.Core.Tests.Unit --include="*.cs" | wc -l`
      2. Count returned number
      3. Run: `grep -r "^namespace.*{$" IrcSharp.Core.Tests.Unit --include="*.cs" | wc -l`
      4. Count returned number (should be 0)
    Expected Result: File-scoped count = 12, Traditional count = 0
    Failure Indicators: File-scoped count < 12 or Traditional count > 0
    Evidence: .sisyphus/evidence/task-3-namespace-verification.txt

  Scenario: Verify no logic changes introduced
    Tool: Bash (git diff command)
    Preconditions: Git repository with current changes
    Steps:
      1. Run: `git diff --stat IrcSharp.Core.Tests.Unit -- "*.cs"`
      2. Spot-check 2 random files: `git diff IrcSharp.Core.Tests.Unit/<filename>`
      3. Confirm only namespace line changed
    Expected Result: Only first line of each file changed
    Failure Indicators: Any file shows changes beyond namespace declaration
    Evidence: .sisyphus/evidence/task-3-logic-verification.txt
  ```

  **Evidence to Capture**:
  - [ ] task-3-namespace-verification.txt - grep counts showing 12 file-scoped, 0 traditional
  - [ ] task-3-logic-verification.txt - git diff showing only namespace line changed

  **Commit**: NO (group with Task 4)
  - Message: `refactor: Convert IrcSharp.Core.Tests.Unit to file-scoped namespaces`
  - Files: `IrcSharp.Core.Tests.Unit/**/*.cs`
  - Pre-commit: `dotnet test IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`

---

- [ ] 4. **Verify IrcSharp.Core.Tests.Unit builds and all tests pass**

  **What to do**:
  - Build the unit test project: `dotnet build IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`
  - Run all tests: `dotnet test IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`
  - Verify 196 tests pass with 0 failures
  - Parse output for test count and pass/fail status

  **Must NOT do**:
  - DO NOT proceed to Wave 3 if tests fail
  - DO NOT ignore test failures (indicates logic changes or broken syntax)

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Build and test execution with output parsing
  - **Skills**: []
    - No additional skills needed

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Sequential (Wave 2, second task)
  - **Blocks**: Task 5
  - **Blocked By**: Task 3

  **References** (CRITICAL - Be Exhaustive):

  **Test Configuration**:
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj` - Test project configuration
  - Expected test count: 196 tests (from Metis analysis)

  **WHY Each Reference Matters**:
  - Test execution verifies behavior preservation after namespace conversion
  - All 196 tests must pass to confirm no logic changes

  **Acceptance Criteria**:

  **If TDD (tests enabled)**:
  - [ ] N/A (verification task)

  **QA Scenarios (MANDATORY — task is INCOMPLETE without these):**

  ```
  Scenario: Build and run all unit tests
    Tool: Bash (dotnet test command)
    Preconditions: Task 3 completed (all 12 test files converted)
    Steps:
      1. Run: `dotnet build IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj 2>&1 | tee .sisyphus/evidence/task-4-build-output.txt`
      2. Verify build succeeded: `grep -q "Build succeeded" .sisyphus/evidence/task-4-build-output.txt`
      3. Run: `dotnet test IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj 2>&1 | tee .sisyphus/evidence/task-4-test-output.txt`
      4. Parse test results: `grep -E "(Passed|Failed|Tests:)" .sisyphus/evidence/task-4-test-output.txt`
      5. Verify test count: `grep -o "[0-9]+ test" .sisyphus/evidence/task-4-test-output.txt`
    Expected Result: Build succeeded, 196 tests passed, 0 failures
    Failure Indicators: Build failed, tests failed, test count != 196
    Evidence: .sisyphus/evidence/task-4-build-output.txt, task-4-test-output.txt

  Scenario: Verify test exit code
    Tool: Bash (echo exit code)
    Preconditions: dotnet test completed
    Steps:
      1. Run: `dotnet test IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj > /dev/null 2>&1; echo $?`
      2. Check exit code
    Expected Result: Exit code = 0 (tests passed)
    Failure Indicators: Exit code != 0 (tests failed or build error)
    Evidence: .sisyphus/evidence/task-4-exit-code.txt
  ```

  **Evidence to Capture**:
  - [ ] task-4-build-output.txt - Build output
  - [ ] task-4-test-output.txt - Full test execution output
  - [ ] task-4-exit-code.txt - Exit code verification

  **Commit**: YES (with Task 3)
  - Message: `refactor: Convert IrcSharp.Core.Tests.Unit to file-scoped namespaces`
  - Files: `IrcSharp.Core.Tests.Unit/**/*.cs`
  - Pre-commit: `dotnet test IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`

---

- [ ] 5. **Convert IrcSharp.Core.Tests.Integration to file-scoped namespaces**

  **What to do**:
  - Transform all 2 C# files in IrcSharp.Core.Tests.Integration directory
  - Use `ast_grep_replace` with pattern: `namespace $NAMESPACE\n{` → `namespace $NAMESPACE;`
  - Language: csharp
  - Paths: IrcSharp.Core.Tests.Integration/**/*.cs
  - Run dry-run first to verify pattern matches

  **Must NOT do**:
  - DO NOT modify test logic
  - DO NOT proceed if Wave 2 tests failed

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Same syntax transformation, smallest file set
  - **Skills**: []
    - No additional skills needed

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Sequential (Wave 3, first task)
  - **Blocks**: Task 6
  - **Blocked By**: Task 4

  **References** (CRITICAL - Be Exhaustive):

  **Pattern References**:
  - `IrcSharp.Core.Tests.Integration/When_Connecting_To_A_Real_Server.cs:1` - Traditional namespace
  - `IrcSharp.Core.Tests.Integration/AssemblyInit.cs:1` - Another integration test file

  **Transformation Pattern**:
  - **Pattern**: `namespace $NAMESPACE\n{`
  - **Rewrite**: `namespace $NAMESPACE;`
  - **Tool**: `ast_grep_replace` with lang=csharp
  - **Dry-run**: YES

  **Acceptance Criteria**:

  **QA Scenarios (MANDATORY — task is INCOMPLETE without these):**

  ```
  Scenario: Verify all 2 integration test files converted
    Tool: Bash (grep command)
    Preconditions: IrcSharp.Core.Tests.Integration directory exists with 2 C# files
    Steps:
      1. Run: `grep -r "^namespace.*;$" IrcSharp.Core.Tests.Integration --include="*.cs" | wc -l`
      2. Run: `grep -r "^namespace.*{$" IrcSharp.Core.Tests.Integration --include="*.cs" | wc -l`
    Expected Result: File-scoped count = 2, Traditional count = 0
    Failure Indicators: Counts don't match expected
    Evidence: .sisyphus/evidence/task-5-namespace-verification.txt
  ```

  **Evidence to Capture**:
  - [ ] task-5-namespace-verification.txt - grep counts showing 2 file-scoped, 0 traditional

  **Commit**: NO (group with Task 6)
  - Message: `refactor: Convert IrcSharp.Core.Tests.Integration to file-scoped namespaces`
  - Files: `IrcSharp.Core.Tests.Integration/**/*.cs`
  - Pre-commit: `dotnet build IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj`

---

- [ ] 6. **Verify IrcSharp.Core.Tests.Integration builds successfully**

  **What to do**:
  - Build the integration test project: `dotnet build IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj`
  - Verify build succeeds with 0 errors
  - Note: Integration tests may require bircd.exe server (build should still succeed)

  **Must NOT do**:
  - DO NOT proceed to final verification if build fails

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Simple build command execution
  - **Skills**: []
    - No additional skills needed

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Sequential (Wave 3, second task)
  - **Blocks**: Task 7
  - **Blocked By**: Task 5

  **References** (CRITICAL - Be Exhaustive):

  **Build Configuration**:
  - `IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj` - Integration test project

  **Acceptance Criteria**:

  **QA Scenarios (MANDATORY — task is INCOMPLETE without these):**

  ```
  Scenario: Build integration tests and verify success
    Tool: Bash (dotnet build command)
    Preconditions: Task 5 completed (2 files converted)
    Steps:
      1. Run: `dotnet build IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj 2>&1 | tee .sisyphus/evidence/task-6-build-output.txt`
      2. Verify: `grep -q "Build succeeded" .sisyphus/evidence/task-6-build-output.txt`
      3. Check error count: `grep -i "error" .sisyphus/evidence/task-6-build-output.txt | wc -l`
    Expected Result: Build succeeded, 0 errors
    Failure Indicators: Build failed or errors present
    Evidence: .sisyphus/evidence/task-6-build-output.txt
  ```

  **Evidence to Capture**:
  - [ ] task-6-build-output.txt - Build output

  **Commit**: YES (with Task 5)
  - Message: `refactor: Convert IrcSharp.Core.Tests.Integration to file-scoped namespaces`
  - Files: `IrcSharp.Core.Tests.Integration/**/*.cs`
  - Pre-commit: `dotnet build IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj`

---

- [ ] 7. **Final verification: Confirm no traditional namespace blocks remain**

  **What to do**:
  - Verify all 63 files use file-scoped namespace syntax
  - Run comprehensive grep checks across all three directories
  - Confirm zero traditional namespace blocks remain (excluding obj/ and AssemblyInfo)
  - Create summary report of conversion completion

  **Must NOT do**:
  - DO NOT mark task complete if any traditional blocks remain
  - DO NOT include obj/ directory or AssemblyInfo.cs files in verification

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Verification command execution and reporting
  - **Skills**: []
    - No additional skills needed

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Sequential (Wave FINAL)
  - **Blocks**: None (final task)
  - **Blocked By**: Task 6

  **References** (CRITICAL - Be Exhaustive):

  **Target Directories**:
  - `IrcSharp.Core` - 54 files
  - `IrcSharp.Core.Tests.Unit` - 12 files
  - `IrcSharp.Core.Tests.Integration` - 2 files
  - **Total**: 68 files (63 with namespaces + 5 without)

  **Acceptance Criteria**:

  **If TDD (tests enabled)**:
  - [ ] N/A (final verification)

  **QA Scenarios (MANDATORY — task is INCOMPLETE without these):**

  ```
  Scenario: Verify zero traditional namespace blocks remain
    Tool: Bash (grep command)
    Preconditions: All previous tasks completed
    Steps:
      1. Run: `grep -r "^namespace.*{$" IrcSharp.Core IrcSharp.Core.Tests.Unit IrcSharp.Core.Tests.Integration --include="*.cs" | grep -v "obj/" | grep -v "AssemblyInfo" > .sisyphus/evidence/task-7-traditional.txt 2>&1`
      2. Count matches: `wc -l < .sisyphus/evidence/task-7-traditional.txt`
      3. Run: `grep -r "^namespace.*;$" IrcSharp.Core IrcSharp.Core.Tests.Unit IrcSharp.Core.Tests.Integration --include="*.cs" | grep -v "obj/" | wc -l`
      4. Verify count = 63
    Expected Result: Traditional count = 0, File-scoped count = 63
    Failure Indicators: Traditional count > 0 or File-scoped count != 63
    Evidence: .sisyphus/evidence/task-7-traditional.txt, .sisyphus/evidence/task-7-summary.txt

  Scenario: Verify total file-scoped namespace count
    Tool: Bash (grep and wc)
    Preconditions: All files converted
    Steps:
      1. Run: `grep -r "^namespace.*;$" IrcSharp.Core IrcSharp.Core.Tests.Unit IrcSharp.Core.Tests.Integration --include="*.cs" | grep -v "obj/" | wc -l`
      2. Capture count
    Expected Result: Count = 63
    Failure Indicators: Count != 63
    Evidence: .sisyphus/evidence/task-7-count.txt

  Scenario: Generate conversion summary report
    Tool: Bash (echo and tee)
    Preconditions: All verification scenarios passed
    Steps:
      1. Run: `echo "=== C# File-Scoped Namespace Conversion Summary ===" > .sisyphus/evidence/task-7-summary.txt`
      2. Append: `echo "Traditional namespace blocks remaining: $(cat .sisyphus/evidence/task-7-traditional.txt | wc -l)" >> .sisyphus/evidence/task-7-summary.txt`
      3. Append: `echo "File-scoped namespaces: $(cat .sisyphus/evidence/task-7-count.txt)" >> .sisyphus/evidence/task-7-summary.txt`
      4. Append: `echo "Conversion Status: $(if [ $(cat .sisyphus/evidence/task-7-traditional.txt | wc -l) -eq 0 ] && [ $(cat .sisyphus/evidence/task-7-count.txt) -eq 63 ]; then echo "COMPLETE"; else echo "INCOMPLETE"; fi)" >> .sisyphus/evidence/task-7-summary.txt`
      5. Display: `cat .sisyphus/evidence/task-7-summary.txt`
    Expected Result: Summary shows "Conversion Status: COMPLETE"
    Failure Indicators: Summary shows "INCOMPLETE"
    Evidence: .sisyphus/evidence/task-7-summary.txt
  ```

  **Evidence to Capture**:
  - [ ] task-7-traditional.txt - List of remaining traditional blocks (should be empty)
  - [ ] task-7-count.txt - File-scoped namespace count (should be 63)
  - [ ] task-7-summary.txt - Conversion summary report

  **Commit**: NO (final verification, no changes)
  - Message: N/A
  - Files: N/A
  - Pre-commit: N/A

---

## Final Verification Wave (MANDATORY — after ALL implementation tasks)

> 4 review agents run in PARALLEL. ALL must APPROVE. Rejection → fix → re-run.

- [ ] F1. **Plan Compliance Audit** — `oracle`
  Read the plan end-to-end. For each "Must Have": verify all 63 files use file-scoped syntax via grep (expect 63 matches). For each "Must NOT Have": search for traditional blocks (expect 0 matches). Check evidence files exist in .sisyphus/evidence/. Compare deliverables against plan (7 tasks completed, 3 commits).
  Output: `Must Have [N/N] | Must NOT Have [N/N] | Tasks [7/7] | VERDICT: APPROVE/REJECT`

- [ ] F2. **Code Quality Review** — `unspecified-high`
  Run `dotnet build IrcSharp.sln` + `dotnet test`. Review all changed files for: only namespace line changed (no logic modifications), no new warnings introduced, consistent semicolon syntax. Check AI slop: no excessive comments, no generic names.
  Output: `Build [PASS/FAIL] | Tests [196/196 pass] | Files [63 modified] | VERDICT`

- [ ] F3. **Real Manual QA** — `unspecified-high`
  Start from clean state. Execute EVERY QA scenario from EVERY task — verify all grep counts match (54, 12, 2, 63), all builds succeed, all 196 tests pass. Test that no traditional blocks remain. Save evidence to `.sisyphus/evidence/final-qa/`.
  Output: `Scenarios [N/N pass] | Integration [PASS] | Edge Cases [0 traditional blocks] | VERDICT`

- [ ] F4. **Scope Fidelity Check** — `deep`
  For each task: read "What to do", read actual diff (git log/diff). Verify 1:1 — all 63 files converted, only namespace syntax changed, no logic modifications. Check "Must NOT do" compliance (no obj/ changes, no AssemblyInfo changes).
  Output: `Tasks [7/7 compliant] | Contamination [CLEAN] | Unaccounted [CLEAN] | VERDICT`

---

## Commit Strategy

**3 Commits Total (one per wave)**:

1. **Commit 1** (after Task 2):
   - Message: `refactor: Convert IrcSharp.Core to file-scoped namespaces`
   - Files: All 54 C# files in IrcSharp.Core
   - Pre-commit: `dotnet build IrcSharp.Core/IrcSharp.Core.csproj`

2. **Commit 2** (after Task 4):
   - Message: `refactor: Convert IrcSharp.Core.Tests.Unit to file-scoped namespaces`
   - Files: All 12 C# files in IrcSharp.Core.Tests.Unit
   - Pre-commit: `dotnet test IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj`

3. **Commit 3** (after Task 6):
   - Message: `refactor: Convert IrcSharp.Core.Tests.Integration to file-scoped namespaces`
   - Files: All 2 C# files in IrcSharp.Core.Tests.Integration
   - Pre-commit: `dotnet build IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj`

**Rationale**: Smaller commits enable easier debugging if issues arise during conversion.

---

## Success Criteria

### Verification Commands
```bash
# Verify no traditional namespace blocks remain
grep -r "^namespace.*{$" IrcSharp.Core IrcSharp.Core.Tests.Unit IrcSharp.Core.Tests.Integration --include="*.cs" | grep -v "obj/" | grep -v "AssemblyInfo" | wc -l
# Expected: 0

# Verify all 63 files use file-scoped syntax
grep -r "^namespace.*;$" IrcSharp.Core IrcSharp.Core.Tests.Unit IrcSharp.Core.Tests.Integration --include="*.cs" | grep -v "obj/" | wc -l
# Expected: 63

# Build all projects
dotnet build IrcSharp.sln
# Expected: Build succeeded

# Run all unit tests
dotnet test IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj
# Expected: 196 tests passed

# Build integration tests
dotnet build IrcSharp.Core.Tests.Integration/IrcSharp.Core.Tests.Integration.csproj
# Expected: Build succeeded
```

### Final Checklist
- [ ] All 63 files converted to file-scoped namespace syntax
- [ ] All 3 projects build without errors
- [ ] All 196 unit tests pass
- [ ] Integration tests build successfully
- [ ] Zero traditional namespace blocks remain (excluding obj/ and AssemblyInfo)
- [ ] No logic changes introduced (verified by test pass rate)
- [ ] 3 commits created (one per wave)
- [ ] All evidence files captured in .sisyphus/evidence/