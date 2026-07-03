using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using cc.isr.Finance.Sep.Ira.WebAssembly.Services;
using cc.isr.Finance.Sep.Ira;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault( args );
builder.RootComponents.Add<App>( "#app" );
builder.RootComponents.Add<HeadOutlet>( "head::after" );

builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( builder.HostEnvironment.BaseAddress ) } );
builder.Services.AddScoped<AppreciatorService>();

await builder.Build().RunAsync();

