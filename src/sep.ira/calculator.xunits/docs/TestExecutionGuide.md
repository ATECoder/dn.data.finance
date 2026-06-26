# SEP IRA Calculator - Unit Test Execution & Documentation

## Project Structure

**Test Project:** `src\sep.ira\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj`

### Test Files:
1. **AppreciatorCalculationTests.cs** - Tests for calculation logic
2. **AppreciatorInputsValidationTests.cs** - Tests for input validation

---

## Running Tests

### Option 1: Visual Studio Test Explorer
1. Open Visual Studio
2. Navigate to **Test > Test Explorer** (or Ctrl+E, T)
3. Click **Run All Tests**
4. Results display in the Test Explorer window

### Option 2: Command Line (dotnet CLI)
```bash
# Run all tests
dotnet test src\sep.ira\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj

# Run tests with detailed output
dotnet test src\sep.ira\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj --verbosity detailed

# Run tests with XML report output
dotnet test src\sep.ira\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj ^
  --logger "trx;LogFileName=TestResults.trx" ^
  --logger "console;verbosity=detailed"
```

### Option 3: Visual Studio Code with Test Explorer Extension
1. Install the "Test Explorer UI" extension
2. Run tests from the Test Explorer sidebar

---

## Test Coverage Summary

### AppreciatorCalculationTests
Validates the core calculation engine for SEP IRA appreciation scenarios:
- Tax calculations (federal, state, capital gains)
- Investment growth projections
- Withdrawal scenarios
- Comparison between Simple Investment vs SEP IRA

### AppreciatorInputsValidationTests
Ensures input parameters are validated correctly:
- Valid parameter ranges
- Input boundary conditions
- Error handling for invalid inputs

---

## Test Results Location

Test results are available in:
- **Visual Studio Test Explorer** - Real-time results
- **Test Reports:** `src\sep.ira\calculator.xunits\docs\`
  - `AppreciatorTestsSummary.md` - High-level test summary
  - `AppreciatorTestsDetails.md` - Detailed test results
  - `AppreciatorTestsReference.md` - Test reference documentation

---

## Generating Test Reports

### Create Trx Report (Visual Studio Test Results Format)
```bash
dotnet test src\sep.ira\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj ^
  --logger "trx;LogFileName=src\sep.ira\calculator.xunits\docs\TestResults.trx"
```

### Create HTML Report (using ReportGenerator)
```bash
# Install ReportGenerator if not already installed
dotnet tool install -g dotnet-reportgenerator-globaltool

# Run tests and generate HTML report
dotnet test src\sep.ira\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj ^
  --logger "trx;LogFileName=coverage.trx" ^
  --collect:"XPlat Code Coverage"

# Generate HTML report from coverage data
reportgenerator ^
  -reports:"**/coverage.cobertura.xml" ^
  -targetdir:"src\sep.ira\calculator.xunits\docs\CoverageReport" ^
  -reporttypes:Html
```

---

## Best Practices for Running Tests

1. **Before each commit:** Run all tests to ensure no regressions
2. **Use CI/CD:** Configure GitHub Actions or Azure DevOps to run tests automatically
3. **Maintain test documentation:** Keep test results and reports up-to-date
4. **Monitor coverage:** Aim for >80% code coverage
5. **Run tests in isolation:** Each test should be independent and repeatable

---

## Troubleshooting Test Failures

| Issue | Solution |
|-------|----------|
| Tests won't compile | Ensure all NuGet packages are restored: `dotnet restore` |
| Tests timeout | Increase timeout in xunit.runner.json config file |
| Platform-specific failures | Run tests on target platform (Windows/Mac/Linux) |
| Floating-point precision | Use `Assert.Equal(expected, actual, 10)` for decimal precision |

---

## Test Execution Commands (Quick Reference)

```bash
# Navigate to solution root first
cd src

# Run all tests
dotnet test

# Run tests with filter
dotnet test --filter "AppreciatorCalculationTests"

# Run with verbose output
dotnet test --verbosity detailed

# Run and generate coverage report
dotnet test /p:CollectCoverage=true /p:CoverageFormat=cobertura

# Run specific test class
dotnet test --filter "FullyQualifiedName~AppreciatorInputsValidationTests"
```

---

## Integration with Build Pipeline

Add to `.csproj` file to run tests as part of build:
```xml
<Target Name="Test" AfterTargets="Build">
  <Exec Command="dotnet test --no-build --no-restore" />
</Target>
```

---

**Last Updated:** 2026-01-18  
**Test Framework:** xUnit  
**Minimum .NET Version:** 8.0  
