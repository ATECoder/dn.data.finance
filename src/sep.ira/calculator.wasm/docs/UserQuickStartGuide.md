# SEP IRA Calculator - User Quick Start Guide

## How to Use the Calculator

### 1. **Enter Your Investment Details**
   - **Invested Amount**: The initial amount you're investing (e.g., $10,000)
   - **Initial Age**: Your age when making the investment (e.g., 75)
   - **Investment Duration**: How many years you plan to hold the investment (e.g., 20 years)
   - **Annual Growth Rate**: Expected annual return as a percentage (e.g., 7%)

### 2. **Set Tax Rates**

   **Federal Taxes:**
   - Initial Federal Tax Rate: Tax rate on initial investment
   - Withdrawal Federal Tax Rate: Tax rate on withdrawal
   - Federal Capital Gains Tax Rate: Tax rate on gains

   **State Taxes:**
   - Initial State Tax Rate: Tax rate on initial investment
   - Withdrawal State Tax Rate: Tax rate on withdrawal
   - State Capital Gains Tax Rate: Tax rate on gains

### 3. **Economic Factors**
   - **Annual Inflation Rate**: Expected annual inflation rate (e.g., 2.75%)

### 4. **Click Calculate**
   The calculator will compute:
   - **SEP IRA Account Balance**: Pre-tax balance of your SEP IRA
   - **Capital Account Balance**: After-tax RMD investments
   - **Taxes Paid Over Time**: Discounted value of RMDs
   - **Net Cash-Out Value**: What you take home after all taxes

### 5. **Review Results**
The results table shows:
- Initial investment and age
- Account balances at withdrawal
- Tax liabilities
- **Final Net Cash-Out Value**: The amount you actually keep

## Default Values

The calculator comes pre-populated with reasonable defaults:
- Investment: $10,000
- Initial Age: 75
- Duration: 20 years
- Growth Rate: 7%
- Federal Tax Rate: 35%
- State Tax Rate: 9.3%
- Inflation Rate: 2.75%

You can modify any of these values and recalculate.

## Sharing the Calculator

This is a web-based application that can be:
1. **Shared via URL** - Send the deployment link to anyone with internet access
2. **Run Locally** - Clone the repository and run locally during development
3. **Deployed to Azure** - Published to a public URL for remote access

## Technical Details

- **Built With**: Blazor WebAssembly (.NET 10)
- **Framework**: Bootstrap 5.3 (CDN-hosted)
- **Calculations**: Pure C# (no external dependencies)
- **Browser Support**: Chrome, Firefox, Edge, Safari

---

**Happy calculating!** 🎉
