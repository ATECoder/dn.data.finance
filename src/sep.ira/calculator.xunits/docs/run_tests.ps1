# ============================================================================
# SEP IRA Calculator - Unit Test Execution Script (PowerShell)
# ============================================================================
# This script runs all unit tests and generates comprehensive reports
# ============================================================================

param(
    [switch]$GenerateHtmlReport = $false,
    [switch]$OpenResults = $false,
    [switch]$RunCoverageAnalysis = $false,
    [string]$Configuration = "Release"
)

# Set error action
$ErrorActionPreference = "Stop"

# Colors for output
$Colors = @{
    Info    = "Cyan"
    Success = "Green"
    Warning = "Yellow"
    Error   = "Red"
}

function Write-Log {
    param([string]$Message, [string]$Level = "Info")
    $color = $Colors[$Level]
    Write-Host "[$Level] $Message" -ForegroundColor $color
}

function Get-Timestamp {
    return Get-Date -Format "yyyyMMdd_HHmmss"
}

# Setup paths
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$testProjectPath = Join-Path $scriptPath "..\..\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj"
$solutionPath = Join-Path $scriptPath "..\..\.."
$docsPath = Join-Path $scriptPath "."
$timestamp = Get-Timestamp

Write-Host ""
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "SEP IRA Calculator - Unit Test Execution" -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

Write-Log "Solution Path: $solutionPath"
Write-Log "Test Project: $testProjectPath"
Write-Log "Docs Directory: $docsPath"
Write-Log "Timestamp: $timestamp"
Write-Log "Configuration: $Configuration"
Write-Host ""

# Verify test project exists
if (-not (Test-Path $testProjectPath)) {
    Write-Log "Test project not found: $testProjectPath" "Error"
    exit 1
}

# Step 1: Restore NuGet packages
Write-Log "STEP 1/5: Restoring NuGet packages..." "Info"
try {
    dotnet restore $solutionPath
    Write-Log "NuGet packages restored successfully" "Success"
} catch {
    Write-Log "Failed to restore NuGet packages: $_" "Error"
    exit 1
}
Write-Host ""

# Step 2: Build solution
Write-Log "STEP 2/5: Building solution..." "Info"
try {
    dotnet build $solutionPath --configuration $Configuration --no-restore
    Write-Log "Solution built successfully" "Success"
} catch {
    Write-Log "Failed to build solution: $_" "Error"
    exit 1
}
Write-Host ""

# Step 3: Run tests
Write-Log "STEP 3/5: Running unit tests..." "Info"
$trxFile = Join-Path $docsPath "TestResults_$timestamp.trx"
try {
    $testArgs = @(
        "test",
        $testProjectPath,
        "--configuration", $Configuration,
        "--no-build",
        "--no-restore",
        "--verbosity", "normal",
        "--logger", "trx;LogFileName=$trxFile",
        "--logger", "console;verbosity=normal"
    )

    if ($RunCoverageAnalysis) {
        $testArgs += @("/p:CollectCoverage=true", "/p:CoverageFormat=cobertura")
    }

    & dotnet $testArgs
    Write-Log "Tests completed" "Success"
} catch {
    Write-Log "Test execution encountered issues: $_" "Warning"
}
Write-Host ""

# Step 4: Generate reports
Write-Log "STEP 4/5: Generating test reports..." "Info"

# Create TRX report summary
$xmlPath = $trxFile
if (Test-Path $xmlPath) {
    Write-Log "TRX report created: $xmlPath" "Success"

    # Parse TRX file for statistics
    try {
        [xml]$trxContent = Get-Content $xmlPath
        $counters = $trxContent.TestRun.ResultSummary.Counters

        $totalTests = [int]$counters.total
        $passed = [int]$counters.passed
        $failed = [int]$counters.failed
        $skipped = [int]$counters.skipped

        Write-Host ""
        Write-Host "Test Results:" -ForegroundColor Cyan
        Write-Host "  Total:  $totalTests" -ForegroundColor White
        Write-Host "  Passed: $passed" -ForegroundColor Green
        Write-Host "  Failed: $failed" -ForegroundColor $(if ($failed -gt 0) { "Red" } else { "Green" })
        Write-Host "  Skipped: $skipped" -ForegroundColor Yellow
        Write-Host ""
    } catch {
        Write-Log "Could not parse TRX file statistics" "Warning"
    }
} else {
    Write-Log "TRX report not found" "Warning"
}

# Create markdown summary
$summaryFile = Join-Path $docsPath "LATEST_TEST_RUN.md"
$summary = @"
# Test Execution Report - $timestamp

**Execution Date:** $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")

**Configuration:** $Configuration

**Test Project:** cc.isr.Finance.Sep.Ira.Calculator.XUnits

**Results Files:**
- TestResults_$timestamp.trx

## How to View Results

1. **Visual Studio Test Explorer**
   - Open Visual Studio
   - Go to Test > Test Explorer
   - View latest results

2. **Visual Studio TRX File**
   - Open TestResults_$timestamp.trx in Visual Studio
   - Detailed test results and output

3. **Documentation**
   - AppreciatorTestsSummary.md - High-level overview
   - AppreciatorTestsDetails.md - Detailed results
   - TestExecutionGuide.md - Execution instructions

## Next Steps

- [ ] Review test results
- [ ] Check for failures or warnings
- [ ] Update documentation as needed
- [ ] Commit changes to repository

"@

Set-Content -Path $summaryFile -Value $summary
Write-Log "Summary report created: $summaryFile" "Success"
Write-Host ""

# Step 5: Open results (optional)
Write-Log "STEP 5/5: Finalizing..." "Info"

if ($OpenResults -and (Test-Path $trxFile)) {
    Write-Log "Opening test results..." "Info"
    & $trxFile
}

Write-Host ""
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "TEST EXECUTION COMPLETE" -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""
Write-Log "Reports location: $docsPath" "Success"
Write-Log "Latest report: $(Split-Path $summaryFile -Leaf)" "Success"
Write-Host ""

# List recent test reports
$recentReports = Get-ChildItem -Path $docsPath -Filter "TestResults_*.trx" | Sort-Object LastWriteTime -Descending | Select-Object -First 5
if ($recentReports) {
    Write-Host "Recent Test Reports:" -ForegroundColor Cyan
    foreach ($report in $recentReports) {
        Write-Host "  - $($report.Name) ($($report.LastWriteTime))"
    }
    Write-Host ""
}

Write-Log "Test execution completed successfully" "Success"
