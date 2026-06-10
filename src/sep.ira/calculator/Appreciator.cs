using CommunityToolkit.Mvvm.ComponentModel;

namespace cc.isr.Finance.Sep.Ira;

/// <summary>   Estimates the Sep IRA appreciations. </summary>
/// <remarks>   2026-06-02. </remarks>
[CLSCompliant( false )]
public partial class Appreciator : ObservableObject
{
    #region " inputs "

    /// <summary>   Gets or sets the investment principal. </summary>
    /// <value> The principal. </value>
    [ObservableProperty]
    public partial double Principal { get; set; } = 10000.00;

    /// <summary>   Gets or sets initial the age. </summary>
    /// <value> The initial age. </value>
    [ObservableProperty]
    public partial int InitialAge { get; set; } = 75;

    /// <summary>   Investment duration (x years) </summary>
    /// <value> The years. </value>
    [ObservableProperty]
    public partial int Years { get; set; } = 20;

    /// <summary>   Gets or sets the present federal tax rate. </summary>
    /// <value> The present federal tax rate. </value>
    [ObservableProperty]
    public partial double PresentFederalTaxRate { get; set; } = 35.0;

    /// <summary>   Gets or sets the future federal tax rate. </summary>
    /// <value> The future federal tax rate. </value>
    [ObservableProperty]
    public partial double FutureFederalTaxRate { get; set; } = 35.0;

    /// <summary>   Gets or sets the present state tax rate. </summary>
    /// <value> The present state tax rate. </value>
    [ObservableProperty]
    public partial double PresentStateTaxRate { get; set; } = 9.3;

    /// <summary>   Gets or sets the future state tax rate. </summary>
    /// <value> The future state tax rate. </value>
    [ObservableProperty]
    public partial double FutureStateTaxRate { get; set; } = 9.3;

    /// <summary>   Gets or sets the capital gains tax rate. </summary>
    /// <value> The capital gains tax rate. </value>
    [ObservableProperty]
    public partial double CapitalGainsTaxRate { get; set; } = 25.0;

    /// <summary>   Gets or sets the inflation rate. </summary>
    /// <value> The inflation rate. </value>
    [ObservableProperty]
    public partial double InflationRate { get; set; } = 2.75;

    // <summary> Assumed annual investment growth rate. </summary>
    [ObservableProperty]
    public partial double AnnualReturn { get; set; } = 7.0;

    /// <summary>   Gets or sets the uniform lifetime table. </summary>
    /// <remarks> Based on IRS Publication 1406 2025. </remarks>
    /// <value> The uniform lifetime table. </value>
    public Dictionary<int, double> UniformLifetimeTable { get; set; } = new Dictionary<int, double>()
    {
        { 72, 27.4 },{ 73, 26.5 },{ 74, 25.5 },{ 75, 24.6 },{ 76, 23.7 },
        { 77, 22.9 },{ 78, 22.0 },{ 79, 21.1 },{ 80, 20.2 },{ 81, 19.4 },
        { 82, 18.5 },{ 83, 17.7 },{ 84, 16.8 },{ 85, 16.0 },{ 86, 15.2 },
        { 87, 14.4 },{ 88, 13.7 },{ 89, 12.9 },{90,12.2},{91, 11.5},{92, 10.8},
        {93, 10.1},{94, 9.5 },{95 ,8.9 },{96, 8.4 },{97, 7.8 },{98, 7.3 },{99 ,6.8 },
        {100, 6.4},{101, 5.9},{102, 5.5},{103, 5.2},{104, 4.9},{105, 4.5},{106, 4.5},
        {107, 4.5},{108, 4.4},{ 109, 4.1},{ 110, 3.9},{111, 3.7},{112, 3.4 },{113, 3.2 },
        {114 ,3.0 },{115 ,2.8 },{116 ,2.6 },{117 ,2.4 },{118, 2.2},{119, 2.0},{120, 1.9}
    };

    #endregion

    #region " outputs "

    /// <summary>   Gets or sets the growth rate. </summary>
    /// <value> The growth rate. </value>
    [ObservableProperty]
    public partial double GrowthRate { get; private set; }

    /// <summary>   Gets or sets the SEP IRA account balance - future value before tax. </summary>
    /// <value> The SEP IRA account balance - future value before tax. </value>
    [ObservableProperty]
    public partial double SepIraAccountBalance { get; private set; }

    /// <summary>   Gets or sets the capital account balance. </summary>
    /// <value> The capital account balance. </value>
    [ObservableProperty]
    public partial double CapitalAccountBalance { get; private set; }

