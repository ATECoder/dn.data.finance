# 🐛 Troubleshooting: "An unhandled error has occurred" in SEP IRA Calculator

## Quick Diagnosis

If you see **"An unhandled error has occurred"** in your browser, follow these steps:

---

## Step 1: Check Browser Console for Error Details

### Windows / Mac / Linux:
1. Press **F12** to open Developer Tools
2. Click the **"Console"** tab
3. Look for red error messages
4. Take note of the exact error

### What to Look For:
```
✅ "Loading SEP IRA Calculator..." → App is initializing (wait a moment)
❌ "Failed to fetch" → Network issue
❌ "WASM" error → WebAssembly runtime issue
❌ "404 Not Found" → File missing
❌ "Cannot read property" → JavaScript error
```

---

## Common Causes & Solutions

### ❌ Issue 1: "Failed to fetch _framework/..."
**Cause**: Browser can't download Blazor runtime files  
**Solution**:
1. **Hard refresh**: Ctrl+Shift+Delete (Windows) or Cmd+Shift+Delete (Mac)
2. **Clear cache**: Clear browser cache and cookies
3. **Try different browser**: Chrome, Firefox, Safari, Edge
4. **Check internet**: Verify you have a stable connection

---

### ❌ Issue 2: "WASM" or "webassembly" error
**Cause**: WebAssembly runtime issue or incompatible browser  
**Solutions**:
1. **Update browser**: Get the latest version of Chrome, Firefox, or Edge
2. **Check browser support**:
   - ✅ Chrome 57+
   - ✅ Firefox 52+
   - ✅ Safari 11.1+
   - ✅ Edge 79+
3. **Disable extensions**: Browser extensions sometimes block WASM
4. **Try private/incognito mode**: Press Ctrl+Shift+N (Windows) or Cmd+Shift+N (Mac)

---

### ❌ Issue 3: "404 Not Found"
**Cause**: Missing static files during deployment  
**Solutions**:
1. **Check deployment**: Confirm all files were uploaded to Azure
2. **Check staticwebapp.config.json**: SPA fallback may not be configured
3. **Publish locally**: Verify it works on `http://localhost:5000`

---

### ❌ Issue 4: "Cannot read property 'xyz' of undefined"
**Cause**: Component loading issue  
**Solutions**:
1. **Check _Imports.razor**: Verify all namespaces are correct
2. **Check App.razor**: Ensure routing is properly configured
3. **Rebuild solution**: Clean and rebuild the project

---

### ❌ Issue 5: "System.TypeLoadException" or ".NET Framework" error
**Cause**: Incompatible .NET version or assembly mismatch  
**Solutions**:
1. **Verify .NET 10 SDK**: Run `dotnet --version` in terminal
2. **Rebuild clean**:
   ```powershell
   dotnet clean
   dotnet restore
   dotnet build
   ```
3. **Check calculator library**: Verify `cc.isr.Finance.Sep.Ira.Calculator.csproj` is targeting `netstandard2.0`

---

## Step 2: Check Network Tab

1. Press **F12**
2. Click **"Network"** tab
3. Refresh page (F5)
4. Look for requests that **failed** (red X or 404)
5. Note the filenames

**Common missing files**:
- `_framework/blazor.web.js` → Not downloaded
- `.dll` files → Assembly download failed
- `*.wasm` → WebAssembly module not found

---

## Step 3: Local Testing

### Test locally before reporting issue:

```powershell
# Navigate to project
cd C:\my\lib\vs\data\finance\src\sep.ira\calculator.wasm

# Build
dotnet build

# Publish locally
dotnet publish --configuration Release -o ./bin/publish

# Run locally
dotnet run
```

**Then visit**: `http://localhost:5000`

---

### If local works but deployed doesn't:
- ✅ **Problem is deployment configuration**
- Review `Deployment_StepByStepGuide.md`
- Check Azure Static Web App settings
- Verify `staticwebapp.config.json` is deployed

### If local also fails:
- ✅ **Problem is code or .NET environment**
- Check .NET 10 SDK: `dotnet --version`
- Clean and rebuild: `dotnet clean && dotnet build`
- Check error in Visual Studio Output panel

---

## Step 4: Check Your Files

