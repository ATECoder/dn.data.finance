using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace cc.isr.Finance.Sep.Ira.ViewModels;

/// <summary>
/// ViewModel for the SEP IRA Appreciator calculator page.
/// </summary>
public partial class AppreciatorViewModel : BaseViewModel
{
    [ObservableProperty]
    public partial decimal InvestedAmount { get; set; }

    [ObservableProperty]
    public partial int InitialAge { get; set; }

    [ObservableProperty]
    public partial int InvestmentDuration { get; set; }

    [ObservableProperty]
    public partial decimal InitialFederalTaxRate { get; set; }

    [ObservableProperty]
    public partial decimal WithdrawalFederalTaxRate { get; set; }

    [ObservableProperty]
    public partial decimal InitialStateTaxRate { get; set; }

    [ObservableProperty]
    public partial decimal WithdrawalStateTaxRate { get; set; }

    [ObservableProperty]
    public partial decimal FederalCapitalGainsTaxRate { get; set; }

    [ObservableProperty]
    public partial decimal StateCapitalGainsTaxRate { get; set; }

    [ObservableProperty]
    public partial decimal AnnualInflationRate { get; set; }

    [ObservableProperty]
    public partial decimal AnnualGrowthRate { get; set; }

    [ObservableProperty]
    public partial string? CalculationResults { get; set; }

    [ObservableProperty]
    public partial string? ErrorMessage { get; set; }

    [ObservableProperty]
    public partial bool IsCalculating { get; set; }

    public AppreciatorViewModel() => this.InitializeDefaults();

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
        this.IsCalculating = false;
    }

    [RelayCommand]
    public async Task Calculate()
    {
        try
        {
            this.ErrorMessage = null;
            this.IsCalculating = true;

            await Task.Run( () =>
            {
                try
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
                        throw new InvalidOperationException( errorMessage );
                    }

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
                }
                catch ( Exception ex )
                {
                    MainThread.BeginInvokeOnMainThread( () =>
                    {
                        this.ErrorMessage = $"Calculation error: {ex.Message}";
                        this.CalculationResults = null;
                    } );
                }
            } );
        }
        catch ( Exception ex )
        {
            this.ErrorMessage = $"Error: {ex.Message}";
            this.CalculationResults = null;
        }
        finally
        {
            this.IsCalculating = false;
        }
    }

    [RelayCommand]
    public void Reset()
    {
        this.InitializeDefaults();
    }
}