    /// <summary>   Total Tax liability upon withdrawal. </summary>
    /// <value> The total tax liability. </value>
    [ObservableProperty]
    public partial double FutureTaxLiability { get; private set; }

    /// <summary>   Gets or sets the present tax liability. </summary>
    /// <value> The present tax liability. </value>
    [ObservableProperty]
    public partial double PresentTaxLiability { get; private set; }

    /// <summary>   Federal Tax liability upon withdrawal. </summary>
    /// <value> The Federal tax liability. </value>
    [ObservableProperty]
    public partial double FutureFederalTaxLiability { get; private set; }

    /// <summary>   Gets or sets the present federal tax liability. </summary>
    /// <value> The present federal tax liability. </value>
    [ObservableProperty]
    public partial double PresentFederalTaxLiability { get; private set; }

    /// <summary>   State Tax liability upon withdrawal. </summary>
    /// <value> The State tax liability. </value>
    [ObservableProperty]
    public partial double FutureStateTaxLiability { get; private set; }

    /// <summary>   Gets or sets the present state tax liability. </summary>
    /// <value> The present state tax liability. </value>
    [ObservableProperty]
    public partial double PresentStateTaxLiability { get; private set; }

    /// <summary>   Gets or sets the capital gain. </summary>
    /// <value> The capital gain. </value>
    [ObservableProperty]
    public partial double CapitalGain { get; private set; }

    /// <summary>   Capital Gains Tax liability upon withdrawal. </summary>
    /// <value> The tax liability. </value>
    [ObservableProperty]
    public partial double CapitalGainsTaxLiability { get; private set; }

    /// <summary>   Gets or sets the net cash out value. </summary>
    /// <value> The net cash out value. </value>
    [ObservableProperty]
    public partial double NetCashOutValue { get; private set; }

    /// <summary>   Gets or sets the final age. </summary>
    /// <value> The final age. </value>
    [ObservableProperty]
    public partial double FinalAge { get; private set; }


    /// <summary>   Gets or sets the discounted (present value) federal taxes paid. </summary>
    /// <value> The federal taxes paid. </value>
    [ObservableProperty]
    public partial double DiscountedFederalTaxesPaid { get; private set; }

    /// <summary>   Gets or sets the discounted state taxes paid. </summary>
    /// <value> The discounted state taxes paid. </value>
    [ObservableProperty]
    public partial double DiscountedStateTaxesPaid { get; private set; }

    #endregion

    #region " calculations "

    /// <summary>   Calculates the future value. </summary>
    /// <remarks>   Calculates the future value of a capital investment after applying
    ///             the present tax rates to the initial principal and then applying 
    ///             the assumed annual growth rate for the specified for number of 
    ///             years and then applying the future capital gains and state taxes.
    /// </remarks>
    public void CalculateFutureValue()
    {
        this.PresentFederalTaxLiability = this.Principal * (this.PresentFederalTaxRate / 100.0);
        this.PresentStateTaxLiability = this.Principal * (this.PresentStateTaxRate / 100.0);
        this.PresentTaxLiability = this.PresentFederalTaxLiability + this.PresentStateTaxLiability;
        double initialCapital = this.Principal * (1 - ((this.PresentFederalTaxRate + this.PresentStateTaxRate) / 100.0));

        double growthRate = 1 + (this.AnnualReturn / 100.0);

        // Future capital is the growth of the initial capital investment,
        // which is subject to capital gains taxes upon withdrawal.
        this.CapitalAccountBalance = initialCapital * Math.Pow( growthRate, this.Years );
        this.CapitalGain = this.CapitalAccountBalance - initialCapital;
        this.CapitalGainsTaxLiability = this.CapitalGain * (this.CapitalGainsTaxRate / 100.0);
        this.FutureFederalTaxLiability = this.CapitalGainsTaxLiability;
        this.FutureStateTaxLiability = this.CapitalGain * (this.FutureStateTaxRate / 100.0);
        this.FutureTaxLiability = this.FutureFederalTaxLiability + this.FutureStateTaxLiability;
        this.NetCashOutValue = this.CapitalAccountBalance - this.FutureTaxLiability;

        // Display the results
        Console.WriteLine( $"--- Capital Investment not using SEP IRA ---" );
        Console.WriteLine( $"      Initial Investment: {this.Principal:C0}" );
        Console.WriteLine( $"                Duration: {this.Years} years" );
        Console.WriteLine( $"   Assumed Annual Growth: {this.AnnualReturn}%" );
        Console.WriteLine( $"Initial Federal Tax Rate: {this.PresentFederalTaxRate}%" );
        Console.WriteLine( $"  Initial State Tax Rate: {this.PresentStateTaxRate}%" );
        Console.WriteLine( $"  Final Federal Tax Rate: {this.FutureFederalTaxRate}%" );
        Console.WriteLine( $"    Final State Tax Rate: {this.FutureStateTaxRate}%" );
        Console.WriteLine( $"  Capital Gains Tax Rate: {this.CapitalGainsTaxRate}%" );
        Console.WriteLine( $"-----------------------------------" );
        Console.WriteLine( $"   Initial Tax Liability: {this.PresentTaxLiability:C0}" );
        Console.WriteLine( $"-----------------------------------" );
        Console.WriteLine( $"       Pre-Tax Account Balance: {this.CapitalAccountBalance:C0}" );
        Console.WriteLine( $"  State Tax Owed on Withdrawal: {this.FutureStateTaxLiability:C0}" );
        Console.WriteLine( $"Federal Tax Owed on Withdrawal: {this.FutureFederalTaxLiability:C0}" );
        Console.WriteLine( $"        Tax Owed on Withdrawal: {this.FutureTaxLiability:C0}" );
        Console.WriteLine( $"            Net Cash-Out Value: {this.NetCashOutValue:C0}" );
    }

