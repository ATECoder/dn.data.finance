# 🎯 Creating a Windows Executable for Your App

## Overview

There are several ways to package your Blazor WebAssembly app as a Windows executable. This guide covers the practical options.

---

## Option 1: Electron (Recommended for Desktop App Feel)

### Why Electron?
- ✅ Looks and feels like a native Windows app
- ✅ Works completely offline
- ✅ Single installer file (.exe or MSI)
- ✅ Auto-updatable
- ✅ Includes Chromium browser (no dependencies)

### Option 1A: ElectronNET (Easiest)

#### Step 1: Add ElectronNET Package

```powershell
cd src/sep.ira/calculator.wasm

# Install ElectronNET CLI
dotnet tool install electronnet.cli -g

# Add ElectronNET to project
dotnet add package ElectronNET
dotnet add package ElectronNET.API
```

#### Step 2: Update Program.cs

```csharp
namespace cc.isr.Finance.Sep.Ira;

using ElectronNET.API;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient 
        { 
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
        });

        await builder.Build().RunAsync();
    }
}
```

#### Step 3: Initialize Electron

```powershell
electronnet init
```

#### Step 4: Build Executable

```powershell
# For Windows x64
electronnet build /target win

# Creates: bin/Desktop/SEP.IRA.Calculator.WebAssembly 1.0.0.exe
```

**Result**: Single .exe file that installs your app

---

## Option 2: Publish as Self-Contained (Simplest)

### Why This?
- ✅ Single-click setup
- ✅ No .NET installation needed on user's computer
- ✅ Includes .NET runtime
- ✅ Fast to create

### Steps:

```powershell
cd src/sep.ira/calculator.wasm

# Publish as self-contained Win64
dotnet publish `
  --configuration Release `
  --runtime win-x64 `
  --self-contained `
  --output ./publish-win64

# Creates folder with .exe and all dependencies (~200 MB)
```

### Package as ZIP

```powershell
# Create ZIP for distribution
Compress-Archive `
  -Path ./publish-win64 `
  -DestinationPath ./SEPIraCalculator-Windows-x64.zip `
  -Force

# User extracts ZIP and runs: Sep.Ira.Calculator.WebAssembly.exe
```

**Result**: ZIP file that users download and extract

---

## Option 3: Use an Installer (Inno Setup)

### Why This?
- ✅ Professional installer
- ✅ Start menu shortcuts
- ✅ Uninstall support
- ✅ Registry entries
- ✅ Desktop shortcut

### Steps:

1. **Publish app first**:
```powershell
dotnet publish `
  --configuration Release `
  --runtime win-x64 `
  --self-contained `
  --output ./publish-win64
```

2. **Download Inno Setup**:
   - Go to: https://jrsoftware.org/isdl.php
   - Install (free, open source)

3. **Create Setup Script**: `setup.iss`

```ini
[Setup]
AppName=SEP IRA Calculator
AppVersion=1.0.0
DefaultDirName={autopf}\SEP IRA Calculator
DefaultGroupName=SEP IRA Calculator
OutputDir=.\dist
OutputBaseFilename=SEPIraCalculator-Setup
Compression=lzma2
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64

[Files]
Source: "publish-win64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\SEP IRA Calculator"; Filename: "{app}\Sep.Ira.Calculator.WebAssembly.exe"
Name: "{group}\Uninstall"; Filename: "{uninstallexe}"
Name: "{commondesktop}\SEP IRA Calculator"; Filename: "{app}\Sep.Ira.Calculator.WebAssembly.exe"

[Run]
Filename: "{app}\Sep.Ira.Calculator.WebAssembly.exe"; Description: "Launch application"; Flags: nowait postinstall skipifsilent
```

4. **Build Installer**:
```powershell
# Using Inno Setup GUI or command line:
iscc setup.iss
```

**Result**: Professional `.exe` installer file

---

## Option 4: MSIX Package (Modern Windows)

### Why This?
- ✅ Can publish to Microsoft Store
- ✅ Built-in Windows 10+ support
- ✅ Automatic updates
- ✅ Modern packaging format

### Steps:

```powershell
# Install MSIX packaging tool
# Download from Microsoft Store or:
# https://www.microsoft.com/en-us/p/msix-packaging-tool/9n5lw3jbcxkf

