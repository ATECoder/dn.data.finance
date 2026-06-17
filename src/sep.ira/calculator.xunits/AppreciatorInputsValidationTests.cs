namespace cc.isr.Finance.Sep.Ira;

/// <summary>   An appreciator inputs validation tests. </summary>
/// <remarks>   2026-06-16. </remarks>
public class AppreciatorInputsValidationTests
{
    private const double ValidInvestedAmount = 50000;
    private const int ValidInitialAge = 50;
    private const int ValidInvestmentDuration = 20;
    private const double ValidTaxRate = 25;
    private const double ValidAnnualInflationRate = 2.75;
    private const double ValidAnnualGrowthRate = 7;

    #region " Invested Amount Validation Tests "

    /// <summary>
    /// (Unit Test Method) validates the inputs with valid invested amount no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithValidInvestedAmountNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with zero invested amount returns error.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithZeroInvestedAmountReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            0, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.InvestedAmount.Minimum:C0}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestedAmount )], errors[0] );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with negative invested amount returns error.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithNegativeInvestedAmountReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            -1000, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.InvestedAmount.Minimum:C0}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestedAmount )], errors[0] );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with excessively high invested amount returns
    /// error.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithExcessivelyHighInvestedAmountReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            2 * AppreciatorInputsRanges.InvestedAmount.Maximum, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.InvestedAmount.Maximum:C0}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestedAmount )], errors[0] );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with maximum invested amount no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithMaximumInvestedAmountNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            AppreciatorInputsRanges.InvestedAmount.Maximum, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    #endregion

    #region " Initial Initial Age Validation Tests "

