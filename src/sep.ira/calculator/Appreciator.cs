using CommunityToolkit.Mvvm.ComponentModel;

namespace cc.isr.Finance.Sep.Ira;

/// <summary>   Estimates the Sep IRA appreciations. </summary>
/// <remarks>   2026-06-02. </remarks>
[CLSCompliant( false )]
public partial class Appreciator : ObservableObject
{
    #region " inputs "

    /// <summary>   Gets or sets the invested amount. </summary>
    /// <value> The invested amount. </value>
    [ObservableProperty]
    public partial decimal InvestedAmount { get; set; } = 10000.00m;

    /// <summary>   Gets or sets the initial age. </summary>
    /// <value> The initial age. </value>
    [ObservableProperty]
    public partial int InitialAge { get; set; } = 75;

    /// <summary>   Gets or sets the duration of the investment in years. </summary>
    /// <value> The investment duration. </value>
    [ObservableProperty]
    public partial int InvestmentDuration { get; set; } = 20;

    /// <summary>   Gets or sets the initial federal tax rate. </summary>
    /// <value> The initial federal tax rate. </value>
    [ObservableProperty]
    public partial decimal InitialFederalTaxRate { get; set; } = 35.0m;

    /// <summary>   Gets or sets the withdrawal federal tax rate. </summary>
    /// <value> The withdrawal federal tax rate. </value>
    [ObservableProperty]
    public partial decimal WithdrawalFederalTaxRate { get; set; } = 35.0m;

    /// <summary>   Gets or sets the initial state tax rate. </summary>
    /// <value> The initial state tax rate. </value>
    [ObservableProperty]
    public partial decimal InitialStateTaxRate { get; set; } = 9.3m;

    /// <summary>   Gets or sets the withdrawal state tax rate. </summary>
    /// <value> The withdrawal state tax rate. </value>
    [ObservableProperty]
    public partial decimal WithdrawalStateTaxRate { get; set; } = 9.3m;

    /// <summary>   Gets or sets the federal capital gains tax rate. </summary>
    /// <value> The federal capital gains tax rate. </value>
    [ObservableProperty]
    public partial decimal FederalCapitalGainsTaxRate { get; set; } = 25.0m;

    /// <summary>   Gets or sets the state capital gains tax rate. </summary>
    /// <value> The state capital gains tax rate. </value>
    [ObservableProperty]
    public partial decimal StateCapitalGainsTaxRate { get; set; } = 9.3m;

    /// <summary>   Gets or sets the annual inflation rate. </summary>
    /// <value> The annual inflation rate. </value>
    [ObservableProperty]
    public partial decimal AnnualInflationRate { get; set; } = 2.75m;

    /// <summary>   Gets or sets the annual growth rate. </summary>
    /// <value> The annual growth rate. </value>
    [ObservableProperty]
    public partial decimal AnnualGrowthRate { get; set; } = 7.0m;

    /// <summary>   Gets or sets the uniform lifetime table. </summary>
    /// <remarks> Based on IRS Publication 1406 2025. </remarks>
    /// <value> The uniform lifetime table. </value>
    public Dictionary<int, decimal> UniformLifetimeTable { get; set; } = new Dictionary<int, decimal>()
    {
        { 72, 27.4m },{ 73, 26.5m },{ 74, 25.5m },{ 75, 24.6m },{ 76, 23.7m },
        { 77, 22.9m },{ 78, 22.0m },{ 79, 21.1m },{ 80, 20.2m },{ 81, 19.4m },
        { 82, 18.5m },{ 83, 17.7m },{ 84, 16.8m },{ 85, 16.0m },{ 86, 15.2m },
        { 87, 14.4m },{ 88, 13.7m },{ 89, 12.9m },{90,12.2m},{91, 11.5m},{92, 10.8m},
        {93, 10.1m},{94, 9.5m },{95 ,8.9m },{96, 8.4m },{97, 7.8m },{98, 7.3m },{99 ,6.8m },
        {100, 6.4m},{101, 5.9m},{102, 5.5m},{103, 5.2m},{104, 4.9m},{105, 4.5m},{106, 4.5m},
        {107, 4.5m},{108, 4.4m},{ 109, 4.1m},{ 110, 3.9m},{111, 3.7m},{112, 3.4m },{113, 3.2m },
        {114 ,3.0m },{115 ,2.8m },{116 ,2.6m },{117 ,2.4m },{118, 2.2m},{119, 2.0m},{120, 1.9m}
    };

