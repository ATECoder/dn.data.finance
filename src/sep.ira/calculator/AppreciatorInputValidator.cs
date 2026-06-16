namespace cc.isr.Finance.Sep.Ira;

/// <summary>   Provides validation logic for SEP-IRA appreciator inputs. </summary>
/// <remarks>   2026-06-11. </remarks>
public static class AppreciatorInputValidator
{
    /// <summary>   Validates all calculator inputs and returns a list of validation errors. </summary>
    /// <remarks>   2026-06-11. </remarks>
    /// <param name="investedAmount">               The invested amount. </param>
    /// <param name="initialAge">                   The initial age. </param>
    /// <param name="investmentDuration">           Duration of the investment. </param>
    /// <param name="initialFederalTaxRate">        The initial federal tax rate. </param>
    /// <param name="withdrawalFederalTaxRate">      The withdrawal federal tax rate. </param>
    /// <param name="initialStateTaxRate">          The initial state tax rate. </param>
    /// <param name="withdrawalStateTaxRate">        The withdrawal state tax rate. </param>
    /// <param name="federalCapitalGainsTaxRate">   The federal capital gains tax rate. </param>
    /// <param name="stateCapitalGainsTaxRate">     The state capital gains tax rate. </param>
    /// <param name="annualInflationRate">          The annual inflation rate. </param>
    /// <param name="annualGrowthRate">             The annual growth rate. </param>
    /// <returns>   A list of validation error messages. Empty if all inputs are valid. </returns>
    public static List<string> ValidateInputs(
        double investedAmount, int initialAge, int investmentDuration,
        double initialFederalTaxRate, double withdrawalFederalTaxRate,
        double initialStateTaxRate, double withdrawalStateTaxRate,
        double federalCapitalGainsTaxRate, double stateCapitalGainsTaxRate,
        double annualInflationRate, double annualGrowthRate )
    {
        List<string> errors = [];

        // InvestedAmount validation
        ValidateInvestedAmount( investedAmount, errors );

        // InitialAge validation
        ValidateInitialAge( initialAge, investmentDuration, errors );

        // InvestmentDuration validation
        ValidateInvestmentDuration( investmentDuration, errors );

        // Tax rates validation
        ValidateTaxRates( initialFederalTaxRate, withdrawalFederalTaxRate,
            initialStateTaxRate, withdrawalStateTaxRate,
            federalCapitalGainsTaxRate, stateCapitalGainsTaxRate, errors );

        // Economic rates validation
        ValidateEconomicRates( annualInflationRate, annualGrowthRate, errors );

        return errors;
    }

