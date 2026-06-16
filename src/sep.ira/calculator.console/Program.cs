using Microsoft.Extensions.Configuration;

using cc.isr.Finance.Sep.Ira;

Appreciator appreciator = new();

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath( AppContext.BaseDirectory )
    .AddJsonFile( "appsettings.json", optional: false, reloadOnChange: true )
    // .AddCommandLine( args )
    // .AddEnvironmentVariables()
    .Build();

// Fetch the specific section and bind it directly to a dictionary type
Dictionary<int, double>? dictionary = config.GetSection( "UniformLifetimeTable" )
    .Get<Dictionary<int, double>>();

// Update the uniform lifetime table with the deserialized dictionary
appreciator.UniformLifetimeTable = dictionary ?? appreciator.UniformLifetimeTable;

// Define inputs
if ( args.Length > 0 ) { appreciator.InvestedAmount = double.Parse( args[0], System.Globalization.CultureInfo.CurrentCulture ); }
if ( args.Length > 1 ) { appreciator.InitialAge = int.Parse( args[1], System.Globalization.CultureInfo.CurrentCulture ); }
if ( args.Length > 2 ) { appreciator.InvestmentDuration = int.Parse( args[2], System.Globalization.CultureInfo.CurrentCulture ); }
if ( args.Length > 3 ) { appreciator.InitialFederalTaxRate = double.Parse( args[3], System.Globalization.CultureInfo.CurrentCulture ); }
if ( args.Length > 4 ) { appreciator.WithdrawalFederalTaxRate = double.Parse( args[4], System.Globalization.CultureInfo.CurrentCulture ); }
if ( args.Length > 5 ) { appreciator.InitialStateTaxRate = double.Parse( args[5], System.Globalization.CultureInfo.CurrentCulture ); }
if ( args.Length > 6 ) { appreciator.WithdrawalStateTaxRate = double.Parse( args[6], System.Globalization.CultureInfo.CurrentCulture ); }
if ( args.Length > 7 ) { appreciator.FederalCapitalGainsTaxRate = double.Parse( args[7], System.Globalization.CultureInfo.CurrentCulture ); }
if ( args.Length > 8 ) { appreciator.StateCapitalGainsTaxRate = double.Parse( args[8], System.Globalization.CultureInfo.CurrentCulture ); }
if ( args.Length > 9 ) { appreciator.AnnualInflationRate = double.Parse( args[9], System.Globalization.CultureInfo.CurrentCulture ); }
if ( args.Length > 10 ) { appreciator.AnnualGrowthRate = double.Parse( args[10], System.Globalization.CultureInfo.CurrentCulture ); }

Console.WriteLine();
AppreciatorReportBuilder.OutputReport( AppreciatorReportBuilder.BuildInputsReport( appreciator, "* Inputs *" ), 1 );

appreciator.CalculateFutureValue();

// List<string[]> skipIraReport = AppreciatorReportBuilder.BuildSimpleCapitalInvestmentReport( appreciator, includeHeader: true );
Dictionary<string, string> skipIraResult = AppreciatorReportBuilder.BuildSimpleCapitalInvestmentResult( appreciator );

// Console.WriteLine();
// AppreciatorReportBuilder.OutputReport( skipIraReport, 2 );

appreciator.CalculateFutureValueSepIraWithRmd( debug: false );

// List<string[]> sepIraReport = AppreciatorReportBuilder.BuildSepIraInvestmentReport( appreciator, includeHeader: true );
Dictionary<string, string> sepIraResult = AppreciatorReportBuilder.BuildSepIraInvestmentResult( appreciator );

// Console.WriteLine();
// AppreciatorReportBuilder.OutputReport( sepIraReport, 2 );

Console.WriteLine();
Console.WriteLine( $"* Simple Investment vs. SEP IRA Comparison from {appreciator.InitialAge} to {appreciator.FinalAge} at {appreciator.AnnualGrowthRate:F1}% growth rate *" );
AppreciatorReportBuilder.OutputReport( AppreciatorReportBuilder.BuildComparisonReport( skipIraResult, sepIraResult, true ), 1 );