    #endregion

    #region " outputs "

    /// <summary>   Gets or sets the SEP IRA account balance - future value before tax. </summary>
    /// <value> The SEP IRA account balance - future value before tax. </value>
    [ObservableProperty]
    public partial decimal SepIraAccountBalance { get; private set; }

    /// <summary>   Gets or sets the capital account balance. </summary>
    /// <value> The capital account balance. </value>
    [ObservableProperty]
    public partial decimal CapitalAccountBalance { get; private set; }

    /// <summary>   Total Tax liability upon withdrawal. </summary>
    /// <value> The total tax liability. </value>
    [ObservableProperty]
    public partial decimal WithdrawalTaxLiability { get; private set; }

    /// <summary>   Gets or sets the initial tax liability. </summary>
    /// <value> The initial tax liability. </value>
    [ObservableProperty]
    public partial decimal InitialTaxLiability { get; private set; }

    /// <summary>   Federal Tax liability upon withdrawal. </summary>
    /// <value> The Federal tax liability. </value>
    [ObservableProperty]
    public partial decimal WithdrawalFederalTaxLiability { get; private set; }

    /// <summary>   Gets or sets the initial federal tax liability. </summary>
    /// <value> The initial federal tax liability. </value>
    [ObservableProperty]
    public partial decimal InitialFederalTaxLiability { get; private set; }

    /// <summary>   State Tax liability upon withdrawal. </summary>
    /// <value> The State tax liability. </value>
    [ObservableProperty]
    public partial decimal WithdrawalStateTaxLiability { get; private set; }

    /// <summary>   Gets or sets the initial state tax liability. </summary>
    /// <value> The initial state tax liability. </value>
    [ObservableProperty]
    public partial decimal InitialStateTaxLiability { get; private set; }

    /// <summary>   Gets or sets the capital gain. </summary>
    /// <value> The capital gain. </value>
    [ObservableProperty]
    public partial decimal CapitalGain { get; private set; }

    /// <summary>   Gets or sets the federal capital gains tax liability. </summary>
    /// <value> The federal capital gains tax liability. </value>
    [ObservableProperty]
    public partial decimal FederalCapitalGainsTaxLiability { get; private set; }

    /// <summary>   Gets or sets the state capital gains tax liability. </summary>
    /// <value> The state capital gains tax liability. </value>
    [ObservableProperty]
    public partial decimal StateCapitalGainsTaxLiability { get; private set; }

    /// <summary>   Gets or sets the net cash out value. </summary>
    /// <value> The net cash out value. </value>
    [ObservableProperty]
    public partial decimal NetCashOutValue { get; private set; }

    /// <summary>   Gets or sets the final age. </summary>
    /// <value> The final age. </value>
    [ObservableProperty]
    public partial decimal FinalAge { get; private set; }

    /// <summary>   Gets or sets the discounted (present value) federal taxes paid. </summary>
    /// <value> The federal taxes paid. </value>
    [ObservableProperty]
    public partial decimal DiscountedFederalTaxesPaid { get; private set; }

    /// <summary>   Gets or sets the discounted state taxes paid. </summary>
    /// <value> The discounted state taxes paid. </value>
    [ObservableProperty]
    public partial decimal DiscountedStateTaxesPaid { get; private set; }

    /// <summary>   Gets or sets the discounted taxes paid. </summary>
    /// <value> The discounted taxes paid. </value>
    [ObservableProperty]
    public partial decimal DiscountedTaxesPaid { get; private set; }

    #endregion

    #region " calculations "

