using System.Text;

namespace cc.isr.Finance.Sep.Ira;

/// <summary>   Builds reports for the appreciator data. </summary>
/// <remarks>   2026-06-13. </remarks>
public static class AppreciatorReportBuilder
{
    /// <summary>   Gets or sets a list of titles for the inputs. </summary>
    /// <value> A list of titles for the inputs. </value>
    public static Dictionary<string, string> Titles { get; set; } = new Dictionary<string, string>()
    {
        { nameof( Appreciator.InvestedAmount ), "Invested Amount ($)" },
        { nameof( Appreciator.InitialAge ), "Initial Age" },
        { nameof( Appreciator.InvestmentDuration ), "Investment Duration in Years" },
        { nameof( Appreciator.InitialFederalTaxRate ), "Present Federal Tax Rate (%)" },
        { nameof( Appreciator.WithdrawalFederalTaxRate ), "Withdrawal Federal Tax Rate (%)" },
        { nameof( Appreciator.InitialStateTaxRate ), "Initial State Tax Rate (%)" },
        { nameof( Appreciator.WithdrawalStateTaxRate ), "Withdrawal State Tax Rate (%)" },
        { nameof( Appreciator.FederalCapitalGainsTaxRate ), "Federal Capital Gains Tax Rate (%)" },
        { nameof( Appreciator.StateCapitalGainsTaxRate ), "State Capital Gains Tax Rate (%)" },
        { nameof( Appreciator.AnnualInflationRate ), "Annual Annual Inflation Rate (%)" },
        { nameof( Appreciator.AnnualGrowthRate ), "Annual Growth Rate (%)" },
        { nameof( Appreciator.SepIraAccountBalance ), "SEP IRA Account Balance" },
        { nameof( Appreciator.CapitalAccountBalance ), "Capital Account Balance" },
        { nameof( Appreciator.InitialTaxLiability ), "Initial Tax Liability" },
        { nameof( Appreciator.WithdrawalFederalTaxLiability ), "Federal Tax Liability Upon Withdrawal" },
        { nameof( Appreciator.InitialFederalTaxLiability ), "Initial Federal Tax Liability" },
        { nameof( Appreciator.WithdrawalStateTaxLiability ), "State Tax Liability Upon Withdrawal" },
        { nameof( Appreciator.InitialStateTaxLiability ), "Initial State Tax Liability" },
        { nameof( Appreciator.CapitalGain ), "Capital Gain" },
        { nameof( Appreciator.FederalCapitalGainsTaxLiability ), "Federal Capital Gains Tax Liability" },
        { nameof( Appreciator.StateCapitalGainsTaxLiability ), "State Capital Gains Tax Liability" },
        { nameof( Appreciator.WithdrawalTaxLiability ), "Total Tax Liability Upon Withdrawal" },
        { nameof( Appreciator.NetCashOutValue ), "Net Cash Out Value" },
        { nameof( Appreciator.FinalAge ), "Final Age" },
        { nameof( Appreciator.DiscountedFederalTaxesPaid ), "Discounted (Present Value) Federal Taxes Paid" },
        { nameof( Appreciator.DiscountedStateTaxesPaid ), "Discounted (Present Value) State Taxes Paid" },
        { nameof( Appreciator.DiscountedTaxesPaid ), "Discounted (Present Value) Taxes Paid" },
        { nameof( Appreciator.CalculateFutureValue ), "Simple Capital Investment" },
        { nameof( Appreciator.CalculateFutureValueSepIraWithRmd ), "SEP IRA Investment with RMD"  },
    };