### Verify these files exist in `/wwwroot`:
```
✅ index.html
✅ css/app.css
✅ css/bootstrap/bootstrap.min.css
✅ (Blazor auto-generates _framework folder)
```

### Verify project configuration:
```xml
<!-- SepIraCalculatorWebAssembly.csproj -->
<TargetFramework>net10.0</TargetFramework>
<PublishTrimmed>true</PublishTrimmed>
<OutputType>Library</OutputType>
<StaticWebAssetBasePath>/</StaticWebAssetBasePath>
```

---

## Step 5: Full Diagnostics Console Logs

### Enable detailed logging in browser:

**In browser console (F12), run:**
```javascript
// Check if Blazor is defined
console.log("Blazor:", typeof Blazor);

// Check loaded modules
console.log("Loaded scripts:", Object.keys(window));

// Check local storage
console.log("LocalStorage:", window.localStorage);

// Check fetch capability
console.log("Fetch available:", typeof fetch);
```

---

## Step 6: Report the Issue

If the problem persists, collect this information:

```
📋 ISSUE REPORT
───────────────
1. Browser: [Chrome / Firefox / Safari / Edge]
2. Browser version: [XX.X]
3. Device: [Windows / Mac / iOS / Android]
4. Error message: [Exact text from F12 console]
5. Network errors (F12 → Network): [404? Failed? WASM error?]
6. Local test result: [Works / Fails / Didn't test]
7. When it started: [After deploy / Every time / First time]
8. Can you access Google.com: [Yes / No] (to test connectivity)
```

---

## Advanced Troubleshooting

### Check if WebAssembly is supported:

```javascript
// In browser console (F12), run:
console.log("WebAssembly supported:", typeof WebAssembly !== 'undefined');
console.log("WebAssembly.Memory:", typeof WebAssembly.Memory);
console.log("WebAssembly.Module:", typeof WebAssembly.Module);
```

### Check network requests:

```javascript
// See all network timing
performance.getEntries().forEach(entry => {
    console.log(entry.name, entry.duration, 'ms');
});
```

### Monitor for errors:

```javascript
// Capture all errors
window.addEventListener('error', e => {
    console.error('ERROR EVENT:', e.error, e.message);
});

window.addEventListener('unhandledrejection', e => {
    console.error('UNHANDLED REJECTION:', e.reason);
});
```

---

## Deployment Quick Fixes

### If deployed to Azure Static Web Apps:

1. **Check `staticwebapp.config.json` exists** in root
   ```json
   {
     "navigationFallback": {
       "rewrite": "/index.html",
       "exclude": ["/_framework/*", "/api/*"]
     },
     "mimeTypes": {
       ".wasm": "application/wasm"
     }
   }
   ```

2. **Verify deployed files**:
   - Go to Azure Portal
   - Check "Static Web App" → "Build Details"
   - Verify build succeeded
   - Check "Artifacts" for wwwroot files

3. **Rebuild and redeploy**:
   ```powershell
   # Force rebuild
   git commit --allow-empty -m "Force rebuild"
   git push origin main
   # Or trigger manually in Azure portal
   ```

---

## Still Having Issues?

### Contact Support with:
1. ✅ Browser console error (F12)
2. ✅ Network tab failures (F12 → Network)
3. ✅ Local test result
4. ✅ Deployment location (Azure / local / other)
5. ✅ Device and browser details

---

## Quick Checklist for Deployment Issues

- [ ] Run locally first (verify it works)
- [ ] Clear browser cache and cookies
- [ ] Try different browser
- [ ] Try incognito/private mode
- [ ] Check F12 console for specific error
- [ ] Check F12 network tab for 404s
- [ ] Verify staticwebapp.config.json exists
- [ ] Check Azure portal build logs
- [ ] Verify .NET 10 SDK is installed
- [ ] Try hard refresh (Ctrl+Shift+Delete)

---

**🎯 Most Common Fixes** (try these first):
1. Hard refresh: **Ctrl+Shift+Delete**
2. Clear cache and try different browser
3. Check F12 Console for exact error
4. Run locally to isolate deployment issue
5. Redeploy from GitHub (GitHub Actions will rebuild)

---

Last updated: 2025-01-16  
SEP IRA Calculator WebAssembly Troubleshooting Guide
