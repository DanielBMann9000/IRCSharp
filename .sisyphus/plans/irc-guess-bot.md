# IRCGuessBot - Number Guessing Game Bot

## TL;DR

> **Quick Summary**: Build a console IRC bot using IRCSharp.Core that plays a "What number am I thinking of?" guessing game. Users start games with `!play <max>` and guess with `!guess <number>`. Tracks wins in-memory per user.

> **Deliverables**:
> - IRCGuessBot console application (.NET 10.0)
> - Command parsing for `!play` and `!guess`
> - In-memory game state and win tracking
> - Configuration via appsettings.json

> **Estimated Effort**: Medium (~10-15 tasks)
> **Parallel Execution**: YES - 4 waves
> **Critical Path**: Task 1 → Task 4 → Task 7 → Task 10 → Task 13 → Task 15

---

## Context

### Original Request
Create a bot project that uses the IRCSharp.Core library to connect to an IRC server, join a channel, and play a "What number am I thinking of?" guessing game with users. The game can be started by a user typing `!play <number>` in the IRC channel. Players can guess by typing `!guess <number>`. The bot should track in memory how many games have been won by which user.

### Interview Summary
**Key Discussions**:
- Configuration: App.config/appsettings.json for server, port, channel, nick
- Single channel at a time (no multi-channel support)
- Win announcement format: "User won in N guesses! Total wins: M"
- Invalid commands: Silently ignored (no feedback)
- Test strategy: Tests after implementation (not TDD)

**Research Findings**:
- IRCSharp.Core uses event-driven pattern with MessagePropagator
- Messages sent via `SendMessageAsync(new PrivMsgMessage(dest, msg))`
- Tests use `Mock<ISocketConnection>` with `ManualResetEvent` for async
- Anti-patterns to avoid: `async void` (except event handlers), busy-waiting

### Metis Review
**Identified Gaps** (addressed):
- Added explicit guardrail: No multi-channel support to keep scope tight
- Clarified error handling: Silent ignore for invalid commands
- Added win announcement format detail: Include guess count + total wins
- Specified test approach: Tests after implementation (user preference)

---

## Work Objectives

### Core Objective
Build a functional IRC bot that hosts number guessing games with win tracking, following IRCSharp.Core conventions and patterns.

### Concrete Deliverables
- IRCGuessBot.csproj - .NET 10.0 console application
- Program.cs - Main entry point with connection lifecycle
- Game/NumberGame.cs - Core game logic (random number generation, guess validation)
- Game/GameSession.cs - Per-channel game state management
- Commands/CommandHandler.cs - Command parsing and dispatch
- Commands/GameCommands.cs - !play and !guess handlers
- State/WinTracker.cs - In-memory user win tracking
- Config/BotConfig.cs - Configuration model
- appsettings.json - Bot configuration file
- Unit tests for game logic and command handling

### Definition of Done
- [x] Bot connects to IRC server specified in appsettings.json
- [x] Bot joins channel specified in appsettings.json
- [x] `!play <number>` starts a game with random number 1 to <number>
- [x] `!guess <number>` checks guesses and provides feedback
- [x] Win announcement includes guess count and user's total wins
- [x] Bot handles all IRC events gracefully (PING/PONG, disconnects)
- [x] All unit tests pass

### Must Have
- Connect to IRC server and join channel
- Parse and execute `!play` and `!guess` commands
- Track game state per channel (one game at a time)
- Track wins in-memory per user
- Announce winner with guess count and total wins
- Configuration via appsettings.json

### Must NOT Have (Guardrails)
- No multi-channel support (one channel at a time)
- No database persistence (in-memory only)
- No hints during game
- No timeouts for guessing
- No error feedback for invalid commands (silent ignore)
- No async void methods (except event handlers)

---

## Verification Strategy

### Test Decision
- **Infrastructure exists**: YES (xUnit in IrcSharp.Core.Tests.Unit)
- **Automated tests**: YES (tests after implementation)
- **Framework**: xUnit (match existing test project)
- **If TDD**: N/A - tests added after implementation

### QA Policy
Every task MUST include agent-executed QA scenarios (see TODO template below).
Evidence saved to `.sisyphus/evidence/task-{N}-{scenario-slug}.{ext}`.

- **Frontend/UI**: N/A (console application)
- **CLI/TUI**: Use interactive_bash (tmux) — Run bot command, send keystrokes, validate output
- **API/Backend**: Use Bash (curl) — N/A for this project
- **Library/Module**: Use Bash (bun/node REPL) — N/A for this project
- **Unit Tests**: Use xunit test runner — Run test commands, assert pass/fail

---

## Execution Strategy

### Parallel Execution Waves

```
Wave 1 (Start Immediately — scaffolding + config):
├── Task 1: Create project structure and solution [quick]
├── Task 2: Add project reference to IRCSharp.Core [quick]
├── Task 3: Create appsettings.json with default config [quick]
├── Task 4: Implement BotConfig model class [quick]
└── Task 5: Add unit test project setup [quick]

Wave 2 (After Wave 1 — core game logic, MAX PARALLEL):
├── Task 6: Implement NumberGame class [deep]
├── Task 7: Implement GameSession class [deep]
├── Task 8: Implement WinTracker class [quick]
├── Task 9: Write unit tests for NumberGame [quick]
├── Task 10: Write unit tests for GameSession [quick]
└── Task 11: Write unit tests for WinTracker [quick]

Wave 3 (After Wave 2 — IRC integration):
├── Task 12: Implement CommandHandler [deep]
├── Task 13: Implement GameCommands handlers [deep]
├── Task 14: Implement Program.cs entry point [deep]
├── Task 15: Write integration tests for command handling [unspecified-high]
└── Task 16: Add error handling for IRC events [quick]

Wave 4 (After Wave 3 — verification):
├── Task 17: Run all unit tests and verify pass [quick]
├── Task 18: Manual QA - test bot connection and commands [unspecified-high]
├── Task 19: Documentation - README with usage instructions [writing]
└── Task 20: Git cleanup and tagging [git]

Critical Path: Task 1 → Task 4 → Task 7 → Task 13 → Task 15 → Task 17 → Task 18
Parallel Speedup: ~65% faster than sequential
Max Concurrent: 6 (Wave 2)
```

### Dependency Matrix

