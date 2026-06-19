namespace cc.isr.Finance.Sep.Ira;

public partial class MainPage : ContentPage
{
    private int _count;

    public MainPage() => this.InitializeComponent();

    private void OnCounterClicked( object? sender, EventArgs e )
    {
        this._count++;

        if ( this._count == 1 )
            this.CounterBtn.Text = $"Clicked {this._count} time";
        else
            this.CounterBtn.Text = $"Clicked {this._count} times";

        SemanticScreenReader.Announce( this.CounterBtn.Text );
    }
}
