# SEP IRA Calculator - Maui Integration Complete

## Overview
The .NET Maui application now integrates the actual `Appreciator` calculator logic from the core `cc.isr.Finance.Sep.Ira.Calculator` library, providing a professional calculator UI similar to the WinForms application.

## Architecture

### ViewModel Layer
**File**: `ViewModels/AppreciatorViewModel.cs`
- Wraps the `Appreciator` model from the core library
- Provides 11 observable input properties for data binding
- Implements calculation logic using `Appreciator.CalculateFutureValue()` and `Appreciator.CalculateFutureValueSepIraWithRmd()`
- Generates formatted reports using `AppreciatorReportBuilder`
- Includes comprehensive input validation
- Async calculation to prevent UI freezing

### View Layer
**File**: `Views/AppreciatorPage.xaml` and `Views/AppreciatorPage.xaml.cs`
- Two-column responsive layout with scrollable input form
- 11 labeled numeric input fields matching the WinForms application
- Results display area with formatted text output
- Calculate and Reset command buttons
- Loading indicator during calculation
- Error message display for validation failures

### Converters
**File**: `Converters/Converters.cs`
- `InvertedBoolConverter`: Inverts boolean values for UI state binding
- `StringNotNullOrEmptyBoolConverter`: Shows/hides error messages based on error text

## Calculator Inputs

All 11 calculator parameters are available for user input:

1. **Invested Amount ($)** - Initial investment amount (default: 10,000)
2. **Initial Age** - Starting age for the calculation (default: 75)
3. **Investment Duration (years)** - How long to invest (default: 20)
4. **Initial Federal Tax Rate (%)** - Current federal tax rate (default: 35.0)
5. **Withdrawal Federal Tax Rate (%)** - Future federal tax rate at withdrawal (default: 35.0)
6. **Initial State Tax Rate (%)** - Current state tax rate (default: 9.3)
7. **Withdrawal State Tax Rate (%)** - Future state tax rate at withdrawal (default: 9.3)
8. **Federal Capital Gains Tax Rate (%)** - Rate on capital gains (default: 25.0)
9. **State Capital Gains Tax Rate (%)** - State rate on capital gains (default: 9.3)
10. **Annual Inflation Rate (%)** - Expected inflation (default: 2.75)
11. **Annual Growth Rate (%)** - Expected investment growth (default: 7.0)

## Calculator Output

The application displays a detailed comparison report showing:

### Simple Capital Investment Section
- Initial tax liability
- Capital account balance after growth
- Capital gains
- Tax liabilities at withdrawal
- Net cash out value after taxes

### SEP IRA Investment Section
- SEP IRA account balance
- RMD (Required Minimum Distribution) calculations
- Tax treatment for SEP IRA withdrawals
- Detailed tax breakdown

### Comparison
- Side-by-side view of both scenarios
- Age progression from initial to final age
- All relevant financial metrics formatted with proper currency/percentage formatting

## Input Validation

The application validates all inputs against ranges defined in the core library:
- `AppreciatorInputsInitialValues` - Provides default values
- `AppreciatorInputsRanges` - Defines min/max ranges for each input
- Custom validation with user-friendly error messages

## Calculations Performed

1. **Simple Capital Investment** (`CalculateFutureValue()`)
   - Calculates growth of a simple investment with tax treatment
   - Applies capital gains taxes at withdrawal

2. **SEP IRA Investment** (`CalculateFutureValueSepIraWithRmd()`)
   - Calculates SEP IRA account growth
   - Applies RMD rules
   - Calculates tax liabilities for required distributions

## Project Structure

