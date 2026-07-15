# Azure Deployment Fix Summary

## Overview

Fixed critical deployment issue preventing Blazor WebAssembly app from running on Azure Static Web Apps. The app was showing "Page Not Found" error with `CtorNotLocated` exception in the browser console.

**Status**: ✅ **RESOLVED** - App now live at: https://mango-island-01996be1e.7.azurestaticapps.net

---

## Problems Encountered

### 1. CtorNotLocated Error on Azure
**Error**: 
```
System.InvalidOperationException: CtorNotLocated, cc.isr.Finance.Sep.Ira.Shared.MainLayout
```

**Cause**: IL trimming (enabled by default on Azure) was aggressively removing component metadata needed by the Blazor dependency injection container.

**Local Status**: ✅ Worked fine locally (no trimming by default during debug)  
**Azure Status**: ❌ Failed with trimmed build (before fix)

---

### 2. MONO_WASM Runtime Initialization Failure
**Error**:
```
Your mono runtime and class libraries are out of sync.
The out of sync library is: System.Private.CoreLib.dll
```

**Cause**: .NET 10 WebAssembly runtime has compatibility issues. The prerelease runtime wasn't properly building or initializing components.

**Root Issue**: Target framework mismatch with prerelease SDK restriction in `global.json`

---

### 3. Package Version Incompatibility
**Error**:
```
NU1202: Package Microsoft.AspNetCore.Components.WebAssembly 10.0.10 
is not compatible with net9.0
```

**Cause**: After downgrading to .NET 9, the NuGet packages were still targeting .NET 10.

---

## Solutions Applied

### Solution 1: Create TrimmerRootDescriptor.xml

**File**: `src/sep.ira/calculator.wasm/TrimmerRootDescriptor.xml`

Explicitly preserves assemblies and namespaces from IL trimming:

```xml
<?xml version="1.0" encoding="utf-8"?>
<linker>
  <!-- Preserve the entire application assembly -->
  <assembly fullname="Sep.Ira.Calculator.WebAssembly" preserve="all" />

  <!-- Preserve all Blazor framework assemblies -->
  <assembly fullname="Microsoft.AspNetCore.Components" preserve="all" />
  <assembly fullname="Microsoft.AspNetCore.Components.Web" preserve="all" />
  <assembly fullname="Microsoft.AspNetCore.Components.WebAssembly" preserve="all" />

  <!-- Preserve dependency injection -->
  <assembly fullname="Microsoft.Extensions.DependencyInjection" preserve="all" />
  <assembly fullname="Microsoft.Extensions.DependencyInjection.Abstractions" preserve="all" />

  <!-- Preserve configuration -->
  <assembly fullname="Microsoft.Extensions.Configuration" preserve="all" />
  <assembly fullname="Microsoft.Extensions.Configuration.Abstractions" preserve="all" />
  <assembly fullname="Microsoft.Extensions.Configuration.Json" preserve="all" />

  <!-- Preserve logging -->
  <assembly fullname="Microsoft.Extensions.Logging" preserve="all" />
  <assembly fullname="Microsoft.Extensions.Logging.Abstractions" preserve="all" />

  <!-- Preserve options and primitives -->
  <assembly fullname="Microsoft.Extensions.Options" preserve="all" />
  <assembly fullname="Microsoft.Extensions.Primitives" preserve="all" />

  <!-- Preserve JSInterop -->
  <assembly fullname="Microsoft.JSInterop" preserve="all" />
  <assembly fullname="Microsoft.JSInterop.WebAssembly" preserve="all" />
</linker>
```

**Project File Update** (`SepIraCalculatorWebAssembly.csproj`):
```xml
<ItemGroup>
  <!-- Trimmer configuration for preserving Blazor components -->
  <TrimmerRootDescriptor Include="TrimmerRootDescriptor.xml" />
</ItemGroup>
```

---

### Solution 2: Downgrade Target Framework to .NET 9

**File**: `SepIraCalculatorWebAssembly.csproj`

**Change**:
```xml
<!-- Before -->
<TargetFramework>net10.0</TargetFramework>

<!-- After -->
<TargetFramework>net9.0</TargetFramework>
```

**Rationale**: 
- .NET 10 is prerelease and has WebAssembly compatibility issues
- .NET 9 is LTS (Long-Term Support) until November 2026
- Production stability is critical

---

### Solution 3: Update NuGet Package Versions

**File**: `SepIraCalculatorWebAssembly.csproj`

**Changes**:
```xml
<!-- Before -->
<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="10.0.10" />
<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="10.0.10" PrivateAssets="all" />

<!-- After -->
<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="9.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="9.0.0" PrivateAssets="all" />
```

---

### Solution 4: Enable Prerelease SDK (Temporary, then Reverted)

**File**: `global.json`

**Original**:
```json
"sdk": {
  "allowPrerelease": false
}
```

**Change** (temporary debugging):
```json
"sdk": {
  "allowPrerelease": true
}
```

**Note**: This was used for diagnosis but reverted when we downgraded to .NET 9 (which is not prerelease).

---

## Implementation Steps

### Step 1: Create Trimmer Configuration
```bash
# Created TrimmerRootDescriptor.xml with aggressive preservation settings
```

### Step 2: Update Project File
```bash
# Added TrimmerRootDescriptor item group to .csproj
```

### Step 3: Downgrade Framework
```bash
# Changed TargetFramework from net10.0 to net9.0
# Ran: dotnet workload restore
```

### Step 4: Update Package Versions
```bash
# Updated NuGet package versions from 10.0.10 to 9.0.0
```

