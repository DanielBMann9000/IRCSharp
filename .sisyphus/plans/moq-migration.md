# Moq Migration: Replace FakeSocketConnection with Moq Framework

## TL;DR

> **Quick Summary**: Migrate 5 unit test files from hand-built `FakeSocketConnection` mock to Moq framework for standardization, with incremental file-by-file approach.
> 
> **Deliverables**:
> - 5 migrated test files using Moq instead of FakeSocketConnection
> - Complete test suite passes with 100% success rate
> - `FakeSocketConnection.cs` deleted after all migrations complete
> 
> **Estimated Effort**: Short (4-6 hours)
> **Parallel Execution**: NO - sequential migration required (one file at a time)
> **Critical Path**: Baseline tests → Migrate File 1 → Verify → Migrate File 2 → ... → Delete FakeSocketConnection

---

## Context

### Original Request
Replace the hand-built mocks in this C# project with the Moq framework for industry-standard compliance.

### Interview Summary
**Key Discussions**:
- **Migration Motivation**: Standardization (industry-standard mocking framework)
- **Migration Approach**: Incremental (one test file at a time, commit by commit)
- **Legacy Handling**: Delete FakeSocketConnection entirely after all migrations
- **Verification**: Run all existing tests to ensure behavior matches

**Research Findings**:
- Only 1 mock class exists: `FakeSocketConnection` (84 lines, simple stub implementing `ISocketConnection`)
- 5 test files currently use FakeSocketConnection
- Moq v4.20.72 is already installed in the project (no new dependencies needed)
- `ISocketConnection` interface has 6 methods + 3 events + 1 property

**Metis Review**:
**Identified Gaps** (addressed):
- Added baseline verification step before migration
- Added explicit guardrails for behavior preservation
- Added per-file acceptance criteria
- Added edge case handling for stateful behavior, events, async methods
- Added migration order strategy (simplest to most complex)
- Added hybrid approach contingency for complex behaviors

---

## Work Objectives

### Core Objective
Replace `FakeSocketConnection` with Moq mocks in all 5 test files, then delete the fake implementation.

### Concrete Deliverables
- 5 migrated test files with Moq mocks
- 100% test pass rate across all unit tests
- `FakeSocketConnection.cs` deleted from codebase

### Definition of Done
- [ ] Run full test suite and capture baseline results (pass/fail counts)
- [ ] All 5 test files migrated with Moq
- [ ] Full test suite passes with 100% success rate
- [ ] `FakeSocketConnection.cs` deleted
- [ ] No references to `FakeSocketConnection` remain in test code
- [ ] Git history shows atomic commits per file migrated

### Must Have
- Incremental migration (one file at a time)
- 1:1 behavior preservation (no test logic changes)
- Atomic commits per file migrated
- Baseline test results captured before migration
- All tests pass after each migration

