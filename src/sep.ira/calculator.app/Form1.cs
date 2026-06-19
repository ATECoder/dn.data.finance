namespace cc.isr.Finance.Sep.Ira;

public partial class Form1 : Form
{
    private TextBox? _resultsLabel;
    private Appreciator? _appreciator;

    public Form1()
    {
        this.InitializeComponent();
        this.InitializeFormLayout();
    }

    private void InitializeFormLayout()
    {
        this.Text = "SEP-IRA Calculator";
        this.Size = new Size( 600, 750 );
        this.StartPosition = FormStartPosition.CenterScreen;

        TableLayoutPanel inputsTableLayout = new()
        {
            Dock = DockStyle.Top,
            ColumnCount = 2,
            RowCount = 11,
            AutoSize = true,
            Padding = new Padding( 10 )
        };

        // Configure column styles
        _ = inputsTableLayout.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 200 ) );
        _ = inputsTableLayout.ColumnStyles.Add( new ColumnStyle( SizeType.Absolute, 100 ) );

        // Add labels and controls for each command-line option
        AddLabelAndControl( inputsTableLayout, 0,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.InvestedAmount )],
            CreateNumericUpDown( AppreciatorInputsInitialValues.InvestedAmount,
                AppreciatorInputsRanges.InvestedAmount.Minimum, AppreciatorInputsRanges.InvestedAmount.Maximum, 100 ) );
        AddLabelAndControl( inputsTableLayout, 1,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.InitialAge )],
            CreateNumericUpDown( AppreciatorInputsInitialValues.InitialAge,
                AppreciatorInputsRanges.InitialAge.Minimum, AppreciatorInputsRanges.InitialAge.Maximum, 1 ) );
        AddLabelAndControl( inputsTableLayout, 2,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.InvestmentDuration )],
            CreateNumericUpDown( AppreciatorInputsInitialValues.InvestmentDuration,
                AppreciatorInputsRanges.InvestmentDuration.Minimum, AppreciatorInputsRanges.InvestmentDuration.Maximum, 1 ) );
        AddLabelAndControl( inputsTableLayout, 3,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.InitialFederalTaxRate )],
            CreateNumericUpDown( AppreciatorInputsInitialValues.InitialFederalTaxRate,
                AppreciatorInputsRanges.InitialFederalTaxRate.Minimum, AppreciatorInputsRanges.InitialFederalTaxRate.Maximum, 0.1m ) );
        AddLabelAndControl( inputsTableLayout, 4,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.WithdrawalFederalTaxRate )],
                CreateNumericUpDown( AppreciatorInputsInitialValues.WithdrawalFederalTaxRate, AppreciatorInputsRanges.WithdrawalFederalTaxRate.Minimum, AppreciatorInputsRanges.WithdrawalFederalTaxRate.Maximum, 0.1m ) );
        AddLabelAndControl( inputsTableLayout, 5,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.InitialStateTaxRate )],
                CreateNumericUpDown( AppreciatorInputsInitialValues.InitialStateTaxRate, AppreciatorInputsRanges.InitialStateTaxRate.Minimum, AppreciatorInputsRanges.InitialStateTaxRate.Maximum, 0.1m ) );
        AddLabelAndControl( inputsTableLayout, 6,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.WithdrawalStateTaxRate )],
                CreateNumericUpDown( AppreciatorInputsInitialValues.WithdrawalStateTaxRate, AppreciatorInputsRanges.WithdrawalStateTaxRate.Minimum, AppreciatorInputsRanges.WithdrawalStateTaxRate.Maximum, 0.1m ) );
        AddLabelAndControl( inputsTableLayout, 7,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.FederalCapitalGainsTaxRate )],
                CreateNumericUpDown( AppreciatorInputsInitialValues.FederalCapitalGainsTaxRate, AppreciatorInputsRanges.FederalCapitalGainsTaxRate.Minimum, AppreciatorInputsRanges.FederalCapitalGainsTaxRate.Maximum, 0.1m ) );
        AddLabelAndControl( inputsTableLayout, 8,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.StateCapitalGainsTaxRate )],
                CreateNumericUpDown( AppreciatorInputsInitialValues.StateCapitalGainsTaxRate, AppreciatorInputsRanges.StateCapitalGainsTaxRate.Minimum, AppreciatorInputsRanges.StateCapitalGainsTaxRate.Maximum, 0.1m ) );
        AddLabelAndControl( inputsTableLayout, 9,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.AnnualInflationRate )],
                CreateNumericUpDown( AppreciatorInputsInitialValues.AnnualInflationRate, AppreciatorInputsRanges.AnnualInflationRate.Minimum, AppreciatorInputsRanges.AnnualInflationRate.Maximum, 0.1m ) );
        AddLabelAndControl( inputsTableLayout, 10,
            AppreciatorReportBuilder.Titles[nameof( Appreciator.AnnualGrowthRate )],
                CreateNumericUpDown( AppreciatorInputsInitialValues.AnnualGrowthRate, AppreciatorInputsRanges.AnnualGrowthRate.Minimum, AppreciatorInputsRanges.AnnualGrowthRate.Maximum, 0.1m ) );

        // Buttons panel
        FlowLayoutPanel buttonPanel = new()
        {
            Dock = DockStyle.Bottom,
            Height = 50,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding( 10 )
        };

        Button calculateButton = new()
        {
            Text = "Calculate",
            Size = new Size( 100, 35 ),
            Margin = new Padding( 5 )
        };
        calculateButton.Click += this.CalculateButton_Click;

        Button resetButton = new()
        {
            Text = "Reset",
            Size = new Size( 100, 35 ),
            Margin = new Padding( 5 )
        };
        resetButton.Click += this.ResetButton_Click;

        buttonPanel.Controls.Add( calculateButton );
        buttonPanel.Controls.Add( resetButton );

        // Results text box
        this._resultsLabel = new()
        {
            Text = "Results:",
            Dock = DockStyle.Top,
            Height = 725,
            Font = new Font( "Consolas", 10, FontStyle.Bold ),
            BorderStyle = BorderStyle.FixedSingle,
            ScrollBars = ScrollBars.Both,
            Multiline = true,
            ReadOnly = true,
        };

        TableLayoutPanel controlsTableLayout = new()
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 3,
            AutoSize = true,
            Padding = new Padding( 10 )
        };

        // Configure column styles
        _ = controlsTableLayout.ColumnStyles.Add( new ColumnStyle( SizeType.Absolute, 10 ) );
        _ = controlsTableLayout.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100 ) );
        _ = controlsTableLayout.ColumnStyles.Add( new ColumnStyle( SizeType.Absolute, 10 ) );

        _ = controlsTableLayout.RowStyles.Add( new RowStyle( SizeType.Percent, 100 ) );
        _ = controlsTableLayout.RowStyles.Add( new RowStyle( SizeType.AutoSize, inputsTableLayout.Height ) );
        _ = controlsTableLayout.RowStyles.Add( new RowStyle( SizeType.AutoSize, buttonPanel.Height ) );

        controlsTableLayout.Controls.Add( this._resultsLabel, 1, 0 );
        controlsTableLayout.Controls.Add( inputsTableLayout, 1, 1 );
        controlsTableLayout.Controls.Add( buttonPanel, 1, 2 );
        this.Controls.Add( controlsTableLayout );
    }

    private static void AddLabelAndControl( TableLayoutPanel table, int row, string labelText, NumericUpDown control )
    {
        Label label = new()
        {
            Text = labelText,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleRight
        };

        table.Controls.Add( label, 0, row );
        table.Controls.Add( control, 1, row );
    }

    private static NumericUpDown CreateNumericUpDown( decimal initialValue, decimal min, decimal max, decimal increment )
    {
        return new NumericUpDown
        {
            Dock = DockStyle.Fill,
            Minimum = min,
            Maximum = max,
            Value = initialValue,
            Increment = increment,
            DecimalPlaces = increment < 1 ? 2 : 0
        };
    }

    private void CalculateButton_Click( object? sender, EventArgs e )
    {
        try
        {
            // Retrieve values from controls
            List<NumericUpDown> controls = this.GetAllNumericControls();
            if ( controls.Count < 11 )
            {
                _ = MessageBox.Show( "Error: Not all input fields are available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            // Extract values
            decimal investedAmount = controls[0].Value;
            int initialAge = ( int ) controls[1].Value;
            int investmentDuration = ( int ) controls[2].Value;
            decimal initialFederalTaxRate = controls[3].Value;
            decimal withdrawalFederalTaxRate = controls[4].Value;
            decimal initialStateTaxRate = controls[5].Value;
            decimal withdrawalStateTaxRate = controls[6].Value;
            decimal federalCapitalGainsTaxRate = controls[7].Value;
            decimal stateCapitalGainsTaxRate = controls[8].Value;
            decimal annualInflationRate = controls[9].Value;
            decimal annualGrowthRate = controls[10].Value;

            // Validate all inputs
            List<string> validationErrors = AppreciatorInputValidator.ValidateInputs(
                investedAmount, initialAge, investmentDuration,
                initialFederalTaxRate, withdrawalFederalTaxRate,
                initialStateTaxRate, withdrawalStateTaxRate,
                federalCapitalGainsTaxRate, stateCapitalGainsTaxRate,
                annualInflationRate, annualGrowthRate );

            if ( validationErrors.Count > 0 )
            {
                string errorMessage = "Please correct the following validation errors:\n\n" +
                    string.Join( "\n", validationErrors );
                _ = MessageBox.Show( errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            if ( this._resultsLabel != null )
            {
                this._resultsLabel.Text = "Calculating...";
            }
            else
            {
                string errorMessage = "Results label not initialized.";
                _ = MessageBox.Show( errorMessage, "Construction Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            // Create appreciator and perform calculation
            this._appreciator = new Appreciator
            {
                InvestedAmount = investedAmount,
                InitialAge = initialAge,
                InvestmentDuration = investmentDuration,
                InitialFederalTaxRate = initialFederalTaxRate,
                WithdrawalFederalTaxRate = withdrawalFederalTaxRate,
                InitialStateTaxRate = initialStateTaxRate,
                WithdrawalStateTaxRate = withdrawalStateTaxRate,
                FederalCapitalGainsTaxRate = federalCapitalGainsTaxRate,
                StateCapitalGainsTaxRate = stateCapitalGainsTaxRate,
                AnnualInflationRate = annualInflationRate,
                AnnualGrowthRate = annualGrowthRate
            };

            this._appreciator.CalculateFutureValue();
            Dictionary<string, string> skipIraResult = AppreciatorReportBuilder.BuildSimpleCapitalInvestmentResult( this._appreciator );

            this._appreciator.CalculateFutureValueSepIraWithRmd( debug: false );
            Dictionary<string, string> sepIraResult = AppreciatorReportBuilder.BuildSepIraInvestmentResult( this._appreciator );

            string title = $"* Simple Investment vs. SEP IRA Comparison *";
            string subtitle = $"-- from {this._appreciator.InitialAge} to {this._appreciator.FinalAge} at {this._appreciator.AnnualGrowthRate:F1}% growth rate --";
            this._resultsLabel.Text = AppreciatorReportBuilder.BuildReport( title, subtitle,
                    AppreciatorReportBuilder.BuildComparisonReport( skipIraResult, sepIraResult, true ), 1 );

            // Display results
            //_ = MessageBox.Show(
            //    "Calculation completed successfully!\n\n" +
            //    $"Invested Amount: {this._appreciator.InvestedAmount:C0}\n" +
            //    $"Account Balance: {this._appreciator.CapitalAccountBalance + this._appreciator.SepIraAccountBalance:C0}\n" +
            //    $"Capital Gain: {this._appreciator.CapitalGain:C0}\n" +
            //    $"Cashout Value: {this._appreciator.NetCashOutValue:C0}",
            //    "Calculation Results",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Information );
        }
        catch ( Exception ex )
        {
            _ = MessageBox.Show( $"Error during calculation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
        }
    }

    private void ResetButton_Click( object? sender, EventArgs e )
    {
        List<NumericUpDown> controls = this.GetAllNumericControls();
        controls[0].Value = AppreciatorInputsInitialValues.InvestedAmount;
        controls[1].Value = AppreciatorInputsInitialValues.InitialAge;
        controls[2].Value = AppreciatorInputsInitialValues.InvestmentDuration;
        controls[3].Value = AppreciatorInputsInitialValues.InitialFederalTaxRate;
        controls[4].Value = AppreciatorInputsInitialValues.WithdrawalFederalTaxRate;
        controls[5].Value = AppreciatorInputsInitialValues.InitialStateTaxRate;
        controls[6].Value = AppreciatorInputsInitialValues.WithdrawalStateTaxRate;
        controls[7].Value = AppreciatorInputsInitialValues.FederalCapitalGainsTaxRate;
        controls[8].Value = AppreciatorInputsInitialValues.StateCapitalGainsTaxRate;
        controls[9].Value = AppreciatorInputsInitialValues.AnnualInflationRate;
        controls[10].Value = AppreciatorInputsInitialValues.AnnualGrowthRate;
    }

    private List<NumericUpDown> GetAllNumericControls()
    {
        List<NumericUpDown> numericControls = [];
        foreach ( Control control in this.Controls )
        {
            if ( control is TableLayoutPanel tableLayout )
            {
                foreach ( Control child in tableLayout.Controls )
                {
                    if ( child is TableLayoutPanel nestedTableLayout )
                    {
                        foreach ( Control nestedChild in nestedTableLayout.Controls )
                        {
                            if ( nestedChild is NumericUpDown numericUpDown )
                            {
                                numericControls.Add( numericUpDown );
                            }
                        }
                    }
                }
            }
        }
        return numericControls;
    }
}
