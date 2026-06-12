namespace cc.isr.Finance.Sep.Ira;

/// <summary>   Provides validation logic for SEP-IRA appreciator inputs. </summary>
/// <remarks>   2026-06-11. </remarks>
public static class AppreciatorInputValidator
{
    /// <summary>   Validates all calculator inputs and returns a list of validation errors. </summary>
    /// <remarks>   2026-06-11. </remarks>
    /// <param name="principal">                The investment principal in dollars. </param>
    /// <param name="initialAge">               The initial age in years. </param>
    /// <param name="years">                    The investment duration in years. </param>
    /// <param name="presentFederalTaxRate">    The present federal tax rate as a percentage. </param>
    /// <param name="futureFederalTaxRate">     The future federal tax rate as a percentage. </param>
    /// <param name="presentStateTaxRate">      The present state tax rate as a percentage. </param>
    /// <param name="futureStateTaxRate">       The future state tax rate as a percentage. </param>
    /// <param name="capitalGainsTaxRate">      The capital gains tax rate as a percentage. </param>
    /// <param name="inflationRate">            The inflation rate as a percentage. </param>
    /// <param name="annualReturn">             The annual return rate as a percentage. </param>
    /// <returns>   A list of validation error messages. Empty if all inputs are valid. </returns>
    public static List<string> ValidateInputs(
        double principal, int initialAge, int years,
        double presentFederalTaxRate, double futureFederalTaxRate,
        double presentStateTaxRate, double futureStateTaxRate,
        double capitalGainsTaxRate, double inflationRate, double annualReturn )
    {
        List<string> errors = [];

        // Principal validation
        ValidatePrincipal( principal, errors );

        // Age validation
        ValidateAge( initialAge, years, errors );

        // Years validation
        ValidateYears( years, errors );

        // Tax rates validation
        ValidateTaxRates( presentFederalTaxRate, futureFederalTaxRate, presentStateTaxRate, futureStateTaxRate, capitalGainsTaxRate, errors );

        // Economic rates validation
        ValidateEconomicRates( inflationRate, annualReturn, errors );

        return errors;
    }

    private static void ValidatePrincipal( double principal, List<string> errors )
    {
        if ( principal <= 0 )
        {
            errors.Add( "• Principal must be greater than $0." );
        }
        if ( principal > 10_000_000 )
        {
            errors.Add( "• Principal should not exceed $10,000,000 (unrealistic value)." );
        }
    }

    private static void ValidateAge( int initialAge, int years, List<string> errors )
    {
        if ( initialAge < 18 )
        {
            errors.Add( "• Initial Age must be at least 18 years old." );
        }
        if ( initialAge > 120 )
        {
            errors.Add( "• Initial Age should not exceed 120 years." );
        }

        int maxAge = initialAge + years;
        if ( maxAge > 150 )
        {
            errors.Add( $"• The combination of Initial Age ({initialAge}) and Years ({years}) results in age {maxAge}, which exceeds 150 years." );
        }
    }

    private static void ValidateYears( int years, List<string> errors )
    {
        if ( years <= 0 )
        {
            errors.Add( "• Years must be greater than 0." );
        }
        if ( years > 100 )
        {
            errors.Add( "• Years should not exceed 100 (unrealistic duration)." );
        }
    }

    private static void ValidateTaxRates(
        double presentFederalTaxRate, double futureFederalTaxRate,
        double presentStateTaxRate, double futureStateTaxRate,
        double capitalGainsTaxRate, List<string> errors )
    {
        // Individual rate validation
        if ( presentFederalTaxRate is < 0 or > 100 )
        {
            errors.Add( "• Present Federal Tax Rate must be between 0% and 100%." );
        }
        if ( futureFederalTaxRate is < 0 or > 100 )
        {
            errors.Add( "• Future Federal Tax Rate must be between 0% and 100%." );
        }
        if ( presentStateTaxRate is < 0 or > 100 )
        {
            errors.Add( "• Present State Tax Rate must be between 0% and 100%." );
        }
        if ( futureStateTaxRate is < 0 or > 100 )
        {
            errors.Add( "• Future State Tax Rate must be between 0% and 100%." );
        }
        if ( capitalGainsTaxRate is < 0 or > 100 )
        {
            errors.Add( "• Capital Gains Tax Rate must be between 0% and 100%." );
        }

        // Combined tax rate validation
        double combinedPresentTax = presentFederalTaxRate + presentStateTaxRate;
        double combinedFutureTax = futureFederalTaxRate + futureStateTaxRate;

        if ( combinedPresentTax > 100 )
        {
            errors.Add( $"• Combined Present Tax Rate ({combinedPresentTax:F1}%) exceeds 100% (Present Federal {presentFederalTaxRate:F1}% + Present State {presentStateTaxRate:F1}%)." );
        }
        if ( combinedFutureTax > 100 )
        {
            errors.Add( $"• Combined Future Tax Rate ({combinedFutureTax:F1}%) exceeds 100% (Future Federal {futureFederalTaxRate:F1}% + Future State {futureStateTaxRate:F1}%)." );
        }
    }

    private static void ValidateEconomicRates( double inflationRate, double annualReturn, List<string> errors )
    {
        if ( inflationRate is < -10 or > 50 )
        {
            errors.Add( "• Inflation Rate must be between -10% and 50%." );
        }

        if ( annualReturn is < -50 or > 100 )
        {
            errors.Add( "• Annual Return must be between -50% and 100%." );
        }
    }
}