    /// <summary>   Calculates the future value of SEP IRA investment with RMD. </summary>
    /// <remarks>   2026-06-02. </remarks>
    /// <param name="debug">    (Optional) True output debug messages. </param>
    public void CalculateFutureValueSepIraWithRmd( bool debug = false )
    {
        // no taxes are paid on the initial principal amount, so we start with the full principal
        // as the initial capital for growth calculations
        this.PresentFederalTaxLiability = 0;
        this.PresentStateTaxLiability = 0;
        this.PresentTaxLiability = this.PresentFederalTaxLiability + this.PresentStateTaxLiability;

        // capital refers to the after tax RMD that is invested in a capital gains account.
        // This is tracked separately from the growth of the initial principal investment
        // because it is subject to capital gains taxes upon withdrawal, whereas the growth of
        // the initial principal investment is subject to ordinary income taxes upon withdrawal.
        double initialCapital = 0;

        // the discounted taxes paid on the RMD are tracked separately from the tax liability upon
        // withdrawal and are discounted back to the present value using the inflation rate to reflect
        // the time value of money.
        this.DiscountedFederalTaxesPaid = 0;
        this.DiscountedStateTaxesPaid = 0;

        // Capital Account Balance tracks the growth of the capital invested from the RMDs
        this.CapitalAccountBalance = 0;

        // This is the future value of the SEP IRA account before taxes, which grows based on the
        // initial principal and the growth rate of the capital invested in the SEP IRA.
        this.SepIraAccountBalance = this.Principal;

        double growthRate = 1 + (this.AnnualReturn / 100.0);
        for ( int age = this.InitialAge; age <= this.InitialAge + this.Years; age++ )
        {
            this.FinalAge = age;

            // RMD is based on the account balance at the end of the previous year,
            // so we calculate that first before applying the RMD for the current year
            double previousYearBalance = this.SepIraAccountBalance;
            double previousYearCapital = this.CapitalAccountBalance;

            // it is assumed that both the SEP IRA and Capital accounts grow at the same rate
            // during the year, so we apply the growth rate to both the SEP IRA account balance
            // and the capital account balance
            double endYearBalance = previousYearBalance * growthRate;
            double endYearCapital = previousYearCapital * growthRate;

            if ( age >= 72 )
            {
                // RMD = Account Balance at End of Previous Year / Uniform Lifetime Table Value for Age
                double rmd = previousYearBalance / this.UniformLifetimeTable[age];

                // The end of year balance of the SEP IRA account is reduced by the RMD amount,
                // which is withdrawn from the account.
                endYearBalance -= rmd;

                // the RMD amount is subject to ordinary income taxes at the present tax rates.
                // it is assumed here that the taxes on the RMD remain unchanged until the end of 
                // the calculation period.
                double federalTax = rmd * (this.PresentFederalTaxRate / 100.0);
                double stateTax = rmd * (this.PresentStateTaxRate / 100.0);

                // The after tax RMD is invested back into the capital account,
                // so we calculate the capital invested as the RMD minus the taxes paid on the RMD.
                // This capital is subject to capital gains taxes upon withdrawal,
                // so we track it separately from the growth of the initial capital investment.
                double capitalInvested = rmd - federalTax - stateTax;
                endYearCapital += capitalInvested;

                // The discounted taxes paid on the RMD are calculated by discounting 
                // the federal and state taxes paid back to the present value using the inflation rate.
                this.DiscountedFederalTaxesPaid += federalTax / Math.Pow( 1 + (this.InflationRate / 100.0), age - this.InitialAge );
                this.DiscountedStateTaxesPaid += stateTax / Math.Pow( 1 + (this.InflationRate / 100.0), age - this.InitialAge );
                if ( debug )
                {
                    Console.WriteLine( $"RMD: {rmd:C0}, Capital Invested: {capitalInvested:C0}" );
                }
            }

            if ( debug )
            {
                Console.WriteLine( $"Age: {age}, SEP IRA: {this.SepIraAccountBalance:C0}" );
                Console.Write( $", Capital: {this.CapitalAccountBalance:C0}" );
                Console.Write( $", Discounted Fed. Taxes Paid: {this.DiscountedFederalTaxesPaid:C0}" );
                Console.Write( $", Discounted State Taxes Paid: {this.DiscountedStateTaxesPaid:C0}" );
            }

            this.CapitalAccountBalance = endYearCapital;
            this.SepIraAccountBalance = endYearBalance;
            this.CapitalGain = this.CapitalAccountBalance - initialCapital;
            this.CapitalGainsTaxLiability = this.CapitalGain * (this.CapitalGainsTaxRate / 100.0);

            // the federal tax liability upon withdrawal is based on the capital gains tax liability
            // plus the taxes owed on the SEP IRA account balance, which is taxed as ordinary income upon withdrawal.
            this.FutureFederalTaxLiability = this.CapitalGainsTaxLiability + (this.SepIraAccountBalance * (this.FutureFederalTaxRate / 100.0));

            // for the state tax liability both the capital gain and the SEP IRA balance are taxed as ordinary income.
            this.FutureStateTaxLiability = (this.SepIraAccountBalance + this.CapitalGain) * (this.FutureStateTaxRate / 100.0);

            // the total tax liability upon withdrawal is the sum of the federal and state tax liabilities.
            this.FutureTaxLiability = this.FutureFederalTaxLiability + this.FutureStateTaxLiability;

            // the net cash-out value is the total account balance (SEP IRA + Capital) minus
            // the total tax liability upon withdrawal.
            this.NetCashOutValue = this.CapitalAccountBalance + this.SepIraAccountBalance - this.FutureTaxLiability;
        }

        // Display the results
        Console.WriteLine( $"--- Capital Investment using SEP IRA ---" );
        Console.WriteLine( $"      Initial Investment: {this.Principal:C0}" );
        Console.WriteLine( $"                Duration: {this.Years} years" );
        Console.WriteLine( $"   Assumed Annual Growth: {this.AnnualReturn}%" );
        Console.WriteLine( $"Initial Federal Tax Rate: {this.PresentFederalTaxRate}%" );
        Console.WriteLine( $"  Initial State Tax Rate: {this.PresentStateTaxRate}%" );
        Console.WriteLine( $"  Final Federal Tax Rate: {this.FutureFederalTaxRate}%" );
        Console.WriteLine( $"    Final State Tax Rate: {this.FutureStateTaxRate}%" );
        Console.WriteLine( $"  Capital Gains Tax Rate: {this.CapitalGainsTaxRate}%" );
        Console.WriteLine( $"          Inflation Rate: {this.InflationRate}%" );
        Console.WriteLine( $"             Initial Age: {this.InitialAge}" );
        Console.WriteLine( $"-----------------------------------" );
        Console.WriteLine( $"         Initial Tax Liability: {this.PresentTaxLiability:C0}" );
        Console.WriteLine( $"  Discounted Taxes Paid on RMD: {this.DiscountedFederalTaxesPaid + this.DiscountedStateTaxesPaid:C0}" );
        Console.WriteLine( $"-----------------------------------" );
        Console.WriteLine( $"       SEP IRA Account Balance: {this.SepIraAccountBalance:C0}" );
        Console.WriteLine( $"                  Capital Gain: {this.CapitalGain:C0}" );
        Console.WriteLine( $" Pre-Tax Total Account Balance: {this.CapitalGain + this.SepIraAccountBalance:C0}" );
        Console.WriteLine( $"  State Tax Owed on Withdrawal: {this.FutureStateTaxLiability:C0}" );
        Console.WriteLine( $"Federal Tax Owed on Withdrawal: {this.FutureFederalTaxLiability:C0}" );
        Console.WriteLine( $"        Tax Owed on Withdrawal: {this.FutureTaxLiability:C0}" );
        Console.WriteLine( $"            Net Cash-Out Value: {this.NetCashOutValue:C0}" );
        Console.WriteLine( $"                     Final Age: {this.FinalAge}" );
    }

    #endregion
}
