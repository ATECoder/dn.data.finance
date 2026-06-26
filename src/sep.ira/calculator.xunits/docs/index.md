# SEP IRA Calculator - Test Documentation & Execution Index

## 📋 Overview

This document provides a complete index of all test-related documentation and execution methods for the SEP IRA Calculator project.

**Framework:** xUnit  
**Language:** C#  
**Target Framework:** .NET 8.0+  
**Last Updated:** 2026-01-18

---

## 📁 Documentation Files

### Core Documentation

| File | Purpose | Audience |
|------|---------|----------|
| **readme.md** | Main test documentation index | Everyone |
| **TestExecutionGuide.md** | Step-by-step guide to run tests | Developers |
| **AppreciatorTestsSummary.md** | High-level test results overview | QA, Managers |
| **AppreciatorTestsDetails.md** | Detailed test method results | Developers, QA |
| **AppreciatorTestsReference.md** | Test structure and naming reference | Developers |
| **AppreciatorTestsCompletionReport.md** | Project completion status | Project Managers |

### Execution Scripts

| File | Type | Usage |
|------|------|-------|
| **run_tests.bat** | Batch Script | Windows Command Prompt |
| **run_tests.ps1** | PowerShell Script | Windows PowerShell |

---

## 🚀 Quick Start

### For Windows Users

#### Using Batch Script (Simple)
```cmd
cd src\sep.ira\calculator.xunits\docs
run_tests.bat
```

#### Using PowerShell Script (Advanced)
```powershell
cd src\sep.ira\calculator.xunits\docs
.\run_tests.ps1 -GenerateHtmlReport -OpenResults
```

#### Using Command Line Directly
```cmd
dotnet test src\sep.ira\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj --verbosity detailed
```

### For Visual Studio Users

1. **Test > Test Explorer** (Ctrl+E, T)
2. **Run All Tests** button
3. View results in Test Explorer pane

---

## 📊 Test Projects

### Test Project: cc.isr.Finance.Sep.Ira.Calculator.XUnits

**Location:** `src\sep.ira\calculator.xunits\`

**Test Classes:**
1. **AppreciatorCalculationTests**
   - Tests core calculation logic
   - Validates investment projections
   - Tests tax scenarios
   - ~25-30 test methods

2. **AppreciatorInputsValidationTests**
   - Tests input parameter validation
   - Boundary condition testing
   - Error handling verification
   - ~20-25 test methods

**Total Test Methods:** ~50+

---

## 🔧 Execution Methods Comparison

| Method | Ease | Speed | Report Generation | Platform |
|--------|------|-------|-------------------|----------|
| **Visual Studio Test Explorer** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | Built-in | Windows |
| **Batch Script (run_tests.bat)** | ⭐⭐⭐⭐ | ⭐⭐⭐ | TRX, Markdown | Windows |
| **PowerShell (run_tests.ps1)** | ⭐⭐⭐ | ⭐⭐⭐ | TRX, HTML, Markdown | Windows |
| **dotnet CLI** | ⭐⭐⭐ | ⭐⭐⭐⭐ | Console | All Platforms |
| **GitHub Actions** | ⭐⭐ | ⭐⭐⭐⭐⭐ | Multiple | Cloud |

---

## 📝 Test Execution Steps (Manual)

### Step 1: Restore Dependencies
```bash
dotnet restore
```

### Step 2: Build Solution
```bash
dotnet build --configuration Release
```

### Step 3: Run Tests
```bash
dotnet test src\sep.ira\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj ^
  --configuration Release ^
  --no-build ^
  --verbosity normal
```

### Step 4: Generate Report
```bash
dotnet test src\sep.ira\calculator.xunits\cc.isr.Finance.Sep.Ira.Calculator.XUnits.csproj ^
  --logger "trx;LogFileName=TestResults.trx"
