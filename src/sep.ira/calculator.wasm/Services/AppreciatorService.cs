namespace cc.isr.Finance.Sep.Ira.WebAssembly.Services;

/// <summary>
/// WASM-compatible calculator service that does NOT depend on the MVVM Toolkit Appreciator.
/// This reimplements the calculations directly to avoid source generator issues in WASM.
/// </summary>
public class AppreciatorService
{
    /// <summary>
    /// Performs a simple capital investment calculation.
    /// </summary>
    public static AppreciationResult CalculateSimpleInvestment(
        decimal investedAmount,
        decimal annualGrowthRate,
        int investmentDuration,
        decimal initialFederalTaxRate,
        decimal initialStateTaxRate,
        decimal federalCapitalGainsTaxRate,
        decimal stateCapitalGainsTaxRate )
    {
        try
        {
            AppreciationResult result = new() { Success = true };

            // Calculate initial taxes
            decimal initialFederalTax = investedAmount * (initialFederalTaxRate / 100.0m);
            decimal initialStateTax = investedAmount * (initialStateTaxRate / 100.0m);
            decimal initialTaxLiability = initialFederalTax + initialStateTax;

            // Capital after taxes
            decimal capitalAfterTax = investedAmount * (1 - ((initialFederalTaxRate + initialStateTaxRate) / 100.0m));

            // Future value calculation
            decimal growthRate = 1 + (annualGrowthRate / 100.0m);
            decimal futureBalance = capitalAfterTax * ( decimal ) System.Math.Pow( ( double ) growthRate, investmentDuration );

            // Capital gain
            decimal capitalGain = futureBalance - capitalAfterTax;

            // Taxes on withdrawal
            decimal federalCapitalGainsTax = capitalGain * (federalCapitalGainsTaxRate / 100.0m);
            decimal stateCapitalGainsTax = capitalGain * (stateCapitalGainsTaxRate / 100.0m);
            decimal withdrawalTax = federalCapitalGainsTax + stateCapitalGainsTax;

            // Net cash-out value
            decimal netCashOut = futureBalance - withdrawalTax;

            result.InitialInvestment = investedAmount;
            result.InitialTaxLiability = initialTaxLiability;
            result.PreTaxAccountBalance = futureBalance;
            result.CapitalGain = capitalGain;
            result.WithdrawalTaxLiability = withdrawalTax;
            result.NetCashOutValue = netCashOut;

            return result;
        }
        catch ( Exception ex )
        {
            return new AppreciationResult
            {
                Success = false,
                ErrorMessage = $"{ex.GetType().Name}: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Performs a SEP IRA investment calculation with RMD.
    /// </summary>
    public static AppreciationResult CalculateSepIraWithRmd(
        decimal investedAmount,
        int initialAge,
        int investmentDuration,
        decimal annualGrowthRate,
        decimal annualInflationRate,
        decimal initialFederalTaxRate,
        decimal initialStateTaxRate,
        decimal withdrawalFederalTaxRate,
        decimal withdrawalStateTaxRate,
        decimal federalCapitalGainsTaxRate,
        decimal stateCapitalGainsTaxRate )
    {
        try
        {
            AppreciationResult result = new()
            {
                Success = true,             // Store input values
                InitialInvestment = investedAmount,
                InitialAge = initialAge,
                FinalAge = initialAge + investmentDuration,
                InvestmentDuration = investmentDuration,
                AnnualGrowthRate = annualGrowthRate,
                AnnualInflationRate = annualInflationRate,
                InitialFederalTaxRate = initialFederalTaxRate,
                InitialStateTaxRate = initialStateTaxRate,
                WithdrawalFederalTaxRate = withdrawalFederalTaxRate,
                WithdrawalStateTaxRate = withdrawalStateTaxRate,
                FederalCapitalGainsTaxRate = federalCapitalGainsTaxRate,
                StateCapitalGainsTaxRate = stateCapitalGainsTaxRate
            };

            // ===== SIMPLE INVESTMENT CALCULATION =====
            decimal simpleInitialFederalTax = investedAmount * (initialFederalTaxRate / 100.0m);
            decimal simpleInitialStateTax = investedAmount * (initialStateTaxRate / 100.0m);
            decimal simpleInitialTaxLiability = simpleInitialFederalTax + simpleInitialStateTax;
            decimal simpleCapitalAfterTax = investedAmount * (1 - ((initialFederalTaxRate + initialStateTaxRate) / 100.0m));

            decimal growthRate = 1 + (annualGrowthRate / 100.0m);
            decimal simpleFutureBalance = simpleCapitalAfterTax * ( decimal ) System.Math.Pow( ( double ) growthRate, investmentDuration );
            decimal simpleCapitalGain = simpleFutureBalance - simpleCapitalAfterTax;
            decimal simpleFederalCapitalGainsTax = simpleCapitalGain * (federalCapitalGainsTaxRate / 100.0m);
            decimal simpleStateCapitalGainsTax = simpleCapitalGain * (stateCapitalGainsTaxRate / 100.0m);
            decimal simpleWithdrawalTax = simpleFederalCapitalGainsTax + simpleStateCapitalGainsTax;
            decimal simpleNetCashOut = simpleFutureBalance - simpleWithdrawalTax;

            result.InitialTaxLiability = simpleInitialTaxLiability;
            result.PreTaxAccountBalance = simpleCapitalAfterTax;
            result.SimpleInvestmentBalance = simpleFutureBalance;
            result.CapitalGain = simpleCapitalGain;
            result.SimpleInvestmentWithdrawalTax = simpleWithdrawalTax;
            result.SimpleInvestmentNetCashOut = simpleNetCashOut;

            // ===== SEP IRA CALCULATION =====
            // IRS Uniform Lifetime Table (simplified)
            Dictionary<int, decimal> uniformLifetimeTable = new()
            {
                { 72, 27.4m }, { 73, 26.5m }, { 74, 25.5m }, { 75, 24.6m }, { 76, 23.7m },
                { 77, 22.9m }, { 78, 22.0m }, { 79, 21.1m }, { 80, 20.2m }, { 81, 19.4m },
                { 82, 18.5m }, { 83, 17.7m }, { 84, 16.8m }, { 85, 16.0m }, { 86, 15.2m },
                { 87, 14.4m }, { 88, 13.7m }, { 89, 12.9m }, { 90, 12.2m }, { 91, 11.5m },
                { 92, 10.8m }, { 93, 10.1m }, { 94, 9.5m }, { 95, 8.9m }, { 96, 8.4m },
                { 97, 7.8m }, { 98, 7.3m }, { 99, 6.8m }, { 100, 6.4m }, { 101, 5.9m },
                { 102, 5.5m }, { 103, 5.2m }, { 104, 4.9m }, { 105, 4.5m }, { 106, 4.5m },
                { 107, 4.5m }, { 108, 4.4m }, { 109, 4.1m }, { 110, 3.9m }, { 111, 3.7m },
                { 112, 3.4m }, { 113, 3.2m }, { 114, 3.0m }, { 115, 2.8m }, { 116, 2.6m },
                { 117, 2.4m }, { 118, 2.2m }, { 119, 2.0m }, { 120, 1.9m }
            };

            // Initial state: no taxes paid on initial investment
            decimal sepIraBalance = investedAmount;
            decimal capitalBalance = 0m;
            decimal discountedFederalTaxesPaid = 0m;
            decimal discountedStateTaxesPaid = 0m;

            decimal inflationFactor = 1 + (annualInflationRate / 100.0m);

            // Calculate year-by-year
            int finalAge = initialAge;
            for ( int age = initialAge; age < initialAge + investmentDuration; age++ )
            {
                finalAge = age + 1;

                // Apply growth
                decimal previousBalance = sepIraBalance;
                decimal previousCapital = capitalBalance;

                sepIraBalance *= growthRate;
                capitalBalance *= growthRate;

                // Calculate RMD if age >= 72
                if ( age >= 72 && uniformLifetimeTable.TryGetValue( age, out decimal rmdDivisor ) )
                {
                    decimal rmd = previousBalance / rmdDivisor;
                    sepIraBalance -= rmd;

                    // Apply taxes to RMD
                    decimal federalTax = rmd * (initialFederalTaxRate / 100.0m);
                    decimal stateTax = rmd * (initialStateTaxRate / 100.0m);

                    // Invest after-tax portion
                    decimal capitalInvested = rmd - federalTax - stateTax;
                    capitalBalance += capitalInvested;

                    // Track discounted taxes
                    int yearsFromNow = finalAge - initialAge;
                    decimal discount = ( decimal ) System.Math.Pow( ( double ) inflationFactor, yearsFromNow );
                    discountedFederalTaxesPaid += federalTax / discount;
                    discountedStateTaxesPaid += stateTax / discount;
                }
            }

            // Calculate final tax liabilities
            decimal sepIraCapitalGain = capitalBalance;
            decimal federalIncomeWithdrawal = sepIraBalance * (withdrawalFederalTaxRate / 100.0m);
            decimal stateIncomeWithdrawal = sepIraBalance * (withdrawalStateTaxRate / 100.0m);
            decimal federalCapitalGainWithdrawal = sepIraCapitalGain * (federalCapitalGainsTaxRate / 100.0m);
            decimal stateCapitalGainWithdrawal = sepIraCapitalGain * (stateCapitalGainsTaxRate / 100.0m);

            decimal totalTaxWithdrawal = federalIncomeWithdrawal + federalCapitalGainWithdrawal +
                                         stateIncomeWithdrawal + stateCapitalGainWithdrawal;

            decimal netCashOut = sepIraBalance + capitalBalance - totalTaxWithdrawal;

            result.FinalAge = finalAge;
            result.SepIraAccountBalance = sepIraBalance;
            result.CapitalAccountBalance = capitalBalance;
            result.DiscountedTaxesPaid = discountedFederalTaxesPaid + discountedStateTaxesPaid;
            result.WithdrawalTaxLiability = totalTaxWithdrawal;
            result.NetCashOutValue = netCashOut;

            return result;
        }
        catch ( Exception ex )
        {
            return new AppreciationResult
            {
                Success = false,
                ErrorMessage = $"{ex.GetType().Name}: {ex.Message}"
            };
        }
    }
}

/// <summary>
/// Data transfer object for appreciation calculation results.
/// </summary>
public class AppreciationResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }

    // Common Input Values
    public decimal InitialInvestment { get; set; }
    public int InitialAge { get; set; }
    public int FinalAge { get; set; }
    public int InvestmentDuration { get; set; }
    public decimal AnnualGrowthRate { get; set; }
    public decimal AnnualInflationRate { get; set; }
    public decimal InitialFederalTaxRate { get; set; }
    public decimal InitialStateTaxRate { get; set; }
    public decimal WithdrawalFederalTaxRate { get; set; }
    public decimal WithdrawalStateTaxRate { get; set; }
    public decimal FederalCapitalGainsTaxRate { get; set; }
    public decimal StateCapitalGainsTaxRate { get; set; }

    // Simple Investment Results
    public decimal InitialTaxLiability { get; set; }
    public decimal PreTaxAccountBalance { get; set; }
    public decimal SimpleInvestmentBalance { get; set; }
    public decimal CapitalGain { get; set; }
    public decimal SimpleInvestmentWithdrawalTax { get; set; }
    public decimal SimpleInvestmentNetCashOut { get; set; }

    // SEP IRA Results
    public decimal SepIraAccountBalance { get; set; }
    public decimal CapitalAccountBalance { get; set; }
    public decimal WithdrawalTaxLiability { get; set; }
    public decimal NetCashOutValue { get; set; }
    public decimal DiscountedTaxesPaid { get; set; }
}
