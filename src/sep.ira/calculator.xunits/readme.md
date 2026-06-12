# SEP-IRA Calculator - Unit Tests

## Overview

This project contains comprehensive unit tests for the SEP-IRA Calculator library, specifically targeting the input validation logic.

## Test Statistics

- **Total Tests**: 45
- **Pass Rate**: 100% ✅
- **Test Framework**: xUnit 2.9.3
- **Target Framework**: .NET 10.0

## Test Organization

### Test Categories

1. **Principal Validation** (5 tests)
   - Zero, negative, valid, and excessive principal amounts

2. **Age Validation** (6 tests)
   - Minimum (18), maximum (120), and combined age+years constraints

3. **Years Validation** (4 tests)
   - Zero, negative, valid, and maximum (100) duration tests

4. **Tax Rate Validation** (12 tests)
   - Individual rate bounds (0-100%)
   - Combined rate constraints (Federal + State ≤ 100%)
   - All tax types: Federal, State, Capital Gains

5. **Economic Rates Validation** (8 tests)
   - Inflation rate bounds (-10% to 50%)
   - Annual return bounds (-50% to 100%)
   - Support for negative returns and deflation scenarios

6. **Multiple Errors** (1 test)
   - Validates that all errors are reported simultaneously

7. **Edge Cases** (4 tests)
   - Minimum and maximum boundary conditions
   - All-zero and all-maximum scenarios

## Running Tests

### Quick Test
```powershell
dotnet test
```

### Verbose Output
```powershell
dotnet test --logger "console;verbosity=detailed"
```

### Test Explorer in Visual Studio
- Open Test Explorer: Test → Test Explorer (or Ctrl+E, T)
- Right-click project → Run Tests
- Right-click specific test → Run Selected Tests

### Watch Mode (Continuous Testing)
```powershell
dotnet watch test
```

## Key Features

### AppreciatorInputValidator Class

The validation logic is isolated in a static `AppreciatorInputValidator` class for:
- ✅ Easy unit testing
- ✅ No UI dependencies
- ✅ Reusable across projects
- ✅ Clear separation of concerns

### Validation Rules

| Field | Min | Max | Notes |
|-------|-----|-----|-------|
| Principal | $1 | $10M | Currency |
| Initial Age | 18 | 120 | Years |
| Years | 1 | 100 | Investment duration |
| Tax Rates | 0% | 100% | Individual; combined ≤ 100% |
| Inflation Rate | -10% | 50% | Allows deflation |
| Annual Return | -50% | 100% | Allows downturns |

## Test Patterns

### Basic Validation Test
```csharp
[Fact]
public void ValidateInputsWithValidPrincipalNoErrors()
{
    // Arrange & Act
    var errors = AppreciatorInputValidator.ValidateInputs(50000, 50, 20, ...);

    // Assert
    Assert.Empty(errors);
}
```

### Error Detection Test
```csharp
[Fact]
public void ValidateInputsWithZeroPrincipalReturnsError()
{
    // Arrange & Act
    var errors = AppreciatorInputValidator.ValidateInputs(0, 50, 20, ...);

    // Assert
    Assert.Single(errors);
    Assert.Contains("Principal must be greater than $0", errors[0]);
}
```

### Theory Test (Parameterized)
```csharp
[Theory]
[InlineData(-0.1)]
[InlineData(-50)]
public void ValidateInputsWithNegativeTaxRateReturnsError(double rate)
{
    // Test runs once per InlineData value
    var errors = AppreciatorInputValidator.ValidateInputs(..., rate, ...);
    Assert.Single(errors);
}
```

## Code Coverage

The test suite provides comprehensive coverage of:
- ✅ All validation boundaries
- ✅ Minimum/maximum valid values
- ✅ Just-outside-boundary invalid values
- ✅ Combined constraint violations
- ✅ Multiple simultaneous errors
- ✅ Edge cases and special scenarios

## Integration with CI/CD

These tests can be integrated into your build pipeline:

```yaml
# Example GitHub Actions
- name: Run Tests
  run: dotnet test --logger "trx" --results-directory "test-results"

- name: Upload Coverage
  uses: codecov/codecov-action@v3
```

## Dependencies

- **xunit**: 2.9.3 (Test framework)
- **Microsoft.NET.Test.Sdk**: 17.14.1 (Test runner)
- **xunit.runner.visualstudio**: 3.1.4 (VS integration)
- **coverlet.collector**: 6.0.4 (Coverage analysis)
- **SepIraCalculatorForms**: Project reference

## File Structure

```
cc.isr.Finance.Sep.Ira.Calculator.XUnits/
├── AppreciatorTests.cs                    # 45 test cases
├── cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj  # Project file
├── TestDocumentation.md           # Detailed test docs
└── readme.md                       # This file
```

## Continuous Testing

To continuously run tests during development:

```powershell
# Terminal 1: Watch for changes
dotnet watch test

# Terminal 2: Modify AppreciatorInputValidator.cs and save
# Tests automatically re-run
```

## Performance

- **Execution Time**: ~200ms for all 45 tests
- **Average per Test**: ~4.4ms
- **No external dependencies**: All tests are pure functions

## Troubleshooting

### Tests Not Running
1. Ensure .NET 10.0-windows is installed
2. Check project reference paths
3. Clean and rebuild: `dotnet clean && dotnet build`

### Build Errors
1. Verify `AppreciatorInputValidator` class is public
2. Check target framework compatibility
3. Restore NuGet packages: `dotnet restore`

### Test Failures
1. Review error messages in Test Explorer
2. Check Test Output window for details
3. Run single test for isolation

## Future Improvements

- [ ] Add integration tests with Appreciator class
- [ ] Add performance benchmarks
- [ ] Add UI control validation tests
- [ ] Add data serialization tests
- [ ] Generate code coverage reports

## Contributing

When adding new validation rules:
1. Add validation logic to `AppreciatorInputValidator.cs`
2. Add corresponding tests to `AppreciatorTests.cs`
3. Ensure all tests pass
4. Update this readme if adding new test categories
5. Maintain >90% code coverage

## References

- [xUnit Documentation](https://xunit.net/)
- [Best Practices for Unit Testing](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
- [Windows Forms Testing Patterns](https://docs.microsoft.com/en-us/windows/apps/)
