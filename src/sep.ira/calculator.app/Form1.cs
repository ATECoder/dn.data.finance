namespace cc.isr.Finance.Sep.Ira;

public partial class Form1 : Form
{
    private Appreciator? _appreciator;

    public Form1()
    {
        this.InitializeComponent();
        this.InitializeFormLayout();
    }

    private void InitializeFormLayout()
    {
        this.Text = "SEP-IRA Calculator";
        this.Size = new Size( 600, 700 );
        this.StartPosition = FormStartPosition.CenterScreen;

        TableLayoutPanel tableLayout = new()
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 12,
            AutoSize = true,
            Padding = new Padding( 10 )
        };

        // Configure column styles
        _ = tableLayout.ColumnStyles.Add( new ColumnStyle( SizeType.Absolute, 200 ) );
        _ = tableLayout.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100 ) );

        // Add labels and controls for each command-line option
        AddLabelAndControl( tableLayout, 0, "Principal ($):", CreateNumericUpDown( 10000, 0, 10000000, 100 ) );
        AddLabelAndControl( tableLayout, 1, "Initial Age:", CreateNumericUpDown( 75, 0, 150, 1 ) );
        AddLabelAndControl( tableLayout, 2, "Years:", CreateNumericUpDown( 20, 0, 100, 1 ) );
        AddLabelAndControl( tableLayout, 3, "Present Federal Tax Rate (%):", CreateNumericUpDown( 35, 0, 100, 0.1m ) );
        AddLabelAndControl( tableLayout, 4, "Future Federal Tax Rate (%):", CreateNumericUpDown( 35, 0, 100, 0.1m ) );
        AddLabelAndControl( tableLayout, 5, "Present State Tax Rate (%):", CreateNumericUpDown( 9.3, 0, 100, 0.1m ) );
        AddLabelAndControl( tableLayout, 6, "Future State Tax Rate (%):", CreateNumericUpDown( 9.3, 0, 100, 0.1m ) );
        AddLabelAndControl( tableLayout, 7, "Capital Gains Tax Rate (%):", CreateNumericUpDown( 25, 0, 100, 0.1m ) );
        AddLabelAndControl( tableLayout, 8, "Inflation Rate (%):", CreateNumericUpDown( 2.75, 0, 100, 0.1m ) );
        AddLabelAndControl( tableLayout, 9, "Annual Return (%):", CreateNumericUpDown( 7, 0, 100, 0.1m ) );

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
        Label resultsLabel = new()
        {
            Text = "Results:",
            Dock = DockStyle.Top,
            Height = 25,
            Font = new Font( this.Font.FontFamily, 10, FontStyle.Bold )
        };

        this.Controls.Add( tableLayout );
        this.Controls.Add( buttonPanel );
    }

    private static void AddLabelAndControl( TableLayoutPanel table, int row, string labelText, NumericUpDown control )
    {
        Label label = new()
        {
            Text = labelText,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };

        table.Controls.Add( label, 0, row );
        table.Controls.Add( control, 1, row );
    }

    private static NumericUpDown CreateNumericUpDown( double initialValue, double min, double max, decimal increment )
    {
        return new NumericUpDown
        {
            Dock = DockStyle.Fill,
            Minimum = ( decimal ) min,
            Maximum = ( decimal ) max,
            Value = ( decimal ) initialValue,
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
            if ( controls.Count < 10 )
            {
                _ = MessageBox.Show( "Error: Not all input fields are available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            // Extract values
            double principal = ( double ) controls[0].Value;
            int initialAge = ( int ) controls[1].Value;
            int years = ( int ) controls[2].Value;
            double presentFederalTaxRate = ( double ) controls[3].Value;
            double futureFederalTaxRate = ( double ) controls[4].Value;
            double presentStateTaxRate = ( double ) controls[5].Value;
            double futureStateTaxRate = ( double ) controls[6].Value;
            double capitalGainsTaxRate = ( double ) controls[7].Value;
            double inflationRate = ( double ) controls[8].Value;
            double annualReturn = ( double ) controls[9].Value;

            // Validate all inputs
            List<string> validationErrors = AppreciatorInputValidator.ValidateInputs(
                principal, initialAge, years,
                presentFederalTaxRate, futureFederalTaxRate,
                presentStateTaxRate, futureStateTaxRate,
                capitalGainsTaxRate, inflationRate, annualReturn );

            if ( validationErrors.Count > 0 )
            {
                string errorMessage = "Please correct the following validation errors:\n\n" + string.Join( "\n", validationErrors );
                _ = MessageBox.Show( errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            // Create appreciator and perform calculation
            this._appreciator = new Appreciator
            {
                Principal = principal,
                InitialAge = initialAge,
                Years = years,
                PresentFederalTaxRate = presentFederalTaxRate,
                FutureFederalTaxRate = futureFederalTaxRate,
                PresentStateTaxRate = presentStateTaxRate,
                FutureStateTaxRate = futureStateTaxRate,
                CapitalGainsTaxRate = capitalGainsTaxRate,
                InflationRate = inflationRate,
                AnnualReturn = annualReturn
            };

            this._appreciator.CalculateFutureValue();
            this._appreciator.CalculateFutureValueSepIraWithRmd( debug: false );

            // Display results
            _ = MessageBox.Show(
                "Calculation completed successfully!\n\n" +
                $"Principal: {this._appreciator.Principal:C0}\n" +
                $"Account Balance: {this._appreciator.CapitalAccountBalance:C0}\n" +
                $"Capital Gain: {this._appreciator.CapitalGain:C0}",
                "Calculation Results",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information );
        }
        catch ( Exception ex )
        {
            _ = MessageBox.Show( $"Error during calculation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
        }
    }

    private void ResetButton_Click( object? sender, EventArgs e )
    {
        List<NumericUpDown> controls = this.GetAllNumericControls();
        controls[0].Value = 10000; // Principal
        controls[1].Value = 75;    // Initial Age
        controls[2].Value = 20;    // Years
        controls[3].Value = 35;    // Present Federal Tax Rate
        controls[4].Value = 35;    // Future Federal Tax Rate
        controls[5].Value = ( decimal ) 9.3;  // Present State Tax Rate
        controls[6].Value = ( decimal ) 9.3;  // Future State Tax Rate
        controls[7].Value = 25;    // Capital Gains Tax Rate
        controls[8].Value = ( decimal ) 2.75; // Inflation Rate
        controls[9].Value = 7;     // Annual Return
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
                    if ( child is NumericUpDown numericUpDown )
                    {
                        numericControls.Add( numericUpDown );
                    }
                }
            }
        }
        return numericControls;
    }
}