### Must NOT Have (Guardrails)
- ❌ No refactoring of test logic beyond mock replacement
- ❌ No new tests or test cases added
- ❌ No production code modifications
- ❌ No Moq version updates or new packages
- ❌ No integration test modifications (out of scope)
- ❌ No documentation updates
- ❌ No code style changes beyond Moq requirements
- ❌ No partial migrations (don't leave files half-migrated)

---

## Verification Strategy (MANDATORY)

### Test Decision
- **Infrastructure exists**: YES (MSTest)
- **Automated tests**: YES (baseline + verification)
- **Framework**: MSTest (Microsoft.VisualStudio.TestPlatform)
- **If verification**: Run full test suite before and after each migration

### QA Policy
Every migration task MUST include verification by running tests. Evidence saved as test output logs.

- **Unit Tests**: Run `msbuild IrcSharp.sln /t:Test` or Visual Studio Test Explorer
- **Verification**: Compare pass/fail counts before and after migration
- **Evidence**: Capture test output for baseline and each migration step

---

## Execution Strategy

### Sequential Migration Waves

```
Wave 0 (Pre-Migration Foundation):
├── Task 1: Capture baseline test results [quick]
├── Task 2: Identify exact test files using FakeSocketConnection [quick]
├── Task 3: Review FakeSocketConnection behavior in detail [deep]
└── Task 4: Plan migration order (simplest to most complex) [quick]

Wave 1 (Migration - File 1):
└── Task 5: Migrate simplest test file to Moq [unspecified-high]

Wave 2 (Verification Wave 1):
├── Task 6: Run full test suite, verify 100% pass [quick]
└── Task 7: Document migration results [quick]

Wave 3 (Migration - File 2):
└── Task 8: Migrate second test file to Moq [unspecified-high]

Wave 4 (Verification Wave 2):
├── Task 9: Run full test suite, verify 100% pass [quick]
└── Task 10: Document migration results [quick]

Wave 5 (Migration - File 3):
└── Task 11: Migrate third test file to Moq [unspecified-high]

Wave 6 (Verification Wave 3):
├── Task 12: Run full test suite, verify 100% pass [quick]
└── Task 13: Document migration results [quick]

Wave 7 (Migration - File 4):
└── Task 14: Migrate fourth test file to Moq [unspecified-high]

Wave 8 (Verification Wave 4):
├── Task 15: Run full test suite, verify 100% pass [quick]
└── Task 16: Document migration results [quick]

Wave 9 (Migration - File 5):
└── Task 17: Migrate fifth test file to Moq [unspecified-high]

Wave 10 (Verification Wave 5):
├── Task 18: Run full test suite, verify 100% pass [quick]
└── Task 19: Document migration results [quick]

Wave FINAL (Post-Migration Cleanup):
├── Task 20: Delete FakeSocketConnection.cs [quick]
├── Task 21: Verify no references to FakeSocketConnection remain [quick]
├── Task 22: Run final test suite, capture results [quick]
└── Task 23: Create migration summary commit [git]
```

**Critical Path**: Task 1 → Task 5 → Task 6 → Task 8 → Task 9 → Task 11 → Task 12 → Task 14 → Task 15 → Task 17 → Task 18 → Task 20 → Task 22

**Sequential Execution**: Each migration must complete and pass before starting next file.

---

## TODOs

> Migration + Verification = ONE Task. Never separate.
> EVERY task MUST have: Recommended Agent Profile + Verification steps + QA Scenarios.
> **A task WITHOUT verification is INCOMPLETE. No exceptions.**

- [ ] 1. Capture baseline test results

  **What to do**:
  - Run full unit test suite before any changes
  - Capture pass/fail counts, execution time, any warnings/errors
  - Save output to `.sisyphus/evidence/baseline-test-results.txt`
  - Document current state of test suite

  **Must NOT do**:
  - Do not modify any test files yet
  - Do not run integration tests (out of scope)
  - Do not change any code

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None needed - simple test execution
  > **Why**: Baseline capture is a straightforward verification task

  **Parallelization**:
  - **Can Run In Parallel**: NO - must run before any migrations
  - **Parallel Group**: Wave 0 (foundation task)
  - **Blocks**: All migration tasks (must have baseline before starting)
  - **Blocked By**: None (can start immediately)

  **References**:
  - Project file: `IrcSharp.Core.Tests.Unit.csproj` - MSTest configuration
  - Solution file: `IrcSharp.sln` - Build configuration

  **Acceptance Criteria**:

  - [ ] Full test suite executed
  - [ ] Test output saved to `.sisyphus/evidence/baseline-test-results.txt`
  - [ ] Pass/fail counts documented
  - [ ] Any existing warnings/errors noted

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Capture baseline test results
    Tool: Bash (msbuild or Visual Studio Test Explorer)
    Preconditions: Clean working directory, no uncommitted changes
    Steps:
      1. Run: msbuild IrcSharp.sln /t:Test /p:Configuration=Release
      2. Capture all output to file
      3. Count passing tests, failing tests, total tests
      4. Note any warnings or errors
    Expected Result: Full test suite completes, output captured
    Failure Indicators: Build fails, no output captured, incomplete test run
    Evidence: .sisyphus/evidence/baseline-test-results.txt
  ```

  **Evidence to Capture**:
  - [ ] Baseline test output file
  - [ ] Pass/fail counts documented
  - [ ] Execution time recorded

  **Commit**: NO (foundation task, no code changes)

- [ ] 2. Identify exact test files using FakeSocketConnection

  **What to do**:
  - Run grep to find all references to `FakeSocketConnection`
  - List all test files that instantiate or reference it
  - Create migration tracker with file list
  - Determine migration order (simplest to most complex)

  **Must NOT do**:
  - Do not modify any files yet
  - Do not assume the 5 files from research - verify with grep

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None - simple search
  > **Why**: grep/search task, no complex reasoning needed

  **Parallelization**:
  - **Can Run In Parallel**: NO - must complete before migrations
  - **Parallel Group**: Wave 0 (foundation task)
  - **Blocks**: All migration tasks (need to know which files to migrate)
  - **Blocked By**: Task 1 (baseline first, then identify files)

  **References**:
  - Research finding: 5 test files use FakeSocketConnection (needs verification)
  - File to review: `IrcSharp.Core.Tests.Unit/TestHelpers.cs` (contains factory method)

  **Acceptance Criteria**:

  - [ ] Grep command executed successfully
  - [ ] Complete list of test files documented
  - [ ] Migration order determined (simplest to most complex)
  - [ ] Migration tracker created

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Identify all test files using FakeSocketConnection
    Tool: Bash (grep)
    Preconditions: Working directory clean
    Steps:
      1. Run: grep -r "FakeSocketConnection" IrcSharp.Core.Tests.Unit/
      2. Count occurrences and list file paths
      3. Identify which files instantiate vs reference
    Expected Result: Complete list of files identified
    Failure Indicators: Grep fails, files not found, incomplete list
    Evidence: .sisyphus/evidence/migration-files-list.txt
  ```

  **Evidence to Capture**:
  - [ ] Grep output saved
  - [ ] File list documented
  - [ ] Migration order determined

  **Commit**: NO (research task, no code changes)

- [ ] 3. Review FakeSocketConnection behavior in detail

  **What to do**:
  - Read `FakeSocketConnection.cs` line by line
  - Document all behaviors: state management, events, async timing, exceptions
  - Identify any edge cases that Moq must replicate
  - Create behavior mapping document

  **Must NOT do**:
  - Do not modify FakeSocketConnection
  - Do not start migration yet

  **Recommended Agent Profile**:
  > **Category**: `deep`
  > **Skills**: None - code review
  > **Why**: Understanding complex behavior requires deep analysis

  **Parallelization**:
  - **Can Run In Parallel**: NO - must complete before migrations
  - **Parallel Group**: Wave 0 (foundation task)
  - **Blocks**: All migration tasks (need behavior understanding)
  - **Blocked By**: Task 2 (after identifying files)

  **References**:
  - Source file: `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs`
  - Interface: `IrcSharp.Core/Connectivity/ISocketConnection.cs`

  **Acceptance Criteria**:

  - [ ] All FakeSocketConnection behaviors documented
  - [ ] Edge cases identified (state, events, async, exceptions)
  - [ ] Behavior mapping created for Moq equivalence

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Document FakeSocketConnection behavior
    Tool: Read (file analysis)
    Preconditions: FakeSocketConnection.cs exists
    Steps:
      1. Read entire FakeSocketConnection.cs file
      2. List all methods and their implementations
      3. Document event raising logic
      4. Document state management (Connected property, message queue)
      5. Document exception throwing conditions
    Expected Result: Complete behavior documentation
    Failure Indicators: Missing behaviors, incomplete documentation
    Evidence: .sisyphus/evidence/fakesocket-behavior-map.txt
  ```

  **Evidence to Capture**:
  - [ ] Behavior mapping document
  - [ ] Edge cases list
  - [ ] Moq equivalence notes

  **Commit**: NO (research task, no code changes)

- [ ] 4. Plan migration order (simplest to most complex)

  **What to do**:
  - Analyze each test file's complexity (lines of code, mock usage)
  - Order files from simplest to most complex
  - Create migration tracker with order
  - Decide if any files need special handling (hybrid approach)

  **Must NOT do**:
  - Do not start migration yet
  - Do not modify any files

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None - simple analysis
  > **Why**: Ordering task based on complexity assessment

  **Parallelization**:
  - **Can Run In Parallel**: NO - must complete before migrations
  - **Parallel Group**: Wave 0 (foundation task)
  - **Blocks**: All migration tasks (need order defined)
  - **Blocked By**: Task 2 (after identifying files)

  **References**:
  - File list from Task 2
  - Behavior map from Task 3

  **Acceptance Criteria**:

  - [ ] Migration order determined
  - [ ] Migration tracker created with ordered file list
  - [ ] Any special handling noted

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Determine migration order
    Tool: Read (analyze test files)
    Preconditions: File list from Task 2
    Steps:
      1. Review each test file's size and complexity
      2. Rank from simplest to most complex
      3. Note any files requiring hybrid approach
    Expected Result: Ordered migration plan
    Failure Indicators: No order determined, incomplete analysis
    Evidence: .sisyphus/evidence/migration-order.txt
  ```

  **Evidence to Capture**:
  - [ ] Migration order document
  - [ ] Complexity assessment per file
  - [ ] Special handling notes

  **Commit**: NO (planning task, no code changes)

- [ ] 5. Migrate simplest test file to Moq

  **What to do**:
  - Read the simplest test file (from migration order)
  - Replace `FakeSocketConnection` instantiation with `Mock<ISocketConnection>`
  - Set up all necessary mocks (methods, events, properties)
  - Preserve test logic exactly (no refactoring)
  - Remove any FakeSocketConnection-specific helper methods
  - Add any Moq-specific setup code needed

  **Must NOT do**:
  - Do not change test logic or assertions
  - Do not modify other test files
  - Do not refactor test naming or structure
  - Do not add new tests or remove existing tests

  **Recommended Agent Profile**:
  > **Category**: `unspecified-high`
  > **Skills**: 
  >   - `git-master`: For atomic commits
  >   - `lsp_prepare_rename`: To verify symbol references
  > **Why**: Requires understanding of test structure and Moq setup

  **Parallelization**:
  - **Can Run In Parallel**: NO - sequential migration required
  - **Parallel Group**: Wave 1 (first file migration)
  - **Blocks**: Task 6 (verification)
  - **Blocked By**: Tasks 1-4 (all foundation tasks)

  **References**:
  - Target file: `[simplest test file from migration order]`
  - Moq docs: `https://github.com/moq/moq4/wiki/Quickstart`
  - FakeSocketConnection behavior map: `.sisyphus/evidence/fakesocket-behavior-map.txt`

  **Acceptance Criteria**:

  - [ ] Mock<ISocketConnection> created
  - [ ] All FakeSocketConnection methods replaced with Moq Setup
  - [ ] All events properly set up with Moq Raise()
  - [ ] Property access mocked with SetupProperty()
  - [ ] Test logic unchanged
  - [ ] No FakeSocketConnection references remain in this file

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify migrated test file structure
    Tool: Read (code review)
    Preconditions: Migration complete
    Steps:
      1. Read migrated file
      2. Verify Mock<ISocketConnection> instantiated
      3. Verify all method setups present
      4. Verify all event setups present
      5. Verify no FakeSocketConnection references remain
      6. Verify test logic unchanged
    Expected Result: File properly migrated to Moq
    Failure Indicators: FakeSocketConnection references remain, test logic changed
    Evidence: .sisyphus/evidence/migration-file-1-review.txt
  ```

  **Evidence to Capture**:
  - [ ] Git diff showing changes
  - [ ] Code review notes
  - [ ] Behavior equivalence verification

  **Commit**: YES - Atomic commit for this file
  - Message: `refactor(tests): migrate [filename] to Moq`
  - Files: `[migrated test file]`
  - Pre-commit: Run tests for this file only

- [ ] 6. Run full test suite, verify 100% pass

  **What to do**:
  - Run full unit test suite
  - Compare pass/fail counts to baseline
  - Verify all tests pass (100% success rate)
  - Document any failures or warnings
  - If failures: debug and fix Moq setup, re-run

  **Must NOT do**:
  - Do not proceed to next file if tests fail
  - Do not ignore test failures

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: 
  >   - `git-master`: For debugging if needed
  > **Why**: Test execution and verification

  **Parallelization**:
  - **Can Run In Parallel**: NO - must complete before next migration
  - **Parallel Group**: Wave 2 (verification)
  - **Blocks**: Task 8 (next file migration)
  - **Blocked By**: Task 5 (file migration)

  **References**:
  - Baseline results: `.sisyphus/evidence/baseline-test-results.txt`
  - Solution file: `IrcSharp.sln`

  **Acceptance Criteria**:

  - [ ] Full test suite executed
  - [ ] All tests pass (100% success rate)
  - [ ] No new warnings introduced
  - [ ] Results documented

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify all tests pass after migration
    Tool: Bash (msbuild)
    Preconditions: File 1 migrated
    Steps:
      1. Run: msbuild IrcSharp.sln /t:Test /p:Configuration=Release
      2. Capture all output
      3. Count passing tests
      4. Compare to baseline (Task 1)
      5. Verify 100% pass rate
    Expected Result: All tests pass
    Failure Indicators: Any test failure, new warnings, execution time >20% deviation
    Evidence: .sisyphus/evidence/migration-file-1-test-results.txt
  ```

  **Evidence to Capture**:
  - [ ] Test output saved
  - [ ] Pass/fail counts documented
  - [ ] Comparison to baseline
  - [ ] Any failures logged

  **Commit**: NO (verification task, no code changes)

- [ ] 7. Document migration results

  **What to do**:
  - Create migration log entry for File 1
  - Record any issues encountered
  - Note any deviations from expected behavior
  - Update migration tracker

  **Must NOT do**:
  - Do not skip documentation

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Documentation task

  **Parallelization**:
  - **Can Run In Parallel**: NO - must complete before next migration
  - **Parallel Group**: Wave 2 (verification)
  - **Blocks**: Task 8 (next file migration)
  - **Blocked By**: Task 6 (test verification)

  **References**:
  - Migration tracker
  - Test results from Task 6

  **Acceptance Criteria**:

  - [ ] Migration log entry created
  - [ ] Issues documented
  - [ ] Tracker updated

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Document migration results
    Tool: Write (create log entry)
    Preconditions: Test results from Task 6
    Steps:
      1. Create log entry for File 1 migration
      2. Record test results
      3. Document any issues or deviations
      4. Update migration tracker
    Expected Result: Complete migration documentation
    Failure Indicators: Missing documentation, incomplete log
    Evidence: .sisyphus/evidence/migration-log-file-1.txt
  ```

  **Evidence to Capture**:
  - [ ] Migration log entry
  - [ ] Tracker updated
  - [ ] Issues documented

  **Commit**: NO (documentation task)

- [ ] 8. Migrate second test file to Moq

  **What to do**:
  - Repeat Task 5 for second file in migration order
  - Apply lessons learned from File 1 migration
  - Replace FakeSocketConnection with Moq
  - Preserve test logic exactly

  **Must NOT do**:
  - Do not change test logic
  - Do not modify other files

  **Recommended Agent Profile**:
  > **Category**: `unspecified-high`
  > **Skills**: 
  >   - `git-master`: For atomic commits
  > **Why**: Mock replacement task

  **Parallelization**:
  - **Can Run In Parallel**: NO - sequential
  - **Parallel Group**: Wave 3 (second file migration)
  - **Blocks**: Task 9
  - **Blocked By**: Task 7 (documentation of File 1)

  **References**:
  - Target file: `[second file from migration order]`
  - Moq patterns from File 1 migration

  **Acceptance Criteria**:

  - [ ] Mock<ISocketConnection> created
  - [ ] All methods mocked
  - [ ] All events mocked
  - [ ] Test logic unchanged
  - [ ] No FakeSocketConnection references

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify migrated test file structure
    Tool: Read (code review)
    Preconditions: Migration complete
    Steps:
      1. Read migrated file
      2. Verify Mock<ISocketConnection> instantiated
      3. Verify all setups present
      4. Verify no FakeSocketConnection references
      5. Verify test logic unchanged
    Expected Result: File properly migrated
    Failure Indicators: FakeSocketConnection references remain
    Evidence: .sisyphus/evidence/migration-file-2-review.txt
  ```

  **Evidence to Capture**:
  - [ ] Git diff
  - [ ] Code review notes

  **Commit**: YES - Atomic commit
  - Message: `refactor(tests): migrate [filename] to Moq`
  - Files: `[migrated test file]`
  - Pre-commit: Run tests for this file only

- [ ] 9. Run full test suite, verify 100% pass

  **What to do**:
  - Repeat Task 6 for File 2
  - Run full test suite
  - Verify all tests pass
  - Document results

  **Must NOT do**:
  - Do not proceed if tests fail

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Test execution

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 4 (verification)
  - **Blocks**: Task 11
  - **Blocked By**: Task 8

  **References**:
  - Baseline: `.sisyphus/evidence/baseline-test-results.txt`

  **Acceptance Criteria**:

  - [ ] All tests pass
  - [ ] Results documented

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify all tests pass after File 2 migration
    Tool: Bash (msbuild)
    Preconditions: File 2 migrated
    Steps:
      1. Run: msbuild IrcSharp.sln /t:Test /p:Configuration=Release
      2. Capture output
      3. Verify 100% pass rate
    Expected Result: All tests pass
    Failure Indicators: Any test failure
    Evidence: .sisyphus/evidence/migration-file-2-test-results.txt
  ```

  **Evidence to Capture**:
  - [ ] Test output
  - [ ] Pass/fail counts

  **Commit**: NO

- [ ] 10. Document migration results

  **What to do**:
  - Repeat Task 7 for File 2
  - Create migration log entry
  - Update tracker

  **Must NOT do**:
  - Do not skip documentation

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Documentation

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 4 (verification)
  - **Blocks**: Task 11
  - **Blocked By**: Task 9

  **References**:
  - Migration tracker
  - Test results

  **Acceptance Criteria**:

  - [ ] Migration log created
  - [ ] Tracker updated

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Document File 2 migration
    Tool: Write
    Preconditions: Test results from Task 9
    Steps:
      1. Create log entry
      2. Record results
      3. Update tracker
    Expected Result: Complete documentation
    Failure Indicators: Missing documentation
    Evidence: .sisyphus/evidence/migration-log-file-2.txt
  ```

  **Evidence to Capture**:
  - [ ] Migration log
  - [ ] Tracker updated

  **Commit**: NO

- [ ] 11. Migrate third test file to Moq

  **What to do**:
  - Repeat Task 5 for File 3
  - Apply lessons learned

  **Must NOT do**:
  - Do not change test logic

  **Recommended Agent Profile**:
  > **Category**: `unspecified-high`
  > **Skills**: 
  >   - `git-master`
  > **Why**: Mock replacement

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 5
  - **Blocks**: Task 12
  - **Blocked By**: Task 10

  **References**:
  - Target file: `[third file from migration order]`
  - Moq patterns from Files 1-2

  **Acceptance Criteria**:

  - [ ] Mock<ISocketConnection> created
  - [ ] All methods/events mocked
  - [ ] Test logic unchanged
  - [ ] No FakeSocketConnection references

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify migrated test file structure
    Tool: Read
    Preconditions: Migration complete
    Steps:
      1. Read migrated file
      2. Verify Mock<ISocketConnection>
      3. Verify all setups present
      4. Verify no FakeSocketConnection references
      5. Verify test logic unchanged
    Expected Result: File properly migrated
    Failure Indicators: FakeSocketConnection references remain
    Evidence: .sisyphus/evidence/migration-file-3-review.txt
  ```

  **Evidence to Capture**:
  - [ ] Git diff
  - [ ] Code review notes

  **Commit**: YES - Atomic commit
  - Message: `refactor(tests): migrate [filename] to Moq`

- [ ] 12. Run full test suite, verify 100% pass

  **What to do**:
  - Repeat Task 6 for File 3

  **Must NOT do**:
  - Do not proceed if tests fail

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Test execution

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 6
  - **Blocks**: Task 14
  - **Blocked By**: Task 11

  **Acceptance Criteria**:

  - [ ] All tests pass

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify all tests pass after File 3 migration
    Tool: Bash (msbuild)
    Preconditions: File 3 migrated
    Steps:
      1. Run: msbuild IrcSharp.sln /t:Test /p:Configuration=Release
      2. Capture output
      3. Verify 100% pass rate
    Expected Result: All tests pass
    Failure Indicators: Any test failure
    Evidence: .sisyphus/evidence/migration-file-3-test-results.txt
  ```

  **Evidence to Capture**:
  - [ ] Test output
  - [ ] Pass/fail counts

  **Commit**: NO

- [ ] 13. Document migration results

  **What to do**:
  - Repeat Task 7 for File 3

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Documentation

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 6
  - **Blocks**: Task 14
  - **Blocked By**: Task 12

  **Acceptance Criteria**:

  - [ ] Migration log created
  - [ ] Tracker updated

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Document File 3 migration
    Tool: Write
    Preconditions: Test results from Task 12
    Steps:
      1. Create log entry
      2. Record results
      3. Update tracker
    Expected Result: Complete documentation
    Failure Indicators: Missing documentation
    Evidence: .sisyphus/evidence/migration-log-file-3.txt
  ```

  **Evidence to Capture**:
  - [ ] Migration log
  - [ ] Tracker updated

  **Commit**: NO

- [ ] 14. Migrate fourth test file to Moq

  **What to do**:
  - Repeat Task 5 for File 4

  **Must NOT do**:
  - Do not change test logic

  **Recommended Agent Profile**:
  > **Category**: `unspecified-high`
  > **Skills**: 
  >   - `git-master`
  > **Why**: Mock replacement

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 7
  - **Blocks**: Task 15
  - **Blocked By**: Task 13

  **References**:
  - Target file: `[fourth file from migration order]`

  **Acceptance Criteria**:

  - [ ] Mock<ISocketConnection> created
  - [ ] All methods/events mocked
  - [ ] Test logic unchanged
  - [ ] No FakeSocketConnection references

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify migrated test file structure
    Tool: Read
    Preconditions: Migration complete
    Steps:
      1. Read migrated file
      2. Verify Mock<ISocketConnection>
      3. Verify all setups present
      4. Verify no FakeSocketConnection references
      5. Verify test logic unchanged
    Expected Result: File properly migrated
    Failure Indicators: FakeSocketConnection references remain
    Evidence: .sisyphus/evidence/migration-file-4-review.txt
  ```

  **Evidence to Capture**:
  - [ ] Git diff
  - [ ] Code review notes

  **Commit**: YES - Atomic commit
  - Message: `refactor(tests): migrate [filename] to Moq`

- [ ] 15. Run full test suite, verify 100% pass

  **What to do**:
  - Repeat Task 6 for File 4

  **Must NOT do**:
  - Do not proceed if tests fail

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Test execution

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 8
  - **Blocks**: Task 17
  - **Blocked By**: Task 14

  **Acceptance Criteria**:

  - [ ] All tests pass

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify all tests pass after File 4 migration
    Tool: Bash (msbuild)
    Preconditions: File 4 migrated
    Steps:
      1. Run: msbuild IrcSharp.sln /t:Test /p:Configuration=Release
      2. Capture output
      3. Verify 100% pass rate
    Expected Result: All tests pass
    Failure Indicators: Any test failure
    Evidence: .sisyphus/evidence/migration-file-4-test-results.txt
  ```

  **Evidence to Capture**:
  - [ ] Test output
  - [ ] Pass/fail counts

  **Commit**: NO

- [ ] 16. Document migration results

  **What to do**:
  - Repeat Task 7 for File 4

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Documentation

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 8
  - **Blocks**: Task 17
  - **Blocked By**: Task 15

  **Acceptance Criteria**:

  - [ ] Migration log created
  - [ ] Tracker updated

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Document File 4 migration
    Tool: Write
    Preconditions: Test results from Task 15
    Steps:
      1. Create log entry
      2. Record results
      3. Update tracker
    Expected Result: Complete documentation
    Failure Indicators: Missing documentation
    Evidence: .sisyphus/evidence/migration-log-file-4.txt
  ```

  **Evidence to Capture**:
  - [ ] Migration log
  - [ ] Tracker updated

  **Commit**: NO

- [ ] 17. Migrate fifth test file to Moq

  **What to do**:
  - Repeat Task 5 for File 5

  **Must NOT do**:
  - Do not change test logic

  **Recommended Agent Profile**:
  > **Category**: `unspecified-high`
  > **Skills**: 
  >   - `git-master`
  > **Why**: Mock replacement

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 9
  - **Blocks**: Task 18
  - **Blocked By**: Task 16

  **References**:
  - Target file: `[fifth file from migration order]`

  **Acceptance Criteria**:

  - [ ] Mock<ISocketConnection> created
  - [ ] All methods/events mocked
  - [ ] Test logic unchanged
  - [ ] No FakeSocketConnection references

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify migrated test file structure
    Tool: Read
    Preconditions: Migration complete
    Steps:
      1. Read migrated file
      2. Verify Mock<ISocketConnection>
      3. Verify all setups present
      4. Verify no FakeSocketConnection references
      5. Verify test logic unchanged
    Expected Result: File properly migrated
    Failure Indicators: FakeSocketConnection references remain
    Evidence: .sisyphus/evidence/migration-file-5-review.txt
  ```

  **Evidence to Capture**:
  - [ ] Git diff
  - [ ] Code review notes

  **Commit**: YES - Atomic commit
  - Message: `refactor(tests): migrate [filename] to Moq`

- [ ] 18. Run full test suite, verify 100% pass

  **What to do**:
  - Repeat Task 6 for File 5

  **Must NOT do**:
  - Do not proceed if tests fail

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Test execution

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 10
  - **Blocks**: Task 20
  - **Blocked By**: Task 17

  **Acceptance Criteria**:

  - [ ] All tests pass

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify all tests pass after File 5 migration
    Tool: Bash (msbuild)
    Preconditions: File 5 migrated
    Steps:
      1. Run: msbuild IrcSharp.sln /t:Test /p:Configuration=Release
      2. Capture output
      3. Verify 100% pass rate
    Expected Result: All tests pass
    Failure Indicators: Any test failure
    Evidence: .sisyphus/evidence/migration-file-5-test-results.txt
  ```

  **Evidence to Capture**:
  - [ ] Test output
  - [ ] Pass/fail counts

  **Commit**: NO

- [ ] 19. Document migration results

  **What to do**:
  - Repeat Task 7 for File 5
  - Create final migration log entry
  - Update tracker to show all files complete

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Documentation

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 10
  - **Blocks**: Task 20
  - **Blocked By**: Task 18

  **Acceptance Criteria**:

  - [ ] Migration log created
  - [ ] Tracker updated
  - [ ] All files marked complete

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Document File 5 migration
    Tool: Write
    Preconditions: Test results from Task 18
    Steps:
      1. Create log entry
      2. Record results
      3. Update tracker to complete
    Expected Result: Complete documentation
    Failure Indicators: Missing documentation
    Evidence: .sisyphus/evidence/migration-log-file-5.txt
  ```

  **Evidence to Capture**:
  - [ ] Migration log
  - [ ] Tracker updated

  **Commit**: NO

- [ ] 20. Delete FakeSocketConnection.cs

  **What to do**:
  - Delete `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs`
  - Verify no references remain in any test file
  - If references exist: fix or rollback deletion

  **Must NOT do**:
  - Do not delete until all 5 files migrated and verified
  - Do not delete if references remain

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: 
  >   - `git-master`: For safe deletion
  > **Why**: File deletion with verification

  **Parallelization**:
  - **Can Run In Parallel**: NO - final cleanup step
  - **Parallel Group**: Wave FINAL
  - **Blocks**: Task 21
  - **Blocked By**: Task 19 (all migrations complete)

  **References**:
  - File to delete: `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs`

  **Acceptance Criteria**:

  - [ ] FakeSocketConnection.cs deleted
  - [ ] No references to FakeSocketConnection remain in test code
  - [ ] Git status shows file as deleted

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Delete FakeSocketConnection.cs
    Tool: Bash (rm)
    Preconditions: All 5 files migrated and verified
    Steps:
      1. Verify no references: grep -r "FakeSocketConnection" IrcSharp.Core.Tests.Unit/
      2. If no references: rm IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs
      3. If references exist: fix references or rollback deletion
      4. Verify git status shows deletion
    Expected Result: File deleted safely
    Failure Indicators: References remain, deletion fails
    Evidence: .sisyphus/evidence/fakesocket-deletion-log.txt
  ```

  **Evidence to Capture**:
  - [ ] Grep verification (no references)
  - [ ] Git status showing deletion
  - [ ] Deletion log

  **Commit**: YES - Final cleanup commit
  - Message: `refactor(tests): remove FakeSocketConnection after Moq migration`
  - Files: `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs (deleted)`
  - Pre-commit: Verify no references remain

- [ ] 21. Verify no references to FakeSocketConnection remain

  **What to do**:
  - Run grep to confirm no references remain
  - Document verification
  - If references found: fix them

  **Must NOT do**:
  - Do not skip verification

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Verification

  **Parallelization**:
  - **Can Run In Parallel**: NO - after deletion
  - **Parallel Group**: Wave FINAL
  - **Blocks**: Task 22
  - **Blocked By**: Task 20

  **References**:
  - Verification command: grep

  **Acceptance Criteria**:

  - [ ] Grep confirms no references
  - [ ] Verification documented

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Verify no FakeSocketConnection references remain
    Tool: Bash (grep)
    Preconditions: File deleted
    Steps:
      1. Run: grep -r "FakeSocketConnection" IrcSharp.Core.Tests.Unit/
      2. Verify no matches found
    Expected Result: Zero references found
    Failure Indicators: Any references found
    Evidence: .sisyphus/evidence/fakesocket-verification.txt
  ```

  **Evidence to Capture**:
  - [ ] Grep output showing no matches
  - [ ] Verification log

  **Commit**: NO

- [ ] 22. Run final test suite, capture results

  **What to do**:
  - Run full test suite after all migrations complete
  - Compare to baseline
  - Document final results
  - Verify 100% pass rate

  **Must NOT do**:
  - Do not skip final verification

  **Recommended Agent Profile**:
  > **Category**: `quick`
  > **Skills**: None
  > **Why**: Final verification

  **Parallelization**:
  - **Can Run In Parallel**: NO - final step
  - **Parallel Group**: Wave FINAL
  - **Blocks**: Task 23
  - **Blocked By**: Task 21

  **References**:
  - Baseline: `.sisyphus/evidence/baseline-test-results.txt`

  **Acceptance Criteria**:

  - [ ] Full test suite executed
  - [ ] All tests pass (100% success rate)
  - [ ] Results compared to baseline
  - [ ] Final results documented

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Final test suite verification
    Tool: Bash (msbuild)
    Preconditions: All migrations complete, FakeSocketConnection deleted
    Steps:
      1. Run: msbuild IrcSharp.sln /t:Test /p:Configuration=Release
      2. Capture all output
      3. Count passing tests
      4. Compare to baseline
      5. Verify 100% pass rate
    Expected Result: All tests pass, results match baseline
    Failure Indicators: Any test failure, results differ from baseline
    Evidence: .sisyphus/evidence/final-test-results.txt
  ```

  **Evidence to Capture**:
  - [ ] Final test output
  - [ ] Comparison to baseline
  - [ ] Pass/fail counts

  **Commit**: NO

- [ ] 23. Create migration summary commit

  **What to do**:
  - Create final commit summarizing the entire migration
  - Include all changes
  - Add migration summary to commit message
  - Push to remote (if applicable)

  **Must NOT do**:
  - Do not skip final commit

  **Recommended Agent Profile**:
  > **Category**: `git-master`
  > **Skills**: 
  >   - `git-master`: For commit
  > **Why**: Final commit with summary

  **Parallelization**:
  - **Can Run In Parallel**: NO - final step
  - **Parallel Group**: Wave FINAL
  - **Blocks**: None (last task)
  - **Blocked By**: Task 22

  **References**:
  - Git history
  - Migration logs

  **Acceptance Criteria**:

  - [ ] Final commit created
  - [ ] Migration summary included
  - [ ] All changes committed
  - [ ] Git history clean

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: Create migration summary commit
    Tool: Bash (git)
    Preconditions: All tasks complete
    Steps:
      1. git add -A
      2. git commit -m "Migrate all tests to Moq framework
      - Replaced FakeSocketConnection with Moq mocks in 5 test files
      - Removed FakeSocketConnection.cs
      - All tests pass (100% success rate)
      - Baseline: [X] tests, Final: [Y] tests"
      3. git log -1 --stat
    Expected Result: Clean final commit
    Failure Indicators: Commit fails, incomplete changes
    Evidence: .sisyphus/evidence/final-commit-log.txt
  ```

  **Evidence to Capture**:
  - [ ] Commit hash
  - [ ] Commit message
  - [ ] Git log output

  **Commit**: YES - Final summary commit

---

## Final Verification Wave (MANDATORY — after ALL implementation tasks)

> 4 review agents run in PARALLEL. ALL must APPROVE. Rejection → fix → re-run.

- [ ] F1. **Plan Compliance Audit** — `oracle`
  Read the plan end-to-end. For each "Must Have": verify implementation exists. For each "Must NOT Have": search codebase for forbidden patterns — reject with file:line if found. Check evidence files exist in `.sisyphus/evidence/`. Compare deliverables against plan.
  Output: `Must Have [N/N] | Must NOT Have [N/N] | Tasks [N/N] | VERDICT: APPROVE/REJECT`

- [ ] F2. **Code Quality Review** — `unspecified-high`
  Run `msbuild IrcSharp.sln /t:Build` + linter if available. Review all changed files for: `var` overuse, unused imports, TODOs, FIXMEs. Check for Moq anti-patterns: over-mocking, brittle mocks, unnecessary setups.
  Output: `Build [PASS/FAIL] | Files [N clean/N issues] | Moq patterns [CLEAN/N issues] | VERDICT`

- [ ] F3. **Real Manual QA** — `unspecified-high`
  Start from clean state. Execute EVERY test suite verification from EVERY task — follow exact steps, capture evidence. Test migration integrity: verify all 5 files migrated, FakeSocketConnection deleted, no references remain. Test edge cases: verify behavior equivalence.
  Output: `Scenarios [N/N pass] | Migration [5/5 files] | Deletion [CLEAN/N issues] | VERDICT`

- [ ] F4. **Scope Fidelity Check** — `deep`
  For each task: read "What to do", read actual diff (git log/diff). Verify 1:1 — everything in spec was migrated (no missing), nothing beyond spec was built (no creep). Check "Must NOT do" compliance. Flag any refactoring beyond mock replacement.
  Output: `Tasks [N/N compliant] | Creep [CLEAN/N issues] | Unaccounted [CLEAN/N files] | VERDICT`

---

## Commit Strategy

**Per File Migration**:
- Message: `refactor(tests): migrate [filename] to Moq`
- Files: `[migrated test file]`
- Pre-commit: Run tests for this file only

**Final Cleanup**:
- Message: `refactor(tests): remove FakeSocketConnection after Moq migration`
- Files: `IrcSharp.Core.Tests.Unit/FakeSocketConnection.cs (deleted)`
- Pre-commit: Verify no references remain

**Summary Commit**:
- Message: `Migrate all tests to Moq framework - see individual commits for details`
- Files: All changes from final commit

---

## Success Criteria

### Verification Commands
```bash
# Baseline
msbuild IrcSharp.sln /t:Test /p:Configuration=Release

# After each migration
msbuild IrcSharp.sln /t:Test /p:Configuration=Release

# Verify no FakeSocketConnection references
grep -r "FakeSocketConnection" IrcSharp.Core.Tests.Unit/

# Final verification
git status
git log --oneline | head -10
```

### Final Checklist
- [ ] All "Must Have" present (5 migrated files, 100% pass rate, FakeSocketConnection deleted)
- [ ] All "Must NOT Have" absent (no refactoring, no new tests, no production changes)
- [ ] All tests pass
- [ ] Evidence files captured for all tasks
- [ ] Git history shows atomic commits per file

---

## Migration Order Strategy

**Recommended Order**: Simplest to most complex

1. **File 1**: `[simplest file]` - Build confidence
2. **File 2**: `[second simplest]` - Apply lessons learned
3. **File 3**: `[third file]` - Continue pattern
4. **File 4**: `[fourth file]` - Continue pattern
5. **File 5**: `[most complex]` - Final challenge

**Determination Method**:
- Count lines of code per test file
- Count FakeSocketConnection usages per file
- Count helper methods per file
- Rank from lowest to highest complexity

---

## Edge Case Handling

### Edge Case 1: Stateful Behavior
**Scenario**: FakeSocketConnection maintains internal state (Connected property, message queue)
**Solution**: Use Moq's `SetupProperty()` for properties, Callbacks for state changes

### Edge Case 2: Event Raising
**Scenario**: FakeSocketConnection raises 3 events
**Solution**: Use Moq's `Raise()` method in test setup, or use Callbacks to simulate event raising

### Edge Case 3: Exception Throwing
**Scenario**: FakeSocketConnection may throw exceptions under certain conditions
**Solution**: Use Moq's `Throws()` method to replicate exception behavior

### Edge Case 4: Async Timing
**Scenario**: FakeSocketConnection has async methods with specific timing
**Solution**: Use Moq's `ReturnsAsync()` for async methods

### Edge Case 5: Hybrid Approach
**Scenario**: Some behaviors cannot be replicated with Moq
**Solution**: Keep FakeSocketConnection for that specific test only, document limitation

---

## Appendix: Moq Setup Patterns

### Basic Setup
```csharp
var mockSocket = new Mock<ISocketConnection>();
mockSocket.Setup(x => x.ConnectAsync(It.IsAny<string>(), It.IsAny<int>()))
          .Returns(Task.CompletedTask);
```

### Property Setup
```csharp
mockSocket.SetupProperty<bool>(x => x.Connected);
```

### Event Setup
```csharp
// Raise event in test
mockSocket.Raise(x => x.OnMessageReceived += null, 
                 new MessageEventArgs { Message = "test" });
```

### Callback Setup
```csharp
var sentMessages = new List<string>();
mockSocket.Setup(x => x.SendMessageAsync(It.IsAny<ISendableMessage>()))
          .Callback((ISendableMessage m) => 
              sentMessages.Add(m.ToMessage()));
```

### Exception Setup
```csharp
mockSocket.Setup(x => x.ConnectAsync(It.IsAny<string>(), It.IsAny<int>()))
          .Throws(new IOException("Simulated error"));
```

---

## Risk Mitigation

### Risk 1: Test Failures After Migration
**Mitigation**: 
- Run full test suite after each migration
- If failures occur: debug Moq setup, revert migration, investigate FakeSocketConnection behavior
- Keep FakeSocketConnection until ALL files migrated

### Risk 2: Behavior Inequivalence
**Mitigation**:
- Document all FakeSocketConnection behaviors before migration
- Test edge cases explicitly
- Use hybrid approach if Moq cannot replicate behavior

### Risk 3: Scope Creep
**Mitigation**:
- Strictly follow "Must NOT Have" guardrails
- Only replace mock, do not refactor test logic
- No new tests, no production code changes

### Risk 4: Incomplete Migration
**Mitigation**:
- Track migration progress with migration tracker
- Verify no FakeSocketConnection references remain before deletion
- Run grep verification before and after deletion

---

## Notes

- **Estimated Timeline**: 4-6 hours total (assuming ~1 hour per file including verification)
- **Rollback Strategy**: Each file has atomic commit - can revert individual file if needed
- **Communication**: Document all issues in migration logs for future reference
- **Knowledge Transfer**: Moq patterns from this migration can be reused for future mock migrations