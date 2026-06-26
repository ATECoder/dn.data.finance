using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace cc.isr.Finance.Sep.Ira.ViewModels;

/// <summary>
/// ViewModel for the SEP IRA Appreciator calculator page.
/// Manages user input, calculation orchestration, and result presentation.
/// </summary>
public partial class AppreciatorViewModel : BaseViewModel, IDisposable
{
    /// <summary>
    /// Initializes a new instance of the AppreciatorViewModel class.
    /// </summary>
    public AppreciatorViewModel() => this.InitializeDefaults();

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
    /// resources.
    /// </summary>
    /// <remarks>   2026-06-25. </remarks>
    public void Dispose()
    {
        this.Dispose( true );
        GC.SuppressFinalize( this );
    }

    private bool _disposed;

    private void Dispose( bool disposing )
    {
        if ( !this._disposed )
        {
            if ( disposing )
            {
                // Dispose managed resources
                this._cancellationTokenSource?.Dispose();
            }
            // Free unmanaged resources (if any) here
            this._disposed = true;
        }
    }

    /// <summary>
    /// Initial investment amount in dollars.
    /// </summary>
    [ObservableProperty]
    public partial decimal InvestedAmount { get; set; }

    /// <summary>
    /// Starting age of the investor.
    /// </summary>
    [ObservableProperty]
    public partial int InitialAge { get; set; }

    /// <summary>
    /// Number of years for the investment projection.
    /// </summary>
    [ObservableProperty]
    public partial int InvestmentDuration { get; set; }

    /// <summary>
    /// Federal tax rate at initial investment (percentage 0-100).
    /// </summary>
    [ObservableProperty]
    public partial decimal InitialFederalTaxRate { get; set; }

    /// <summary>
    /// Federal tax rate at withdrawal time (percentage 0-100).
    /// </summary>
    [ObservableProperty]
    public partial decimal WithdrawalFederalTaxRate { get; set; }

    /// <summary>
    /// State income tax rate at initial investment (percentage 0-100).
    /// </summary>
    [ObservableProperty]
    public partial decimal InitialStateTaxRate { get; set; }

    /// <summary>
    /// State income tax rate at withdrawal time (percentage 0-100).
    /// </summary>
    [ObservableProperty]
    public partial decimal WithdrawalStateTaxRate { get; set; }

    /// <summary>
    /// Federal capital gains tax rate (percentage 0-100).
    /// </summary>
    [ObservableProperty]
    public partial decimal FederalCapitalGainsTaxRate { get; set; }

    /// <summary>
    /// State capital gains tax rate (percentage 0-100).
    /// </summary>
    [ObservableProperty]
    public partial decimal StateCapitalGainsTaxRate { get; set; }

    /// <summary>
    /// Annual inflation rate (percentage 0-100).
    /// </summary>
    [ObservableProperty]
    public partial decimal AnnualInflationRate { get; set; }

    /// <summary>
    /// Annual investment growth rate (percentage 0-100).
    /// </summary>
    [ObservableProperty]
    public partial decimal AnnualGrowthRate { get; set; }

    /// <summary>
    /// Formatted calculation results in fixed-width font compatible format.
    /// </summary>
    [ObservableProperty]
    public partial string? CalculationResults { get; set; }

    /// <summary>
    /// Error message to display to the user.
    /// </summary>
    [ObservableProperty]
    public partial string? ErrorMessage { get; set; }

    /// <summary>
    /// Indicates whether a calculation is currently in progress.
    /// </summary>
    [ObservableProperty]
    public partial bool IsCalculating { get; set; }

    /// <summary>
    /// Indicates whether the last calculation was successful.
    /// </summary>
    [ObservableProperty]
    public partial bool HasResults { get; set; }

    /// <summary>
    /// Initializes form fields with default values.
    /// </summary>
    private void InitializeDefaults()
    {
        this.InvestedAmount = 10000;
        this.InitialAge = 75;
        this.InvestmentDuration = 20;
        this.InitialFederalTaxRate = 35.0m;
        this.WithdrawalFederalTaxRate = 35.0m;
        this.InitialStateTaxRate = 9.3m;
        this.WithdrawalStateTaxRate = 9.3m;
        this.FederalCapitalGainsTaxRate = 25.0m;
        this.StateCapitalGainsTaxRate = 9.3m;
        this.AnnualInflationRate = 2.75m;
        this.AnnualGrowthRate = 7.0m;
        this.ErrorMessage = null;
        this.CalculationResults = null;
        this.HasResults = false;
        this.IsCalculating = false;
    }

