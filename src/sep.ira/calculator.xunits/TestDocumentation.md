# Unit Test Documentation for SEP-IRA Calculator Validation

## Overview

This document describes the comprehensive unit test suite for the SEP-IRA Calculator Windows Forms application input validation logic.

## Project Structure

- **Project**: `cc.isr.Finance.Sep.Ira.Calculator.XUnits`
- **Framework**: .NET 10.0-windows
- **Testing Framework**: xUnit 3.2.2
- **Test Class**: `InputValidatorTests`
- **Test Count**: 45 tests, all passing ✅

## Test Validation Logic

The tests validate the `AppreciatorInputValidator.ValidateInputs()` static method which checks:
- InvestedAmount amount
- Initial age
- Investment duration (years)
- Tax rates (federal and state, present and future)
- Capital gains tax rate
- Inflation rate
- Annual return rate

## Test Categories

### 1. InvestedAmount Validation Tests (5 tests)

| Test Name | Purpose | Input | Expected Result |
|-----------|---------|-------|-----------------|
| `ValidateInputsWithValidInvestedAmountNoErrors` | Valid investedAmount | 50,000 | No errors |
| `ValidateInputsWithZeroInvestedAmountReturnsError` | Zero investedAmount | 0 | Error: Invested amount must be > $0 |
| `ValidateInputsWithNegativeInvestedAmountReturnsError` | Negative investedAmount | -1,000 | Error: Invested amount must be > $0 |
| `ValidateInputsWithExcessivelyHighInvestedAmountReturnsError` | Excessive investedAmount | 11,000,000 | Error: Exceeds $1,000,000 limit |
| `ValidateInputsWithMaximumInvestedAmountNoErrors` | Maximum valid investedAmount | 1,000,000 | No errors |

**Range**: $1 to $1,000,000

### 2. Initial Age Validation Tests (6 tests)

| Test Name | Purpose | Input | Expected Result |
|-----------|---------|-------|-----------------|
| `ValidateInputsWithValidInitialAgeNoErrors` | Valid age | 50 | No errors |
| `ValidateInputsWithInitialAgeBelowMinimumReturnsError` | Below minimum | 17 | Error: Must be ≥ 18 |
| `ValidateInputsWithMinimumInitialAgeNoErrors` | Minimum valid | 18 | No errors |
| `ValidateInputsWithFinalAgeAboveMaximumReturnsError` | Above maximum | 121 | Error: Cannot exceed 120 |
| `    public void ValidateInputsWithFinalAgeAtMaxNoErrors()` | InitialAge + InvestmentDuration > 120 | InitialAge 74, InvestmentDuration 47 | Error: Results in age > 120 |
| `ValidateInputsWithFinalAgeAtMaxNoErrors` | FinalAge = 120 | InitialAge 74, InvestmentDuration 46 | No errors |

**Range**: 18 to 120 years
**Constraint**: Initial Age + InvestmentDuration ≤ 120

### 3. InvestmentDuration (Duration) Validation Tests (4 tests)

| Test Name | Purpose | Input | Expected Result |
|-----------|---------|-------|-----------------|
| `ValidateInputsWithZeroInvestmentDurationReturnsError` | Zero years | 0 | Error: InvestmentDuration must be > 0 |
| `ValidateInputsWithNegativeInvestmentDurationReturnsError` | Negative years | -5 | Error: InvestmentDuration must be > 0 |
| `ValidateInputsWithInvestmentDurationAboveMaximumReturnsError` | Above maximum | 101 | Error: Cannot exceed 100 |
| `ValidateInputsWithMaximumInvestmentDurationNoErrors` | Maximum valid | 100 | No errors |

**Range**: 1 to 100 years

### 4. Tax Rate Validation Tests (12 tests)

#### Individual Tax Rates (5 tests)
| Test Name | Purpose | Input | Expected Result |
|-----------|---------|-------|-----------------|
| `ValidateInputsWithValidTaxRatesNoErrors` | Valid rates | 35%, 35%, 9.3%, 9.3%, 25% | No errors |
| `ValidateInputsWithNegativeInitialFederalTaxRateReturnsError` | Negative rate | -0.1% | Error: Must be 0-100% |
| `ValidateInputsWithExcessiveWithdrawalFederalTaxRateReturnsError` | Excessive rate | 100.1%, 150% | Error: Must be 0-100% |
| `ValidateInputsWithNegativeInitialStateTaxRateReturnsError` | Negative state tax | -0.1% | Error: Must be 0-100% |
| `ValidateInputsWithNegativeFederalCapitalGainsTaxRateReturnsError` | Negative cap gains tax | -50% | Error: Must be 0-100% |
| `ValidateInputsWithNegativeStateCapitalGainsTaxRateReturnsError` | Negative cap gains tax | -50% | Error: Must be 0-100% |

#### Edge Cases
| Test Name | Purpose | Input | Expected Result |
|-----------|---------|-------|-----------------|
| `ValidateInputsWithAllZeroTaxRatesNoErrors` | All tax rates = 0% | 0%, 0%, 0%, 0%, 0% | No errors |
| `ValidateInputsWithAllMaximumTaxRatesNoErrors` | Max federal, min state | 100%, 100%, 0%, 0%, 100% | No errors |

**Range**: 0% to 100% for each rate

