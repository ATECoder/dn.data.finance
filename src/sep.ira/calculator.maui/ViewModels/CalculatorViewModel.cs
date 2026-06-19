using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace cc.isr.Finance.Sep.Ira.ViewModels;

/// <summary>
/// View model for the SEP IRA calculator functionality.
/// </summary>
public partial class CalculatorViewModel : BaseViewModel
{

    /// <summary>
    /// Gets or sets the annual income for calculation.
    /// </summary>
    [ObservableProperty]
    public partial string AnnualIncomeInput { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the calculation result.
    /// </summary>
    [ObservableProperty]
    public partial decimal CalculatedAmount { get; private set; }

    /// <summary>
    /// Gets or sets the annual income value.
    /// </summary>
    [ObservableProperty]
    public partial decimal AnnualIncome { get; set; }

    /// <summary>
    /// Calculates the SEP IRA contribution based on the current annual income.
    /// </summary>
    [RelayCommand]
    public void Calculate()
    {
        if ( decimal.TryParse( this.AnnualIncomeInput, out decimal income ) )
        {
            this.AnnualIncome = income;
            // Simple calculation: 25% of gross income (This is a simplified example)
            // In production, use the actual ICalculator from cc.isr.Finance.Sep.Ira.Calculator
            this.CalculatedAmount = income * 0.25m;
        }
        else
        {
            this.CalculatedAmount = 0;
        }
    }

    /// <summary>
    /// Clears the calculator inputs and results.
    /// </summary>
    [RelayCommand]
    public void Clear()
    {
        this.AnnualIncomeInput = string.Empty;
        this.AnnualIncome = 0;
        this.CalculatedAmount = 0;
    }
}