# Use MSIX Packaging Tool to convert your published app to .msix
```

---

## 🎯 Comparison Table

| Method | Setup | File Size | Installation | Updates | Complexity |
|--------|-------|-----------|--------------|---------|-----------|
| **Electron** | Excellent | ~150 MB | One-click | Built-in | Medium |
| **ZIP + EXE** | Basic | ~200 MB | Extract + Run | Manual | Very Simple |
| **Inno Setup** | Professional | ~200 MB | Installer | Manual | Medium |
| **MSIX** | Modern | ~200 MB | Microsoft Store | Auto | High |
| **Web URL** | Best | 0 MB | None | Auto | Simple |

---

## 🚀 Recommended Flow

### For Quick Sharing
1. Use **Option 2 (ZIP)**
2. Create: `SEPIraCalculator-Windows-x64.zip`
3. Share the ZIP file
4. Users: Extract → Run `.exe`

### For Professional Distribution
1. Use **Option 3 (Inno Setup)**
2. Create: `SEPIraCalculator-Setup.exe`
3. Users run installer
4. Appears in Start Menu + Desktop shortcut

### For Desktop App Experience
1. Use **Option 1 (Electron)**
2. Create: `SEP.IRA.Calculator.WebAssembly 1.0.0.exe`
3. Users double-click to install
4. Professional app icon, updatable

---

## 📝 Quick Script: Build Everything

Create `build-windows.ps1`:

```powershell
#!/usr/bin/env pwsh

# Build Windows Executable for SEP IRA Calculator

Write-Host "🔨 Building SEP IRA Calculator for Windows..." -ForegroundColor Green
Write-Host ""

$ProjectPath = "src/sep.ira/calculator.wasm"
$ReleaseDir = "$ProjectPath/publish-win64"
$ZipName = "SEPIraCalculator-Windows-x64.zip"

# Step 1: Clean
Write-Host "📦 Cleaning previous builds..." -ForegroundColor Yellow
if (Test-Path $ReleaseDir) {
    Remove-Item $ReleaseDir -Recurse -Force
}

# Step 2: Publish
Write-Host "🔨 Publishing self-contained application..." -ForegroundColor Yellow
dotnet publish $ProjectPath `
  --configuration Release `
  --runtime win-x64 `
  --self-contained `
  --output $ReleaseDir

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Publish failed!" -ForegroundColor Red
    exit 1
}

# Step 3: Create ZIP
Write-Host "📦 Creating ZIP package..." -ForegroundColor Yellow
Compress-Archive `
  -Path $ReleaseDir `
  -DestinationPath $ZipName `
  -Force

$FileSize = (Get-Item $ZipName).Length / 1MB
Write-Host "✅ Created: $ZipName ($([Math]::Round($FileSize, 2)) MB)" -ForegroundColor Green
Write-Host ""
Write-Host "📤 Ready to share!" -ForegroundColor Green
Write-Host ""
Write-Host "User Instructions:" -ForegroundColor Cyan
Write-Host "1. Download: $ZipName"
Write-Host "2. Extract the ZIP file"
Write-Host "3. Run: Sep.Ira.Calculator.WebAssembly.exe"
Write-Host ""
```

**Run it**:
```powershell
./build-windows.ps1
```

---

## 📋 Step-by-Step: Create ZIP (Simplest Option)

### For You (Developer)

```powershell
# 1. Navigate to project
cd C:\my\lib\vs\data\finance\src\sep.ira\calculator.wasm

# 2. Publish
dotnet publish `
  --configuration Release `
  --runtime win-x64 `
  --self-contained `
  --output ./publish-win64

# 3. Create ZIP
Compress-Archive `
  -Path ./publish-win64 `
  -DestinationPath ../SEPIraCalculator-Windows-x64.zip `
  -Force

# 4. Share the ZIP file
Write-Host "✅ Done! Share: ../SEPIraCalculator-Windows-x64.zip"
```

### For Users

1. Download: `SEPIraCalculator-Windows-x64.zip`
2. Right-click → "Extract All"
3. Choose folder location
4. Find: `Sep.Ira.Calculator.WebAssembly.exe`
5. Double-click to run
6. (Optional) Create desktop shortcut

---

## 🔑 Key Points

- **Self-contained** = Includes .NET runtime, no installation needed
- **win-x64** = 64-bit Windows (most common)
- **Release** = Optimized, smaller file size
- **File size** = ~200 MB (includes .NET runtime)
- **Works offline** = Yes, completely standalone
- **Updates** = Manual (create new ZIP for new version)

---

## 🚀 Next Steps

1. **Choose your method** (I recommend ZIP for quick start, Electron for polished app)
2. **Run the build command**
3. **Test the executable** on your Windows machine
4. **Share with users**

Which option would you like to implement first?

- [ ] Quick ZIP file (5 minutes)
- [ ] Electron app (15 minutes)
- [ ] Inno Setup installer (30 minutes)

---

## 📚 References

- [Electron.NET](https://github.com/ElectronNET/Electron.NET)
- [Inno Setup](https://jrsoftware.org/)
- [MSIX Packaging Tool](https://docs.microsoft.com/en-us/windows/msix/packaging-tool/tool-overview)
- [.NET Self-Contained Deployment](https://docs.microsoft.com/en-us/dotnet/core/deploying/)