- **1-5**: — (foundation tasks, no dependencies)
- **6-8**: 4 (configuration must exist before game logic)
- **9-11**: 6-8 (tests depend on implementation)
- **12-14**: 6-8 (command handlers depend on game classes)
- **15**: 12-14 (integration tests depend on implementation)
- **16**: 14 (error handling in program entry point)
- **17**: 6-11, 15 (all tests must pass)
- **18**: 14, 16 (manual QA after implementation complete)
- **19**: 1-18 (documentation last)
- **20**: 19 (git cleanup after everything complete)

### Agent Dispatch Summary

- **Wave 1**: 5 tasks — All `quick` (scaffolding, config)
- **Wave 2**: 6 tasks — T6-8 → `deep`, T9-11 → `quick`
- **Wave 3**: 5 tasks — T12-14 → `deep`, T15 → `unspecified-high`, T16 → `quick`
- **Wave 4**: 4 tasks — T17 → `quick`, T18 → `unspecified-high`, T19 → `writing`, T20 → `git`

---

## TODOs

> Implementation + Test = ONE Task. Never separate.
> EVERY task MUST have: Recommended Agent Profile + Parallelization info + QA Scenarios.

- [x] 1. **Create project structure and solution**

  **What to do**:
  - Create IRCGuessBot directory
  - Create IRCGuessBot.csproj (.NET 10.0 console app)
  - Create directory structure: Game/, Commands/, State/, Config/, Tests/
  - Add basic Program.cs with placeholder Main()
  - Create solution file (optional, or use existing IrcSharp.sln)

  **Must NOT do**:
  - Don't add any business logic yet
  - Don't create test files yet (Wave 1 is scaffolding only)

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Simple file creation and directory structure
  - **Skills**: [`git-master`]
    - `git-master`: Ensure proper git initialization if needed
  - **Skills Evaluated but Omitted**:
    - `visual-engineering`: Not applicable (console app)

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (with Tasks 2-5)
  - **Blocks**: None (foundation task)
  - **Blocked By**: None (can start immediately)

  **References** (CRITICAL - Be Exhaustive):

  **Pattern References** (existing code to follow):
  - `IrcSharp.Core/IrcSharp.Core.csproj` — Project structure pattern
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj` — Test project structure pattern

  **WHY Each Reference Matters**:
  - Match existing .NET 10.0 target framework
  - Follow SDK-style project format

  **Acceptance Criteria**:

  **QA Scenarios (MANDATORY — task is INCOMPLETE without these)**:

  ```
  Scenario: Project structure verification
    Tool: Bash (ls, cat)
    Preconditions: Clean working directory
    Steps:
      1. Run: ls IRCGuessBot/
      2. Verify directories exist: Game/, Commands/, State/, Config/, Tests/
      3. Verify IRCGuessBot.csproj exists
    Expected Result: All directories and files present
    Failure Indicators: Missing directories or files
    Evidence: .sisyphus/evidence/task-1-structure-verification.txt

  Scenario: Project file validation
    Tool: Bash (cat)
    Preconditions: IRCGuessBot.csproj created
    Steps:
      1. Run: cat IRCGuessBot/IRCGuessBot.csproj
      2. Verify TargetFramework is net10.0
      3. Verify ProjectType is Exe
    Expected Result: Valid .NET 10.0 console app project
    Failure Indicators: Wrong target framework or project type
    Evidence: .sisyphus/evidence/task-1-project-validation.txt
  ```

  **Evidence to Capture**:
  - [x] Directory structure listing
  - [x] Project file contents
  - [x] Build success (msbuild IRCGuessBot.sln)

  **Commit**: YES
  - Message: `feat(scaffolding): create project structure`
  - Files: `IRCGuessBot/IRCGuessBot.csproj`, `IRCGuessBot/Program.cs`, directories

---

- [x] 2. **Add project reference to IRCSharp.Core**

  **What to do**:
  - Add ProjectReference to IRCSharp.Core in IRCGuessBot.csproj
  - Verify build succeeds with the reference
  - Update .gitignore if needed (exclude bin/obj)

  **Must NOT do**:
  - Don't add any other package references yet

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Simple project file edit
  - **Skills**: [`git-master`]
    - `git-master`: Track project file changes properly

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (with Tasks 1, 3-5)
  - **Blocks**: None
  - **Blocked By**: None

  **References**:
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj:22` — Project reference pattern

  **Acceptance Criteria**:
  - [x] IRCGuessBot.csproj contains ProjectReference to IrcSharp.Core
  - [x] msbuild IRCGuessBot.sln succeeds
  - [x] No build errors

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Project reference verification
    Tool: Bash (cat)
    Preconditions: IRCGuessBot.csproj exists
    Steps:
      1. Run: cat IRCGuessBot/IRCGuessBot.csproj
      2. Verify ItemGroup contains ProjectReference to IrcSharp.Core
    Expected Result: Valid project reference present
    Failure Indicators: Missing or incorrect reference path
    Evidence: .sisyphus/evidence/task-2-project-ref.txt

  Scenario: Build validation
    Tool: Bash (msbuild)
    Preconditions: Project reference added
    Steps:
      1. Run: msbuild IRCGuessBot.sln /t:Restore /p:Configuration=Release
      2. Verify no errors in output
    Expected Result: Restore succeeds
    Failure Indicators: Restore errors
    Evidence: .sisyphus/evidence/task-2-build-restore.log
  ```

  **Evidence to Capture**:
  - [x] Project file contents showing reference
  - [x] Build output log

  **Commit**: YES
  - Message: `build: add IRCSharp.Core reference`
  - Files: `IRCGuessBot/IRCGuessBot.csproj`

---

- [x] 3. **Create appsettings.json with default config**

  **What to do**:
  - Create appsettings.json in IRCGuessBot/ directory
  - Add configuration sections: Server, Port, Channel, Nickname
  - Include example/commented values
  - Ensure JSON is valid

  **Must NOT do**:
  - Don't add complex configuration hierarchy yet

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Simple JSON file creation
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (with Tasks 1-2, 4-5)
  - **Blocks**: None
  - **Blocked By**: None

  **References**:
  - Standard JSON configuration format

  **Acceptance Criteria**:
  - [x] appsettings.json exists with valid JSON
  - [x] Contains Server, Port, Channel, Nickname fields
  - [x] Values are examples/placeholders

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: JSON validity check
    Tool: Bash (cat, jq if available)
    Preconditions: appsettings.json created
    Steps:
      1. Run: cat IRCGuessBot/appsettings.json
      2. Verify JSON parses without errors
    Expected Result: Valid JSON structure
    Failure Indicators: JSON parsing errors
    Evidence: .sisyphus/evidence/task-3-json-validity.txt

  Scenario: Config fields verification
    Tool: Bash (grep)
    Preconditions: appsettings.json exists
    Steps:
      1. Run: grep -E '"(Server|Port|Channel|Nickname)"' IRCGuessBot/appsettings.json
      2. Verify all four fields present
    Expected Result: All configuration fields present
    Failure Indicators: Missing fields
    Evidence: .sisyphus/evidence/task-3-config-fields.txt
  ```

  **Evidence to Capture**:
  - [x] JSON file contents
  - [x] Field presence verification

  **Commit**: YES
  - Message: `config: add appsettings.json with defaults`
  - Files: `IRCGuessBot/appsettings.json`

