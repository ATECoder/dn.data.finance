namespace cc.isr.Finance.Sep.Ira;

/// <summary>   An appreciator inputs initial values. </summary>
/// <remarks>   2026-06-15. </remarks>
public static class AppreciatorInputsInitialValues
{
    /// <summary>   Gets or sets the invested amount. </summary>
    /// <value> The invested amount. </value>
    public static double InvestedAmount { get; set; } = 10000.00;

    /// <summary>   Gets or sets the initial age. </summary>
    /// <value> The initial age. </value>
    public static int InitialAge { get; set; } = 75;

    /// <summary>   Gets or sets the duration of the investment in years. </summary>
    /// <value> The investment duration. </value>
    public static int InvestmentDuration { get; set; } = 20;

    /// <summary>   Gets or sets the initial federal tax rate. </summary>
    /// <value> The initial federal tax rate. </value>
    public static double InitialFederalTaxRate { get; set; } = 35.0;

    /// <summary>   Gets or sets the withdrawal federal tax rate. </summary>
    /// <value> The withdrawal federal tax rate. </value>
    public static double WithdrawalFederalTaxRate { get; set; } = 35.0;

    /// <summary>   Gets or sets the initial state tax rate. </summary>
    /// <value> The initial state tax rate. </value>
    public static double InitialStateTaxRate { get; set; } = 9.3;

    /// <summary>   Gets or sets the withdrawal state tax rate. </summary>
    /// <value> The withdrawal state tax rate. </value>
    public static double WithdrawalStateTaxRate { get; set; } = 9.3;

    /// <summary>   Gets or sets the federal capital gains tax rate. </summary>
    /// <value> The federal capital gains tax rate. </value>
    public static double FederalCapitalGainsTaxRate { get; set; } = 25.0;

    /// <summary>   Gets or sets the state capital gains tax rate. </summary>
    /// <value> The state capital gains tax rate. </value>
    public static double StateCapitalGainsTaxRate { get; set; } = 9.3;

    /// <summary>   Gets or sets the annual inflation rate. </summary>
    /// <value> The annual inflation rate. </value>
    public static double AnnualInflationRate { get; set; } = 2.75;

    /// <summary>   Gets or sets the annual growth rate. </summary>
    /// <value> The annual growth rate. </value>
    public static double AnnualGrowthRate { get; set; } = 7.0;

    /// <summary>   Gets or sets the uniform lifetime table. </summary>
    /// <remarks> Based on IRS Publication 1406 2025. </remarks>
    /// <value> The uniform lifetime table. </value>
    public static Dictionary<int, double> UniformLifetimeTable { get; set; } = new Dictionary<int, double>()
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
}