    private static void ValidateInvestedAmount( double investedAmount, List<string> errors )
    {
        if ( investedAmount < AppreciatorInputsRanges.InvestedAmount.Minimum )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestedAmount )]} must be greater than {AppreciatorInputsRanges.InvestedAmount.Minimum:C0}." );
        }
        if ( investedAmount > AppreciatorInputsRanges.InvestedAmount.Maximum )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestedAmount )]} should not exceed {AppreciatorInputsRanges.InvestedAmount.Maximum:C0}." );
        }
    }

    private static void ValidateInitialAge( int initialAge, int investmentDuration, List<string> errors )
    {
        if ( initialAge < AppreciatorInputsRanges.InitialAge.Minimum )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialAge )]} must be at least {AppreciatorInputsRanges.InitialAge.Minimum} years old." );
        }
        if ( initialAge > AppreciatorInputsRanges.InitialAge.Maximum )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialAge )]} should not exceed {AppreciatorInputsRanges.InitialAge.Maximum} years." );
        }

        int maxAge = initialAge + investmentDuration;
        if ( maxAge > AppreciatorInputsRanges.FinalAge.Maximum )
        {
            errors.Add( $"• The combination of {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialAge )]} ({initialAge}) and {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestmentDuration )]} ({investmentDuration}) results in age {maxAge}, which exceeds {AppreciatorInputsRanges.FinalAge.Maximum:F0} years." );
        }
    }

    private static void ValidateInvestmentDuration( int investmentDuration, List<string> errors )
    {
        if ( investmentDuration < AppreciatorInputsRanges.InvestmentDuration.Minimum )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestmentDuration )]} must be greater than {AppreciatorInputsRanges.InvestmentDuration.Minimum}." );
        }
        if ( investmentDuration > AppreciatorInputsRanges.InvestmentDuration.Maximum )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestmentDuration )]} should not exceed {AppreciatorInputsRanges.InvestmentDuration.Maximum}." );
        }
    }

    private static void ValidateTaxRates(
        double initialFederalTaxRate, double withdrawalFederalTaxRate,
        double initialStateTaxRate, double withdrawalStateTaxRate,
        double federalCapitalGainsTaxRate, double stateCapitalGainsTaxRate, List<string> errors )
    {
        // Individual rate validation
        if ( !AppreciatorInputsRanges.InitialFederalTaxRate.Contains( initialFederalTaxRate ) )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialFederalTaxRate )]} must be between {AppreciatorInputsRanges.InitialFederalTaxRate.Minimum}% and {AppreciatorInputsRanges.InitialFederalTaxRate.Maximum}%." );
        }
        if ( !AppreciatorInputsRanges.WithdrawalFederalTaxRate.Contains( withdrawalFederalTaxRate ) )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.WithdrawalFederalTaxRate )]} must be between {AppreciatorInputsRanges.WithdrawalFederalTaxRate.Minimum}% and {AppreciatorInputsRanges.WithdrawalFederalTaxRate.Maximum}%." );
        }
        if ( !AppreciatorInputsRanges.InitialStateTaxRate.Contains( initialStateTaxRate ) )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialStateTaxRate )]} must be between {AppreciatorInputsRanges.InitialStateTaxRate.Minimum}% and {AppreciatorInputsRanges.InitialStateTaxRate.Maximum}%." );
        }
        if ( !AppreciatorInputsRanges.WithdrawalStateTaxRate.Contains( withdrawalStateTaxRate ) )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.WithdrawalStateTaxRate )]} must be between {AppreciatorInputsRanges.WithdrawalStateTaxRate.Minimum}% and {AppreciatorInputsRanges.WithdrawalStateTaxRate.Maximum}%." );
        }
        if ( !AppreciatorInputsRanges.FederalCapitalGainsTaxRate.Contains( federalCapitalGainsTaxRate ) )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.FederalCapitalGainsTaxRate )]} must be between {AppreciatorInputsRanges.FederalCapitalGainsTaxRate.Minimum}% and {AppreciatorInputsRanges.FederalCapitalGainsTaxRate.Maximum}%." );
        }
        if ( !AppreciatorInputsRanges.StateCapitalGainsTaxRate.Contains( stateCapitalGainsTaxRate ) )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.StateCapitalGainsTaxRate )]} must be between {AppreciatorInputsRanges.StateCapitalGainsTaxRate.Minimum}% and {AppreciatorInputsRanges.StateCapitalGainsTaxRate.Maximum}%." );
        }
    }

    private static void ValidateEconomicRates( double anualInflationRate, double annualGrowthRate, List<string> errors )
    {
        if ( !AppreciatorInputsRanges.AnnualInflationRate.Contains( anualInflationRate ) )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.AnnualInflationRate )]} must be between {AppreciatorInputsRanges.AnnualInflationRate.Minimum}% and {AppreciatorInputsRanges.AnnualInflationRate.Maximum}%." );
        }

        if ( !AppreciatorInputsRanges.AnnualGrowthRate.Contains( annualGrowthRate ) )
        {
            errors.Add( $"• {AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.AnnualGrowthRate )]} must be between {AppreciatorInputsRanges.AnnualGrowthRate.Minimum}% and {AppreciatorInputsRanges.AnnualGrowthRate.Maximum}%." );
        }
    }
}
