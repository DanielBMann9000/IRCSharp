# Initialize Hierarchical AGENTS.md Files

## TL;DR

> **Quick Summary**: Regenerate hierarchical AGENTS.md files for IRCSharp project with updated structure, conventions, and anti-patterns.
> 
> **Deliverables**: 
> - Updated root `AGENTS.md` (74 lines → ~80 lines)
> - Updated `IrcSharp.Core/AGENTS.md`
> - New `IrcSharp.Core/Connectivity/AGENTS.md` (~40 lines)
> - Updated `IrcSharp.Core/Messages/AGENTS.md`
> - Updated `IrcSharp.Core/Messages/Propagation/AGENTS.md`
> 
> **Estimated Effort**: Short (20-30 minutes)
> **Parallel Execution**: YES - 2 waves
> **Critical Path**: Wave 1 (root + core) → Wave 2 (subdirectories)

---

## Context

### Original Request
User triggered `/init-deep` command to generate hierarchical AGENTS.md files with complexity-scoring for subdirectories.

### Interview Summary
**Key Discussions**:
- Project is IRCSharp, a C#/.NET Framework 4.5 IRC library
- Currently in heavy refactoring cycle
- Existing AGENTS.md files exist at root and some subdirectories
- Need to update existing files and create missing ones

**Research Findings**:
- Directory structure: 7 main directories (root, IrcSharp.Core, Connectivity, Messages, Propagation, Tests.Unit, Tests.Integration)
- Total code: ~4500 lines across 60+ C# files
- Large files: MessagePropagator (545 lines), test files (500+ lines each)
- Anti-patterns identified: `async void` in Reconnect(), busy waiting, large class

### Metis Review
**Identified Gaps** (addressed):
- Scope clarity: Only update/create AGENTS.md in core library directories
- Guardrails: Do NOT modify source code files, only .md documentation
- Test strategy: No automated tests needed (documentation update)

---

## Work Objectives

### Core Objective
Regenerate hierarchical AGENTS.md files for IRCSharp with accurate structure, conventions, and anti-patterns.

### Concrete Deliverables
- `AGENTS.md` (root) - Updated with current structure
- `IrcSharp.Core/AGENTS.md` - Updated core library overview
- `IrcSharp.Core/Connectivity/AGENTS.md` - NEW file for connection management
- `IrcSharp.Core/Messages/AGENTS.md` - Updated message types overview
- `IrcSharp.Core/Messages/Propagation/AGENTS.md` - Updated propagation system

### Definition of Done
- [ ] All AGENTS.md files exist and are telegraphic (30-80 lines)
- [ ] No redundant content between parent/child files
- [ ] All anti-patterns documented with file:line references
- [ ] Commands section includes build/test commands

### Must Have
- Accurate directory structure and file paths
- Specific anti-patterns with line numbers
- Telegraphic style (no generic advice)
- Parent files cover overview, children cover specifics

### Must NOT Have (Guardrails)
- NO modifications to .cs source files
- NO generic advice that applies to all projects
- NO redundant content between hierarchy levels
- NO files >100 lines (trim if needed)

---

## Verification Strategy (MANDATORY)

> **ZERO HUMAN INTERVENTION** — ALL verification is agent-executed. No exceptions.

### Test Decision
- **Infrastructure exists**: NO (documentation update, no tests needed)
- **Automated tests**: None
- **Framework**: N/A

### QA Policy
Every task includes agent-executed verification:
- **File existence**: Bash `test -f` command
- **Line count**: Bash `wc -l` command
- **Content validation**: Bash `grep` for specific sections
- **Evidence saved to**: `.sisyphus/evidence/task-{N}-{scenario-slug}.txt`

---

## Execution Strategy

### Parallel Execution Waves

```
Wave 1 (Start Immediately — foundation):
├── Task 1: Update root AGENTS.md [quick]
├── Task 2: Update IrcSharp.Core/AGENTS.md [quick]
└── Task 3: Update IrcSharp.Core/Messages/AGENTS.md [quick]

Wave 2 (After Wave 1 — subdirectories):
├── Task 4: Create IrcSharp.Core/Connectivity/AGENTS.md [quick]
├── Task 5: Update IrcSharp.Core/Messages/Propagation/AGENTS.md [quick]
└── Task 6: Final validation (all files exist, correct line counts) [quick]
```

**Critical Path**: Task 1 → Task 4
**Parallel Speedup**: ~40% faster than sequential
**Max Concurrent**: 3 (Wave 1)

---

