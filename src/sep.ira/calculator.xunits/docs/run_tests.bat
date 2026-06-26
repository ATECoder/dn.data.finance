@echo off
REM ============================================================================
REM SEP IRA Calculator - Unit Test Execution Script
REM ============================================================================
REM This script runs all unit tests and generates reports in the docs folder
REM ============================================================================

setlocal enabledelayedexpansion

echo.
echo ============================================================================
echo SEP IRA Calculator - Unit Test Execution
echo ============================================================================
echo.

REM Set paths
set SOLUTION_DIR=%~dp0..\..\
set TEST_PROJECT=%SOLUTION_DIR%sep.ira\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj
set DOCS_DIR=%SOLUTION_DIR%sep.ira\calculator.xunits\docs
set TIMESTAMP=%date:~-4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%%time:~6,2%

REM Clean timestamp format (remove spaces)
set TIMESTAMP=%TIMESTAMP: =0%

echo [INFO] Solution Directory: %SOLUTION_DIR%
echo [INFO] Test Project: %TEST_PROJECT%
echo [INFO] Docs Directory: %DOCS_DIR%
echo [INFO] Timestamp: %TIMESTAMP%
echo.

REM Check if test project exists
if not exist "%TEST_PROJECT%" (
    echo [ERROR] Test project not found: %TEST_PROJECT%
    exit /b 1
)

REM Restore NuGet packages
echo [STEP 1/4] Restoring NuGet packages...
dotnet restore "%SOLUTION_DIR%"
if errorlevel 1 (
    echo [ERROR] Failed to restore NuGet packages
    exit /b 1
)
echo [OK] Packages restored successfully
echo.

REM Build solution
echo [STEP 2/4] Building solution...
dotnet build "%SOLUTION_DIR%" --configuration Release --no-restore
if errorlevel 1 (
    echo [ERROR] Failed to build solution
    exit /b 1
)
echo [OK] Solution built successfully
echo.

REM Run tests with TRX report
echo [STEP 3/4] Running tests...
dotnet test "%TEST_PROJECT%" ^
    --configuration Release ^
    --no-build ^
    --no-restore ^
    --verbosity normal ^
    --logger "trx;LogFileName=%DOCS_DIR%\TestResults_%TIMESTAMP%.trx" ^
    --logger "console;verbosity=normal"

if errorlevel 1 (
    echo [WARNING] Some tests failed. Check the detailed report.
) else (
    echo [OK] All tests passed
)
echo.

REM Generate console-friendly report
echo [STEP 4/4] Generating report summary...
echo.
echo ============================================================================
echo TEST EXECUTION COMPLETE
echo ============================================================================
echo.
echo Test Reports Location: %DOCS_DIR%
echo TRX Report: TestResults_%TIMESTAMP%.trx
echo.
echo To view detailed results:
echo   - Visual Studio: Open TestResults_*.trx file
echo   - Console: Review output above
echo.
echo Previous Reports:
dir /B "%DOCS_DIR%\TestResults_*.trx" 2>nul
if errorlevel 1 (
    echo   (No previous reports found)
)
echo.

REM Create summary report
echo [INFO] Creating summary report...
set SUMMARY_FILE=%DOCS_DIR%\LATEST_TEST_RUN.md
(
    echo # Test Execution Report - %TIMESTAMP%
    echo.
    echo **Execution Date:** %date% %time%
    echo.
    echo **Test Project:** cc.isr.Finance.Sep.Ira.Calculator.XUnits
    echo.
    echo **Results Files:**
    echo - TestResults_%TIMESTAMP%.trx
    echo.
    echo **How to View Results:**
    echo 1. **Visual Studio:** Open TestResults_*.trx in Visual Studio
    echo 2. **TRX File:** Located in docs folder
    echo 3. **Details:** See AppreciatorTestsDetails.md and AppreciatorTestsSummary.md
    echo.
    echo **Next Steps:**
    echo - Review test failures if any
    echo - Update test documentation
    echo - Check code coverage metrics
    echo.
) > "%SUMMARY_FILE%"

echo [OK] Summary report created: %SUMMARY_FILE%
echo.
echo ============================================================================
pause
exit /b 0
