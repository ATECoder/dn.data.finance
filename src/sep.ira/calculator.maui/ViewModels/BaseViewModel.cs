using CommunityToolkit.Mvvm.ComponentModel;

namespace cc.isr.Finance.Sep.Ira.ViewModels;

/// <summary>
/// Base view model providing common MVVM functionality for all view models in the application.
/// </summary>
public abstract partial class BaseViewModel : ObservableObject
{
    /// <summary>
    /// Gets or sets a value indicating whether the view model is currently loading data.
    /// </summary>
    [ObservableProperty]
    public partial bool IsLoading { get; set; }

    /// <summary>
    /// Initializes the view model asynchronously.
    /// </summary>
    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
}