---

- [x] 4. **Implement BotConfig model class**

  **What to do**:
  - Create Config/BotConfig.cs
  - Define class matching appsettings.json structure
  - Add properties: Server (string), Port (int), Channel (string), Nickname (string)
  - Include JSON deserialization attributes (System.Text.Json or Newtonsoft)
  - Add validation (Port must be > 0, values not null/empty)

  **Must NOT do**:
  - Don't add file loading logic yet (that's for Program.cs)
  - Don't add complex validation rules

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Simple POCO class
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Sequential (Wave 1, last task)
  - **Blocks**: Tasks 6-8 (game logic depends on config)
  - **Blocked By**: Tasks 1-3 (project structure and config file must exist)

  **References**:
  - `IrcSharp.Core/Model/IrcUserInfo.cs` — Model pattern

  **Acceptance Criteria**:
  - [x] BotConfig.cs exists with correct properties
  - [x] Class can be instantiated
  - [x] Basic validation present (Port > 0)

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Model class compilation
    Tool: Bash (msbuild)
    Preconditions: BotConfig.cs created
    Steps:
      1. Run: msbuild IRCGuessBot.sln /t:Build /p:Configuration=Release
      2. Verify no compilation errors
    Expected Result: Build succeeds
    Failure Indicators: Compilation errors
    Evidence: .sisyphus/evidence/task-4-compile.txt

  Scenario: Model instantiation
    Tool: Bash (dotnet run with test program)
    Preconditions: Build succeeds
    Steps:
      1. Create simple test to instantiate BotConfig
      2. Verify properties can be set
    Expected Result: Class instantiates correctly
    Failure Indicators: Null reference or validation errors
    Evidence: .sisyphus/evidence/task-4-instantiation.txt
  ```

  **Evidence to Capture**:
  - [x] Build output
  - [x] Test run output

  **Commit**: YES
  - message: `model: add BotConfig class`
  - Files: `IRCGuessBot/Config/BotConfig.cs`

---

- [x] 5. **Add unit test project setup**

  **What to do**:
  - Create IRCGuessBot.Tests/ directory
  - Create IRCGuessBot.Tests.csproj (xUnit, .NET 10.0)
  - Add ProjectReference to IRCGuessBot
  - Add test packages: xunit, xunit.runner.visualstudio, Moq
  - Create initial test class structure (NumberGameTests, GameSessionTests, etc.)

  **Must NOT do**:
  - Don't write actual test cases yet (Wave 2)

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Project setup similar to existing test projects
  - **Skills**: [`git-master`]

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 1 (with Tasks 1-4)
  - **Blocks**: Tasks 9-11 (tests depend on test project structure)
  - **Blocked By**: None

  **References**:
  - `IrcSharp.Core.Tests.Unit/IrcSharp.Core.Tests.Unit.csproj` — Test project structure

  **Acceptance Criteria**:
  - [x] Test project created with correct structure
  - [x] References IRCGuessBot project
  - [x] Build succeeds

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Test project structure
    Tool: Bash (ls, cat)
    Preconditions: Test project created
    Steps:
      1. Run: ls IRCGuessBot.Tests/
      2. Verify .csproj exists
      3. Run: cat IRCGuessBot.Tests/IRCGuessBot.Tests.csproj
    Expected Result: Valid test project structure
    Failure Indicators: Missing files or incorrect structure
    Evidence: .sisyphus/evidence/task-5-test-structure.txt

  Scenario: Test project build
    Tool: Bash (msbuild)
    Preconditions: Test project created
    Steps:
      1. Run: msbuild IRCGuessBot.sln /t:Build /p:Configuration=Release
      2. Verify test project builds
    Expected Result: Build succeeds
    Failure Indicators: Build errors
    Evidence: .sisyphus/evidence/task-5-test-build.txt
  ```

  **Evidence to Capture**:
  - [x] Test project structure
  - [x] Project file contents
  - [x] Build output

  **Commit**: YES
  - message: `test: add unit test project structure`
  - Files: `IRCGuessBot.Tests/IRCGuessBot.Tests.csproj`, test directory structure

---

