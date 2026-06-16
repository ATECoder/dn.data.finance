namespace cc.isr.Finance.Sep.Ira;

/// <summary>   An appreciator inputs ranges. </summary>
/// <remarks>   2026-06-15. </remarks>
public static class AppreciatorInputsRanges
{
    /// <summary>   Gets or sets the invested amount. </summary>
    /// <value> The invested amount. </value>
    public static NumericalRange InvestedAmount { get; set; } = new NumericalRange( 1, 1000000 );

    /// <summary>   Gets or sets the initial age. </summary>
    /// <value> The initial age. </value>
    public static NumericalRange InitialAge { get; set; } = new NumericalRange( 18, 119 );

    /// <summary>   Gets or sets the final age. </summary>
    /// <value> The final age. </value>
    public static NumericalRange FinalAge { get; set; } = new NumericalRange( 19, 120 );

    /// <summary>   Gets or sets the duration of the investment in years. </summary>
    /// <value> The investment duration. </value>
    public static NumericalRange InvestmentDuration { get; set; } = new NumericalRange( 1, 102 );

    /// <summary>   Gets or sets the initial federal tax rate. </summary>
    /// <value> The initial federal tax rate. </value>
    public static NumericalRange InitialFederalTaxRate { get; set; } = new NumericalRange( 0, 45 );

    /// <summary>   Gets or sets the withdrawal federal tax rate. </summary>
    /// <value> The withdrawal federal tax rate. </value>
    public static NumericalRange WithdrawalFederalTaxRate { get; set; } = new NumericalRange( 0, 45 );

    /// <summary>   Gets or sets the initial state tax rate. </summary>
    /// <value> The initial state tax rate. </value>
    public static NumericalRange InitialStateTaxRate { get; set; } = new NumericalRange( 0, 25 );

    /// <summary>   Gets or sets the withdrawal state tax rate. </summary>
    /// <value> The withdrawal state tax rate. </value>
    public static NumericalRange WithdrawalStateTaxRate { get; set; } = new NumericalRange( 0, 25 );

    /// <summary>   Gets or sets the federal capital gains tax rate. </summary>
    /// <value> The federal capital gains tax rate. </value>
    public static NumericalRange FederalCapitalGainsTaxRate { get; set; } = new NumericalRange( 0, 45 );

    /// <summary>   Gets or sets the state capital gains tax rate. </summary>
    /// <value> The state capital gains tax rate. </value>
    public static NumericalRange StateCapitalGainsTaxRate { get; set; } = new NumericalRange( 0, 25 );

    /// <summary>   Gets or sets the annual inflation rate. </summary>
    /// <value> The annual inflation rate. </value>
    public static NumericalRange AnnualInflationRate { get; set; } = new NumericalRange( -10, 50 );

    /// <summary>   Gets or sets the annual growth rate. </summary>
    /// <value> The annual growth rate. </value>
    public static NumericalRange AnnualGrowthRate { get; set; } = new NumericalRange( -50, 100 );
}
