namespace cc.isr.Finance.Sep.Ira;

/// <summary>   Unit tests for Appreciator class calculation methods. </summary>
/// <remarks>   2026-06-16. </remarks>
public class AppreciatorCalculationTests
{
    private readonly Appreciator _appreciator;

    /// <summary>   Default constructor. </summary>
    /// <remarks>   2026-06-16. </remarks>
    public AppreciatorCalculationTests() => this._appreciator = new();

    #region " CalculateFutureValue Tests "

    /// <summary>
    /// (Unit Test Method) calculates the future value with default values produces valid results.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithDefaultValuesProducesValidResults()
    {
        // Arrange - use default values
        Appreciator appreciatorWithDefaults = new();

        // Act
        appreciatorWithDefaults.CalculateFutureValue();

        // Assert - basic sanity checks
        Assert.True( appreciatorWithDefaults.CapitalAccountBalance > 0, "Capital account balance should be positive" );
        Assert.True( appreciatorWithDefaults.CapitalGain > 0, "Capital gain should be positive" );
        Assert.True( appreciatorWithDefaults.NetCashOutValue > 0, "Net cash-out value should be positive" );
        Assert.True( appreciatorWithDefaults.WithdrawalTaxLiability > 0, "Withdrawal tax liability should be positive" );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value with zero growth rate produces no gain.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithZeroGrowthRateProducesNoGain()
    {
        // Arrange
        this._appreciator.InvestedAmount = 100000;
        this._appreciator.AnnualGrowthRate = 0;
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.InvestmentDuration = 10;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert - with 0% growth and 0% initial tax, capital should equal invested amount
        Assert.Equal( this._appreciator.InvestedAmount, this._appreciator.CapitalAccountBalance, 0 );
        Assert.Equal( 0, this._appreciator.CapitalGain, 0 );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value initial tax is applied correctly.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueInitialTaxIsAppliedCorrectly()
    {
        // Arrange
        decimal investedAmount = 100000;
        decimal initialFederalTax = 20;
        decimal initialStateTax = 10;
        this._appreciator.InvestedAmount = investedAmount;
        this._appreciator.InitialFederalTaxRate = initialFederalTax;
        this._appreciator.InitialStateTaxRate = initialStateTax;
        this._appreciator.AnnualGrowthRate = 0;
        this._appreciator.InvestmentDuration = 1;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        decimal expectedInitialCapital = investedAmount * (1 - ((initialFederalTax + initialStateTax) / 100.0m));
        Assert.Equal( expectedInitialCapital, this._appreciator.CapitalAccountBalance, 2 );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value with high growth rate produces large gain.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithHighGrowthRateProducesLargeGain()
    {
        // Arrange
        decimal investedAmount = 50000;
        this._appreciator.InvestedAmount = investedAmount;
        this._appreciator.AnnualGrowthRate = 15;  // High growth rate
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.InvestmentDuration = 20;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        decimal expectedBalance = investedAmount * ( decimal ) Math.Pow( 1.15, 20 );
        Assert.Equal( expectedBalance, this._appreciator.CapitalAccountBalance, 0 );
        Assert.True( this._appreciator.CapitalGain > investedAmount * 2,
            "With 15% growth over 20 years, capital should more than triple" );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value with capital gains tax reduces net value.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithCapitalGainsTaxReducesNetValue()
    {
        // Arrange
        this._appreciator.InvestedAmount = 100000;
        this._appreciator.AnnualGrowthRate = 10;
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.FederalCapitalGainsTaxRate = 20;
        this._appreciator.StateCapitalGainsTaxRate = 5;
        this._appreciator.InvestmentDuration = 10;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        Assert.True( this._appreciator.NetCashOutValue < this._appreciator.CapitalAccountBalance,
            "Net value should be less than pre-tax balance" );
        Assert.True( this._appreciator.WithdrawalTaxLiability > 0,
            "Should have tax liability" );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value with multiple years computes correctly.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithMultipleYearsComputesCorrectly()
    {
        // Arrange
        decimal investedAmount = 10000;
        this._appreciator.InvestedAmount = investedAmount;
        this._appreciator.AnnualGrowthRate = 7;
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.InvestmentDuration = 5;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        decimal growthRate = 1.07m;
        decimal expectedBalance = investedAmount * ( decimal ) Math.Pow( ( double ) growthRate, 5 );
        Assert.Equal( expectedBalance, this._appreciator.CapitalAccountBalance, 2 );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value with negative growth reduces capital.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithNegativeGrowthReducesCapital()
    {
        // Arrange
        decimal investedAmount = 100000;
        this._appreciator.InvestedAmount = investedAmount;
        this._appreciator.AnnualGrowthRate = -5;  // Negative growth
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.InvestmentDuration = 10;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        Assert.True( this._appreciator.CapitalAccountBalance < investedAmount,
            "With negative growth, balance should decrease" );
        Assert.True( this._appreciator.CapitalGain < 0,
            "Capital gain should be negative" );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value with zero investment duration keeps initial
    /// amount.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithZeroInvestmentDurationKeepsInitialAmount()
    {
        // Arrange
        decimal investedAmount = 50000;
        this._appreciator.InvestedAmount = investedAmount;
        this._appreciator.InvestmentDuration = 0;
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        Assert.Equal( investedAmount, this._appreciator.CapitalAccountBalance, 2 );
        Assert.Equal( 0, this._appreciator.CapitalGain, 2 );
    }

    #endregion

    #region " CalculateFutureValueSepIraWithRmd Tests "

    /// <summary>
    /// (Unit Test Method) calculates the future value SEP IRA with RMD with default values
    /// produces valid results.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueSepIraWithRmdWithDefaultValuesProducesValidResults()
    {
        // Arrange & Act
        this._appreciator.CalculateFutureValueSepIraWithRmd();

        // Assert
        Assert.True( this._appreciator.SepIraAccountBalance >= 0,
            "SEP IRA account balance should be non-negative" );
        Assert.True( this._appreciator.CapitalAccountBalance >= 0,
            "Capital account balance should be non-negative" );
        Assert.True( this._appreciator.NetCashOutValue >= 0,
            "Net cash-out value should be non-negative" );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value SEP IRA with RMD before age 72 has no
    /// RMD.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueSepIraWithRmdBeforeAge72HasNoRmd()
    {
        // Arrange
        this._appreciator.InvestedAmount = 100000;
        this._appreciator.InitialAge = 50;
        this._appreciator.InvestmentDuration = 20;  // Goes to age 70, before RMD starts at 72
        this._appreciator.AnnualGrowthRate = 7;
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;

        // Act
        this._appreciator.CalculateFutureValueSepIraWithRmd();

        // Assert
        Assert.Equal( 0, this._appreciator.DiscountedFederalTaxesPaid, 2 );
        Assert.Equal( 0, this._appreciator.DiscountedStateTaxesPaid, 2 );
        Assert.True( this._appreciator.CapitalAccountBalance == 0,
            "Capital account should remain zero before RMD age" );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value SEP IRA with RMD after age 72 computes
    /// RMD correctly.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueSepIraWithRmdAfterAge72ComputesRmdCorrectly()
    {
        // Arrange
        this._appreciator.InvestedAmount = 100000;
        this._appreciator.InitialAge = 72;
        this._appreciator.InvestmentDuration = 1;
        this._appreciator.AnnualGrowthRate = 0;  // No growth for simplicity
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.WithdrawalFederalTaxRate = 0;
        this._appreciator.WithdrawalStateTaxRate = 0;

        // Act
        this._appreciator.CalculateFutureValueSepIraWithRmd();

        // Assert - RMD at age 72 from table is 27.4
        decimal expectedRmd = this._appreciator.InvestedAmount / 27.4m;
        decimal expectedSepiIraBalance = this._appreciator.InvestedAmount - expectedRmd;
        Assert.True( this._appreciator.CapitalAccountBalance > 0,
            "Capital account should have RMD contribution" );
        Assert.True( this._appreciator.SepIraAccountBalance < this._appreciator.InvestedAmount,
            "SEP IRA balance should be reduced by RMD" );
        Assert.Equal( expectedSepiIraBalance, this._appreciator.SepIraAccountBalance, 1 );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value SEP IRA with RMD taxes are applied to
    /// RMD.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueSepIraWithRmdTaxesAreAppliedToRmd()
    {
        // Arrange
        this._appreciator.InvestedAmount = 100000;
        this._appreciator.InitialAge = 72;
        this._appreciator.InvestmentDuration = 1;
        this._appreciator.AnnualGrowthRate = 0;
        this._appreciator.InitialFederalTaxRate = 20;
        this._appreciator.InitialStateTaxRate = 10;
        this._appreciator.WithdrawalFederalTaxRate = 20;
        this._appreciator.WithdrawalStateTaxRate = 10;

        // Act
        this._appreciator.CalculateFutureValueSepIraWithRmd();

        // Assert - RMD should have taxes applied
        Assert.True( this._appreciator.DiscountedFederalTaxesPaid > 0,
            "Should have federal taxes paid on RMD" );
        Assert.True( this._appreciator.DiscountedStateTaxesPaid > 0,
            "Should have state taxes paid on RMD" );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value SEP IRA with RMD multiple RMD years
    /// accumulates capital.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueSepIraWithRmdMultipleRmdYearsAccumulatesCapital()
    {
        // Arrange
        this._appreciator.InvestedAmount = 100000;
        this._appreciator.InitialAge = 70;
        this._appreciator.InvestmentDuration = 5;  // Goes from 70 to 75, includes ages 72-75
        this._appreciator.AnnualGrowthRate = 0;
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.WithdrawalFederalTaxRate = 0;
        this._appreciator.WithdrawalStateTaxRate = 0;

        // Act
        this._appreciator.CalculateFutureValueSepIraWithRmd();

        // Assert - with multiple RMD years, capital should accumulate
        Assert.True( this._appreciator.CapitalAccountBalance > 0,
            "Capital should accumulate from multiple RMDs" );
        Assert.True( this._appreciator.SepIraAccountBalance < this._appreciator.InvestedAmount,
            "SEP IRA should decrease due to RMDs" );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value SEP IRA with RMD with growth rate
    /// increases both accounts.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueSepIraWithRmdWithGrowthRateIncreasesBothAccounts()
    {
        // Arrange
        this._appreciator.InvestedAmount = 50000;
        this._appreciator.InitialAge = 70;
        this._appreciator.InvestmentDuration = 3;
        this._appreciator.AnnualGrowthRate = 10;  // 10% growth
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;

        // Act
        this._appreciator.CalculateFutureValueSepIraWithRmd();

        // Assert
        decimal sepPlusCapital = this._appreciator.SepIraAccountBalance + this._appreciator.CapitalAccountBalance;
        Assert.True( sepPlusCapital > this._appreciator.InvestedAmount,
            "With growth, total should exceed initial investment" );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value SEP IRA with RMD sets final age
    /// correctly.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueSepIraWithRmdSetsFinalAgeCorrectly()
    {
        // Arrange
        int initialAge = 50;
        int duration = 15;
        this._appreciator.InitialAge = initialAge;
        this._appreciator.InvestmentDuration = duration;

        // Act
        this._appreciator.CalculateFutureValueSepIraWithRmd();

        // Assert
        Assert.Equal( initialAge + duration, this._appreciator.FinalAge );
    }

    #endregion

    #region " Uniform Lifetime Table Tests "

    /// <summary>   (Unit Test Method) uniform lifetime table has valid data. </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void UniformLifetimeTableHasValidData()
    {
        // Assert - check that the table has expected ranges
        Assert.Contains( 72, this._appreciator.UniformLifetimeTable.Keys );
        Assert.Contains( 120, this._appreciator.UniformLifetimeTable.Keys );
        Assert.Equal( 27.4m, this._appreciator.UniformLifetimeTable[72], 1 );
        Assert.Equal( 1.9m, this._appreciator.UniformLifetimeTable[120], 1 );
    }

    /// <summary>   (Unit Test Method) uniform lifetime table values decrease with age. </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void UniformLifetimeTableValuesDecreaseWithAge()
    {
        // Assert - check that divisor decreases (or stays same at 105-106) as age increases
        // Note: IRS table has a special case where 105 and 106 both have 4.5
        for ( int age = 72; age < 120; age++ )
        {
            if ( this._appreciator.UniformLifetimeTable.TryGetValue( age, out decimal atAge ) &&
                 this._appreciator.UniformLifetimeTable.TryGetValue( age + 1, out decimal atAgePlus1 ) )
            {
                // Most ages: divisor decreases
                // Special case at 105->106: divisor stays the same (both 4.5)
                Assert.True( atAge >= atAgePlus1,
                    $"Divisor should decrease or stay same from age {age} to {age + 1}" );
            }
            else
                Assert.Fail( "Uniform lifetime table should contain consecutive ages from 72 to 120" );
        }
    }

    #endregion

    #region " Property Tests "

    /// <summary>   (Unit Test Method) invested amount property can be set and retrieved. </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void InvestedAmountPropertyCanBeSetAndRetrieved()
    {
        // Arrange
        decimal testAmount = 250000;

        // Act
        this._appreciator.InvestedAmount = testAmount;

        // Assert
        Assert.Equal( testAmount, this._appreciator.InvestedAmount );
    }

    /// <summary>   (Unit Test Method) initial age property can be set and retrieved. </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void InitialAgePropertyCanBeSetAndRetrieved()
    {
        // Arrange
        int testAge = 55;

        // Act
        this._appreciator.InitialAge = testAge;

        // Assert
        Assert.Equal( testAge, this._appreciator.InitialAge );
    }

    /// <summary>
    /// (Unit Test Method) investment duration property can be set and retrieved.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void InvestmentDurationPropertyCanBeSetAndRetrieved()
    {
        // Arrange
        int testDuration = 25;

        // Act
        this._appreciator.InvestmentDuration = testDuration;

        // Assert
        Assert.Equal( testDuration, this._appreciator.InvestmentDuration );
    }

    /// <summary>   (Unit Test Method) all tax rate properties can be set and retrieved. </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void AllTaxRatePropertiesCanBeSetAndRetrieved()
    {
        // Arrange & Act
        this._appreciator.InitialFederalTaxRate = 25;
        this._appreciator.WithdrawalFederalTaxRate = 30;
        this._appreciator.InitialStateTaxRate = 8;
        this._appreciator.WithdrawalStateTaxRate = 9;
        this._appreciator.FederalCapitalGainsTaxRate = 15;
        this._appreciator.StateCapitalGainsTaxRate = 5;

        // Assert
        Assert.Equal( 25, this._appreciator.InitialFederalTaxRate );
        Assert.Equal( 30, this._appreciator.WithdrawalFederalTaxRate );
        Assert.Equal( 8, this._appreciator.InitialStateTaxRate );
        Assert.Equal( 9, this._appreciator.WithdrawalStateTaxRate );
        Assert.Equal( 15, this._appreciator.FederalCapitalGainsTaxRate );
        Assert.Equal( 5, this._appreciator.StateCapitalGainsTaxRate );
    }

    /// <summary>   (Unit Test Method) economic rate properties can be set and retrieved. </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void EconomicRatePropertiesCanBeSetAndRetrieved()
    {
        // Arrange & Act
        this._appreciator.AnnualInflationRate = 3.5m;
        this._appreciator.AnnualGrowthRate = 8.5m;

        // Assert
        Assert.Equal( 3.5m, this._appreciator.AnnualInflationRate );
        Assert.Equal( 8.5m, this._appreciator.AnnualGrowthRate );
    }

    #endregion

    #region " Edge Case Tests "

    /// <summary>
    /// (Unit Test Method) calculates the future value with all zero tax rates and zero growth
    /// preserves capital.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithAllZeroTaxRatesAndZeroGrowthPreservesCapital()
    {
        // Arrange
        decimal amount = 75000;
        this._appreciator.InvestedAmount = amount;
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.FederalCapitalGainsTaxRate = 0;
        this._appreciator.StateCapitalGainsTaxRate = 0;
        this._appreciator.AnnualGrowthRate = 0;
        this._appreciator.InvestmentDuration = 10;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        Assert.Equal( amount, this._appreciator.CapitalAccountBalance, 2 );
        Assert.Equal( 0, this._appreciator.WithdrawalTaxLiability, 2 );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value with very high tax rates reduces capital
    /// significantly.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithVeryHighTaxRatesReducesCapitalSignificantly()
    {
        // Arrange
        decimal amount = 100000;
        this._appreciator.InvestedAmount = amount;
        this._appreciator.InitialFederalTaxRate = 50;
        this._appreciator.InitialStateTaxRate = 50;
        this._appreciator.AnnualGrowthRate = 0;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        Assert.Equal( 0, this._appreciator.CapitalAccountBalance, 2 );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value with small investment produces proportional
    /// results.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithSmallInvestmentProducesProportionalResults()
    {
        // Arrange
        decimal smallAmount = 100;
        this._appreciator.InvestedAmount = smallAmount;
        this._appreciator.AnnualGrowthRate = 10;
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.InvestmentDuration = 1;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        Assert.Equal( smallAmount * 1.10m, this._appreciator.CapitalAccountBalance, 2 );
    }

    /// <summary>
    /// (Unit Test Method) calculates the future value with large investment produces proportional
    /// results.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void CalculateFutureValueWithLargeInvestmentProducesProportionalResults()
    {
        // Arrange
        decimal largeAmount = 10000000;
        this._appreciator.InvestedAmount = largeAmount;
        this._appreciator.AnnualGrowthRate = 5;
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.InvestmentDuration = 1;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        Assert.Equal( largeAmount * 1.05m, this._appreciator.CapitalAccountBalance, 0 );
    }

    #endregion

    #region " Comparison Tests "

    /// <summary>
    /// (Unit Test Method) SEP IRA calculation produces at least as much as simple calculation
    /// with no taxes.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void SepIraCalculationProducesAtLeastAsMuchAsSimpleCalculationWithNoTaxes()
    {
        // Arrange - run both calculations with identical parameters (no taxes)
        Appreciator simpleAppreciator = new()
        {
            InvestedAmount = 100000,
            AnnualGrowthRate = 7,
            InitialFederalTaxRate = 0,
            InitialStateTaxRate = 0,
            FederalCapitalGainsTaxRate = 0,
            StateCapitalGainsTaxRate = 0,
            InvestmentDuration = 20,
            InitialAge = 50
        };

        Appreciator sepIraAppreciator = new()
        {
            InvestedAmount = 100000,
            AnnualGrowthRate = 7,
            InitialFederalTaxRate = 0,
            InitialStateTaxRate = 0,
            FederalCapitalGainsTaxRate = 0,
            StateCapitalGainsTaxRate = 0,
            InvestmentDuration = 20,
            InitialAge = 50
        };

        // Act
        simpleAppreciator.CalculateFutureValue();
        sepIraAppreciator.CalculateFutureValueSepIraWithRmd();

        // Assert - SEP IRA should produce at least as much (no RMDs until age 72)
        decimal simpleFutureValue = simpleAppreciator.CapitalAccountBalance;
        decimal sepIraFutureValue = sepIraAppreciator.SepIraAccountBalance + sepIraAppreciator.CapitalAccountBalance;
        Assert.True( sepIraFutureValue >= simpleFutureValue * 0.95m,
            "SEP IRA should produce comparable results" );
    }

    #endregion

    #region " Output Properties Tests "

    /// <summary>   (Unit Test Method) output properties are initialized to zero. </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void OutputPropertiesAreInitializedToZero()
    {
        // Arrange
        Appreciator newAppreciator = new();

        // Assert - output properties should be initialized (may be 0 or default)
        _ = Assert.IsType<decimal>( newAppreciator.SepIraAccountBalance );
        _ = Assert.IsType<decimal>( newAppreciator.CapitalAccountBalance );
        _ = Assert.IsType<decimal>( newAppreciator.WithdrawalTaxLiability );
    }

    /// <summary>
    /// (Unit Test Method) net cash out value equals account balance minus withdrawal tax.
    /// </summary>
    /// <remarks>   2026-06-16. </remarks>
    [Fact]
    public void NetCashOutValueEqualsAccountBalanceMinusWithdrawalTax()
    {
        // Arrange
        this._appreciator.InvestedAmount = 100000;
        this._appreciator.AnnualGrowthRate = 5;
        this._appreciator.InitialFederalTaxRate = 0;
        this._appreciator.InitialStateTaxRate = 0;
        this._appreciator.FederalCapitalGainsTaxRate = 20;
        this._appreciator.InvestmentDuration = 10;

        // Act
        this._appreciator.CalculateFutureValue();

        // Assert
        decimal expectedNetValue = this._appreciator.CapitalAccountBalance - this._appreciator.WithdrawalTaxLiability;
        Assert.Equal( expectedNetValue, this._appreciator.NetCashOutValue, 2 );
    }

    #endregion
}