- [x] 6. **Implement NumberGame class**

  **What to do**:
  - Create Game/NumberGame.cs
  - Generate random number between 1 and max
  - Validate guess (must be within range)
  - Determine if guess is correct, too high, or too low
  - Track number of guesses
  - Determine game over state

  **Must NOT do**:
  - Don't add hints or advanced features
  - Don't add persistence

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `deep`
    - Reason: Core business logic with state management
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 2, sequential (GameSession depends on this)
  - **Blocks**: Task 7 (GameSession needs NumberGame)
  - **Blocked By**: Task 4 (BotConfig must exist)

  **References**:
  - Random number generation pattern in C# (Random class or RandomNumberGenerator)

  **Acceptance Criteria**:
  - [x] NumberGame.cs exists with core methods
  - [x] Can start new game with max value
  - [x] Can check guess and get feedback
  - [x] Tracks guess count correctly
  - [x] Detects game over when guessed correctly

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Game initialization
    Tool: Bash (dotnet test or manual test)
    Preconditions: NumberGame.cs created
    Steps:
      1. Create NumberGame instance with max=100
      2. Verify game is active
      3. Verify guess count is 0
    Expected Result: Game starts correctly
    Failure Indicators: Game not active or wrong guess count
    Evidence: .sisyphus/evidence/task-6-init.txt

  Scenario: Guess validation
    Tool: Bash (dotnet test or manual test)
    Preconditions: Game active
    Steps:
      1. Guess number 50 (within range 1-100)
      2. Verify feedback received (too high/low/correct)
      3. Guess number 0 (out of range)
      4. Verify invalid guess handled
    Expected Result: Valid guesses processed, invalid rejected
    Failure Indicators: Wrong feedback or no error on invalid guess
    Evidence: .sisyphus/evidence/task-6-guess-validation.txt

  Scenario: Game completion
    Tool: Bash (dotnet test or manual test)
    Preconditions: Game active
    Steps:
      1. Make multiple guesses until correct
      2. Verify game over state
      3. Verify guess count matches actual attempts
    Expected Result: Game ends correctly with accurate count
    Failure Indicators: Game doesn't end or wrong count
    Evidence: .sisyphus/evidence/task-6-completion.txt
  ```

  **Evidence to Capture**:
  - [x] Manual test output showing game flow
  - [x] Guess count verification

  **Commit**: YES
  - message: `game: implement NumberGame core logic`
  - Files: `IRCGuessBot/Game/NumberGame.cs`

---

- [x] 7. **Implement GameSession class**

  **What to do**:
  - Create Game/GameSession.cs
  - Track current NumberGame instance
  - Track channel name
  - Track if game is active
  - Provide methods: StartGame(max), ProcessGuess(number), GetStatus()
  - Handle game state transitions

  **Must NOT do**:
  - Don't add multiple game support
  - Don't add persistence

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `deep`
    - Reason: State management wrapper around NumberGame
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 2, sequential (WinTracker can run parallel)
  - **Blocks**: Tasks 12-14 (command handlers need GameSession)
  - **Blocked By**: Task 6 (NumberGame must exist)

  **References**:
  - State management pattern

  **Acceptance Criteria**:
  - [x] GameSession.cs exists with core methods
  - [x] Can start new game
  - [x] Can process guess and get feedback
  - [x] Tracks game state correctly (active/inactive)
  - [x] Prevents multiple concurrent games

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Session game start
    Tool: Bash (dotnet test or manual test)
    Preconditions: GameSession.cs created
    Steps:
      1. Create GameSession for channel "#test"
      2. Call StartGame(100)
      3. Verify game is active
    Expected Result: Game starts in session
    Failure Indicators: Game not active
    Evidence: .sisyphus/evidence/task-7-start.txt

  Scenario: Guess processing
    Tool: Bash (dotnet test or manual test)
    Preconditions: Game active in session
    Steps:
      1. Call ProcessGuess(50)
      2. Verify feedback returned
      3. Verify game still active if not guessed
    Expected Result: Guess processed correctly
    Failure Indicators: Wrong feedback or game state issues
    Evidence: .sisyphus/evidence/task-7-process.txt

  Scenario: Game end detection
    Tool: Bash (dotnet test or manual test)
    Preconditions: Game active
    Steps:
      1. Make correct guess
      2. Verify game ends
      3. Try to guess again (should be rejected)
    Expected Result: Game ends, subsequent guesses rejected
    Failure Indicators: Game doesn't end or accepts guesses after end
    Evidence: .sisyphus/evidence/task-7-end.txt
  ```

  **Evidence to Capture**:
  - [x] Session state verification
  - [x] Guess processing output

  **Commit**: YES
  - message: `game: implement GameSession state management`
  - Files: `IRCGuessBot/Game/GameSession.cs`

---

- [x] 8. **Implement WinTracker class**

  **What to do**:
  - Create State/WinTracker.cs
  - Use Dictionary<string, int> for <username, winCount>
  - Add method: RecordWin(username) — increments win count
  - Add method: GetWins(username) — returns win count
  - Add method: GetAllWins() — returns all users with wins
  - Thread-safe access (ConcurrentDictionary or lock)

  **Must NOT do**:
  - Don't add persistence (file/database)
  - Don't add complex query methods

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Simple dictionary wrapper
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 2 (with Task 7, independent)
  - **Blocks**: None (command handlers can use it)
  - **Blocked By**: None

  **References**:
  - Dictionary usage pattern in C#
  - Thread-safe collection (ConcurrentDictionary)

  **Acceptance Criteria**:
  - [x] WinTracker.cs exists with core methods
  - [x] Can record wins per user
  - [x] Can retrieve win counts
  - [x] Thread-safe (ConcurrentDictionary)

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Record win
    Tool: Bash (dotnet test or manual test)
    Preconditions: WinTracker.cs created
    Steps:
      1. Create WinTracker instance
      2. Call RecordWin("user1")
      3. Call RecordWin("user1") again
      4. GetWins("user1")
    Expected Result: Count is 2
    Failure Indicators: Wrong count or null
    Evidence: .sisyphus/evidence/task-8-record.txt

  Scenario: Multiple users
    Tool: Bash (dotnet test or manual test)
    Preconditions: WinTracker exists
    Steps:
      1. Record wins for user1, user2, user3
      2. Verify each has correct count
      3. GetAllWins() returns all users
    Expected Result: All users tracked correctly
    Failure Indicators: Missing users or wrong counts
    Evidence: .sisyphus/evidence/task-8-multiple.txt
  ```

  **Evidence to Capture**:
  - [x] Win count verification
  - [x] Multiple user tracking

  **Commit**: YES
  - message: `state: implement WinTracker in-memory storage`
  - Files: `IRCGuessBot/State/WinTracker.cs`

---

- [x] 9. **Write unit tests for NumberGame**

  **What to do**:
  - Create Tests/NumberGameTests.cs
  - Test: Game starts with correct max value
  - Test: Valid guesses return correct feedback
  - Test: Invalid guesses (out of range) are rejected
  - Test: Game ends when correct guess made
  - Test: Guess count is accurate
  - Use xUnit with Moq if needed (though NumberGame has no dependencies)

  **Must NOT do**:
  - Don't test implementation details, only behavior

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Standard unit test patterns
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 2, sequential (follows implementation)
  - **Blocks**: None (can run immediately after implementation)
  - **Blocked By**: Task 6 (NumberGame must be implemented)

  **References**:
  - `IrcSharp.Core.Tests.Unit/When_Receiving_Messages.cs` — Test structure pattern
  - xUnit test naming: `When_<Scenario>_<ExpectedResult>`

  **Acceptance Criteria**:
  - [x] NumberGameTests.cs exists with test methods
  - [x] All tests pass when run
  - [x] Tests cover happy path and edge cases

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Run NumberGame tests
    Tool: Bash (dotnet test)
    Preconditions: NumberGameTests.cs created
    Steps:
      1. Run: dotnet test IRCGuessBot.Tests/IRCGuessBot.Tests.csproj --filter "FullyQualifiedName~NumberGameTests"
      2. Verify all tests pass
      3. Verify no failures
    Expected Result: All tests pass
    Failure Indicators: Test failures
    Evidence: .sisyphus/evidence/task-9-numbertgame-tests.xml (test report)

  Scenario: Test coverage verification
    Tool: Bash (dotnet test with coverage)
    Preconditions: Tests pass
    Steps:
      1. Run tests with coverage tool (dotnet test --collect:"XPlat Code Coverage")
      2. Verify key methods are covered
    Expected Result: Core methods tested
    Failure Indicators: Low coverage on critical paths
    Evidence: .sisyphus/evidence/task-9-coverage.txt
  ```

  **Evidence to Capture**:
  - [x] Test run output
  - [x] Test report XML
  - [x] Coverage report

  **Commit**: YES
  - message: `test: add NumberGame unit tests`
  - Files: `IRCGuessBot.Tests/NumberGameTests.cs`

