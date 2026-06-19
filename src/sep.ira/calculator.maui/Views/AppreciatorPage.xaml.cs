using cc.isr.Finance.Sep.Ira.ViewModels;

namespace cc.isr.Finance.Sep.Ira.Views;

/// <summary>
/// Appreciator calculator page for SEP IRA appreciation calculations.
/// </summary>
public partial class AppreciatorPage : ContentPage
{
    public AppreciatorPage()
    {
        this.InitializeComponent();
        this.BindingContext = new AppreciatorViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if ( this.BindingContext is BaseViewModel viewModel )
        {
            await viewModel.InitializeAsync();
        }
    }
}