    /// <summary>   Calculates the future value. </summary>
    /// <remarks>   Calculates the future value of a capital investment after applying
    ///             the present tax rates to the initial investedAmount and then applying 
    ///             the assumed annual growth rate for the specified for number of 
    ///             years and then applying the future capital gains and state taxes.
    /// </remarks>
    public void CalculateFutureValue()
    {
        this.InitialFederalTaxLiability = this.InvestedAmount * (this.InitialFederalTaxRate / 100.0m);
        this.InitialStateTaxLiability = this.InvestedAmount * (this.InitialStateTaxRate / 100.0m);
        this.InitialTaxLiability = this.InitialFederalTaxLiability + this.InitialStateTaxLiability;
        decimal initialCapital = this.InvestedAmount * (1 - ((this.InitialFederalTaxRate + this.InitialStateTaxRate) / 100.0m));

        decimal growthRate = 1 + (this.AnnualGrowthRate / 100.0m);

        // zero irelevant outcomes
        this.DiscountedFederalTaxesPaid = 0;
        this.DiscountedStateTaxesPaid = 0;
        this.DiscountedTaxesPaid = 0;
        this.SepIraAccountBalance = 0;
        this.WithdrawalFederalTaxLiability = 0;
        this.WithdrawalStateTaxLiability = 0;

        // Future capital is the growth of the initial capital investment,
        // which is subject to capital gains taxes upon withdrawal.
        this.CapitalAccountBalance = initialCapital * ( decimal ) Math.Pow( ( double ) growthRate, this.InvestmentDuration );
        this.CapitalGain = this.CapitalAccountBalance - initialCapital;
        this.FederalCapitalGainsTaxLiability = this.CapitalGain * (this.FederalCapitalGainsTaxRate / 100.0m);
        this.StateCapitalGainsTaxLiability = this.CapitalGain * (this.StateCapitalGainsTaxRate / 100.0m);
        this.WithdrawalTaxLiability = this.FederalCapitalGainsTaxLiability + this.StateCapitalGainsTaxLiability;
        this.NetCashOutValue = this.CapitalAccountBalance - this.WithdrawalTaxLiability;

        // Display the results
        // Console.WriteLine( $"- Simple Capital Investment -" );
        // Console.WriteLine( $"            Initial Investment: {this.InvestedAmount:C0}" );
        // Console.WriteLine( $"                      Duration: {this.InvestmentDuration} years" );
        // Console.WriteLine( $"         Assumed Annual Growth: {this.AnnualGrowthRate:F1}%" );
        // Console.WriteLine( $"      Initial Federal Tax Rate: {this.InitialFederalTaxRate:F1}%" );
        // Console.WriteLine( $"        Initial State Tax Rate: {this.InitialStateTaxRate:F1}%" );
        // Console.WriteLine( $"Federal Capital Gains Tax Rate: {this.FederalCapitalGainsTaxRate:F1}%" );
        // Console.WriteLine( $"  State Capital Gains Tax Rate: {this.StateCapitalGainsTaxRate:F1}%" );
        // Console.WriteLine( $"         Initial Tax Liability: {this.InitialTaxLiability:C0}" );
        // Console.WriteLine( $"       Pre-Tax Account Balance: {this.CapitalAccountBalance:C0}" );
        // Console.WriteLine( $"  State Tax Owed on Withdrawal: {this.StateCapitalGainsTaxLiability:C0}" );
        // Console.WriteLine( $"Federal Tax Owed on Withdrawal: {this.FederalCapitalGainsTaxLiability:C0}" );
        // Console.WriteLine( $"        Tax Owed on Withdrawal: {this.WithdrawalTaxLiability:C0}" );
        // Console.WriteLine( $"            Net Cash-Out Value: {this.NetCashOutValue:C0}" );
    }