---

- [x] 10. **Write unit tests for GameSession**

  **What to do**:
  - Create Tests/GameSessionTests.cs
  - Test: Session starts game correctly
  - Test: ProcessGuess delegates to NumberGame
  - Test: Game ends when correct guess made
  - Test: Cannot start game while one is active
  - Test: Cannot guess when no game active

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Unit test patterns
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 2 (with Task 9, 11)
  - **Blocks**: None
  - **Blocked By**: Task 7 (GameSession must be implemented)

  **Acceptance Criteria**:
  - [x] GameSessionTests.cs exists
  - [x] All tests pass
  - [x] Tests cover state transitions

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Run GameSession tests
    Tool: Bash (dotnet test)
    Preconditions: GameSessionTests.cs created
    Steps:
      1. Run: dotnet test --filter "FullyQualifiedName~GameSessionTests"
      2. Verify all tests pass
    Expected Result: All tests pass
    Failure Indicators: Test failures
    Evidence: .sisyphus/evidence/task-10-session-tests.xml
  ```

  **Evidence to Capture**:
  - [x] Test run output
  - [x] Test report

  **Commit**: YES
  - message: `test: add GameSession unit tests`
  - Files: `IRCGuessBot.Tests/GameSessionTests.cs`

---

- [x] 11. **Write unit tests for WinTracker**

  **What to do**:
  - Create Tests/WinTrackerTests.cs
  - Test: RecordWin increments count
  - Test: GetWins returns correct count
  - Test: Multiple users tracked independently
  - Test: Thread-safe (optional, use ConcurrentDictionary)

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Simple unit tests
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 2 (with Tasks 9, 10)
  - **Blocks**: None
  - **Blocked By**: Task 8 (WinTracker must be implemented)

  **Acceptance Criteria**:
  - [x] WinTrackerTests.cs exists
  - [x] All tests pass
  - [x] Tests cover concurrent access if applicable

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Run WinTracker tests
    Tool: Bash (dotnet test)
    Preconditions: WinTrackerTests.cs created
    Steps:
      1. Run: dotnet test --filter "FullyQualifiedName~WinTrackerTests"
      2. Verify all tests pass
    Expected Result: All tests pass
    Failure Indicators: Test failures
    Evidence: .sisyphus/evidence/task-11-tracker-tests.xml
  ```

  **Evidence to Capture**:
  - [x] Test run output
  - [x] Test report

  **Commit**: YES
  - message: `test: add WinTracker unit tests`
  - Files: `IRCGuessBot.Tests/WinTrackerTests.cs`

---

