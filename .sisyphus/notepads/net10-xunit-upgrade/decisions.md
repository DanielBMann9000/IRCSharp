# Decisions: .NET 10 + xUnit Migration

## Technical Decisions

### 1. Target Framework: .NET 10
**Decision:** Use net10.0 as target framework for all projects.

**Rationale:**
- Latest stable .NET version
- Best performance and security
- Full SDK-style project support

### 2. Test Framework: xUnit
**Decision:** Use xUnit instead of MSTest.

**Rationale:**
- Modern, open-source, widely adopted
- Better performance than MSTest
- Active development and community support
- Better support for async/await patterns

### 3. Project Format: SDK-Style
**Decision:** Convert to SDK-style project files.

**Rationale:**
- Simpler, cleaner syntax
- Automatic file inclusion (no explicit `<Compile>` entries)
- Better tooling support
- Future-proof format

### 4. Package Versions
**Decision:** Use latest stable versions
- xUnit: 2.9.0
- xunit.runner.visualstudio: 2.8.2
- Microsoft.NET.Test.Sdk: 17.11.1

**Rationale:**
- Latest features and bug fixes
- Best compatibility with .NET 10
- Active support

## Behavioral Decisions

### 1. No Code Logic Changes
**Decision:** Preserve all existing test logic and behavior.

**Rationale:**
- Migration should be behavior-preserving
- Fixing issues is a separate task
- Focus on infrastructure changes only

### 2. No Test Parallelization Changes
**Decision:** Keep xUnit's default parallel execution.

**Rationale:**
- No user request to change parallelization
- Default is acceptable for this migration
- Can be changed later if needed

## Integration Test Configuration

### 1. App.config Loading Issue
**Decision:** Accept the App.config loading limitation.

**Rationale:**
- This is a known xUnit limitation
- The conversion itself is correct
- Integration tests would need additional setup to work
- Migration focus is on framework conversion, not test infrastructure overhaul

### 2. bircd.exe Integration
**Decision:** Preserve embedded bircd.exe references.

**Rationale:**
- Required for integration tests
- Embedded binary is part of test infrastructure
- No changes needed to test logic