    /// <summary>
    /// (Unit Test Method) validates the inputs with valid initial age no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithValidInitialAgeNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with initial age below minimum returns error.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithInitialAgeBelowMinimumReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ( int ) AppreciatorInputsRanges.InitialAge.Minimum - 1, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.InitialAge.Minimum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialAge )], errors[0] );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with minimum initial age no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithMinimumInitialAgeNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ( int ) AppreciatorInputsRanges.InitialAge.Minimum, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with initial age above maximum returns error.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithInitialAgeAboveMaximumReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ( int ) AppreciatorInputsRanges.InitialAge.Maximum + 1, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Equal( 2, errors.Count );
        Assert.Contains( $"{AppreciatorInputsRanges.InitialAge.Maximum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialAge )], errors[0] );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with final age exceeding maximum returns error.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithFinalAgeExceedingMaxReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, AppreciatorInputsInitialValues.InitialAge,
            ( int ) AppreciatorInputsRanges.FinalAge.Maximum - AppreciatorInputsInitialValues.InitialAge + 1,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.FinalAge.Maximum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialAge )], errors[0] );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with final age at maximum no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithFinalAgeAtMaxNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, AppreciatorInputsInitialValues.InitialAge,
            ( int ) AppreciatorInputsRanges.FinalAge.Maximum - AppreciatorInputsInitialValues.InitialAge,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    #endregion

    #region " Investment Duration Validation Tests "

    /// <summary>
    /// (Unit Test Method) validates the inputs with zero investment duration returns error.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithZeroInvestmentDurationReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, 0,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.InvestmentDuration.Minimum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestmentDuration )], errors[0] );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with negative investment duration returns error.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithNegativeInvestmentDurationReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, -5,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.InvestmentDuration.Minimum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestmentDuration )], errors[0] );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with investment duration above maximum returns
    /// error.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithInvestmentDurationAboveMaximumReturnsError()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ( int ) AppreciatorInputsRanges.InvestmentDuration.Maximum + 1,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.True( errors.Count >= 1, "Should have at least one error" );
        string expectedValue = $"{AppreciatorInputsRanges.InvestmentDuration.Maximum:F0}";
        Assert.Contains( errors, e => e.Contains( expectedValue ) );
        expectedValue = AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestmentDuration )];
        Assert.Contains( errors, e => e.Contains( expectedValue ) );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with maximum investment duration no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithMaximumInvestmentDurationNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ( int ) AppreciatorInputsRanges.InitialAge.Minimum, ( int ) AppreciatorInputsRanges.InvestmentDuration.Maximum,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    #endregion

    #region " Tax Rate Validation Tests "

    /// <summary>
    /// (Unit Test Method) validates the inputs with valid tax rates no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithValidTaxRatesNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    /// <summary>
    /// (Data-driven Unit Test Method) validates the inputs with negative initial federal tax rate
    /// returns error described by taxRate.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    /// <param name="taxRate">  The tax rate. </param>
    [Theory]
    [InlineData( -0.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithNegativeInitialFederalTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            taxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.InitialFederalTaxRate.Maximum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialFederalTaxRate )], errors[0] );
    }

    /// <summary>
    /// (Data-driven Unit Test Method) validates the inputs with excessive withdrawal federal tax
    /// rate returns error described by taxRate.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    /// <param name="taxRate">  The tax rate. </param>
    [Theory]
    [InlineData( 100.1 )]
    [InlineData( 150 )]
    public void ValidateInputsWithExcessiveWithdrawalFederalTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, taxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.True( errors.Count >= 1, "Should have at least one error" );
        Assert.Contains( $"{AppreciatorInputsRanges.WithdrawalFederalTaxRate.Maximum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.WithdrawalFederalTaxRate )], errors[0] );
    }

    /// <summary>
    /// (Data-driven Unit Test Method) validates the inputs with negative initial state tax rate
    /// returns error described by taxRate.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    /// <param name="taxRate">  The tax rate. </param>
    [Theory]
    [InlineData( -0.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithNegativeInitialStateTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, taxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.InitialStateTaxRate.Maximum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialStateTaxRate )], errors[0] );
    }

    /// <summary>
    /// (Data-driven Unit Test Method) validates the inputs with negative withdrawal state tax rate
    /// returns error described by taxRate.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    /// <param name="taxRate">  The tax rate. </param>
    [Theory]
    [InlineData( -0.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithNegativeWithdrawalStateTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, taxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.WithdrawalStateTaxRate.Maximum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.WithdrawalStateTaxRate )], errors[0] );
    }

    /// <summary>
    /// (Data-driven Unit Test Method) validates the inputs with negative federal capital gains tax
    /// rate returns error described by taxRate.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    /// <param name="taxRate">  The tax rate. </param>
    [Theory]
    [InlineData( -0.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithNegativeFederalCapitalGainsTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            taxRate, ValidTaxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.FederalCapitalGainsTaxRate.Maximum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.FederalCapitalGainsTaxRate )], errors[0] );
    }

    /// <summary>
    /// (Data-driven Unit Test Method) validates the inputs with negative state capital gains tax
    /// rate returns error described by taxRate.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    /// <param name="taxRate">  The tax rate. </param>
    [Theory]
    [InlineData( -0.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithNegativeStateCapitalGainsTaxRateReturnsError( double taxRate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, taxRate, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.StateCapitalGainsTaxRate.Maximum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.StateCapitalGainsTaxRate )], errors[0] );
    }

    #endregion

    #region " Economic Rates Validation Tests "

    /// <summary>
    /// (Unit Test Method) validates the inputs with valid inflation rate no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithValidInflationRateNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, AppreciatorInputsInitialValues.AnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    /// <summary>
    /// (Data-driven Unit Test Method) validates the inputs with inflation rate below minimum returns
    /// error described by rate.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    /// <param name="rate"> The rate. </param>
    [Theory]
    [InlineData( -10.1 )]
    [InlineData( -50 )]
    public void ValidateInputsWithInflationRateBelowMinimumReturnsError( double rate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, rate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.AnnualInflationRate.Maximum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.AnnualInflationRate )], errors[0] );
    }

    /// <summary>
    /// (Data-driven Unit Test Method) validates the inputs with inflation rate above maximum returns
    /// error described by rate.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    /// <param name="rate"> The rate. </param>
    [Theory]
    [InlineData( 50.1 )]
    [InlineData( 100 )]
    public void ValidateInputsWithInflationRateAboveMaximumReturnsError( double rate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, rate, ValidAnnualGrowthRate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.AnnualInflationRate.Maximum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.AnnualInflationRate )], errors[0] );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with valid annual growth rage no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithValidAnnualGrowthRageNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, AppreciatorInputsInitialValues.AnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    /// <summary>
    /// (Data-driven Unit Test Method) validates the inputs with annual growth rage below minimum
    /// returns error described by rate.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    /// <param name="rate"> The rate. </param>
    [Theory]
    [InlineData( -50.1 )]
    [InlineData( -100 )]
    public void ValidateInputsWithAnnualGrowthRageBelowMinimumReturnsError( double rate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, rate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.AnnualGrowthRate.Minimum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.AnnualGrowthRate )], errors[0] );
    }

    /// <summary>
    /// (Data-driven Unit Test Method) validates the inputs with annual growth rage above maximum
    /// returns error described by rate.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    /// <param name="rate"> The rate. </param>
    [Theory]
    [InlineData( 100.1 )]
    [InlineData( 150 )]
    public void ValidateInputsWithAnnualGrowthRageAboveMaximumReturnsError( double rate )
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, rate );

        // Assert
        _ = Assert.Single( errors );
        Assert.Contains( $"{AppreciatorInputsRanges.AnnualGrowthRate.Minimum}", errors[0] );
        Assert.Contains( AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.AnnualGrowthRate )], errors[0] );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with negative return no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithNegativeReturnNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            ValidTaxRate, ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, ValidAnnualInflationRate, -10 );

        // Assert
        Assert.Empty( errors );
    }

    #endregion

    #region " Multiple Errors Tests "

    /// <summary>
    /// (Unit Test Method) validates the inputs with multiple errors returns all errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithMultipleErrorsReturnsAllErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            -1000,      // Invalid invested Amount
            17,            // Invalid initial age
            0,       // Invalid investment duration
            150,   // Invalid tax rate
            ValidTaxRate, ValidTaxRate, ValidTaxRate,
            ValidTaxRate, ValidTaxRate, -50, 150 );  // Invalid inflation and return

        // Assert
        Assert.True( errors.Count >= 4, $"Expected at least 4 errors, got {errors.Count}" );
        string expectedValue = $"{AppreciatorInputsRanges.InvestmentDuration.Minimum:C0}";
        Assert.Contains( errors, e => e.Contains( expectedValue ) );
        expectedValue = AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestmentDuration )];
        Assert.Contains( errors, e => e.Contains( expectedValue ) );

        expectedValue = $"{AppreciatorInputsRanges.InitialFederalTaxRate.Minimum}";
        Assert.Contains( errors, e => e.Contains( expectedValue ) );
        expectedValue = AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InitialFederalTaxRate )];
        Assert.Contains( errors, e => e.Contains( expectedValue ) );

        expectedValue = $"{AppreciatorInputsRanges.InvestmentDuration.Minimum}";
        Assert.Contains( errors, e => e.Contains( expectedValue ) );
        expectedValue = AppreciatorReportBuilder.Titles[nameof( AppreciatorInputsRanges.InvestmentDuration )];
        Assert.Contains( errors, e => e.Contains( expectedValue ) );
    }

    #endregion

    #region " Edge Cases Tests "

    /// <summary>
    /// (Unit Test Method) validates the inputs with all zero tax rates no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithAllZeroTaxRatesNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            0, 0, 0, 0,
            0, 0, ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with all maximum tax rates no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithAllMaximumTaxRatesNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            ValidInvestedAmount, ValidInitialAge, ValidInvestmentDuration,
            AppreciatorInputsRanges.InitialFederalTaxRate.Maximum, AppreciatorInputsRanges.WithdrawalFederalTaxRate.Maximum,
            AppreciatorInputsRanges.InitialStateTaxRate.Maximum, AppreciatorInputsRanges.WithdrawalStateTaxRate.Maximum,
            AppreciatorInputsRanges.FederalCapitalGainsTaxRate.Maximum, AppreciatorInputsRanges.StateCapitalGainsTaxRate.Maximum,
            ValidAnnualInflationRate, ValidAnnualGrowthRate );

        // Assert
        Assert.Empty( errors );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with minimum valid inputs no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithMinimumValidInputsNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            AppreciatorInputsRanges.InvestedAmount.Minimum,
            ( int ) AppreciatorInputsRanges.InitialAge.Minimum,
            ( int ) AppreciatorInputsRanges.InvestmentDuration.Minimum,
            AppreciatorInputsRanges.InitialFederalTaxRate.Minimum, AppreciatorInputsRanges.WithdrawalFederalTaxRate.Minimum,
            AppreciatorInputsRanges.InitialStateTaxRate.Minimum, AppreciatorInputsRanges.WithdrawalStateTaxRate.Minimum,
            AppreciatorInputsRanges.FederalCapitalGainsTaxRate.Minimum, AppreciatorInputsRanges.StateCapitalGainsTaxRate.Minimum,
            AppreciatorInputsRanges.AnnualInflationRate.Minimum, AppreciatorInputsRanges.AnnualGrowthRate.Minimum );

        // Assert
        Assert.Empty( errors );
    }

    /// <summary>
    /// (Unit Test Method) validates the inputs with maximum valid inputs no errors.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void ValidateInputsWithMaximumValidInputsNoErrors()
    {
        // Arrange & Act
        List<string> errors = AppreciatorInputValidator.ValidateInputs(
            AppreciatorInputsRanges.InvestedAmount.Maximum,
            ( int ) AppreciatorInputsRanges.InitialAge.Maximum,
            ( int ) AppreciatorInputsRanges.InvestmentDuration.Minimum,
            AppreciatorInputsRanges.InitialFederalTaxRate.Maximum,
            AppreciatorInputsRanges.WithdrawalFederalTaxRate.Maximum,
            AppreciatorInputsRanges.InitialStateTaxRate.Maximum,
            AppreciatorInputsRanges.WithdrawalStateTaxRate.Maximum,
            AppreciatorInputsRanges.FederalCapitalGainsTaxRate.Maximum,
            AppreciatorInputsRanges.StateCapitalGainsTaxRate.Maximum,
            AppreciatorInputsRanges.AnnualInflationRate.Maximum,
            AppreciatorInputsRanges.AnnualGrowthRate.Maximum );

        // Assert
        Assert.Empty( errors );
    }

    #endregion
}
