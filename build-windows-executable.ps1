#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Build Windows Executable for SEP IRA Calculator WebAssembly

.DESCRIPTION
    Creates a self-contained Windows executable (.zip) for distribution.
    No .NET installation required on user's machine.

.PARAMETER BuildType
    Build configuration: 'Release' (default) or 'Debug'

.PARAMETER OutputPath
    Output directory for the ZIP file. Default: project root

.EXAMPLE
    .\build-windows-executable.ps1
    .\build-windows-executable.ps1 -BuildType Release -OutputPath C:\Releases
#>

param(
    [ValidateSet('Release', 'Debug')]
    [string]$BuildType = 'Release',

    [string]$OutputPath = '.'
)

$ErrorActionPreference = 'Stop'

# Colors for output
function Write-Success { Write-Host $args[0] -ForegroundColor Green }
function Write-Info { Write-Host $args[0] -ForegroundColor Cyan }
function Write-Warning { Write-Host $args[0] -ForegroundColor Yellow }
function Write-Error2 { Write-Host $args[0] -ForegroundColor Red }

# Configuration
$ProjectPath = "src/sep.ira/calculator.wasm"
$ProjectName = "Sep.Ira.Calculator.WebAssembly"
$Runtime = "win-x64"
$PublishDir = "$ProjectPath/publish-windows"
$ZipName = "SEPIraCalculator-Windows-x64.zip"
$ZipPath = "$OutputPath/$ZipName"

Write-Info "╔════════════════════════════════════════════════════════════╗"
Write-Info "║   SEP IRA Calculator - Windows Executable Builder           ║"
Write-Info "╚════════════════════════════════════════════════════════════╝"
Write-Host ""

# Step 1: Validate project exists
Write-Info "📋 Validating project..."
if (-not (Test-Path "$ProjectPath/SepIraCalculatorWebAssembly.csproj")) {
    Write-Error2 "❌ Project not found: $ProjectPath/SepIraCalculatorWebAssembly.csproj"
    exit 1
}
Write-Success "✅ Project found"
Write-Host ""

# Step 2: Clean previous builds
Write-Info "🧹 Cleaning previous builds..."
if (Test-Path $PublishDir) {
    Remove-Item $PublishDir -Recurse -Force | Out-Null
    Write-Success "✅ Cleaned: $PublishDir"
}
Write-Host ""

# Step 3: Restore dependencies
Write-Info "📦 Restoring dependencies..."
dotnet restore $ProjectPath
if ($LASTEXITCODE -ne 0) {
    Write-Error2 "❌ Restore failed!"
    exit 1
}
Write-Success "✅ Dependencies restored"
Write-Host ""

# Step 4: Build project
Write-Info "🔨 Building project (configuration: $BuildType)..."
dotnet build $ProjectPath `
    --configuration $BuildType `
    --no-restore `
    --nologo

if ($LASTEXITCODE -ne 0) {
    Write-Error2 "❌ Build failed!"
    exit 1
}
Write-Success "✅ Build successful"
Write-Host ""

# Step 5: Publish as self-contained
Write-Info "📤 Publishing self-contained application..."
Write-Info "   Runtime: $Runtime"
Write-Info "   Output:  $PublishDir"
Write-Info ""

dotnet publish $ProjectPath `
    --configuration $BuildType `
    --runtime $Runtime `
    --self-contained `
    --no-build `
    --output $PublishDir `
    --nologo

if ($LASTEXITCODE -ne 0) {
    Write-Error2 "❌ Publish failed!"
    exit 1
}
Write-Success "✅ Publish successful"
Write-Host ""

# Step 6: Create ZIP package
Write-Info "📦 Creating ZIP package..."
if (Test-Path $ZipPath) {
    Remove-Item $ZipPath -Force | Out-Null
}

Compress-Archive `
    -Path $PublishDir `
    -DestinationPath $ZipPath `
    -Force | Out-Null

if ($LASTEXITCODE -ne 0) {
    Write-Error2 "❌ ZIP creation failed!"
    exit 1
}

$FileSize = (Get-Item $ZipPath).Length / 1MB
Write-Success "✅ ZIP created: $ZipPath"
Write-Success "   Size: $([Math]::Round($FileSize, 2)) MB"
Write-Host ""

# Step 7: Verify contents
Write-Info "🔍 Verifying ZIP contents..."
$ZipContents = Add-Type -AssemblyName System.IO.Compression.FileSystem -PassThru | `
    ForEach-Object { [System.IO.Compression.ZipFile]::OpenRead($ZipPath) } | `
    ForEach-Object { $_.Entries } | `
    Measure-Object
Write-Success "✅ ZIP contains $($ZipContents.Count) files"
Write-Host ""

# Summary
Write-Info "╔════════════════════════════════════════════════════════════╗"
Write-Success "✅ BUILD COMPLETE!"
Write-Info "╚════════════════════════════════════════════════════════════╝"
Write-Host ""

Write-Info "📦 Distribution Package:"
Write-Host "   File: $ZipPath" -ForegroundColor White
Write-Host "   Size: $([Math]::Round($FileSize, 2)) MB" -ForegroundColor White
Write-Host ""

Write-Info "📤 User Instructions:"
Write-Host "   1. Download: $ZipName"
Write-Host "   2. Extract the ZIP file"
Write-Host "   3. Run: $ProjectName.exe"
Write-Host ""

Write-Info "💾 Next Steps:"
Write-Host "   • Share the ZIP file with users"
Write-Host "   • Users extract and run the .exe"
Write-Host "   • No .NET installation required"
Write-Host "   • Works completely offline"
Write-Host ""

Write-Success "Ready to distribute! 🚀"