    /// <summary>   Builds inputs result. </summary>
    /// <remarks>   2026-06-13. </remarks>
    /// <param name="appreciator">  The appreciator. </param>
    /// <returns>   A Dictionary&lt;string,string&gt; </returns>
    [CLSCompliant( false )]
    public static Dictionary<string, string> BuildInputsResult( Appreciator appreciator )
    {
        return new Dictionary<string, string>()
        {
            { $"{nameof( Appreciator.InvestedAmount )}", $"{appreciator.InvestedAmount:C0}" },
            { $"{nameof( Appreciator.InitialAge )}", $"{appreciator.InitialAge}" },
            { $"{nameof( Appreciator.InvestmentDuration )}", $"{appreciator.InvestmentDuration}" },
            { $"{nameof( Appreciator.AnnualGrowthRate )}", $"{appreciator.AnnualGrowthRate:F1}" },
            { $"{nameof( Appreciator.InitialFederalTaxRate )}", $"{appreciator.InitialFederalTaxRate:F1}" },
            { $"{nameof( Appreciator.InitialStateTaxRate )}", $"{appreciator.InitialStateTaxRate:F1}" },
            { $"{nameof( Appreciator.WithdrawalFederalTaxRate )}", $"{appreciator.WithdrawalFederalTaxRate:F1}" },
            { $"{nameof( Appreciator.WithdrawalStateTaxRate )}", $"{appreciator.WithdrawalStateTaxRate:F1}" },
            { $"{nameof( Appreciator.FederalCapitalGainsTaxRate )}", $"{appreciator.FederalCapitalGainsTaxRate:F1}" },
            { $"{nameof( Appreciator.StateCapitalGainsTaxRate )}", $"{appreciator.StateCapitalGainsTaxRate:F1}" },
            { $"{nameof( Appreciator.AnnualInflationRate )}", $"{appreciator.AnnualInflationRate:F2}" },
        };
    }

    /// <summary>   Builds inputs report. </summary>
    /// <remarks>   2026-06-13. </remarks>
    /// <param name="appreciator">  The appreciator. </param>
    /// <param name="title">        (Optional) The title. </param>
    /// <returns>   A List&lt;string[]&gt; </returns>
    [CLSCompliant( false )]
    public static List<string[]> BuildInputsReport( Appreciator appreciator, string title = "* Inputs *" )
    {
        List<string[]> result = [];
        result.Add( [$"{Titles[nameof( Appreciator.InvestedAmount )]}", $"{appreciator.InvestedAmount:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.InitialAge )]}", $"{appreciator.InitialAge}"] );
        result.Add( [$"{Titles[nameof( Appreciator.InvestmentDuration )]}", $"{appreciator.InvestmentDuration}"] );
        result.Add( [$"{Titles[nameof( Appreciator.AnnualGrowthRate )]}", $"{appreciator.AnnualGrowthRate:F1}"] );
        result.Add( [$"{Titles[nameof( Appreciator.InitialFederalTaxRate )]}", $"{appreciator.InitialFederalTaxRate:F1}"] );
        result.Add( [$"{Titles[nameof( Appreciator.InitialStateTaxRate )]}", $"{appreciator.InitialStateTaxRate:F1}"] );
        result.Add( [$"{Titles[nameof( Appreciator.WithdrawalFederalTaxRate )]}", $"{appreciator.WithdrawalFederalTaxRate:F1}"] );
        result.Add( [$"{Titles[nameof( Appreciator.WithdrawalStateTaxRate )]}", $"{appreciator.WithdrawalStateTaxRate:F1}"] );
        result.Add( [$"{Titles[nameof( Appreciator.FederalCapitalGainsTaxRate )]}", $"{appreciator.FederalCapitalGainsTaxRate:F1}"] );
        result.Add( [$"{Titles[nameof( Appreciator.StateCapitalGainsTaxRate )]}", $"{appreciator.StateCapitalGainsTaxRate:F1}"] );
        result.Add( [$"{Titles[nameof( Appreciator.AnnualInflationRate )]}", $"{appreciator.AnnualInflationRate:F1}"] );
        if ( !string.IsNullOrWhiteSpace( title ) )
            result.Insert( 0, [title, string.Empty] );

        return result;
    }