- [x] 12. **Implement CommandHandler**

  **What to do**:
  - Create Commands/CommandHandler.cs
  - Parse incoming IRC messages for commands starting with `!`
  - Dispatch to appropriate handler based on command name
  - Handle unknown commands (silently ignore per requirements)
  - Extract username from IRC message format (user!user@host)

  **Must NOT do**:
  - Don't add complex command routing yet
  - Don't add permission checks

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `deep`
    - Reason: Command parsing and dispatch logic
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 3, sequential
  - **Blocks**: Task 13 (GameCommands depends on CommandHandler)
  - **Blocked By**: Tasks 6-8 (game logic must exist)

  **References**:
  - IRC message parsing pattern from `When_Receiving_Messages.cs`
  - Username extraction from IRC format `user!user@host`

  **Acceptance Criteria**:
  - [x] CommandHandler.cs exists with parse and dispatch
  - [x] Correctly identifies `!play` and `!guess` commands
  - [x] Extracts username from IRC message
  - [x] Unknown commands silently ignored

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Command parsing
    Tool: Bash (dotnet test or manual test)
    Preconditions: CommandHandler.cs created
    Steps:
      1. Parse message "!play 100" from user "TestUser"
      2. Verify command name is "play" and argument is "100"
      3. Parse "!guess 50"
      4. Verify command name is "guess" and argument is "50"
    Expected Result: Commands parsed correctly
    Failure Indicators: Wrong parsing or extraction
    Evidence: .sisyphus/evidence/task-12-parse.txt

  Scenario: Unknown command handling
    Tool: Bash (dotnet test or manual test)
    Preconditions: CommandHandler exists
    Steps:
      1. Parse "!unknown arg"
      2. Verify no exception thrown
      3. Verify command not dispatched
    Expected Result: Unknown command silently ignored
    Failure Indicators: Exception thrown or command dispatched
    Evidence: .sisyphus/evidence/task-12-unknown.txt
  ```

  **Evidence to Capture**:
  - [x] Parsing test output
  - [x] Unknown command handling verification

  **Commit**: YES
  - message: `commands: implement CommandHandler parsing`
  - Files: `IRCGuessBot/Commands/CommandHandler.cs`

---

- [x] 13. **Implement GameCommands handlers**

  **What to do**:
  - Create Commands/GameCommands.cs
  - Implement !play handler:
    - Parse max number from argument
    - Validate number (positive integer)
    - Start game in GameSession
    - Announce game start to channel
  - Implement !guess handler:
    - Parse guess number from argument
    - Process guess in GameSession
    - If won: increment WinTracker, announce winner with guess count + total wins
    - If in progress: announce too high/too low
  - Handle invalid input (silently ignore per requirements)

  **Must NOT do**:
  - Don't add hints or advanced features
  - Don't add error feedback to channel

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `deep`
    - Reason: Game logic integration with IRC commands
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 3, sequential
  - **Blocks**: Task 14 (Program uses GameCommands)
  - **Blocked By**: Task 12 (CommandHandler must exist), Tasks 6-8 (game logic)

  **References**:
  - IRC message sending from IrcConnection examples
  - PrivMsgMessage usage pattern

  **Acceptance Criteria**:
  - [x] GameCommands.cs implements !play and !guess
  - [x] !play starts game and announces
  - [x] !guess processes and provides feedback
  - [x] Win announcement includes guess count + total wins
  - [x] Invalid input silently ignored

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: !play command
    Tool: Bash (dotnet test or manual test)
    Preconditions: GameCommands.cs created
    Steps:
      1. Simulate "!play 100" command
      2. Verify GameSession starts new game
      3. Verify game start announcement would be sent
    Expected Result: Game starts correctly
    Failure Indicators: Game not started or wrong announcement
    Evidence: .sisyphus/evidence/task-13-play.txt

  Scenario: !guess command - correct guess
    Tool: Bash (dotnet test or manual test)
    Preconditions: Game active
    Steps:
      1. Make correct guess
      2. Verify game ends
      3. Verify WinTracker incremented for user
      4. Verify announcement includes guess count + total wins
    Expected Result: Win announced correctly
    Failure Indicators: Wrong announcement or tracker not updated
    Evidence: .sisyphus/evidence/task-13-guess-win.txt

  Scenario: !guess command - incorrect guess
    Tool: Bash (dotnet test or manual test)
    Preconditions: Game active
    Steps:
      1. Make incorrect guess
      2. Verify feedback (too high/too low)
      3. Verify game still active
    Expected Result: Correct feedback, game continues
    Failure Indicators: Wrong feedback or game ends prematurely
    Evidence: .sisyphus/evidence/task-13-guess-incorrect.txt
  ```

  **Evidence to Capture**:
  - [x] Command handling output
  - [x] Announcement verification

  **Commit**: YES
  - message: `commands: implement !play and !guess handlers`
  - Files: `IRCGuessBot/Commands/GameCommands.cs`

---

- [x] 14. **Implement Program.cs entry point**

  **What to do**:
  - Update Program.cs with main application logic
  - Load configuration from appsettings.json
  - Create IrcConnection instance
  - Subscribe to OnPrivMsgMessageReceived event
  - Initialize GameSession and WinTracker
  - Connect to IRC server
  - Join specified channel
  - Run message loop (async main with await Task.Delay(-1) or similar)
  - Handle PING/PONG automatically (via MessagePropagator)
  - Handle disconnect/reconnect gracefully

  **Must NOT do**:
  - Don't add complex error handling beyond basic try/catch
  - Don't add logging framework yet

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `deep`
    - Reason: Main application lifecycle and event wiring
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 3, last task
  - **Blocks**: Tasks 15-16 (integration tests and error handling depend on this)
  - **Blocked By**: Tasks 1-13 (all components must be implemented)

  **References**:
  - `When_Receiving_Messages.cs` — Event subscription pattern
  - `IrcConnection.cs` — Connection lifecycle
  - Console app async main pattern (`async Task Main()`)

  **Acceptance Criteria**:
  - [x] Program.cs loads appsettings.json
  - [x] Connects to IRC server
  - [x] Joins specified channel
  - [x] Handles incoming messages and dispatches commands
  - [x] Gracefully handles disconnects

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Configuration loading
    Tool: Bash (dotnet run with test config)
    Preconditions: Program.cs created
    Steps:
      1. Run bot with test appsettings.json
      2. Verify configuration is loaded (check logs or debug output)
    Expected Result: Config loaded from file
    Failure Indicators: Config not loaded or wrong values
    Evidence: .sisyphus/evidence/task-14-config.txt

  Scenario: Connection and join
    Tool: interactive_bash (tmux)
    Preconditions: Bot configured to test server
    Steps:
      1. Start bot: dotnet run
      2. Verify connection to server
      3. Verify join to channel
    Expected Result: Bot connects and joins channel
    Failure Indicators: Connection fails or doesn't join
    Evidence: .sisyphus/evidence/task-14-connect.log (terminal output)

  Scenario: Message handling
    Tool: interactive_bash (tmux)
    Preconditions: Bot running
    Steps:
      1. Send "!play 100" from test client
      2. Verify bot responds with game start
      3. Send "!guess 50"
      4. Verify bot responds with feedback
    Expected Result: Messages processed correctly
    Failure Indicators: No response or wrong response
    Evidence: .sisyphus/evidence/task-14-messages.log
  ```

  **Evidence to Capture**:
  - [x] Terminal output showing connection and messages
  - [x] Configuration load verification

  **Commit**: YES
  - message: `app: implement Program.cs entry point`
  - Files: `IRCGuessBot/Program.cs`

---

- [x] 15. **Write integration tests for command handling**

  **What to do**:
  - Create Tests/CommandIntegrationTests.cs
  - Use Mock<ISocketConnection> to simulate IRC server
  - Test: Full !play flow (start game, announce)
  - Test: Full !guess flow (guess, feedback, win)
  - Test: WinTracker integration (record and retrieve wins)
  - Use ManualResetEvent for async verification

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `unspecified-high`
    - Reason: Integration testing requires understanding of full flow
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 3 (can run after Program.cs)
  - **Blocks**: None
  - **Blocked By**: Task 14 (Program.cs must be implemented)

  **References**:
  - `IrcSharp.Core.Tests.Unit/When_Receiving_Messages.cs` — Integration test pattern with Mock<ISocketConnection>
  - `TestHelpers.RunSendableEventFiringTest` — Async test helper pattern

  **Acceptance Criteria**:
  - [x] CommandIntegrationTests.cs exists
  - [x] Tests simulate full IRC message flow
  - [x] All tests pass

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Run integration tests
    Tool: Bash (dotnet test)
    Preconditions: CommandIntegrationTests.cs created
    Steps:
      1. Run: dotnet test --filter "FullyQualifiedName~CommandIntegrationTests"
      2. Verify all tests pass
    Expected Result: All integration tests pass
    Failure Indicators: Test failures
    Evidence: .sisyphus/evidence/task-15-integration.xml
  ```

  **Evidence to Capture**:
  - [x] Test run output
  - [x] Test report XML

  **Commit**: YES
  - message: `test: add command integration tests`
  - Files: `IRCGuessBot.Tests/CommandIntegrationTests.cs`

