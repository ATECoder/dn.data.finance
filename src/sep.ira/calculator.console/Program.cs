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
#pragma warning disable CA1305 // Specify IFormatProvider
if ( args.Length > 0 ) { appreciator.Principal = double.Parse( args[0] ); }
if ( args.Length > 1 ) { appreciator.InitialAge = int.Parse( args[1] ); }
if ( args.Length > 2 ) { appreciator.Years = int.Parse( args[2] ); }
if ( args.Length > 3 ) { appreciator.PresentFederalTaxRate = double.Parse( args[3] ); }
if ( args.Length > 4 ) { appreciator.FutureFederalTaxRate = double.Parse( args[4] ); }
if ( args.Length > 5 ) { appreciator.PresentStateTaxRate = double.Parse( args[5] ); }
if ( args.Length > 6 ) { appreciator.FutureStateTaxRate = double.Parse( args[6] ); }
if ( args.Length > 7 ) { appreciator.CapitalGainsTaxRate = double.Parse( args[7] ); }
if ( args.Length > 8 ) { appreciator.InflationRate = double.Parse( args[8] ); }
if ( args.Length > 9 ) { appreciator.AnnualReturn = double.Parse( args[9] ); }
#pragma warning restore CA1305 // Specify IFormatProvider

Console.WriteLine();
appreciator.CalculateFutureValue();

Console.WriteLine();
appreciator.CalculateFutureValueSepIraWithRmd( debug: false );