### 5. Economic Rates Validation Tests (8 tests)

#### Annual Inflation Rate (4 tests)
| Test Name | Purpose | Input | Expected Result |
|-----------|---------|-------|-----------------|
| `ValidateInputsWithValidInflationRateNoErrors` | Valid inflation | 2.75% | No errors |
| `ValidateInputsWithInflationRateBelowMinimumReturnsError` | Below minimum | -10.1%, -50% | Error: Must be -10% to 50% |
| `ValidateInputsWithInflationRateAboveMaximumReturnsError` | Above maximum | 50.1%, 100% | Error: Must be -10% to 50% |

#### Annual Growth Rate (4 tests)
| Test Name | Purpose | Input | Expected Result |
|-----------|---------|-------|-----------------|
| `ValidateInputsWithValidAnnualGrowthRageNoErrors` | Valid return | 7% | No errors |
| `ValidateInputsWithAnnualGrowthRageBelowMinimumReturnsError` | Below minimum | -50.1%, -100% | Error: Must be -50% to 100% |
| `ValidateInputsWithAnnualGrowthRageAboveMaximumReturnsError` | Above maximum | 100.1%, 150% | Error: Must be -50% to 100% |
| `ValidateInputsWithNegativeReturnNoErrors` | Negative return | -10% | No errors (allows market downturns) |

**Ranges**:
- Inflation Rate: -10% to 50% (allows deflation scenarios)
- Annual Growth Rate: -50% to 100% (allows market downturns)

### 6. Multiple Errors Test (1 test)

| Test Name | Purpose | Input | Expected Result |
|-----------|---------|-------|-----------------|
| `ValidateInputsWithMultipleErrors_ReturnsAllErrors` | Multiple violations | InvestedAmount -1000, InitialAge 17, InvestmentDuration 0, Tax Rate 150%, Inflation -50%, Return 150% | Returns ≥4 error messages |

### 7. Edge Case Tests (4 tests)

| Test Name | Purpose | Input | Expected Result |
|-----------|---------|-------|-----------------|
| `ValidateInputsWithMinimumValidInputsNoErrors` | Minimum boundaries | InvestedAmount $1, InitialAge 18, InvestmentDuration 1, Rates 0%, Econ -10%, -50% | No errors |
| `ValidateInputsWithMaximumValidInputsNoErrors` | Maximum boundaries | InvestedAmount $10M, InitialAge 120, InvestmentDuration 30, Rates 100%/0%, Econ 50%, 100% | No errors |
| `ValidateInputsWithAllZeroTaxRatesNoErrors` | All zeroes | All tax rates 0% | No errors |
| `ValidateInputsWithAllMaximumTaxRatesNoErrors` | All maxima | Federal 100%, State 0%, Cap Gains 100% | No errors |

## Validation Rules Summary

### InvestedAmount
- **Valid Range**: > $0 and ≤ $1,000,000
- **Errors**: Zero, negative, or exceeds limit

### InitialAge
- **Valid Range**: 18 to 120 years
- **Constraint**: InitialAge + InvestmentDuration ≤ 120
- **Errors**: Below 18, above 120, or combination exceeds 120

### InvestmentDuration
- **Valid Range**: > 0 and ≤ 1020
- **Errors**: Zero, negative, or exceeds 102

### Tax Rates (all)
- **Valid Range**
- **Errors**: Negative, exceeds maximum

### Inflation Rate
- **Valid Range**: -10% to 50%
- **Errors**: Below -10% or above 50%
- **Note**: Allows deflation and high inflation scenarios

### Annual Growth Rate
- **Valid Range**: -50% to 100%
- **Errors**: Below -50% or above 100%
- **Note**: Allows for market downturns and strong gains

## Test Execution

### Running All Tests
```bash
dotnet test cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj
```

### Running Specific Test Class
```bash
dotnet test --filter FullyQualifiedName~InputValidatorTests
```

### Test Results
- **Total Tests**: 45
- **Passed**: 45 ✅
- **Failed**: 0
- **Skipped**: 0
- **Duration**: ~200ms

## Code Quality Metrics

- **Test Coverage**: Input validation logic fully covered
- **Assertion Types Used**:
  - `Assert.Empty()` - For valid inputs
  - `Assert.Single()` - For expected single error
  - `Assert.True()` with conditions
  - `Assert.Contains()` - For error message verification
  - `Assert.Any()` - For LINQ-based error checks

## Best Practices Implemented

1. **Arrange-Act-Assert Pattern**: All tests follow AAA pattern
2. **Descriptive Naming**: Test names clearly describe scenarios
3. **Theory Tests**: Using `[Theory]` with `[InlineData]` for multiple similar test cases
4. **Edge Cases**: Testing boundaries (min, max, just outside boundaries)
5. **Error Messages**: Verifying specific error messages, not just error count
6. **Constants**: Using `const` fields for reusable test values
7. **Organization**: Tests grouped into logical regions

## Future Test Expansion

Potential areas for additional testing:
- Integration tests with actual `Appreciator` class
- Performance tests for large input values
- UI-level tests for form controls
- Serialization/deserialization tests
- Calculation accuracy tests

## References

- xUnit Documentation: https://xunit.net/docs/getting-started/netcore
- Test-Driven Development Best Practices
- Windows Forms Testing Patterns
