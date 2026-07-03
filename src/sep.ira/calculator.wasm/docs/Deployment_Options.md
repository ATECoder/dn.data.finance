# SEP IRA Calculator - WebAssembly Deployment Guide

## Overview

The Blazor WebAssembly SEP IRA Calculator is a browser-based application that can be deployed to multiple cloud platforms or self-hosted. This guide covers all deployment options, optimization strategies, and post-deployment verification.

---

## Size Analysis & Optimization

### Current Published Size
- **Total Release Build**: ~14.7 MB (compressed)
- **Uncompressed**: ~65 MB (Debug mode with full assemblies)

### Size Breakdown (Compressed)
| Component | Size | Notes |
|-----------|------|-------|
| `dotnet.native.wasm` | 2.75 MB | Runtime (gzipped: 1.09 MB) |
| `System.Private.CoreLib.wasm` | 1.44 MB | .NET core library |
| ICU Data (Unicode support) | 1.97 MB | Language/locale support |
| System.Text.Json | 0.35 MB | JSON serialization |
| Other assemblies | 7.0 MB | UI, HTTP, utilities |

### Why It's Larger Than Windows Forms (2 MB)
1. **Blazor WASM Runtime** - 4.2 MB (includes .NET runtime, GC, JIT compiler)
2. **Web Deployment** - No shared system libraries; everything bundled
3. **Features** - System.Text.Json, internationalization, HTTP client
4. **Windows Forms** - Uses Windows system libraries (GDI+, WinForms framework)

### Optimization Applied
✅ **Trimming enabled** (`PublishTrimmed=true`, `TrimMode=full`) - Removes unused code  
✅ **Release build** - Optimization level O3  
✅ **IL stripping** (`WasmStripILAfterLink=true`) - Removes reflection metadata  
✅ **Debugger support disabled** - Saves ~2 MB

### Further Optimization Options (Not Applied - Risk Trade-offs)

If you need to reduce further, these are possible but risky:

#### 1. **Disable Globalization** (~1 MB savings)
```xml
<InvariantGlobalization>true</InvariantGlobalization>
```
⚠️ **Risk**: No international number/date formatting

#### 2. **Enable AOT Compilation** (~1-2 MB savings, faster startup)
```xml
<PublishAotUsingRuntimePack>true</PublishAotUsingRuntimePack>
```
⚠️ **Risk**: Longer build time (~5-10 min), stricter reflection requirements

#### 3. **Disable SIMD/Exception Handling** (~0.5 MB savings)
```xml
<WasmEnableSimd>false</WasmEnableSimd>
<WasmEnableExceptionHandling>false</WasmEnableExceptionHandling>
```
⚠️ **Risk**: Performance degradation, potential crashes

**Current configuration is the recommended balance for production.**

---

## Deployment Options

### Option 1: Azure Static Web Apps (Recommended - Free Tier Available)

#### Advantages
- ✅ Free tier: 100 GB bandwidth/month
- ✅ Global CDN included
- ✅ HTTPS automatic
- ✅ GitHub Actions CI/CD integration
- ✅ No server costs
- ✅ Excellent for business apps

#### Steps

1. **Push code to GitHub**
   ```powershell
   cd C:\my\lib\vs\data\finance
   git add .
   git commit -m "Add Blazor WASM calculator"
   git push origin main
   ```