## TODOs

- [ ] 1. Update root AGENTS.md

  **What to do**:
  - Read existing `AGENTS.md` (74 lines)
  - Update timestamp to 2026-03-04
  - Refine STRUCTURE section with full hierarchy
  - Update ANTI-PATTERNS with specific line references
  - Ensure telegraphic style, no redundancy

  **Must NOT do**:
  - Modify any .cs source files
  - Add generic advice applicable to all projects
  - Exceed 100 lines

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `writing`
    - Reason: Documentation generation and refinement
  - **Skills**: `[]`
    - No additional skills needed for markdown documentation

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (with Tasks 2, 3)
  - **Blocks**: Task 4 (Connectivity AGENTS.md needs parent context)
  - **Blocked By**: None (can start immediately)

  **References** (CRITICAL - Be Exhaustive):

  **Pattern References** (existing code to follow):
  - `AGENTS.md:1-74` - Current root structure to update
  - `IrcSharp.Core/AGENTS.md` - Parent-child relationship pattern
  - `IrcSharp.Core/Messages/AGENTS.md` - Subdirectory AGENTS.md pattern

  **WHY Each Reference Matters**:
  - Root AGENTS.md: Maintain consistency with existing format
  - Subdirectory files: Ensure proper parent-child hierarchy

  **Acceptance Criteria**:
  - [ ] File updated with timestamp 2026-03-04
  - [ ] STRUCTURE section shows full hierarchy (4 levels)
  - [ ] ANTI-PATTERNS section lists all 5 identified anti-patterns
  - [ ] Line count: 70-90 lines
  - [ ] Bash `grep -c "## OVERVIEW" AGENTS.md` → 1
  - [ ] Bash `grep -c "## ANTI-PATTERNS" AGENTS.md` → 1

  **QA Scenarios (MANDATORY)**:

  ```
  Scenario: File exists and has correct structure
    Tool: Bash
    Preconditions: Task 1 completed
    Steps:
      1. Bash: `test -f AGENTS.md && echo "EXISTS" || echo "MISSING"`
      2. Bash: `wc -l AGENTS.md | awk '{print $1}'`
      3. Bash: `grep -c "## OVERVIEW" AGENTS.md`
      4. Bash: `grep -c "## ANTI-PATTERNS" AGENTS.md`
    Expected Result: EXISTS, 70-90 lines, 1, 1
    Failure Indicators: MISSING, line count outside range, missing sections
    Evidence: .sisyphus/evidence/task-1-structure.txt

  Scenario: Content validation - anti-patterns present
    Tool: Bash
    Preconditions: Task 1 completed
    Steps:
      1. Bash: `grep -c "async void" AGENTS.md`
      2. Bash: `grep -c "busy waiting" AGENTS.md`
      3. Bash: `grep -c "MessagePropagator" AGENTS.md`
    Expected Result: ≥1, ≥1, ≥1
    Failure Indicators: 0 matches for any anti-pattern
    Evidence: .sisyphus/evidence/task-1-anti-patterns.txt
  ```

  **Evidence to Capture**:
  - [ ] task-1-structure.txt: File existence and line count
  - [ ] task-1-anti-patterns.txt: Anti-pattern validation

  **Commit**: YES
  - Message: `docs: update root AGENTS.md with current structure`
  - Files: `AGENTS.md`

---

