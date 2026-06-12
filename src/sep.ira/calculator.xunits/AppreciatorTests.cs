namespace cc.isr.Finance.Sep.Ira;

public class AppreciatorTests
{
    private const double ValidPrincipal = 50000;
    private const int ValidInitialAge = 50;
    private const int ValidYears = 20;
    private const double ValidTaxRate = 25;
    private const double ValidInflationRate = 2.75;
    private const double ValidAnnualReturn = 7;

    #region Principal Validation Tests

    [Fact]
    public void ValidateInputsWithValidPrincipalNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            50000, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    [Fact]
    public void ValidateInputsWithZeroPrincipalReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            0, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Principal must be greater than $0", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithNegativePrincipalReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            -1000, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Principal must be greater than $0", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithExcessivelyHighPrincipalReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            11_000_000, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Principal should not exceed $10,000,000", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithMaximumPrincipalNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            10_000_000, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    #endregion

    #region Age Validation Tests

    [Fact]
    public void ValidateInputsWithValidAgeNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, 50, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    [Fact]
    public void ValidateInputsWithAgeBelowMinimumReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, 17, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Initial Age must be at least 18 years old", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithMinimumAgeNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, 18, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    [Fact]
    public void ValidateInputsWithAgeAboveMaximumReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, 121, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Initial Age should not exceed 120 years", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithAgeYearsCombinationExceeding150ReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, 100, 51,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "exceeds 150 years", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithAgeYearsCombinationAt150NoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, 100, 50,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    #endregion

    #region Years Validation Tests

    [Fact]
    public void ValidateInputsWithZeroYearsReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, 0,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Years must be greater than 0", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithNegativeYearsReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, -5,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Years must be greater than 0", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithYearsAboveMaximumReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, 101,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.True( errors.Count >= 1, "Should have at least one error" );
        Assert.Contains( errors, e => e.Contains( "Years should not exceed 100" ) );
    }

    [Fact]
    public void ValidateInputsWithMaximumYearsNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, 100,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    #endregion

    #region Tax Rate Validation Tests

    [Fact]
    public void ValidateInputsWithValidTaxRatesNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            35, 35, 9.3, 9.3,
            25, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    [Theory]
    [InlineData( -0.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithNegativePresentFederalTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            taxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Present Federal Tax Rate must be between 0% and 100%", errors[0] );
    }

    [Theory]
    [InlineData( 100.1 )]
    [InlineData( 150 )]
    public void ValidateInputsWithExcessiveFutureFederalTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, taxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.True( errors.Count >= 1, "Should have at least one error" );
        Assert.Contains( errors, e => e.Contains( "Future Federal Tax Rate must be between 0% and 100%" ) );
    }

    [Theory]
    [InlineData( -0.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithNegativePresentStateTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, taxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Present State Tax Rate must be between 0% and 100%", errors[0] );
    }

    [Theory]
    [InlineData( -0.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithNegativeFutureStateTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, taxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Future State Tax Rate must be between 0% and 100%", errors[0] );
    }

    [Theory]
    [InlineData( -0.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithNegativeCapitalGainsTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            taxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Capital Gains Tax Rate must be between 0% and 100%", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithCombinedPresentTaxRateExceeding100ReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            60, ValidTaxRate, 50, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Combined Present Tax Rate", errors[0] );
        Assert.Contains( "exceeds 100%", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithCombinedFutureTaxRateExceeding100ReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, 60, ValidTaxRate, 50,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Combined Future Tax Rate", errors[0] );
        Assert.Contains( "exceeds 100%", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithCombinedTaxRatesAt100NoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            60, 60, 40, 40,
            ValidTaxRate, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    #endregion

    #region Economic Rates Validation Tests

    [Fact]
    public void ValidateInputsWithValidInflationRateNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, 2.75, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    [Theory]
    [InlineData( -10.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithInflationRateBelowMinimumReturnsError( double rate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, rate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Inflation Rate must be between -10% and 50%", errors[0] );
    }

    [Theory]
    [InlineData( 50.1 )]
    [InlineData( 100 )]
    public void ValidateInputsWithInflationRateAboveMaximumReturnsError( double rate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, rate, ValidAnnualReturn );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Inflation Rate must be between -10% and 50%", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithValidAnnualReturnNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, 7 );

        // Assert
        Assert.Empty( errors );
    }

    [Theory]
    [InlineData( -50.1 )]
    [InlineData( -100 )]
    public void ValidateInputsWithAnnualReturnBelowMinimumReturnsError( double rate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, rate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Annual Return must be between -50% and 100%", errors[0] );
    }

    [Theory]
    [InlineData( 100.1 )]
    [InlineData( 150 )]
    public void ValidateInputsWithAnnualReturnAboveMaximumReturnsError( double rate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, rate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( "Annual Return must be between -50% and 100%", errors[0] );
    }

    [Fact]
    public void ValidateInputsWithNegativeReturnNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidInflationRate, -10 );

        // Assert
        Assert.Empty( errors );
    }

    #endregion

    #region Multiple Errors Tests

    [Fact]
    public void ValidateInputsWithMultipleErrorsReturnsAllErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            -1000,      // Invalid principal
            17,         // Invalid age
            0,          // Invalid years
            150,        // Invalid tax rate
            ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, -50, 150 );  // Invalid inflation and return

        // Assert
        Assert.True( errors.Count >= 4, $"Expected at least 4 errors, got {errors.Count}" );
        Assert.Contains( errors, e => e.Contains( "Principal must be greater than $0" ) );
        Assert.Contains( errors, e => e.Contains( "Initial Age must be at least 18" ) );
        Assert.Contains( errors, e => e.Contains( "Years must be greater than 0" ) );
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void ValidateInputsWithAllZeroTaxRatesNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            0, 0, 0, 0,
            0, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    [Fact]
    public void ValidateInputsWithAllMaximumTaxRatesNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidPrincipal, ValidInitialAge, ValidYears,
            100, 100, 0, 0,
            100, ValidInflationRate, ValidAnnualReturn );

        // Assert
        Assert.Empty( errors );
    }

    [Fact]
    public void ValidateInputsWithMinimumValidInputsNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            1, 18, 1,
            0, 0, 0, 0,
            0, -10, -50 );

        // Assert
        Assert.Empty( errors );
    }

    [Fact]
    public void ValidateInputsWithMaximumValidInputsNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            10_000_000, 120, 30,
            100, 100, 0, 0,
            100, 50, 100 );

        // Assert
        Assert.Empty( errors );
    }

    #endregion
}
