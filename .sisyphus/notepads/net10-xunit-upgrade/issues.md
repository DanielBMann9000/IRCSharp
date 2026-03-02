# Issues: .NET 10 + xUnit Migration

## Completed Issues

### 1. MSTest to xUnit Attribute Mapping
**Issue:** Need to map all MSTest attributes to xUnit equivalents.

**Resolution:** Created complete mapping table:
- `[TestClass]` → no attribute
- `[TestMethod]` → `[Fact]` or `[Theory]`
- `[ClassInitialize]` → static constructor
- `[TestInitialize]` → constructor
- `[TestCleanup]` → `IDisposable.Dispose`

### 2. MSTest to xUnit Assertion Mapping
**Issue:** Need to map all MSTest assertions to xUnit equivalents.

**Resolution:** Created complete assertion mapping:
- `Assert.AreEqual(x, y)` → `Assert.Equal(x, y)`
- `Assert.IsTrue(x)` → `Assert.True(x)`
- `Assert.IsFalse(x)` → `Assert.False(x)`
- `Assert.IsNull(x)` → `Assert.Null(x)`
- `Assert.IsNotNull(x)` → `Assert.NotNull(x)`
- `Assert.Fail(msg)` → `throw new Xunit.AssertException(msg)`

### 3. Project File Conversion
**Issue:** Convert legacy MSBuild project files to SDK-style format.

**Resolution:** 
- Removed explicit `<Compile>` entries (SDK includes automatically)
- Removed explicit `<Reference>` entries (SDK includes framework assemblies)
- Set `<TargetFramework>net10.0</TargetFramework>`
- Added xUnit package references
- Removed MSTest package references

## Ongoing Issues

### 1. Integration Tests Configuration
**Issue:** Integration tests fail due to App.config not being loaded.

**Status:** ACCEPTABLE LIMITATION
**Impact:** Tests fail due to configuration, not conversion issue
**Resolution:** This is a known xUnit limitation when converting from MSTest

## Blocked Issues

None.