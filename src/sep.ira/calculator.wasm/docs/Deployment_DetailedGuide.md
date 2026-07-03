# SEP IRA Calculator - Blazor WebAssembly Deployment Guide

## Table of Contents
1. [Local Development & Testing](#local-development--testing)
2. [Build for Release](#build-for-release)
3. [Deployment Options](#deployment-options)
4. [Production Considerations](#production-considerations)
5. [Troubleshooting](#troubleshooting)

---

## Local Development & Testing

### Running Locally

#### Option 1: Visual Studio (Recommended for Development)
1. Right-click **SepIraCalculatorWebAssembly** project in Solution Explorer
2. Select **Set as Startup Project**
3. Press **F5** or click **Debug** → **Start Debugging**
4. Browser opens automatically at `https://localhost:5001/` (or assigned port)

#### Option 2: Command Line
```powershell
# From solution root
cd src/sep.ira/calculator.wasm
dotnet watch run
```

The app will:
- Live reload on file changes (with `dotnet watch`)
- Display at `https://localhost:5001/`
- Show developer console for debugging

### Testing Locally
1. **Form Validation**: Test input fields with various values
2. **Calculations**: Verify results match MAUI app output
3. **Error Handling**: Test invalid inputs
4. **Responsiveness**: Resize browser to test mobile layout
5. **Browser Console**: Check for any JavaScript errors (F12)

---

## Build for Release

### Create Release Build

#### Option 1: Visual Studio
1. Select **Release** configuration from toolbar dropdown
2. Right-click solution → **Build** (or **Rebuild All**)
3. Output: `bin/Release/net10.0/`

#### Option 2: Command Line
```powershell
# From solution root
dotnet build --configuration Release

# Or specific project
cd src/sep.ira/calculator.wasm
dotnet build --configuration Release
```

### Publish for Deployment

#### Option 1: Visual Studio
1. Right-click **SepIraCalculatorWebAssembly** project
2. Select **Publish**
3. Configure profile (see deployment options below)
4. Click **Publish**

#### Option 2: Command Line
```powershell
cd src/sep.ira/calculator.wasm

# Publish to folder
dotnet publish --configuration Release --output ./publish

# Publish with no runtime
dotnet publish --configuration Release -p:PublishTrimmed=true -p:PublishReadyToRun=false
```

**Output Location**: `publish/wwwroot/` contains the static files to deploy

---

## Deployment Options

### Option A: Azure Static Web Apps (Recommended)

**Advantages**: Free tier available, auto CI/CD from GitHub, global CDN, easy setup

#### Steps:
1. **Commit to GitHub**
   ```powershell
   cd C:\my\lib\vs\data\finance
   git add .
   git commit -m "Add SEP IRA Calculator WebAssembly project"
   git push
   ```

2. **Create Azure Static Web App**
   - Visit [Azure Portal](https://portal.azure.com)
   - Search "Static Web Apps" → Create
   - Link to your GitHub repo (`https://github.com/ATECoder/dn.data.finance`)
   - Select branch: `main`
   - Build preset: **.NET (Isolated)**
   - App location: `src/sep.ira/calculator.wasm`
   - Output location: `publish/wwwroot`

3. **Azure creates GitHub Actions workflow** (`.github/workflows/azure-static-web-apps-*.yml`)
   - Automatically builds and deploys on push
   - Accessible at `https://<app-name>.azurestaticapps.net`

#### Configuration File
Create `src/sep.ira/calculator.wasm/staticwebapp.config.json`:
```json
{
  "navigationFallback": {
    "rewrite": "/index.html",
    "exclude": [
      "*.{css,scss,js,png,gif,ico,jpg,jpeg,svg}",
      "/api/*"
    ]
  },
  "mimeTypes": {
    ".wasm": "application/wasm"
  }
}
```

### Option B: Azure App Service

**Advantages**: More control, can add backend APIs

#### Steps:
1. **Publish to folder**
   ```powershell
   cd src/sep.ira/calculator.wasm
   dotnet publish --configuration Release --output ./publish
   ```

2. **Create Azure App Service**
   - Azure Portal → App Services → Create
   - Runtime: `.NET 10` (or your target runtime)
   - Pricing: B1 (free tier available)

3. **Deploy**
   - Option A: Zip and upload `publish/wwwroot/` folder
   - Option B: Setup GitHub Actions to deploy on push
   - Option C: Use Visual Studio Publish profile

4. **Configure for Blazor**
   - App Service Settings:
     - `WEBSITE_RUN_FROM_PACKAGE`: 1
     - `SCM_DO_BUILD_DURING_DEPLOYMENT`: false

### Option C: Docker Container

**Advantages**: Portable, consistent across environments

#### Dockerfile
Create `src/sep.ira/calculator.wasm/Dockerfile`:
```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY ["src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj", "src/sep.ira/calculator.wasm/"]
COPY ["src/sep.ira/calculator/cc.isr.Finance.Sep.Ira.Calculator.csproj", "src/sep.ira/calculator/"]
RUN dotnet restore "src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish "src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj"

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80 443
ENTRYPOINT ["dotnet", "cc.isr.Finance.Sep.Ira.dll"]
```

#### Build & Run
```powershell
# Build image
docker build -t sep-ira-calculator:latest -f src/sep.ira/calculator.wasm/Dockerfile .

# Run container
docker run -p 8080:80 sep-ira-calculator:latest

# Access at http://localhost:8080
```

### Option D: GitHub Pages

**Advantages**: Free hosting, directly from repo

**Note**: Blazor WASM requires specific configuration

#### Steps:
1. **Create deployment workflow** (`.github/workflows/deploy-gh-pages.yml`):
```yaml
name: Deploy to GitHub Pages

on:
  push:
    branches: [ main ]
  workflow_dispatch:

jobs:
  deploy-to-github-pages:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3

      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '10.0.x'

      - name: Publish
        run: dotnet publish src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj -c Release -o publish

      - name: Deploy to GitHub Pages
        uses: peaceiris/actions-gh-pages@v3
        with:
          github_token: ${{ secrets.GITHUB_TOKEN }}
          publish_dir: ./publish/wwwroot
          cname: sep-ira-calculator.example.com  # Optional: custom domain
```

2. **Enable GitHub Pages**
   - Repo Settings → Pages
   - Source: Deploy from a branch
   - Branch: `gh-pages` (created by workflow)

3. **Access at** `https://ATECoder.github.io/dn.data.finance/`

### Option E: Self-Hosted IIS

**Advantages**: Full control, on-premises

#### Steps:
1. **Publish application**
   ```powershell
   dotnet publish --configuration Release -o C:\inetpub\wwwroot\sep-ira-calculator
   ```

2. **Configure IIS**
   - Add website pointing to `C:\inetpub\wwwroot\sep-ira-calculator`
   - Configure .wasm MIME type (if not auto-detected):
     - MIME Type: `.wasm` → `application/wasm`
   - Enable static content
   - Set default document: `index.html`

3. **Configure URL Rewriting** (for SPA routing)
   - Install URL Rewrite module
   - Add rule to rewrite 404s to `index.html`

---

## Production Considerations

### Performance Optimization

#### 1. Trimming (Reduce Bundle Size)
Update `SepIraCalculatorWebAssembly.csproj`:
```xml
<PropertyGroup>
  <PublishTrimmed>true</PublishTrimmed>
  <TrimMode>link</TrimMode>
  <InvariantGlobalization>true</InvariantGlobalization>
</PropertyGroup>
```

#### 2. Enable AOT Compilation (Optional, for better startup)
```xml
<PropertyGroup>
  <PublishAot>true</PublishAot>
</PropertyGroup>
```

#### 3. Cache Busting
In `wwwroot/index.html`, add version to assets:
```html
<script src="_framework/blazor.web.js?v=1.0.0"></script>
```

### Security

#### 1. HTTPS Only
- Enforce HTTPS redirects
- Azure: Enable HTTPS only in settings
- IIS: Enable SSL bindings
- Docker: Use reverse proxy (nginx, Traefik)

#### 2. Content Security Policy (CSP)
Add to `wwwroot/index.html` `<head>`:
```html
<meta http-equiv="Content-Security-Policy" 
      content="default-src 'self'; script-src 'self' 'unsafe-inline' 'unsafe-eval'">
```

#### 3. CORS Headers (if needed for APIs)
Not applicable for static WASM app, but important if adding backend APIs

#### 4. Remove Debug Info in Production
Update `SepIraCalculatorWebAssembly.csproj`:
```xml
<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
  <DebugType>none</DebugType>
  <DebugSymbols>false</DebugSymbols>
</PropertyGroup>
```

### Monitoring & Logging

#### 1. Application Insights (Azure)
Add to `Program.cs`:
```csharp
builder.Services.AddApplicationInsightsTelemetry();
```

Install NuGet package:
```powershell
dotnet add package Microsoft.ApplicationInsights.AspNetCore
```

#### 2. Browser Console Errors
Monitor in development console (F12) for any WASM errors

#### 3. HTTP Status Codes
Configure error pages:
- 404: `wwwroot/404.html`
- 500: `wwwroot/500.html`

### CDN Configuration

For Azure Static Web Apps CDN is **automatic**.

For other hosts, configure:
- **CloudFlare** or **Cloudflare Pages**
- **Azure CDN**
- **Akamai**

Set cache headers in response:
```
Cache-Control: public, max-age=31536000
```

---

## Deployment Checklist

- [ ] Solution builds without warnings/errors
- [ ] All input validation working
- [ ] Calculations produce correct results
- [ ] Error messages display properly
- [ ] Responsive design tested on mobile
- [ ] HTTPS enabled
- [ ] Performance profiled (bundle size < 5MB typical)
- [ ] README updated with usage instructions
- [ ] Credentials/secrets removed from code
- [ ] Deployment profile created
- [ ] CI/CD pipeline configured
- [ ] Monitoring/logging enabled
- [ ] Tested in target deployment environment

---

## Troubleshooting

### Common Issues

#### Issue: "WASM file not found" or "Failed to load WASM"
**Solution**:
1. Verify `.wasm` MIME type is `application/wasm`
2. Check `staticwebapp.config.json` exists
3. Enable gzip compression in web server

#### Issue: "Assembly loading failed"
**Solution**:
1. Ensure all dependencies are properly referenced
2. Check for circular dependencies
3. Verify .NET target framework compatibility

#### Issue: "Application blank/not loading"
**Solution**:
1. Check browser console for errors (F12)
2. Verify `index.html` is in `wwwroot`
3. Clear browser cache
4. Check routing configuration

#### Issue: "404 on page refresh"
**Solution**:
1. Configure SPA fallback routing to `index.html`
2. Update web server rewrite rules
3. Verify `staticwebapp.config.json` navigation fallback

#### Issue: "Large bundle size"
**Solution**:
1. Enable trimming in .csproj
2. Remove unused NuGet packages
3. Use AOT compilation
4. Enable gzip compression

### Debug Commands

```powershell
# Check bundle size
du -sh src/sep.ira/calculator.wasm/bin/Release/net10.0/publish/wwwroot/_framework

# Test locally with specific port
dotnet watch run --urls https://localhost:5001

# Publish with verbose output
dotnet publish -v diag --configuration Release
```

---

## Deployment Summary by Platform

| Platform | Ease | Cost | Scalability | Recommendation |
|----------|------|------|-------------|-----------------|
| **Azure Static Web Apps** | ⭐⭐⭐⭐⭐ | Free | ⭐⭐⭐⭐ | **Best for most** |
| **GitHub Pages** | ⭐⭐⭐⭐ | Free | ⭐⭐⭐ | Public access only |
| **Azure App Service** | ⭐⭐⭐⭐ | $$ | ⭐⭐⭐⭐⭐ | Enterprise apps |
| **Docker + Cloud** | ⭐⭐⭐ | $ | ⭐⭐⭐⭐⭐ | Complex deployments |
| **IIS Self-Hosted** | ⭐⭐⭐ | Infrastructure | ⭐⭐ | On-premises |

---

## Next Steps

1. **Choose a deployment platform** from the options above
2. **Create deployment profile** in Visual Studio
3. **Test in staging environment** before production
4. **Set up CI/CD pipeline** for automated deployments
5. **Monitor application** after deployment
6. **Gather user feedback** and iterate

For questions specific to your hosting provider, consult their Blazor WebAssembly documentation.
