# SEP IRA Calculator - Blazor WebAssembly Application

A modern web-based implementation of the SEP IRA (Simplified Employee Pension Individual Retirement Account) calculator. This Blazor WebAssembly application provides a responsive, browser-based interface for calculating and comparing simple investment returns against SEP IRA investments.

## 🎯 Features

- **Interactive Calculator**: Input investment parameters including:
  - Initial investment amount
  - Age and investment duration
  - Federal and state tax rates (initial and withdrawal)
  - Capital gains tax rates
  - Annual inflation and growth rates

- **Comprehensive Analysis**: Compares:
  - Simple investment returns (after taxes)
  - SEP IRA investment returns (with RMD calculations)
  - Side-by-side comparison report

- **Responsive Design**: Fully responsive Bootstrap-based UI works on:
  - Desktop browsers
  - Tablets
  - Mobile devices

- **Error Handling**: 
  - Input validation
  - Calculation error reporting
  - User-friendly error messages

- **Performance**:
  - Fast calculation engine (uses netstandard2.0 library)
  - Async processing with cancellation support
  - Optimized for WebAssembly deployment

## 🚀 Quick Start

### Local Development

#### Prerequisites
- .NET 10.0 SDK or later
- Visual Studio 2026 Community (or any IDE supporting Blazor)

#### Run Locally
```powershell
# Option 1: Visual Studio
# 1. Open SepIraCalculator.slnx
# 2. Set SepIraCalculatorWebAssembly as startup project
# 3. Press F5 or click Debug

# Option 2: Command Line
cd src/sep.ira/calculator.wasm
dotnet watch run
```

App will open at `https://localhost:5001/`

### Quick Testing
1. Enter investment amount: `10000`
2. Keep default values or adjust as needed
3. Click **Calculate**
4. Review results in the output section

## 📁 Project Structure

```
calculator.wasm/
├── Pages/
│   └── Index.razor              # Main calculator page
├── Shared/
│   └── MainLayout.razor         # Layout component
├── wwwroot/
│   ├── index.html               # HTML host page
│   └── css/
│       └── app.css              # Application styling
├── App.razor                    # Root component
├── Program.cs                   # Application startup
├── _Imports.razor               # Global imports
├── SepIraCalculatorWebAssembly.csproj
├── DEPLOYMENT.md                # Detailed deployment guide
├── QUICK_START_DEPLOYMENT.md    # Quick deployment steps
├── Dockerfile                   # Docker configuration
├── docker-compose.yml           # Docker Compose for testing
└── staticwebapp.config.json     # Azure Static Web Apps config
```

## 🛠️ Build & Deployment

### Build for Release
```powershell
dotnet build --configuration Release
```

### Publish to Folder
```powershell
dotnet publish --configuration Release --output ./publish
```

### Docker Build
```powershell
docker build -t sep-ira-calculator:latest -f Dockerfile .
docker run -p 8080:80 sep-ira-calculator:latest
```

### Deploy to Azure Static Web Apps (Recommended)
See [QUICK_START_DEPLOYMENT.md](QUICK_START_DEPLOYMENT.md) for step-by-step instructions.

## 📚 Architecture

### Technology Stack
- **Frontend**: Blazor WebAssembly (.NET 10.0)
- **UI Framework**: Bootstrap 5
- **Styling**: CSS 3
- **Calculation Engine**: cc.isr.Finance.Sep.Ira.Calculator (netstandard2.0)

### Component Flow
```
App.razor (Root)
├── MainLayout.razor (Layout wrapper)
└── Pages/
    └── Index.razor (Calculator page)
        ├── Input form (user parameters)
        ├── Calculation engine
        └── Results display
```

### Key Components

#### Index.razor (Main Calculator)
- Manages form state for all tax parameters
- Handles async calculations
- Displays results and errors
- Supports cancellation of in-progress calculations

#### Calculation Process
1. User inputs parameters via form
2. Click "Calculate" button
3. Async task validates and processes calculation
4. Appreciator engine generates comparison
5. Results display in formatted table

## 🔧 Configuration

### Application Settings
Configure in `Program.cs`:
```csharp
// Add logging
builder.Services.AddLogging();

// Add HTTPClient factory for API calls (future enhancement)
builder.Services.AddHttpClient();
```

### Build Configuration
In `SepIraCalculatorWebAssembly.csproj`:
```xml
<!-- Release trimming for smaller bundle -->
<PublishTrimmed>true</PublishTrimmed>

<!-- Strong name signing -->
<SignAssembly>true</SignAssembly>
```

## 📊 Performance

### Bundle Size
- Typical release build: 3-4 MB (compressed)
- WASM file: ~2 MB
- Frameworks and libraries: ~1-2 MB