    /// <summary>   Builds simple capital investment result. </summary>
    /// <remarks>   2026-06-13. </remarks>
    /// <param name="appreciator">  The appreciator. </param>
    /// <returns>   A Dictionary&lt;string,string&gt; </returns>
    [CLSCompliant( false )]
    public static Dictionary<string, string> BuildSimpleCapitalInvestmentResult( Appreciator appreciator )
    {
        return new Dictionary<string, string>()
        {
            { $"{nameof( Appreciator.InitialFederalTaxLiability )}", $"{appreciator.InitialFederalTaxLiability:C0}" },
            { $"{nameof( Appreciator.InitialStateTaxLiability )}", $"{appreciator.InitialStateTaxLiability:C0}" },
            { $"{nameof( Appreciator.InitialTaxLiability )}", $"{appreciator.InitialTaxLiability:C0}" },
            { $"{nameof( Appreciator.CapitalAccountBalance )}", $"{appreciator.CapitalAccountBalance:C0}" },
            { $"{nameof( Appreciator.CapitalGain )}", $"{appreciator.CapitalGain:C0}" },
            { $"{nameof( Appreciator.FederalCapitalGainsTaxLiability )}", $"{appreciator.FederalCapitalGainsTaxLiability:C0}" },
            { $"{nameof( Appreciator.StateCapitalGainsTaxLiability )}", $"{appreciator.StateCapitalGainsTaxLiability:C0}" },
            { $"{nameof( Appreciator.WithdrawalTaxLiability )}", $"{appreciator.WithdrawalTaxLiability:C0}" },
            { $"{nameof( Appreciator.NetCashOutValue )}", $"{appreciator.NetCashOutValue:C0}" }
        };
    }

    /// <summary>   Builds simple capital investment report. </summary>
    /// <remarks>   2026-06-13. </remarks>
    /// <param name="appreciator">      The appreciator. </param>
    /// <param name="includeHeader">    (Optional) True to include, false to exclude the header. </param>
    /// <param name="headerPrefix">     (Optional) The header prefix. </param>
    /// <param name="headerSuffix">     (Optional) The header suffix. </param>
    /// <returns>   A List&lt;string[]&gt; </returns>
    [CLSCompliant( false )]
    public static List<string[]> BuildSimpleCapitalInvestmentReport( Appreciator appreciator, bool includeHeader = false,
        string headerPrefix = "* ", string headerSuffix = " *" )
    {
        List<string[]> result = [];
        result.Add( [$"{Titles[nameof( Appreciator.InitialFederalTaxLiability )]}", $"{appreciator.InitialFederalTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.InitialStateTaxLiability )]}", $"{appreciator.InitialStateTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.InitialTaxLiability )]}", $"{appreciator.InitialTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.CapitalAccountBalance )]}", $"{appreciator.CapitalAccountBalance:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.CapitalGain )]}", $"{appreciator.CapitalGain:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.FederalCapitalGainsTaxLiability )]}", $"{appreciator.FederalCapitalGainsTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.StateCapitalGainsTaxLiability )]}", $"{appreciator.StateCapitalGainsTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.WithdrawalTaxLiability )]}", $"{appreciator.WithdrawalTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.NetCashOutValue )]}", $"{appreciator.NetCashOutValue:C0}"] );
        if ( includeHeader )
        {
            result.Insert( 0, ["Item", "Value"] );
            result.Insert( 0, [ $"{headerPrefix}{Titles[nameof( Appreciator.CalculateFutureValue )]}{headerSuffix}",
                string.Empty] );
        }
        return result;
    }

    /// <summary>   Output report. </summary>
    /// <remarks>   2026-06-13. </remarks>
    /// <param name="report">           The report. </param>
    /// <param name="headerRowsCount">  Number of header rows. </param>
    public static void OutputReport( List<string[]> report, int headerRowsCount )
    {
        int columnCount = report[0].Length;
        int[] columnWidths = new int[columnCount];
        // Calculate maximum width for each column
        for ( int i = 0; i < columnCount; i++ )
        {
            columnWidths[i] = report.Max( row => row[i].Length );
        }

        // Output the report with proper alignment
        foreach ( string[] row in report )
        {
            bool isHeader = report.IndexOf( row ) < headerRowsCount;
            for ( int i = 0; i < columnCount; i++ )
            {
                if ( i == 0 )
                {
                    Console.Write( row[i].PadLeft( columnWidths[i] ) ); // Add padding to right align
                    if ( isHeader )
                        Console.Write( "  " ); // Separator between title and value")
                    else
                        Console.Write( ": " ); // Separator between title and value")
                }
                else
                {
                    Console.Write( row[i].PadRight( columnWidths[i] + 2 ) ); // Add padding for spacing
                }
            }
            Console.WriteLine();
        }
    }