- [ ] 2. Update IrcSharp.Core/AGENTS.md

  **What to do**:
  - Read existing `IrcSharp.Core/AGENTS.md`
  - Update timestamp
  - Refine CODE MAP with accurate symbol counts
  - Update ANTI-PATTERNS with specific references
  - Ensure no redundancy with root AGENTS.md

  **Must NOT do**:
  - Repeat content from root AGENTS.md
  - Modify .cs source files
  - Exceed 80 lines

  **Recommended Agent Profile**:
  - **Category**: `writing`
    - Reason: Documentation update for core library
  - **Skills**: `[]`

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (with Tasks 1, 3)
  - **Blocks**: None
  - **Blocked By**: None

  **References**:
  - `IrcSharp.Core/AGENTS.md` - Current file to update
  - `AGENTS.md` - Parent file for context (avoid redundancy)
  - `IrcSharp.Core/Connectivity/IrcConnection.cs:1-187` - Verify anti-pattern line numbers

  **Acceptance Criteria**:
  - [ ] Timestamp updated to 2026-03-04
  - [ ] CODE MAP lists 5+ key symbols
  - [ ] ANTI-PATTERNS includes async void, busy waiting, large class
  - [ ] Line count: 60-80 lines
  - [ ] Bash `wc -l IrcSharp.Core/AGENTS.md` → 60-80

  **QA Scenarios**:

  ```
  Scenario: File exists and within line limits
    Tool: Bash
    Preconditions: Task 2 completed
    Steps:
      1. Bash: `test -f IrcSharp.Core/AGENTS.md && echo "EXISTS"`
      2. Bash: `wc -l IrcSharp.Core/AGENTS.md | awk '{print $1}'`
    Expected Result: EXISTS, 60-80
    Failure Indicators: MISSING, line count outside range
    Evidence: .sisyphus/evidence/task-2-validation.txt

  Scenario: Anti-patterns documented
    Tool: Bash
    Preconditions: Task 2 completed
    Steps:
      1. Bash: `grep -i "async void" IrcSharp.Core/AGENTS.md`
      2. Bash: `grep -i "busy waiting" IrcSharp.Core/AGENTS.md`
    Expected Result: Both patterns found
    Failure Indicators: Missing anti-pattern documentation
    Evidence: .sisyphus/evidence/task-2-anti-patterns.txt
  ```

  **Evidence to Capture**:
  - [ ] task-2-validation.txt: File existence and line count
  - [ ] task-2-anti-patterns.txt: Anti-pattern validation

  **Commit**: YES (groups with Task 1)
  - Message: `docs: update IrcSharp.Core AGENTS.md`
  - Files: `IrcSharp.Core/AGENTS.md`

---

- [ ] 3. Update IrcSharp.Core/Messages/AGENTS.md

  **What to do**:
  - Read existing `IrcSharp.Core/Messages/AGENTS.md`
  - Update timestamp
  - Verify 42 message types listed accurately
  - Update ANTI-PATTERNS (TokenizeArguments, large propagator)
  - Ensure consistency with Propagation AGENTS.md

  **Must NOT do**:
  - Repeat parent (IrcSharp.Core) content
  - Exceed 80 lines
  - Modify message .cs files

  **Recommended Agent Profile**:
  - **Category**: `writing`
  - **Skills**: `[]`

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (with Tasks 1, 2)
  - **Blocks**: Task 5 (Propagation update)
  - **Blocked By**: None

  **References**:
  - `IrcSharp.Core/Messages/AGENTS.md` - Current file
  - `IrcSharp.Core/Messages/Propagation/AGENTS.md` - Child file consistency
  - `IrcSharp.Core/Messages/Propagation/MessagePropagator.cs:534` - TokenizeArguments line

  **Acceptance Criteria**:
  - [ ] Timestamp 2026-03-04
  - [ ] All 6 message categories listed
  - [ ] ANTI-PATTERNS: TokenizeArguments (line 534), large propagator
  - [ ] Line count: 50-80 lines
  - [ ] Bash `wc -l IrcSharp.Core/Messages/AGENTS.md` → 50-80

  **QA Scenarios**:

  ```
  Scenario: File validation
    Tool: Bash
    Preconditions: Task 3 completed
    Steps:
      1. Bash: `test -f IrcSharp.Core/Messages/AGENTS.md && echo "EXISTS"`
      2. Bash: `wc -l IrcSharp.Core/Messages/AGENTS.md | awk '{print $1}'`
      3. Bash: `grep -c "Connection registration" IrcSharp.Core/Messages/AGENTS.md`
    Expected Result: EXISTS, 50-80, ≥1
    Failure Indicators: MISSING, wrong line count, missing categories
    Evidence: .sisyphus/evidence/task-3-validation.txt
  ```

  **Evidence to Capture**:
  - [ ] task-3-validation.txt

  **Commit**: YES (groups with Task 1)
  - Message: `docs: update Messages AGENTS.md`
  - Files: `IrcSharp.Core/Messages/AGENTS.md`

---