### Step 5: Full Clean Rebuild
```powershell
cd C:\my\lib\vs\data\finance\src\sep.ira\calculator.wasm
Remove-Item -Recurse -Force bin, obj
dotnet clean
dotnet restore
dotnet publish -c Release
```

### Step 6: Local Testing with Trimming
```bash
# Tested with PublishTrimmed=true locally using dotnet serve
# Verified: browser console shows "Index.razor initialized" ✅
```

### Step 7: Deploy to Azure
```bash
git add .
git commit -m "Fix: Downgrade to .NET 9, update ASP.NET Core packages to 9.0.0, enhance TrimmerRootDescriptor"
git push
```

---

## Testing Checklist

- ✅ App builds without errors locally
- ✅ App runs with `PublishTrimmed=false`
- ✅ App runs with `PublishTrimmed=true` locally (trimming emulation)
- ✅ Browser console shows no red errors (only CDN tracking warnings)
- ✅ "Index.razor initialized" message appears
- ✅ Calculator UI renders correctly
- ✅ GitHub Actions workflow completes successfully
- ✅ Azure deployment completes successfully
- ✅ https://mango-island-01996be1e.7.azurestaticapps.net loads correctly

---

## Key Learnings

### 1. Always Test Trimming Locally
**Lesson**: Azure Static Web Apps uses full IL trimming by default. Never rely on "works locally" if you haven't tested with `PublishTrimmed=true`.

**Action**: Add trimming testing to pre-deployment checklist.

---

### 2. .NET 9 vs .NET 10 for WebAssembly
**Lesson**: Prerelease frameworks have edge cases. .NET 10 had runtime initialization issues that don't appear until you test with actual trimming.

**Recommendation**: Use stable LTS versions for production WebAssembly deployments.

---

### 3. TrimmerRootDescriptor is Critical
**Lesson**: Blazor components use reflection for DI. Without explicit preservation rules, the trimmer removes constructors needed at runtime.

**Best Practice**: For Blazor WebAssembly projects with trimming enabled, use `TrimmerRootDescriptor.xml` or add `[DynamicallyAccessedMembers]` attributes to components.

---

### 4. Browser Cache Matters
**Lesson**: Switching between .NET versions leaves old WASM files in browser cache, causing "out of sync" errors.

**Action**: Hard refresh with `Ctrl+Shift+Delete` when testing major version changes.

---

## Files Modified

| File | Changes |
|------|---------|
| `SepIraCalculatorWebAssembly.csproj` | • TargetFramework: net10.0 → net9.0<br>• Package versions: 10.0.10 → 9.0.0<br>• Added TrimmerRootDescriptor item group |
| `TrimmerRootDescriptor.xml` | ✨ **Created** - Preservation rules for all framework assemblies |
| `global.json` | Verified `allowPrerelease: false` (correct for .NET 9) |

---

## Azure Deployment Configuration

**Build Settings** (Azure Static Web Apps):
- Build Preset: .NET (Isolated)
- App location: `src/sep.ira/calculator.wasm`
- Output location: `publish/wwwroot`

**Triggered automatically on**:
- Push to `main` branch
- GitHub Actions workflow: `.github/workflows/azure-static-web-apps-*.yml`

---

## References

- [Microsoft Docs: Blazor WebAssembly Deployment](https://learn.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/webassembly)
- [IL Trimming Guide](https://learn.microsoft.com/en-us/dotnet/core/deploying/trimming/trim-self-contained)
- [TrimmerRootDescriptor Format](https://github.com/dotnet/runtime/blob/main/docs/design/specs/runtime-loader-spec.md)
- [.NET 9 Release Notes](https://github.com/dotnet/core/blob/main/release-notes/9.0/9.0.0/9.0.0.md)

---

## Troubleshooting Guide

### If You See "Page Not Found" on Azure

1. **Check browser console** (F12 → Console)
2. **Look for error patterns**:
   - `CtorNotLocated` → Update `TrimmerRootDescriptor.xml`
   - `out of sync library` → Clear browser cache
   - Runtime errors → Check .NET version compatibility
3. **Test locally with trimming**:
   ```powershell
   dotnet publish -c Release
   dotnet serve -p 8080
   ```
4. **Check Azure deployment logs**: Azure Portal → Static Web App → Deployment
5. **Live URL for testing**: https://mango-island-01996be1e.7.azurestaticapps.net

### If "dotnet workload restore" Fails

```powershell
# Ensure .NET 9 SDK is installed
dotnet --list-sdks

# Manually install WebAssembly workload
dotnet workload install wasi-experimental
```

### If NuGet Package Versions Are Inconsistent

```powershell
# Clean everything
Remove-Item -Recurse -Force bin, obj
dotnet clean
dotnet nuget locals all --clear
dotnet restore
```

---

## Future Upgrades

When upgrading to .NET 10 or later in the future:

1. ✅ Test locally with full trimming enabled **first**
2. ✅ Update package versions to match framework version
3. ✅ Update or verify `TrimmerRootDescriptor.xml` compatibility
4. ✅ Test on staging environment before production
5. ✅ Have rollback plan ready

---

## Success Criteria Met

| Criterion | Status |
|-----------|--------|
| App builds without errors | ✅ |
| App runs locally | ✅ |
| App runs on Azure | ✅ |
| No console errors (F12) | ✅ (except CDN tracking warnings) |
| Calculator UI visible | ✅ |
| Calculator functional | ✅ |
| Deployment automated | ✅ |

---

**Deployment Date**: 2024  
**Last Updated**: After successful Azure deployment  
**Status**: 🟢 Production Ready