    /// <summary>   Builds a report. </summary>
    /// <remarks>   2026-06-15. </remarks>
    /// <param name="title">            The title. </param>
    /// <param name="subtitle">         The subtitle. </param>
    /// <param name="report">           The report. </param>
    /// <param name="headerRowsCount">  Number of header rows. </param>
    /// <returns>   A string. </returns>
    public static string BuildReport( string title, string subtitle, List<string[]> report, int headerRowsCount )
    {
        StringBuilder stringBuilder = new( title );
        _ = stringBuilder.AppendLine();
        _ = stringBuilder.AppendLine( subtitle );
        int columnCount = report[0].Length;
        int[] columnWidths = new int[columnCount];
        // Calculate maximum width for each column
        for ( int i = 0; i < columnCount; i++ )
        {
            columnWidths[i] = report.Max( row => row[i].Length );
        }

        // Output the report with proper alignment
        foreach ( string[] row in report )
        {
            bool isHeader = report.IndexOf( row ) < headerRowsCount;
            for ( int i = 0; i < columnCount; i++ )
            {
                if ( i == 0 )
                {
                    _ = stringBuilder.Append( row[i].PadLeft( columnWidths[i] ) ); // Add padding to right align
                    if ( isHeader )
                        _ = stringBuilder.Append( "  " ); // Separator between title and value")
                    else
                        _ = stringBuilder.Append( ": " ); // Separator between title and value")
                }
                else
                {
                    _ = stringBuilder.Append( row[i].PadRight( columnWidths[i] + 2 ) ); // Add padding for spacing
                }
            }
            _ = stringBuilder.AppendLine();
        }
        return stringBuilder.ToString();
    }

    /// <summary>   Builds separator ira investment result. </summary>
    /// <remarks>   2026-06-13. </remarks>
    /// <param name="appreciator">  The appreciator. </param>
    /// <returns>   A Dictionary&lt;string,string&gt; </returns>
    [CLSCompliant( false )]
    public static Dictionary<string, string> BuildSepIraInvestmentResult( Appreciator appreciator )
    {
        return new Dictionary<string, string>()
        {
            { $"{nameof( Appreciator.InitialFederalTaxLiability )}", $"{appreciator.InitialFederalTaxLiability:C0}" },
            { $"{nameof( Appreciator.InitialStateTaxLiability )}", $"{appreciator.InitialStateTaxLiability:C0}" },
            { $"{nameof( Appreciator.InitialTaxLiability )}", $"{appreciator.InitialTaxLiability:C0}" },
            { $"{nameof( Appreciator.DiscountedTaxesPaid )}", $"{appreciator.DiscountedTaxesPaid:C0}" },
            { $"{nameof( Appreciator.SepIraAccountBalance )}", $"{appreciator.SepIraAccountBalance:C0}" },
            { $"{nameof( Appreciator.CapitalAccountBalance )}", $"{appreciator.CapitalAccountBalance:C0}" },
            { $"{nameof( Appreciator.CapitalGain )}", $"{appreciator.CapitalGain:C0}" },
            { $"{nameof( Appreciator.WithdrawalFederalTaxLiability )}", $"{appreciator.WithdrawalFederalTaxLiability:C0}" },
            { $"{nameof( Appreciator.WithdrawalStateTaxLiability )}", $"{appreciator.WithdrawalStateTaxLiability:C0}" },
            { $"{nameof( Appreciator.FederalCapitalGainsTaxLiability )}", $"{appreciator.FederalCapitalGainsTaxLiability:C0}" },
            { $"{nameof( Appreciator.StateCapitalGainsTaxLiability )}", $"{appreciator.StateCapitalGainsTaxLiability:C0}" },
            { $"{nameof( Appreciator.WithdrawalTaxLiability )}", $"{appreciator.WithdrawalTaxLiability:C0}" },
            { $"{nameof( Appreciator.NetCashOutValue )}", $"{appreciator.NetCashOutValue:C0}" }
        };
    }