- [ ] 4. Create IrcSharp.Core/Connectivity/AGENTS.md

  **What to do**:
  - Create NEW file (does not exist)
  - Include OVERVIEW (1 line): Socket I/O, connection lifecycle
  - STRUCTURE: List 5 files (IrcConnection, SocketConnection, ISocketConnection, etc.)
  - WHERE TO LOOK table: Connection tasks
  - CONVENTIONS: Async/await, event-driven patterns
  - ANTI-PATTERNS: async void Reconnect() (line 135), busy waiting (lines 98-101)
  - Target: 40-60 lines

  **Must NOT do**:
  - Repeat parent (IrcSharp.Core) content
  - Exceed 80 lines
  - Modify .cs files

  **Recommended Agent Profile**:
  - **Category**: `writing`
  - **Skills**: `[]`

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Sequential (depends on Wave 1)
  - **Blocks**: None
  - **Blocked By**: Task 1 (root context)

  **References**:
  - `IrcSharp.Core/AGENTS.md` - Parent for context
  - `IrcSharp.Core/Connectivity/IrcConnection.cs:135` - async void Reconnect
  - `IrcSharp.Core/Connectivity/IrcConnection.cs:98-101` - busy waiting
  - `IrcSharp.Core/Connectivity/SocketConnection.cs:1-142` - Socket wrapper

  **Acceptance Criteria**:
  - [ ] File created (new)
  - [ ] OVERVIEW section present
  - [ ] ANTI-PATTERNS: async void line 135, busy waiting lines 98-101
  - [ ] Line count: 40-60 lines
  - [ ] Bash `wc -l IrcSharp.Core/Connectivity/AGENTS.md` → 40-60

  **QA Scenarios**:

  ```
  Scenario: File created with correct content
    Tool: Bash
    Preconditions: Task 4 completed
    Steps:
      1. Bash: `test -f IrcSharp.Core/Connectivity/AGENTS.md && echo "EXISTS" || echo "MISSING"`
      2. Bash: `wc -l IrcSharp.Core/Connectivity/AGENTS.md | awk '{print $1}'`
      3. Bash: `grep -c "async void" IrcSharp.Core/Connectivity/AGENTS.md`
      4. Bash: `grep -c "Reconnect" IrcSharp.Core/Connectivity/AGENTS.md`
    Expected Result: EXISTS, 40-60, ≥1, ≥1
    Failure Indicators: MISSING, wrong line count, missing anti-patterns
    Evidence: .sisyphus/evidence/task-4-creation.txt
  ```

  **Evidence to Capture**:
  - [ ] task-4-creation.txt: File creation and validation

  **Commit**: YES
  - Message: `docs: add Connectivity AGENTS.md`
  - Files: `IrcSharp.Core/Connectivity/AGENTS.md`

---

- [ ] 5. Update IrcSharp.Core/Messages/Propagation/AGENTS.md

  **What to do**:
  - Read existing `IrcSharp.Core/Messages/Propagation/AGENTS.md`
  - Update timestamp
  - Verify MessagePropagator (545 lines) documented
  - Update ANTI-PATTERNS: large class, manual dictionary updates
  - Ensure no redundancy with parent (Messages) AGENTS.md

  **Must NOT do**:
  - Repeat parent content
  - Exceed 80 lines
  - Modify .cs files

  **Recommended Agent Profile**:
  - **Category**: `writing`
  - **Skills**: `[]`

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 2 (with Task 4, after Wave 1)
  - **Blocks**: None
  - **Blocked By**: Task 3 (Messages AGENTS.md)

  **References**:
  - `IrcSharp.Core/Messages/Propagation/AGENTS.md` - Current file
  - `IrcSharp.Core/Messages/Propagation/MessagePropagator.cs:1-545` - Verify content
  - `IrcSharp.Core/Messages/AGENTS.md` - Parent for consistency

  **Acceptance Criteria**:
  - [ ] Timestamp 2026-03-04
  - [ ] MessagePropagator (545 lines) documented
  - [ ] ANTI-PATTERNS: large class, manual updates
  - [ ] Line count: 40-70 lines
  - [ ] Bash `wc -l IrcSharp.Core/Messages/Propagation/AGENTS.md` → 40-70

  **QA Scenarios**:

  ```
  Scenario: File updated correctly
    Tool: Bash
    Preconditions: Task 5 completed
    Steps:
      1. Bash: `test -f IrcSharp.Core/Messages/Propagation/AGENTS.md && echo "EXISTS"`
      2. Bash: `wc -l IrcSharp.Core/Messages/Propagation/AGENTS.md | awk '{print $1}'`
      3. Bash: `grep -c "MessagePropagator" IrcSharp.Core/Messages/Propagation/AGENTS.md`
    Expected Result: EXISTS, 40-70, ≥2
    Failure Indicators: MISSING, wrong line count, missing content
    Evidence: .sisyphus/evidence/task-5-validation.txt
  ```

  **Evidence to Capture**:
  - [ ] task-5-validation.txt

  **Commit**: YES (groups with Task 4)
  - Message: `docs: update Propagation AGENTS.md`
  - Files: `IrcSharp.Core/Messages/Propagation/AGENTS.md`