```
calculator.maui/
├── ViewModels/
│   ├── BaseViewModel.cs              - Base class for all VMs
│   └── AppreciatorViewModel.cs       - Main calculator ViewModel
├── Views/
│   ├── AppreciatorPage.xaml          - Calculator UI
│   └── AppreciatorPage.xaml.cs       - Code-behind
├── Converters/
│   └── Converters.cs                 - UI value converters
├── Resources/
│   └── Styles/
│       ├── Styles.xaml               - Updated with converters
│       ├── Colors.xaml
│       └── Fonts/
├── Models/                           - (For future use)
├── Services/                         - (For future use)
├── AppShell.xaml                     - Navigation shell
├── AppShell.xaml.cs
├── App.xaml
├── App.xaml.cs
├── MauiProgram.cs
└── SepIraCalculatorMauiApp.csproj   - Project file

```

## Key Features

### ✅ Multi-Platform Support
- Windows (Primary development platform)
- iOS, Android, macOS (Configured and ready)

### ✅ MVVM Architecture
- Clean separation of concerns
- Data binding using CommunityToolkit.MVVM
- Async operations for responsive UI

### ✅ Professional UI
- Responsive layout that adapts to different screen sizes
- Theme-aware colors (light/dark mode support)
- Scrollable inputs and results
- Loading indicator during calculation
- Comprehensive error messaging

### ✅ Accurate Calculations
- Uses the actual `Appreciator` class from the core library
- Same calculation engine as the WinForms application
- Detailed formatted reports with professional formatting

## Running the Application

### From Visual Studio
1. Set `SepIraCalculatorMauiApp` as the startup project
2. Select your target platform (Windows, iOS, Android, or macOS)
3. Press F5 or click the Run button

### From Command Line
```bash
cd src/sep.ira/calculator.maui

# Windows
dotnet run -f net10.0-windows10.0.19041.0

# Android (requires Android SDK)
dotnet run -f net10.0-android

# iOS (requires macOS with Xcode)
dotnet run -f net10.0-ios

# macOS Catalyst
dotnet run -f net10.0-maccatalyst
```

## Usage Example

1. **Launch** the application
2. **Enter** your financial parameters in the input fields
3. **Click Calculate** to run the analysis
4. **Review** the detailed comparison report
5. **Click Reset** to start a new calculation

## Technical Details

### Dependencies
- `Microsoft.Maui.Controls` - .NET Maui framework
- `CommunityToolkit.Mvvm` (v8.4.2) - MVVM pattern implementation
- `cc.isr.Finance.Sep.Ira.Calculator` - Core calculator library

### Assembly Configuration
- **Namespace**: `cc.isr.Finance.Sep.Ira`
- **Assembly Name**: `Sep.Ira.Calculator.Maui.App`
- **Code Signing**: Enabled with keyPair.snk
- **CLS Compliant**: Yes

### Build Information
- **Target Framework**: .NET 10.0
- **Build Status**: ✅ 0 Errors, 18 Warnings (informational)

## Future Enhancements

Potential improvements for future versions:

1. **Data Persistence**
   - Save/load calculation scenarios
   - Export results to PDF or Excel

2. **Advanced Features**
   - Multiple scenario comparison
   - Chart visualizations
   - Sensitivity analysis

3. **Cross-Platform Enhancements**
   - iOS/Android specific optimizations
   - Platform-specific features (e.g., share, notifications)

4. **Accessibility**
   - Enhanced screen reader support
   - High contrast modes
   - Keyboard navigation

## Troubleshooting

### Build Issues
- If converters aren't found, ensure `xmlns:converters="clr-namespace:cc.isr.Finance.Sep.Ira.Converters"` is in XAML
- Ensure all projects build successfully first: `dotnet build src/sep.ira/SepIraCalculator.slnx`

### Runtime Issues
- Check that input values are within the valid ranges displayed in error messages
- Ensure the core calculator library (`cc.isr.Finance.Sep.Ira.Calculator`) is properly referenced

### UI Layout
- The scrollable layout adapts to smaller screens
- Results are displayed in a monospace font (Courier) for better formatting
- Input fields are responsive and reposition on narrow screens

## Support

For questions or issues:
1. Check the validation error messages for input validation issues
2. Verify calculations match the WinForms application
3. Report issues with the GitHub repository at: https://github.com/ATECoder/dn.data.finance

---

**Last Updated**: June 2026
**Status**: ✅ Production Ready