    /// <summary>   Builds separator ira investment report. </summary>
    /// <remarks>   2026-06-13. </remarks>
    /// <param name="appreciator">      The appreciator. </param>
    /// <param name="includeHeader">    (Optional) True to include, false to exclude the header. </param>
    /// <param name="headerPrefix">     (Optional) The header prefix. </param>
    /// <param name="headerSuffix">     (Optional) The header suffix. </param>
    /// <returns>   A List&lt;string[]&gt; </returns>
    [CLSCompliant( false )]
    public static List<string[]> BuildSepIraInvestmentReport( Appreciator appreciator, bool includeHeader = false,
        string headerPrefix = "* ", string headerSuffix = " *" )
    {
        List<string[]> result = [];
        result.Add( [$"{Titles[nameof( Appreciator.InitialFederalTaxLiability )]}", $"{appreciator.InitialFederalTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.InitialStateTaxLiability )]}", $"{appreciator.InitialStateTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.InitialTaxLiability )]}", $"{appreciator.InitialTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.DiscountedTaxesPaid )]}", $"{appreciator.DiscountedTaxesPaid:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.SepIraAccountBalance )]}", $"{appreciator.SepIraAccountBalance:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.CapitalAccountBalance )]}", $"{appreciator.CapitalAccountBalance:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.CapitalGain )]}", $"{appreciator.CapitalGain:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.WithdrawalFederalTaxLiability )]}", $"{appreciator.WithdrawalFederalTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.WithdrawalStateTaxLiability )]}", $"{appreciator.WithdrawalStateTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.FederalCapitalGainsTaxLiability )]}", $"{appreciator.FederalCapitalGainsTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.StateCapitalGainsTaxLiability )]}", $"{appreciator.StateCapitalGainsTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.WithdrawalTaxLiability )]}", $"{appreciator.WithdrawalTaxLiability:C0}"] );
        result.Add( [$"{Titles[nameof( Appreciator.NetCashOutValue )]}", $"{appreciator.NetCashOutValue:C0}"] );
        if ( includeHeader )
        {
            result.Insert( 0, ["Item", "Value"] );
            result.Insert( 0,
                [$"{headerPrefix}{Titles[nameof( Appreciator.CalculateFutureValueSepIraWithRmd )]}{headerSuffix}",
                string.Empty] );
        }
        return result;
    }

    /// <summary>   Builds comparison report. </summary>
    /// <remarks>   2026-06-13. </remarks>
    /// <param name="simpleResult">   The simple investement result. </param>
    /// <param name="sepIraResult">   The SEP IRA result. </param>
    /// <returns>   A string[][]. </returns>
    [CLSCompliant( false )]
    public static string[][] BuildComparisonReport( Dictionary<string, string> simpleResult, Dictionary<string, string> sepIraResult )
    {
        return [
            [ $"{Titles[nameof( Appreciator.InitialFederalTaxLiability )]}", $"{simpleResult[nameof( Appreciator.InitialFederalTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.InitialFederalTaxLiability )]}" ],
            [ $"{Titles[nameof( Appreciator.InitialStateTaxLiability )]}", $"{simpleResult[nameof( Appreciator.InitialStateTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.InitialStateTaxLiability )]}" ],
            [ $"{Titles[nameof( Appreciator.InitialTaxLiability )]}", $"{simpleResult[nameof( Appreciator.InitialTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.InitialTaxLiability )]}" ],
            [ $"{Titles[nameof( Appreciator.DiscountedTaxesPaid )]}", "", $"{sepIraResult[nameof( Appreciator.DiscountedTaxesPaid )]}" ],
            [ $"{Titles[nameof( Appreciator.SepIraAccountBalance )]}", "", $"{sepIraResult[nameof( Appreciator.SepIraAccountBalance )]}" ],
            [ $"{Titles[nameof( Appreciator.CapitalAccountBalance )]}", $"{simpleResult[nameof( Appreciator.CapitalAccountBalance )]}", $"{sepIraResult[nameof( Appreciator.CapitalAccountBalance )]}" ],
            [ $"{Titles[nameof( Appreciator.CapitalGain )]}", $"{simpleResult[nameof( Appreciator.CapitalGain )]}", $"{sepIraResult[nameof( Appreciator.CapitalGain )]}" ],
            [ $"{Titles[nameof( Appreciator.WithdrawalFederalTaxLiability )]}", "", $"{sepIraResult[nameof( Appreciator.WithdrawalFederalTaxLiability )]}" ],
            [ $"{Titles[nameof( Appreciator.WithdrawalStateTaxLiability )]}", "", $"{sepIraResult[nameof( Appreciator.WithdrawalStateTaxLiability )]}" ],
            [ $"{Titles[nameof( Appreciator.FederalCapitalGainsTaxLiability )]}", $"{simpleResult[nameof( Appreciator.FederalCapitalGainsTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.FederalCapitalGainsTaxLiability )]}" ],
            [ $"{Titles[nameof( Appreciator.StateCapitalGainsTaxLiability )]}", $"{simpleResult[nameof( Appreciator.StateCapitalGainsTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.StateCapitalGainsTaxLiability )]}" ],
            [ $"{Titles[nameof( Appreciator.WithdrawalTaxLiability )]}", $"{simpleResult[nameof( Appreciator.WithdrawalTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.WithdrawalTaxLiability )]}" ],
            [ $"{Titles[nameof( Appreciator.NetCashOutValue )]}", $"{simpleResult[nameof( Appreciator.NetCashOutValue )]}", $"{sepIraResult[nameof( Appreciator.NetCashOutValue )]}" ]
        ];
    }

