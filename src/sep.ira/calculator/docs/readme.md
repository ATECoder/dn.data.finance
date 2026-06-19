# ISR SEP IRA Calculator<sub>&trade;</sub> Library:

This library calculates the future value of Capital invested in a SEP IRA account or in a capital gains account.

## The Uniform Lifetime Table (IRS Table III)

The Uniform Lifetime Table (IRS [2025 Uniform Lifetime Table]) is the standard life expectancy table
used by most IRA owners to calculate their Required Minimum Distributions (RMDs).

### Who Should Use This Table?

The Uniform Lifetime Table is used by:
* An unmarried IRA owner; or
* A married owner whose spouse is not more than 10 years younger; or
* A married owner whose spouse is not the sole beneficiary of the retirement account.

Note: If the owner's spouse is the sole beneficiary and is more than 10 years younger than the owner, then the Joint Life and Last Survivor Table must be used instead. This library uses the [2025 Uniform Lifetime Table]. 

### How to Calculate Your RMD

The Required Minimum Distribution (RMD) equals to the tax-deferred retirement account's ending balance on December 31st of the previous year divided by the "Distribution Period"
factor that corresponds to the owner's age or the owner's birthday in the current tax year.

### Uniform Lifetime Table (Table III) Values

Below is the [2025 Uniform Lifetime Table] distribution periods.

| Age | Distribution Period | | Age | Distribution Period | | Age | Distribution Period |
| :--- | :--- | | :--- | :--- | | :--- | :--- |
| 72 | 27.4 | | 90 | 12.2 | | 108 | 4.4 |
| 73 | 26.5 | | 91 | 11.5 | | 109 | 4.1 |
| 74 | 25.5 | | 92 | 10.8 | | 110 | 3.9 |
| 75 | 24.6 | | 93 | 10.1 | | 111 | 3.7 |
| 76 | 23.7 | | 94 | 9.5 | | 112 | 3.4 |
| 77 | 22.9 | | 95 | 8.9 | | 113 | 3.2 |
| 78 | 22.0 | | 96 | 8.4 | | 114 | 3.0 |
| 79 | 21.1 | | 97 | 7.8 | | 115 | 2.8 |
| 80 | 20.2 | | 98 | 7.3 | | 116 | 2.6 |
| 81 | 19.4 | | 99 | 6.8 | | 117 | 2.4 |
| 82 | 18.5 | | 100 | 6.4 | | 118 | 2.2 |
| 83 | 17.7 | | 101 | 5.9 | | 119 | 2.0 |
| 84 | 16.8 | | 102 | 5.5 | | 120 | 1.9 |
| 85 | 16.0 | | 103 | 5.2 | | | |
| 86 | 15.2 | | 104 | 4.9 | | | |
| 87 | 14.4 | | 105 | 4.5 | | | |
| 88 | 13.7 | | 106 | 4.5 | | | |
| 89 | 12.9 | | 107 | 4.5 | | | |


## Critical Financial Context

### Tax Treatment

In a SEP IRA, your investments grow tax-deferred.
When you withdraw the money, the entire amount (both the initial deposit and the growth) is taxed at your ordinary income tax rate.

### Capital Gains

Capital gains tax rates do not apply to traditional or SEP IRA distributions.
The variable is omitted from the math because the IRS treats all standard retirement account distributions as ordinary income.

### RMDs (Required Minimum Distributions)

IRS regulations require account holders to start taking Mandatory Minimum Distributions (RMDs) annually by age 73 (or 75 depending on birth year legislation updates).


## Feedback

SEP IRA Calculator is released as open source under the MIT license.
Bug reports and contributions are welcome at the [Repository].

&copy;  2026 Integrated Scientific Resources, Inc. All rights reserved.

[Repository]: https://www.github.com/atecoder/dn.data.finance
[2025 Uniform Lifetime Table]: https://www.irs.gov/publications/p590b#en_US_2025_publink100090310