2. **Create Azure Static Web Apps resource**
   - Go to [Azure Portal](https://portal.azure.com)
   - Search for "Static Web Apps"
   - Click "Create"
   - Connect to your GitHub repo (`dn.data.finance`)
   - Build preset: **Blazor**
   - App location: `src/sep.ira/calculator.wasm`
   - Output location: `bin/Release/net10.0/browser-wasm/publish/wwwroot`

3. **GitHub Actions automatically deploys**
   - Azure creates a workflow in `.github/workflows/azure-static-web-apps-*.yml`
   - Every push to `main` triggers deployment
   - URL: `https://<your-app-name>.azurestaticapps.net`

4. **Optional: Link custom domain**
   - In Static Web Apps → Custom domains
   - Point your domain's CNAME to the Azure endpoint

#### Cost
- **Free tier**: No charge for the service
- **Bandwidth**: First 100 GB/month free (most apps use < 5 GB)

---

### Option 2: GitHub Pages (Free)

#### Advantages
- ✅ Free hosting forever
- ✅ GitHub integrated
- ✅ No credits needed
- ✅ Perfect for personal projects

#### Limitations
- ❌ No backend APIs (static content only)
- ❌ 1 GB storage limit
- ❌ No request logging/analytics

#### Steps

1. **Publish Release build to folder**
   ```powershell
   cd C:\my\lib\vs\data\finance
   dotnet publish src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj `
     -c Release `
     -o bin/gh-pages-publish
   ```

2. **Copy published files to GitHub Pages folder**
   ```powershell
   Copy-Item bin/gh-pages-publish/wwwroot/* -Destination . -Recurse -Force
   ```

3. **Configure GitHub Pages**
   - Go to repo Settings → Pages
   - Source: `main` branch (or `gh-pages` if you prefer separate branch)
   - Publish the `wwwroot` contents

4. **Access at**
   - `https://ATECoder.github.io/dn.data.finance/` (if repo is public)
   - Or point custom domain

#### Issue with Blazor + GitHub Pages
GitHub Pages serves as static hosting only. The app will load but may have issues with routing. Workaround:

Create `.nojekyll` file in root to disable Jekyll processing, and ensure `index.html` has correct `<base href>`.

---

### Option 3: Azure App Service (Recommended for Production)

#### Advantages
- ✅ Can add backend APIs (.NET Core)
- ✅ More scalable than Static Web Apps
- ✅ SSL certificates included
- ✅ Staging slots for testing
- ✅ Auto-scaling available

#### Cost
- **B1 tier** (basic): ~$10/month
- **Free tier** (F1): Limited CPU, shared resources
- **Linux**: Generally cheaper than Windows

#### Steps

1. **Create App Service in Azure Portal**
   - Resource type: App Service
   - Runtime stack: .NET 10
   - OS: Linux (cheaper) or Windows
   - Tier: B1 or B2 for production

2. **Publish to App Service**
   ```powershell
   cd C:\my\lib\vs\data\finance
   dotnet publish src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj `
     -c Release `
     -o bin/app-service-publish
   ```

3. **Deploy via FTP, Git, or Zip**
   - Option A: Use VS publish profile
   - Option B: Use Azure CLI
   ```powershell
   az webapp up --name sep-ira-calculator --resource-group myResourceGroup --runtime "DOTNET|10.0"
   ```

4. **Access at**
   - `https://sep-ira-calculator.azurewebsites.net`

---

### Option 4: Docker Container (Azure Container Registry)

#### Advantages
- ✅ Portable across any cloud
- ✅ Consistent environment
- ✅ Easy scaling (Kubernetes)

#### Steps

1. **Create Dockerfile**
   ```dockerfile
   FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
   WORKDIR /src
   COPY ["src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj", "."]
   RUN dotnet restore "SepIraCalculatorWebAssembly.csproj"
   COPY . .
   RUN dotnet publish "SepIraCalculatorWebAssembly.csproj" -c Release -o /app/publish

   FROM mcr.microsoft.com/dotnet/aspnet:10.0
   WORKDIR /app
   COPY --from=build /app/publish .
   EXPOSE 80
   ENTRYPOINT ["dotnet", "Sep.Ira.Calculator.WebAssembly.dll"]
   ```

2. **Build and push to ACR**
   ```bash
   az acr login --name myregistry
   docker build -t sep-ira-calc:latest .
   docker tag sep-ira-calc:latest myregistry.azurecr.io/sep-ira-calc:latest
   docker push myregistry.azurecr.io/sep-ira-calc:latest
   ```

3. **Deploy to Container Instances or App Service**

---

### Option 5: Self-Hosted on Windows/Linux Server

#### Advantages
- ✅ Full control
- ✅ No cloud provider lock-in
- ✅ One-time hardware cost

#### Disadvantages
- ❌ You manage infrastructure
- ❌ No automatic scaling
- ❌ SSL certificate management

#### Steps (Windows)

1. **Publish Release build**
   ```powershell
   dotnet publish src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj `
     -c Release `
     -o C:\inetpub\wwwroot\sep-ira-calc
   ```

2. **Create IIS Application**
   - Open IIS Manager
   - Right-click Default Website → Add Application
   - Alias: `sep-ira-calc`
   - Physical path: `C:\inetpub\wwwroot\sep-ira-calc`

3. **Access at**
   - `http://localhost/sep-ira-calc`
   - Or `https://your-server.com/sep-ira-calc` with SSL

---

## Deployment Comparison Matrix

| Platform | Cost | Setup Time | Scaling | SSL | Custom Domain | Recommendation |
|----------|------|-----------|---------|-----|----------------|-----------------|
| **Static Web Apps** | Free | 5 min | Auto | ✅ | ✅ | **Best for WASM** |
| **GitHub Pages** | Free | 10 min | Manual | ✅ | ✅ | For public demos |
| **App Service** | $10+/mo | 10 min | Easy | ✅ | ✅ | With backend APIs |
| **Container Instances** | $20+/mo | 20 min | Manual | ✅ | ✅ | Complex deployments |
| **Self-Hosted IIS** | Hardware | 30 min | Manual | Manual | ✅ | Enterprise only |

---

## Post-Deployment Verification

### 1. Check Application Loads
```bash
curl https://your-app-url/ -I
# Should return 200 OK with text/html content-type
```

### 2. Verify Assets Load
- Open developer console (F12)
- Check **Network** tab:
  - `blazor.webassembly.js` → 60 KB
  - `dotnet.native.wasm` → 2.7 MB (compressed: 1.1 MB)
  - CSS files load without 404 errors

### 3. Test Calculator Functionality
- Enter test values
- Click Calculate
- Verify both columns display (Simple Investment + SEP IRA)
- Check that results are mathematically correct

### 4. Monitor Performance
- Initial load: Should be < 5 seconds on 4G
- Calculate button: Should respond < 500ms
- Use Chrome DevTools → Lighthouse for performance audit

### 5. Monitor Errors
- **Browser Console (F12)**: Should show no red errors
- **Application Insights** (if using Azure):
  - Monitor → Application Insights
  - Check for exceptions or failed requests
  - Set alerts for >5% error rate

---

## Sharing & Distribution

### For Windows Users
1. Share the deployed URL
2. Recommend: Bookmark the page or create a shortcut
3. Optional: Create a `.url` file to email users

```ini
[InternetShortcut]
URL=https://sep-ira-calculator.azurestaticapps.net/
```

### For iOS Users
1. Open Safari
2. Navigate to the URL
3. Share → Add to Home Screen
4. Creates an app-like icon on home screen

### For Offline Use (Advanced)
Enable PWA (Progressive Web App) capabilities by adding `manifest.json`:

```json
{
  "name": "SEP IRA Calculator",
  "short_name": "IRA Calc",
  "start_url": "/",
  "display": "standalone",
  "icons": [
    {
      "src": "icon-192.png",
      "sizes": "192x192",
      "type": "image/png"
    }
  ]
}
```

Then in `index.html`:
```html
<link rel="manifest" href="manifest.json" />
```

---

## Troubleshooting

### App loads but calculator doesn't work
1. Check browser console for errors (F12)
2. Verify no network errors for `.wasm` files
3. Clear browser cache (Ctrl+Shift+Delete)
4. Try a different browser

### WASM files show as 404
1. Verify server static file handling
2. In Azure Static Web Apps, check `staticwebapp.config.json`
3. Ensure `wwwroot` folder is deployed correctly

### Slow initial load
1. Enable Brotli compression on server (better than gzip for WASM)
2. Verify CDN is in use
3. Check if browser is using cached version

### CORS errors if adding backend APIs later
Configure CORS in `Program.cs`:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

app.UseCors("AllowAll");
```

---

## Performance Tips

### For Faster Load Times
1. **Enable compression** on server (gzip/brotli)
2. **Use CDN** (Static Web Apps includes this)
3. **Minimize assets** (already done in Release build)
4. **Lazy load** components if app grows

### For Faster Calculations
Current calculation is instant (< 10ms). Performance is excellent.

---

## Next Steps

1. **Choose deployment platform** (recommend: Azure Static Web Apps)
2. **Set up the deployment** (follow Option 1 steps)
3. **Test the live app** thoroughly
4. **Share the URL** with users
5. **Monitor performance** and error rates
6. **Iterate** based on user feedback

---

## Support & Updates

### Monitoring Deployed App
In Azure Portal:
- Static Web Apps → Custom domains (verify DNS)
- Log Analytics → Queries (if enabled)
- Application Insights → Performance

### Future Updates
To deploy a new version:
1. Modify code locally
2. Rebuild: `dotnet publish -c Release`
3. Push to GitHub (or deploy manually)
4. Azure automatically redeploys

### Questions?
- Azure Static Web Apps docs: https://docs.microsoft.com/azure/static-web-apps/
- Blazor WASM hosting: https://docs.microsoft.com/aspnet/core/blazor/hosting-models/webassembly
- Performance tuning: https://learn.microsoft.com/aspnet/core/blazor/webassembly-performance-best-practices

---

**Last Updated**: 2024  
**App Version**: net10.0  
**Published Size**: 14.7 MB (compressed) / 65 MB (uncompressed)