    /// <summary>   Builds comparison report. </summary>
    /// <remarks>   2026-06-13. </remarks>
    /// <param name="simpleResult">     The simple investement report. </param>
    /// <param name="sepIraResult">     The SEP IRA report. </param>
    /// <param name="includeHeaders">   True to include, false to exclude the headers. </param>
    /// <returns>   A string[][]. </returns>
    [CLSCompliant( false )]
    public static List<string[]> BuildComparisonReport( Dictionary<string, string> simpleResult,
        Dictionary<string, string> sepIraResult, bool includeHeaders )
    {
        List<string[]> result = [];
        result.Add( [$"{Titles[nameof( Appreciator.InitialFederalTaxLiability )]}", $"{simpleResult[nameof( Appreciator.InitialFederalTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.InitialFederalTaxLiability )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.InitialStateTaxLiability )]}", $"{simpleResult[nameof( Appreciator.InitialStateTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.InitialStateTaxLiability )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.InitialTaxLiability )]}", $"{simpleResult[nameof( Appreciator.InitialTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.InitialTaxLiability )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.DiscountedTaxesPaid )]}", "", $"{sepIraResult[nameof( Appreciator.DiscountedTaxesPaid )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.SepIraAccountBalance )]}", "", $"{sepIraResult[nameof( Appreciator.SepIraAccountBalance )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.CapitalAccountBalance )]}", $"{simpleResult[nameof( Appreciator.CapitalAccountBalance )]}", $"{sepIraResult[nameof( Appreciator.CapitalAccountBalance )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.CapitalGain )]}", $"{simpleResult[nameof( Appreciator.CapitalGain )]}", $"{sepIraResult[nameof( Appreciator.CapitalGain )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.WithdrawalFederalTaxLiability )]}", "", $"{sepIraResult[nameof( Appreciator.WithdrawalFederalTaxLiability )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.WithdrawalStateTaxLiability )]}", "", $"{sepIraResult[nameof( Appreciator.WithdrawalStateTaxLiability )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.FederalCapitalGainsTaxLiability )]}", $"{simpleResult[nameof( Appreciator.FederalCapitalGainsTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.FederalCapitalGainsTaxLiability )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.StateCapitalGainsTaxLiability )]}", $"{simpleResult[nameof( Appreciator.StateCapitalGainsTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.StateCapitalGainsTaxLiability )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.WithdrawalTaxLiability )]}", $"{simpleResult[nameof( Appreciator.WithdrawalTaxLiability )]}", $"{sepIraResult[nameof( Appreciator.WithdrawalTaxLiability )]}"] );
        result.Add( [$"{Titles[nameof( Appreciator.NetCashOutValue )]}", $"{simpleResult[nameof( Appreciator.NetCashOutValue )]}", $"{sepIraResult[nameof( Appreciator.NetCashOutValue )]}"] );
        if ( includeHeaders )
        {
            result.Insert( 0, ["Item", "Skip IRA", "SEP IRA"] );
        }
        return result;
    }
}