---

- [x] 16. **Add error handling for IRC events**

  **What to do**:
  - Add try/catch blocks around message processing
  - Handle connection failures gracefully
  - Add basic logging (Console.WriteLine for now)
  - Handle PING/PONG automatically (MessagePropagator already handles this)
  - Handle unexpected disconnections

  **Must NOT do**:
  - Don't add complex logging framework
  - Don't add notification services

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Basic error handling patterns
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: YES
  - **Parallel Group**: Wave 3 (can run after Program.cs)
  - **Blocks**: None
  - **Blocked By**: Task 14 (Program.cs must exist)

  **Acceptance Criteria**:
  - [x] Error handling added to message processing
  - [x] Connection failures logged
  - [x] No crashes on unexpected errors

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Error handling verification
    Tool: Bash (dotnet run with error scenario)
    Preconditions: Error handling added
    Steps:
      1. Run bot with invalid server address
      2. Verify graceful error message
      3. Verify bot doesn't crash
    Expected Result: Error handled gracefully
    Failure Indicators: Crash or no error message
    Evidence: .sisyphus/evidence/task-16-error.txt
  ```

  **Evidence to Capture**:
  - [x] Error message output
  - [x] Graceful shutdown verification

  **Commit**: YES
  - message: `error: add IRC event error handling`
  - Files: Program.cs modifications

---

- [x] 17. **Run all unit tests and verify pass**

  **What to do**:
  - Run full test suite: dotnet test
  - Verify all tests pass
  - Fix any failures
  - Verify test coverage is reasonable (80%+)

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `quick`
    - Reason: Test execution and verification
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 4, sequential
  - **Blocks**: Tasks 18-20 (verification must pass first)
  - **Blocked By**: Tasks 1-16 (all implementation and tests must exist)

  **Acceptance Criteria**:
  - [x] All unit tests pass
  - [x] No failures or errors
  - [x] Reasonable test coverage

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Full test suite
    Tool: Bash (dotnet test)
    Preconditions: All tests written
    Steps:
      1. Run: dotnet test
      2. Verify all tests pass
      3. Verify no failures
    Expected Result: All tests pass
    Failure Indicators: Test failures
    Evidence: .sisyphus/evidence/task-17-all-tests.xml
  ```

  **Evidence to Capture**:
  - [x] Full test run output
  - [x] Test report

  **Commit**: NO (verification step, no code changes expected)

---

- [x] 18. **Manual QA - test bot connection and commands**

  **What to do**:
  - Set up test IRC server (or use public test server)
  - Start bot and verify connection
  - Test !play command with various max values
  - Test !guess command with correct and incorrect guesses
  - Verify win announcements include guess count + total wins
  - Test edge cases: invalid input, no game active
  - Capture terminal output as evidence

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `unspecified-high`
    - Reason: Manual testing requires human judgment
  - **Skills**: []

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 4, sequential
  - **Blocks**: Tasks 19-20 (QA must pass first)
  - **Blocked By**: Task 17 (all tests must pass)

  **Acceptance Criteria**:
  - [x] Bot connects successfully
  - [x] All commands work as expected
  - [x] Win announcements correct
  - [x] Edge cases handled

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Full bot manual QA
    Tool: interactive_bash (tmux)
    Preconditions: All tests pass
    Steps:
      1. Start bot: dotnet run
      2. Verify connection to IRC server
      3. Join channel
      4. Send !play 100
      5. Send !guess 50 (should get feedback)
      6. Make correct guess
      7. Verify win announcement with guess count + total wins
      8. Test invalid input (!play abc, !guess xyz)
      9. Verify silent ignore (no response)
    Expected Result: All scenarios work correctly
    Failure Indicators: Any command fails or wrong behavior
    Evidence: .sisyphus/evidence/task-18-manual-qa.log (terminal session)

  Scenario: Multiple game sessions
    Tool: interactive_bash (tmux)
    Preconditions: Bot running
    Steps:
      1. Start game with !play 100
      2. Guess until win
      3. Start new game with !play 50
      4. Verify new game starts (not previous)
      5. Check win tracker shows correct wins per user
    Expected Result: Multiple games tracked correctly
    Failure Indicators: Game state confusion or wrong win count
    Evidence: .sisyphus/evidence/task-18-multi-session.log
  ```

  **Evidence to Capture**:
  - [x] Terminal session logs
  - [x] Command/response transcripts
  - [x] Win announcement screenshots

  **Commit**: NO (verification step)

---

- [x] 19. **Documentation - README with usage instructions**

  **What to do**:
  - Create README.md in IRCGuessBot/ directory
  - Include: project overview, build instructions, configuration, usage examples
  - Document all commands (!play, !guess)
  - Document configuration options
  - Include example appsettings.json
  - Add troubleshooting section

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `writing`
    - Reason: Documentation creation
  - **Skills**: [`git-master`]

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 4, sequential
  - **Blocks**: Task 20 (git cleanup after docs complete)
  - **Blocked By**: Task 18 (QA must pass first)

  **Acceptance Criteria**:
  - [x] README.md exists with comprehensive documentation
  - [x] Build instructions clear
  - [x] Configuration documented
  - [x] Usage examples provided

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Documentation completeness
    Tool: Bash (cat, grep)
    Preconditions: README.md created
    Steps:
      1. Run: cat IRCGuessBot/README.md
      2. Verify sections: overview, build, config, usage, examples
      3. Verify example appsettings.json included
    Expected Result: Comprehensive documentation
    Failure Indicators: Missing sections or unclear content
    Evidence: .sisyphus/evidence/task-19-readme.txt
  ```

  **Evidence to Capture**:
  - [x] README.md contents
  - [x] Section verification

  **Commit**: YES
  - message: `docs: add README with usage instructions`
  - Files: `IRCGuessBot/README.md`