    /// <summary>   Calculates the future value of SEP IRA investment with RMD. </summary>
    /// <remarks>   2026-06-02. </remarks>
    /// <param name="debug">    (Optional) True output debug messages. </param>
    public void CalculateFutureValueSepIraWithRmd( bool debug = false )
    {
        // no taxes are paid on the initial investedAmount amount, so we start with the full investedAmount
        // as the initial capital for growth calculations
        this.InitialFederalTaxLiability = 0;
        this.InitialStateTaxLiability = 0;
        this.InitialTaxLiability = this.InitialFederalTaxLiability + this.InitialStateTaxLiability;

        // capital refers to the after tax RMD that is invested in a capital gains account.
        // This is tracked separately from the growth of the initial investedAmount investment
        // because it is subject to capital gains taxes upon withdrawal, whereas the growth of
        // the initial investedAmount investment is subject to ordinary income taxes upon withdrawal.
        decimal initialCapital = 0;

        // the discounted taxes paid on the RMD are tracked separately from the tax liability upon
        // withdrawal and are discounted back to the present value using the inflation rate to reflect
        // the time value of money.
        this.DiscountedFederalTaxesPaid = 0;
        this.DiscountedStateTaxesPaid = 0;

        // Capital Account Balance tracks the growth of the capital invested from the RMDs
        this.CapitalAccountBalance = 0;

        // This is the future value of the SEP IRA account before taxes, which grows based on the
        // initial investedAmount and the growth rate of the capital invested in the SEP IRA.
        this.SepIraAccountBalance = this.InvestedAmount;

        decimal growthRate = 1 + (this.AnnualGrowthRate / 100.0m);
        for ( int age = this.InitialAge; age < this.InitialAge + this.InvestmentDuration; age++ )
        {
            int endYearAge = age + 1;
            this.FinalAge = endYearAge;

            // RMD is based on the account balance at the end of the previous year,
            // so we calculate that first before applying the RMD for the current year
            decimal previousYearBalance = this.SepIraAccountBalance;
            decimal previousYearCapital = this.CapitalAccountBalance;

            // it is assumed that both the SEP IRA and Capital accounts grow at the same rate
            // during the year, so we apply the growth rate to both the SEP IRA account balance
            // and the capital account balance
            decimal endYearBalance = previousYearBalance * growthRate;
            decimal endYearCapital = previousYearCapital * growthRate;

            if ( age >= 72 )
            {
                // RMD = Account Balance at End of Previous Year / Uniform Lifetime Table Value for InitialAge
                decimal rmd = previousYearBalance / this.UniformLifetimeTable[age];

                // The end of year balance of the SEP IRA account is reduced by the RMD amount,
                // which is withdrawn from the account.
                endYearBalance -= rmd;

                // the RMD amount is subject to ordinary income taxes at the present tax rates.
                // it is assumed here that the taxes on the RMD remain unchanged until the end of 
                // the calculation period.
                decimal federalTax = rmd * (this.InitialFederalTaxRate / 100.0m);
                decimal stateTax = rmd * (this.InitialStateTaxRate / 100.0m);

                // The after tax RMD is invested back into the capital account,
                // so we calculate the capital invested as the RMD minus the taxes paid on the RMD.
                // This capital is subject to capital gains taxes upon withdrawal,
                // so we track it separately from the growth of the initial capital investment.
                decimal capitalInvested = rmd - federalTax - stateTax;
                endYearCapital += capitalInvested;

                // The discounted taxes paid on the RMD are calculated by discounting 
                // the federal and state taxes paid back to the present value using the inflation rate.
                this.DiscountedFederalTaxesPaid += federalTax / ( decimal ) Math.Pow( ( double ) (1 + (this.AnnualInflationRate / 100.0m)), endYearAge - this.InitialAge );
                this.DiscountedStateTaxesPaid += stateTax / ( decimal ) Math.Pow( ( double ) (1 + (this.AnnualInflationRate / 100.0m)), endYearAge - this.InitialAge );
                this.DiscountedTaxesPaid = this.DiscountedFederalTaxesPaid + this.DiscountedStateTaxesPaid;
                if ( debug )
                {
                    Console.WriteLine( $"RMD: {rmd:C0}, Capital Invested: {capitalInvested:C0}" );
                }
            }

            if ( debug )
            {
                Console.WriteLine( $"InitialAge: {age}, SEP IRA: {this.SepIraAccountBalance:C0}" );
                Console.Write( $", Capital: {this.CapitalAccountBalance:C0}" );
                Console.Write( $", Discounted Fed. Taxes Paid: {this.DiscountedFederalTaxesPaid:C0}" );
                Console.Write( $", Discounted State Taxes Paid: {this.DiscountedStateTaxesPaid:C0}" );
            }

            this.CapitalAccountBalance = endYearCapital;
            this.SepIraAccountBalance = endYearBalance;
            this.CapitalGain = this.CapitalAccountBalance - initialCapital;

            // the federal tax liability upon withdrawal is based on the capital gains tax liability
            // plus the taxes owed on the SEP IRA account balance, which is taxed as ordinary income upon withdrawal.
            this.WithdrawalFederalTaxLiability = this.SepIraAccountBalance * (this.WithdrawalFederalTaxRate / 100.0m);
            this.FederalCapitalGainsTaxLiability = this.CapitalGain * (this.FederalCapitalGainsTaxRate / 100.0m);

            // for the state tax liability both the capital gain and the SEP IRA balance are taxed as ordinary income.
            this.WithdrawalStateTaxLiability = this.SepIraAccountBalance * (this.WithdrawalStateTaxRate / 100.0m);
            this.StateCapitalGainsTaxLiability = this.CapitalGain * (this.StateCapitalGainsTaxRate / 100.0m);

            // the total tax liability upon withdrawal is the sum of the federal and state tax liabilities.
            this.WithdrawalTaxLiability = this.WithdrawalFederalTaxLiability + this.FederalCapitalGainsTaxLiability +
                this.WithdrawalStateTaxLiability + this.StateCapitalGainsTaxLiability;

            // the net cash-out value is the total account balance (SEP IRA + Capital) minus
            // the total tax liability upon withdrawal.
            this.NetCashOutValue = this.CapitalAccountBalance + this.SepIraAccountBalance - this.WithdrawalTaxLiability;
        }

        // Display the results
        // Console.WriteLine( $"--- Capital Investment using SEP IRA ---" );
        // Console.WriteLine( $"      Initial Investment: {this.InvestedAmount:C0}" );
        // Console.WriteLine( $"                Duration: {this.InvestmentDuration} years" );
        // Console.WriteLine( $"   Assumed Annual Growth: {this.AnnualGrowthRate:F1}%" );
        // Console.WriteLine( $"Initial Federal Tax Rate: {this.InitialFederalTaxRate:F1}%" );
        // Console.WriteLine( $"  Initial State Tax Rate: {this.InitialStateTaxRate:F1}%" );
        // Console.WriteLine( $"  Final Federal Tax Rate: {this.WithdrawalFederalTaxRate:F1}%" );
        // Console.WriteLine( $"    Final State Tax Rate: {this.WithdrawalStateTaxRate:F1}%" );
        // Console.WriteLine( $"  Capital Gains Tax Rate: {this.FederalCapitalGainsTaxRate:F1}%" );
        // Console.WriteLine( $"          Inflation Rate: {this.AnnualInflationRate:F1}%" );
        // Console.WriteLine( $"             Initial Age: {this.InitialAge}" );
        // Console.WriteLine( $"-----------------------------------" );
        // Console.WriteLine( $"         Initial Tax Liability: {this.InitialTaxLiability:C0}" );
        // Console.WriteLine( $"  Discounted Taxes Paid on RMD: {this.DiscountedFederalTaxesPaid + this.DiscountedStateTaxesPaid:C0}" );
        // Console.WriteLine( $"-----------------------------------" );
        // Console.WriteLine( $"       SEP IRA Account Balance: {this.SepIraAccountBalance:C0}" );
        // Console.WriteLine( $"                  Capital Gain: {this.CapitalGain:C0}" );
        // Console.WriteLine( $" Pre-Tax Total Account Balance: {this.CapitalGain + this.SepIraAccountBalance:C0}" );
        // Console.WriteLine( $"         State Income Tax Owed: {this.WithdrawalStateTaxLiability:C0}" );
        // Console.WriteLine( $"       Federal Income Tax Owed: {this.WithdrawalFederalTaxLiability:C0}" );
        // Console.WriteLine( $"  State Capital Gains Tax Owed: {this.StateCapitalGainsTaxLiability:C0}" );
        // Console.WriteLine( $"Federal Capital Gains Tax Owed: {this.FederalCapitalGainsTaxLiability:C0}" );
        // Console.WriteLine( $"        Tax Owed on Withdrawal: {this.WithdrawalTaxLiability:C0}" );
        // Console.WriteLine( $"            Net Cash-Out Value: {this.NetCashOutValue:C0}" );
        // Console.WriteLine( $"                     Final Age: {this.FinalAge}" );
    }

    #endregion
}