    /// <summary>
    /// Executes the calculation based on current input parameters.
    /// </summary>
    [RelayCommand]
    public async Task Calculate()
    {
        try
        {
            // Clear previous state
            this.ErrorMessage = null;
            this.CalculationResults = null;
            this.HasResults = false;
            this.IsCalculating = true;

            // Create cancellation token for this calculation
            this._cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = this._cancellationTokenSource.Token;

            // Execute calculation on background thread
            await Task.Run( () =>
            {
                try
                {
                    // Validate inputs before calculation
                    this.ValidateInputs();

                    cancellationToken.ThrowIfCancellationRequested();

                    Appreciator appreciator = new()
                    {
                        InvestedAmount = this.InvestedAmount,
                        InitialAge = this.InitialAge,
                        InvestmentDuration = this.InvestmentDuration,
                        InitialFederalTaxRate = this.InitialFederalTaxRate,
                        WithdrawalFederalTaxRate = this.WithdrawalFederalTaxRate,
                        InitialStateTaxRate = this.InitialStateTaxRate,
                        WithdrawalStateTaxRate = this.WithdrawalStateTaxRate,
                        FederalCapitalGainsTaxRate = this.FederalCapitalGainsTaxRate,
                        StateCapitalGainsTaxRate = this.StateCapitalGainsTaxRate,
                        AnnualInflationRate = this.AnnualInflationRate,
                        AnnualGrowthRate = this.AnnualGrowthRate
                    };

                    appreciator.CalculateFutureValue();
                    Dictionary<string, string> skipIraResult = AppreciatorReportBuilder.BuildSimpleCapitalInvestmentResult( appreciator );

                    appreciator.CalculateFutureValueSepIraWithRmd( debug: false );
                    Dictionary<string, string> sepIraResult = AppreciatorReportBuilder.BuildSepIraInvestmentResult( appreciator );

                    string title = $"* Simple Investment vs. SEP IRA Comparison *";
                    string subtitle = $"-- from {appreciator.InitialAge} to {appreciator.FinalAge} at {appreciator.AnnualGrowthRate:F1}% growth rate --";

                    MainThread.BeginInvokeOnMainThread( () => this.CalculationResults = AppreciatorReportBuilder.BuildReport( title, subtitle,
                            AppreciatorReportBuilder.BuildComparisonReport( skipIraResult, sepIraResult, true ), 1 ) );

                    this.HasResults = true;
                }
                catch ( OperationCanceledException )
                {
                    throw; // Re-throw cancellation
                }
                catch ( Exception ex )
                {
                    MainThread.BeginInvokeOnMainThread( () =>
                    {
                        this.ErrorMessage = $"Calculation error: {ex.Message}";
                        this.CalculationResults = null;
                    } );
                }
            }, cancellationToken );
        }
        catch ( OperationCanceledException )
        {
            // Calculation was cancelled
            await MainThread.InvokeOnMainThreadAsync( () =>
            {
                this.ErrorMessage = "Calculation was cancelled.";
            } );
            Debug.WriteLine( "Calculation cancelled by user" );
        }
        catch ( ArgumentException ex )
        {
            // Input validation failed
            await MainThread.InvokeOnMainThreadAsync( () =>
            {
                this.ErrorMessage = $"Invalid input: {ex.Message}";
            } );
            Debug.WriteLine( $"Validation error: {ex.Message}" );
        }
        catch ( Exception ex )
        {
            // Unexpected error
            await MainThread.InvokeOnMainThreadAsync( () =>
            {
                this.ErrorMessage = $"Error during calculation: {ex.Message}";
            } );
            Debug.WriteLine( $"Calculation error: {ex}", ex );
        }
        finally
        {
            this.IsCalculating = false;
            this._cancellationTokenSource?.Dispose();
            this._cancellationTokenSource = null;
        }
    }

    /// <summary>
    /// Validates all input parameters before calculation.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
    private void ValidateInputs()
    {
        // Validate all inputs
        List<string> validationErrors = AppreciatorInputValidator.ValidateInputs(
            this.InvestedAmount, this.InitialAge, this.InvestmentDuration,
            this.InitialFederalTaxRate, this.WithdrawalFederalTaxRate,
            this.InitialStateTaxRate, this.WithdrawalStateTaxRate,
            this.FederalCapitalGainsTaxRate, this.StateCapitalGainsTaxRate,
            this.AnnualInflationRate, this.AnnualGrowthRate );

        if ( validationErrors.Count > 0 )
        {
            string errorMessage = "Please correct the following validation errors:\n\n" +
                string.Join( "\n", validationErrors );
            throw new ArgumentException( errorMessage );
        }
    }

    private CancellationTokenSource? _cancellationTokenSource;

    /// <summary>
    /// Cancels the current calculation if one is in progress.
    /// </summary>
    [RelayCommand]
    public void CancelCalculation()
    {
        if ( this.IsCalculating && this._cancellationTokenSource != null )
        {
            this._cancellationTokenSource.Cancel();
        }
    }

    /// <summary>
    /// Resets all fields to default values.
    /// </summary>
    [RelayCommand]
    public void Reset()
    {
        this.InitializeDefaults();
        MainThread.BeginInvokeOnMainThread( () =>
        {
            // Ensure UI update happens on main thread
            this.CalculationResults = null;
            this.HasResults = false;
            this.ErrorMessage = null;
        } );
    }
}
