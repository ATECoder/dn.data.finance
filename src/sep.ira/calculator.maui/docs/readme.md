# SEP IRA Calculator Maui App

## Installation & Running

### Prerequisites
- Visual Studio 2026 (or later)
- .NET 10.0 SDK
- For non-Windows platforms: Platform-specific SDKs (iOS, Android, etc.)

### Quick Start
1. **Open the solution**:
   ```bash
   cd C:\my\lib\vs\data\finance
   # Open SepIraCalculator.slnx in Visual Studio
   ```

2. **Select startup project**: Set `SepIraCalculatorMauiApp` as the startup project

3. **Run the app**:
   - Press `F5` or click the Run button
   - Choose your target platform from the dropdown (Windows is default)

## Using the Calculator

### Basic Workflow

1. **Enter Financial Parameters**
   - Invested Amount: Your initial investment ($)
   - Age: Your current age
   - Duration: Years until withdrawal
   - Tax Rates: Federal and state percentages
   - Growth Rate: Expected annual investment growth (%)

2. **Calculate**
   - Click the **Calculate** button
   - Wait for the analysis to complete (shows loading indicator)

3. **Review Results**
   - Simple Investment scenario (taxable account)
   - SEP IRA scenario (tax-advantaged account)
   - Side-by-side comparison
   - Detailed breakdowns including:
     - Account balances
     - Tax liabilities
     - Net cash value
     - RMD calculations (for SEP IRA)

4. **Reset** (optional)
   - Click **Reset** to clear all inputs and start over
   - Returns all fields to their default values

## Default Values

These are used when you click Reset:

| Parameter | Default Value |
|-----------|--------------|
| Invested Amount | $10,000 |
| Initial Age | 75 |
| Investment Duration | 20 years |
| Initial Federal Tax Rate | 35.0% |
| Withdrawal Federal Tax Rate | 35.0% |
| Initial State Tax Rate | 9.3% |
| Withdrawal State Tax Rate | 9.3% |
| Federal Capital Gains Tax | 25.0% |
| State Capital Gains Tax | 9.3% |
| Annual Inflation Rate | 2.75% |
| Annual Growth Rate | 7.0% |

## Valid Input Ranges

All inputs are validated against defined ranges:

| Parameter | Min | Max |
|-----------|-----|-----|
| Invested Amount | Variable | Variable |
| Initial Age | 18 | 150 |
| Duration | 1 | 70 years |
| Tax Rates | 0% | 100% |
| Growth Rate | -50% | 100% |

**Note**: If you enter an invalid value, you'll see a specific error message.

## Understanding the Results

### Simple Capital Investment
Shows what happens if you invest in a regular taxable account:
- Money grows at your specified annual growth rate
- You pay capital gains taxes when you withdraw
- Net result is your money after all taxes

### SEP IRA Investment
Shows what happens if you invest in a SEP IRA:
- Pre-tax contributions (deductible)
- Tax-deferred growth
- Required distributions starting at age 73
- Taxes only on withdrawals

### Key Metrics
- **Account Balance**: Total value at withdrawal
- **Capital Gain**: Profit from growth
- **Tax Liability**: Taxes owed at withdrawal
- **Net Cash Value**: Money in your pocket after taxes
- **RMD**: Required Minimum Distribution (SEP IRA only)

## Features

✅ **Responsive Design**
- Works on desktop, tablet, and mobile screens
- Dark mode support

✅ **Real-Time Validation**
- Input checking before calculation
- Helpful error messages

✅ **Professional Output**
- Formatted currency values
- Detailed financial breakdown
- Ready for financial planning

✅ **Fast Calculations**
- Async processing keeps UI responsive
- Loading indicator shows progress

## Troubleshooting

### I'm getting a validation error
- Check your input values are reasonable
- The error message will tell you what's wrong and the valid range

### The calculation seems wrong
- Compare with the WinForms app to verify
- Check your input parameters match what you intended

### The app won't start
- Verify .NET 10.0 is installed: `dotnet --version`
- Rebuild the solution: `dotnet build`
- Check build output for detailed errors

### Results don't look right
- Verify all input values are entered correctly
- Try with the default values first
- Compare results with the WinForms application

## Tips for Best Results

1. **Be realistic with your assumptions**
   - Growth rates between 5-10% are typical
   - Use current tax rates or reasonable future estimates

2. **Run multiple scenarios**
   - Try different growth rates to see sensitivity
   - Compare different ages/durations
   - Use Reset between scenarios

3. **Compare both scenarios**
   - The app shows both simple investment and SEP IRA
   - The difference shows the tax advantage of SEP IRA
   - Use this for financial planning decisions

## Advanced Usage

### From Command Line
```bash
# Build only
dotnet build src/sep.ira/SepIraCalculator.slnx

# Run with specific framework
dotnet run -f net10.0-windows10.0.19041.0

# Run tests
dotnet test src/sep.ira/calculator.xunits
```

### Platform-Specific Builds
```bash
# Windows
dotnet build -f net10.0-windows10.0.19041.0

# Android
dotnet build -f net10.0-android

# iOS
dotnet build -f net10.0-ios

# macOS
dotnet build -f net10.0-maccatalyst
```

## Getting Help

1. **Check the error message** - It usually tells you exactly what's wrong
2. **Verify your inputs** - Review all 11 input fields
3. **Check the README files** - CALCULATOR_INTEGRATION_README.md has detailed info
4. **Compare with WinForms** - Run the same scenario in SepIraCalculatorFormsApp

## Key Files
- **Main App**: `src/sep.ira/calculator.maui/`
- **Calculator Logic**: `src/sep.ira/calculator/Appreciator.cs`
- **WinForms Version**: `src/sep.ira/calculator.app/Form1.cs` (for comparison)

## Documentation

- [SEP IRA Calculator Documentation](src/sep.ira/calculator/readme.md)
- 

---

**Need more help?** See CALCULATOR_INTEGRATION_README.md for complete technical documentation.