---

- [x] 20. **Git cleanup and tagging**

  **What to do**:
  - Review all commits for consistency
  - Add tag v1.0.0
  - Ensure .gitignore is complete (bin/, obj/, appsettings.json if not tracked)
  - Final git status check
  - Verify all evidence files captured

  **Recommended Agent Profile**:
  > Select category + skills based on task domain. Justify each choice.
  - **Category**: `git-master`
    - Reason: Git operations and cleanup
  - **Skills**: [`git-master`]

  **Parallelization**:
  - **Can Run In Parallel**: NO
  - **Parallel Group**: Wave 4, final task
  - **Blocks**: None (final task)
  - **Blocked By**: Task 19 (documentation must be complete)

  **Acceptance Criteria**:
  - [x] All changes committed
  - [x] Tag v1.0.0 added
  - [x] Git status clean
  - [x] Evidence files captured

  **QA Scenarios (MANDATORY)**:
  ```
  Scenario: Git tag verification
    Tool: Bash (git)
    Preconditions: All commits complete
    Steps:
      1. Run: git tag -l
      2. Verify v1.0.0 exists
      3. Run: git log --oneline
      4. Verify commit history is clean
    Expected Result: Tag exists, history clean
    Failure Indicators: Missing tag or messy history
    Evidence: .sisyphus/evidence/task-20-git-status.txt

  Scenario: Evidence completeness
    Tool: Bash (ls)
    Preconditions: All tasks complete
    Steps:
      1. Run: ls .sisyphus/evidence/
      2. Verify all task evidence files present
      3. Verify no critical evidence missing
    Expected Result: All evidence captured
    Failure Indicators: Missing evidence files
    Evidence: .sisyphus/evidence/task-20-evidence-list.txt
  ```

  **Evidence to Capture**:
  - [x] Git tag list
  - [x] Commit history
  - [x] Evidence file list

  **Commit**: YES (final commit with tag)
  - message: `chore: release v1.0.0`
  - Files: All final changes

---

## Final Verification Wave (MANDATORY — after ALL implementation tasks)

> 4 review agents run in PARALLEL. ALL must APPROVE. Rejection → fix → re-run.

- [x] F1. **Plan Compliance Audit** — `oracle`
  Read the plan end-to-end. For each "Must Have": verify implementation exists (read file, curl endpoint, run command). For each "Must NOT Have": search codebase for forbidden patterns — reject with file:line if found. Check evidence files exist in .sisyphus/evidence/. Compare deliverables against plan.
  Output: `Must Have [N/N] | Must NOT Have [N/N] | Tasks [N/N] | VERDICT: APPROVE/REJECT`

- [x] F2. **Code Quality Review** — `unspecified-high`
  Run `tsc --noEmit` + linter + `bun test`. Review all changed files for: `as any`/@ts-ignore, empty catches, console.log in prod, commented-out code, unused imports. Check AI slop: excessive comments, over-abstraction, generic names (data/result/item/temp).
  Output: `Build [PASS/FAIL] | Lint [PASS/FAIL] | Tests [N pass/N fail] | Files [N clean/N issues] | VERDICT`

- [x] F3. **Real Manual QA** — `unspecified-high` (+ `playwright` skill if UI)
  Start from clean state. Execute EVERY QA scenario from EVERY task — follow exact steps, capture evidence. Test cross-task integration (features working together, not isolation). Test edge cases: empty state, invalid input, rapid actions. Save to `.sisyphus/evidence/final-qa/`.
  Output: `Scenarios [N/N pass] | Integration [N/N] | Edge Cases [N tested] | VERDICT`

- [x] F4. **Scope Fidelity Check** — `deep`
  For each task: read "What to do", read actual diff (git log/diff). Verify 1:1 — everything in spec was built (no missing), nothing beyond spec was built (no creep). Check "Must NOT do" compliance. Detect cross-task contamination: Task N touching Task M's files. Flag unaccounted changes.
  Output: `Tasks [N/N compliant] | Contamination [CLEAN/N issues] | Unaccounted [CLEAN/N files] | VERDICT`

---

## Commit Strategy

- **1**: `feat(scaffolding): create project structure` — IRCGuessBot/, Program.cs, directories
- **2**: `build: add IRCSharp.Core reference` — IRCGuessBot.csproj
- **3**: `config: add appsettings.json with defaults` — appsettings.json
- **4**: `model: add BotConfig class` — Config/BotConfig.cs
- **5**: `test: add unit test project structure` — IRCGuessBot.Tests/, .csproj
- **6**: `game: implement NumberGame core logic` — Game/NumberGame.cs
- **7**: `game: implement GameSession state management` — Game/GameSession.cs
- **8**: `state: implement WinTracker in-memory storage` — State/WinTracker.cs
- **9**: `test: add NumberGame unit tests` — Tests/NumberGameTests.cs
- **10**: `test: add GameSession unit tests` — Tests/GameSessionTests.cs
- **11**: `test: add WinTracker unit tests` — Tests/WinTrackerTests.cs
- **12**: `commands: implement CommandHandler parsing` — Commands/CommandHandler.cs
- **13**: `commands: implement !play and !guess handlers` — Commands/GameCommands.cs
- **14**: `app: implement Program.cs entry point` — Program.cs
- **15**: `test: add command integration tests` — Tests/CommandIntegrationTests.cs
- **16**: `error: add IRC event error handling` — Program.cs modifications
- **17**: (verification, no commit)
- **18**: (verification, no commit)
- **19**: `docs: add README with usage instructions` — README.md
- **20**: `chore: release v1.0.0` — All final changes, tag v1.0.0

---

## Success Criteria

### Verification Commands
```bash
# Build
msbuild IRCGuessBot.sln /p:Configuration=Release

# Run tests
dotnet test IRCGuessBot.Tests/IRCGuessBot.Tests.csproj

# Run bot (with test config)
cd IRCGuessBot
dotnet run
```

### Final Checklist
- [x] All "Must Have" present
- [x] All "Must NOT Have" absent
- [x] All tests pass (unit + integration)
- [x] Manual QA passed
- [x] Documentation complete
- [x] Git tag v1.0.0 added
- [x] Evidence files captured in .sisyphus/evidence/