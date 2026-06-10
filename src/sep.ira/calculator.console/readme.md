# ISR SEP IRA Calculator<sub>&trade;</sub> Console Application:

This application calculates the future value of Capital invested in a SEP IRA account or in a capital gains account.

## Command line

Usage: 
```
SepIraCalculator.exe InitialCapital InitialAge DurationInYears InitialFederalTaxRate WithdrawlFederalTaxRate InitialStateTaxRate WithdrawlStateTaxRate WithdrawlCapitalGainsTax InflationRate AverageInvestmentReturnRate
```

e.g., 
```
SepIraCalculator.exe 50000 74 20 35.0 35.0 9.3 9.3 25.0 2.75 7.0"
```

## Critical Financial Context

### Tax Treatment

In a SEP IRA, investments grow tax-deferred.
When money is withdrawn form a SEP IRA, the entire amount withdrawn (both the initial deposit and the growth) is taxed at the ordinary income tax rate.

### Capital Gains

Capital gains tax rates do not apply to traditional or SEP IRA distributions.

However, any capital that is reinvested after distributing taxed funds from the SEP IRA is subject to capital gains tax.

### RMDs (Required Minimum Distributions)

IRS regulations require account holders to start taking Mandatory Minimum Distributions (RMDs) annually by age 73 (or 75 depending on birth year legislation updates).

## Feedback

SEP IRA Calculator is released as open source under the MIT license.
Bug reports and contributions are welcome at the [Repository].

&copy;  2026 Integrated Scientific Resources, Inc. All rights reserved.

[Repository]: https://www.github.com/atecoder/dn.data.finance
[2025 Uniform Lifetime Table]: https://www.irs.gov/publications/p590b#en_US_2025_publink100090310
