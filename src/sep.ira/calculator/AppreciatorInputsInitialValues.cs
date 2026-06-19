namespace cc.isr.Finance.Sep.Ira;

/// <summary>   An appreciator inputs initial values. </summary>
/// <remarks>   2026-06-15. </remarks>
public static class AppreciatorInputsInitialValues
{
    /// <summary>   Gets or sets the invested amount. </summary>
    /// <value> The invested amount. </value>
    public static decimal InvestedAmount { get; set; } = 10000.00m;

    /// <summary>   Gets or sets the initial age. </summary>
    /// <value> The initial age. </value>
    public static int InitialAge { get; set; } = 75;

    /// <summary>   Gets or sets the duration of the investment in years. </summary>
    /// <value> The investment duration. </value>
    public static int InvestmentDuration { get; set; } = 20;

    /// <summary>   Gets or sets the initial federal tax rate. </summary>
    /// <value> The initial federal tax rate. </value>
    public static decimal InitialFederalTaxRate { get; set; } = 35.0m;

    /// <summary>   Gets or sets the withdrawal federal tax rate. </summary>
    /// <value> The withdrawal federal tax rate. </value>
    public static decimal WithdrawalFederalTaxRate { get; set; } = 35.0m;

    /// <summary>   Gets or sets the initial state tax rate. </summary>
    /// <value> The initial state tax rate. </value>
    public static decimal InitialStateTaxRate { get; set; } = 9.3m;

    /// <summary>   Gets or sets the withdrawal state tax rate. </summary>
    /// <value> The withdrawal state tax rate. </value>
    public static decimal WithdrawalStateTaxRate { get; set; } = 9.3m;

    /// <summary>   Gets or sets the federal capital gains tax rate. </summary>
    /// <value> The federal capital gains tax rate. </value>
    public static decimal FederalCapitalGainsTaxRate { get; set; } = 25.0m;

    /// <summary>   Gets or sets the state capital gains tax rate. </summary>
    /// <value> The state capital gains tax rate. </value>
    public static decimal StateCapitalGainsTaxRate { get; set; } = 9.3m;

    /// <summary>   Gets or sets the annual inflation rate. </summary>
    /// <value> The annual inflation rate. </value>
    public static decimal AnnualInflationRate { get; set; } = 2.75m;

    /// <summary>   Gets or sets the annual growth rate. </summary>
    /// <value> The annual growth rate. </value>
    public static decimal AnnualGrowthRate { get; set; } = 7.0m;

    /// <summary>   Gets or sets the uniform lifetime table. </summary>
    /// <remarks> Based on IRS Publication 1406 2025. </remarks>
    /// <value> The uniform lifetime table. </value>
    public static Dictionary<int, decimal> UniformLifetimeTable { get; set; } = new Dictionary<int, decimal>()
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
}