### Load Time
- Initial load: ~2-5 seconds (depending on network)
- Subsequent loads: <1 second (with caching)
- Calculation time: <1 second

## 🔒 Security

- **HTTPS Only**: All deployments use HTTPS
- **No Backend Dependencies**: Completely static client-side application
- **Input Validation**: All user inputs validated before calculation
- **CORS**: Not applicable (no external API calls)

## 📱 Browser Support

| Browser | Version | Support |
|---------|---------|---------|
| Chrome | Latest | ✅ Fully supported |
| Firefox | Latest | ✅ Fully supported |
| Safari | Latest | ✅ Fully supported |
| Edge | Latest | ✅ Fully supported |
| IE 11 | Any | ❌ Not supported (WebAssembly requirement) |

## 🐛 Troubleshooting

### Issue: Application loads blank
**Solution**: 
- Clear browser cache (Ctrl+Shift+Delete)
- Check browser console for errors (F12)
- Verify wwwroot/index.html exists
- Review deployment logs

### Issue: WASM not loading
**Solution**:
- Verify .wasm MIME type is `application/wasm`
- Check staticwebapp.config.json is present
- Enable gzip compression
- Check network tab in DevTools

### Issue: Calculation results incorrect
**Solution**:
- Verify calculator library version matches
- Check input parameters are valid decimal numbers
- Review calculation logic in AppreciatorViewModel of MAUI app

### Issue: Slow initial load
**Solution**:
- Enable CDN caching
- Configure gzip/brotli compression
- Use production build (Release configuration)
- Check network bandwidth

## 📚 Related Projects

- **MAUI App**: Desktop/mobile version - `src/sep.ira/calculator.maui/`
- **Console App**: CLI version - `src/sep.ira/calculator.console/`
- **Forms App**: Legacy WinForms version - `src/sep.ira/calculator.app/`
- **Calculator Library**: Core engine - `src/sep.ira/calculator/`
- **Unit Tests**: Test suite - `src/sep.ira/calculator.xunits/`

## 🚀 Deployment

### Azure Static Web Apps (Free Tier Available)
- Global CDN
- Automatic CI/CD from GitHub
- Custom domains support
- Staging environments

### Docker
- Build: `docker build -t sep-ira-calculator .`
- Run: `docker run -p 8080:80 sep-ira-calculator`

### GitHub Pages
- Free hosting
- Automatic deployment

See [DEPLOYMENT.md](DEPLOYMENT.md) for detailed options.

## 📋 Environment Variables

Not applicable for WebAssembly (static site).

For Azure deployments:
- `APPINSIGHTS_INSTRUMENTATIONKEY`: (optional) Application Insights monitoring

## 🧪 Testing

### Manual Testing Checklist
- [ ] Form loads with default values
- [ ] Invalid input shows error message
- [ ] Calculate button processes and shows results
- [ ] Reset button clears form to defaults
- [ ] Cancel button stops calculation
- [ ] Results are mathematically correct
- [ ] Layout is responsive on mobile
- [ ] HTTPS connection works
- [ ] App works offline (after initial load)

### Test Cases
```
Test 1: Valid Input
Input: $10,000, 75 years old, 20 years, 7% growth
Expected: Results show comparison table

Test 2: Invalid Amount
Input: "abc", other fields valid
Expected: Error message "Invalid input"

Test 3: Zero Amount
Input: 0
Expected: Calculation completes with $0 result

Test 4: Cancel During Calculation
Action: Click Calculate, then immediately click Cancel
Expected: Calculation stops, results not displayed
```

## 📞 Support & Issues

For issues or feature requests:
1. Check troubleshooting section above
2. Review browser console (F12)
3. Check deployment logs (if deployed)
4. Open issue on GitHub: https://github.com/ATECoder/dn.data.finance/issues

## 📄 License

This project is part of the Finance Data solution and follows the same license terms.

## 🎓 Learning Resources

- [Microsoft Blazor Documentation](https://learn.microsoft.com/aspnet/core/blazor)
- [WebAssembly Fundamentals](https://webassembly.org/)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.0)
- [SEP IRA Rules (IRS.gov)](https://www.irs.gov/retirement-plans/sep-ira-contribution-limits)

## 📝 Changelog

### v1.0.0 (Initial Release)
- ✅ Blazor WebAssembly application
- ✅ Complete calculator UI
- ✅ Integration with SEP IRA calculator library
- ✅ Responsive Bootstrap design
- ✅ Deployment files (Docker, GitHub Actions, Azure config)

---

**Last Updated**: 2026-01-15  
**Status**: Production Ready ✅  
**Maintainer**: [@ATECoder](https://github.com/ATECoder)