```

---

## 📈 Test Report Types

### TRX Report (Visual Studio)
- **Format:** XML
- **Extension:** `.trx`
- **Viewer:** Visual Studio
- **Command:** `--logger "trx;LogFileName=output.trx"`

### Console Output
- **Format:** Text
- **Real-time:** Yes
- **Best for:** Quick feedback
- **Command:** `--verbosity detailed`

### Markdown Report
- **Format:** Markdown
- **Viewer:** Any text editor or GitHub
- **Best for:** Documentation
- **Created by:** Script automation

### HTML Report (Optional)
- **Format:** HTML
- **Viewer:** Web browser
- **Best for:** Sharing results
- **Tool:** ReportGenerator (additional install)

---

## 🔍 Analyzing Test Results

### Visual Studio Test Explorer
1. Open **Test > Test Explorer**
2. Click on failed test
3. View output in panel below
4. Click **Debug** to run with debugger

### From TRX File
1. Double-click `TestResults_*.trx`
2. Opens in Visual Studio
3. View detailed results
4. See stack traces for failures

### From Console Output
1. Look for red text (failures)
2. Note exception messages
3. Check assertion details
4. Review output messages

---

## 🐛 Troubleshooting

### Tests Not Discovered
**Solution:** Rebuild and refresh Test Explorer
```bash
dotnet clean
dotnet build
```

### NuGet Restore Fails
**Solution:** Delete lock file and retry
```bash
rm packages.lock.json
dotnet restore
```

### Tests Timeout
**Solution:** Increase timeout in configuration
```bash
dotnet test --verbosity detailed --configuration Release
```

### Platform-Specific Failures
**Solution:** Run on target platform
```bash
# Windows
dotnet test --runtime win-x64

# Linux
dotnet test --runtime linux-x64
```

---

## 📋 Test Checklist

Before committing code:

- [ ] All tests pass locally
- [ ] No new compiler warnings
- [ ] Code coverage maintained >80%
- [ ] Test documentation updated
- [ ] Commit message references test results

---

## 🔄 Continuous Integration

### GitHub Actions
Add to `.github/workflows/tests.yml`:
```yaml
- name: Run Tests
  run: dotnet test --verbosity detailed --configuration Release
```

### Azure DevOps
Add to `azure-pipelines.yml`:
```yaml
- task: DotNetCoreCLI@2
  inputs:
    command: 'test'
    projects: '**/calculator.xunits/*.csproj'
```

---

## 📚 Documentation Map

```
docs/
├── readme.md                           # Main index
├── TestExecutionGuide.md             # Execution instructions
├── AppreciatorTestsSummary.md          # Test summary
├── AppreciatorTestsDetails.md          # Detailed results
├── AppreciatorTestsReference.md        # Reference guide
├── AppreciatorTestsCompletionReport.md # Completion status
├── run_tests.bat                       # Batch execution script
├── run_tests.ps1                       # PowerShell execution script
├── LATEST_TEST_RUN.md                  # Latest execution report
└── TestResults_*.trx                   # Test result files (auto-generated)
```

---

## 🎯 Best Practices

1. **Run Tests Frequently**
   - Before each commit
   - After merging branches
   - Before deployment

2. **Maintain Documentation**
   - Update test results regularly
   - Document new test cases
   - Keep this index current

3. **Monitor Coverage**
   - Aim for >80% coverage
   - Add tests for new features
   - Remove dead code

4. **Automate Testing**
   - Use CI/CD pipelines
   - Run on multiple platforms
   - Generate reports automatically

5. **Review Results**
   - Investigate failures quickly
   - Document known issues
   - Share results with team

---

## 📞 Support & Questions

### Getting Help

1. **Check TestExecutionGuide.md** for command help
2. **Review test output** for error details
3. **See Troubleshooting section** above
4. **Examine test source code** in project

### Common Questions

**Q: How do I run a single test?**
```bash
dotnet test --filter "TestClassName"
```

**Q: How do I see detailed failure information?**
```bash
dotnet test --verbosity detailed
```

**Q: Where are test reports saved?**
```
src\sep.ira\calculator.xunits\docs\
```

---

## 📅 Maintenance Schedule

- **Daily:** Run tests locally before push
- **Every Commit:** CI/CD runs tests automatically
- **Weekly:** Review test coverage metrics
- **Monthly:** Update documentation
- **Quarterly:** Analyze and improve tests

---

**Framework Version:** xUnit 2.4.x  
**Last Updated:** 2026-01-18  
**Status:** ✅ Active  
**Maintainer:** Development Team
