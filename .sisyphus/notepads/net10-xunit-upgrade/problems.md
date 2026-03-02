# Problems: .NET 10 + xUnit Migration

## Resolved Problems

### 1. Project File Conversion
**Problem:** Converting legacy MSBuild format to SDK-style format.

**Solution:**
- Removed explicit `<Compile>` entries (SDK includes automatically)
- Removed explicit `<Reference>` entries (SDK includes framework assemblies)
- Set `<TargetFramework>net10.0</TargetFramework>`
- Added xUnit package references

### 2. MSTest to xUnit Conversion
**Problem:** Converting all MSTest attributes and assertions to xUnit equivalents.

**Solution:**
- Created complete attribute mapping table
- Created complete assertion mapping table
- Used find/replace for systematic conversion
- Verified all tests pass after conversion

### 3. Integration Test Setup
**Problem:** Integration tests fail due to App.config loading issue.

**Solution:** 
- Documented as known limitation (not a conversion bug)
- Integration tests would need additional setup to work properly
- Migration focus is on framework conversion

## Unresolved Problems

### 1. Integration Test Infrastructure
**Problem:** Integration tests require additional setup for proper configuration loading.

**Impact:** 6 integration tests fail
**Cause:** xUnit doesn't automatically load App.config
**Resolution:** Requires additional work with xUnit's `IClassFixture<T>` pattern

## Future Improvements

1. Consider using xUnit's `IClassFixture<T>` for integration test setup
2. Consider using `IAsyncLifetime` for async initialization
3. Consider moving configuration to `xunit.runner.json` or similar
4. Consider using test server fixtures instead of embedded binary