---

- [ ] 6. Final validation - all AGENTS.md files

  **What to do**:
  - Verify all 5 AGENTS.md files exist
  - Check line counts within limits (root: 70-90, others: 40-80)
  - Validate no critical content missing (OVERVIEW, ANTI-PATTERNS)
  - Ensure hierarchy consistency (no redundancy)

  **Must NOT do**:
  - Modify files (only read and report)
  - Skip any validation

  **Recommended Agent Profile**:
  - **Category**: `quick`
  - **Skills**: `[]`

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Sequential (final wave)
  - **Blocks**: None
  - **Blocked By**: All Wave 2 tasks

  **References**:
  - All 5 AGENTS.md files for validation

  **Acceptance Criteria**:
  - [ ] All 5 files exist
  - [ ] All line counts within limits
  - [ ] All have OVERVIEW and ANTI-PATTERNS sections
  - [ ] Bash validation script passes
  - [ ] Evidence file created with validation report

  **QA Scenarios**:

  ```
  Scenario: All files exist and valid
    Tool: Bash
    Preconditions: All previous tasks completed
    Steps:
      1. Bash: `for f in AGENTS.md IrcSharp.Core/AGENTS.md IrcSharp.Core/Connectivity/AGENTS.md IrcSharp.Core/Messages/AGENTS.md IrcSharp.Core/Messages/Propagation/AGENTS.md; do test -f $f && echo "$f: EXISTS" || echo "$f: MISSING"; done`
      2. Bash: `for f in AGENTS.md IrcSharp.Core/AGENTS.md IrcSharp.Core/Connectivity/AGENTS.md IrcSharp.Core/Messages/AGENTS.md IrcSharp.Core/Messages/Propagation/AGENTS.md; do echo "$f: $(wc -l < $f) lines"; done`
    Expected Result: All EXISTS, line counts within limits
    Failure Indicators: Any MISSING, line counts outside limits
    Evidence: .sisyphus/evidence/task-6-final-validation.txt
  ```

  **Evidence to Capture**:
  - [ ] task-6-final-validation.txt: Complete validation report

  **Commit**: YES
  - Message: `docs: validate all AGENTS.md files`
  - Files: (none - validation only)

---

## Final Verification Wave (MANDATORY)

- [ ] F1. **Plan Compliance Audit** — `oracle`
  Read the plan end-to-end. Verify all 5 AGENTS.md files will be created/updated. Check that no .cs files are modified. Ensure all QA scenarios are agent-executable.
  Output: `Must Have [5/5] | Must NOT Have [0 violations] | VERDICT: APPROVE/REJECT`

- [ ] F2. **Code Quality Review** — `unspecified-high`
  Verify all AGENTS.md files are telegraphic, no redundancy, correct format.
  Output: `Build [N/A] | Files [5 clean] | VERDICT`

---

## Commit Strategy

- **Wave 1** (Tasks 1-3): `docs: update core AGENTS.md files`
  - Files: `AGENTS.md`, `IrcSharp.Core/AGENTS.md`, `IrcSharp.Core/Messages/AGENTS.md`

- **Wave 2** (Tasks 4-5): `docs: add/update subdirectory AGENTS.md`
  - Files: `IrcSharp.Core/Connectivity/AGENTS.md`, `IrcSharp.Core/Messages/Propagation/AGENTS.md`

- **Final** (Task 6): `docs: validate AGENTS.md hierarchy`
  - Files: (validation only, no changes)

---

## Success Criteria

### Verification Commands
```bash
# Check all files exist
test -f AGENTS.md && test -f IrcSharp.Core/AGENTS.md && test -f IrcSharp.Core/Connectivity/AGENTS.md && test -f IrcSharp.Core/Messages/AGENTS.md && test -f IrcSharp.Core/Messages/Propagation/AGENTS.md && echo "ALL EXISTS" || echo "MISSING"

# Check line counts
wc -l AGENTS.md IrcSharp.Core/AGENTS.md IrcSharp.Core/Connectivity/AGENTS.md IrcSharp.Core/Messages/AGENTS.md IrcSharp.Core/Messages/Propagation/AGENTS.md
```

### Final Checklist
- [ ] All 5 AGENTS.md files exist
- [ ] Root: 70-90 lines, others: 40-80 lines
- [ ] All have OVERVIEW, ANTI-PATTERNS sections
- [ ] No .cs files modified
- [ ] No redundant content between hierarchy levels