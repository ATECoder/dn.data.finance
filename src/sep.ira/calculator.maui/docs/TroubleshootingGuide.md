# MAUI SEP IRA Calculator - Comprehensive Troubleshooting Guide

## Quick Diagnostics

### Application Won't Start
**Symptoms:** App crashes on launch or hangs
**Steps:**
1. Check debug output for exceptions
2. Verify AppreciatorPage namespace: `cc.isr.Finance.Sep.Ira.Views.AppreciatorPage`
3. Ensure BindingContext is set: `BindingContext = new AppreciatorViewModel();`

### Page Won't Load
**Symptoms:** XAML parse exception or blank screen
**Common Causes:**
| Cause | Error | Fix |
|-------|-------|-----|
| Converter missing | "StaticResource not found" | Register in App.xaml |
| Binding invalid | "Binding failed" | Check property names match |
| XAML syntax | "XamlParseException" | Validate XML is well-formed |

### Controls Don't Render
**Symptoms:** Labels/buttons invisible
**Troubleshooting:**
```csharp
// Check if visibility bindings are correct
// Ensure HasResults property initialized to false
// For Entry controls, ensure BackgroundColor set
```

---

## Error Reference Guide

### CS0246: Type or Namespace Not Found

**Full Error:** `'AppreciatorInputs' could not be found`

**Likely Cause:** Incorrect namespace for AppreciatorInputs

**Investigation:**
```bash
# Search for AppreciatorInputs definition
grep -r "class AppreciatorInputs" src\sep.ira\calculator\
grep -r "struct AppreciatorInputs" src\sep.ira\calculator\
```

**Resolution:** One of three options:

**Option A: If nested in Appreciator class**
```csharp
// Keep current reference (correct):
var inputs = new global::cc.isr.Finance.Sep.Ira.Appreciator.AppreciatorInputs { ... };
```

**Option B: If separate class in same namespace**
```csharp
// Update to:
var inputs = new AppreciatorInputs { ... };

// Add using:
using cc.isr.Finance.Sep.Ira;
```

**Option C: If in different namespace**
```csharp
// Add using:
using cc.isr.Finance.Sep.Ira.Calculators;

// Update to:
var inputs = new AppreciatorInputs { ... };
```

---

### XLS0413: Property 'Stroke' Not Found

**Full Error:** `The property 'Stroke' was not found in type 'Border'`

**Cause:** XAML compiler doesn't recognize Stroke property

**Investigation:**
```bash
# Verify Border is from correct namespace
# Check MAUI version is current
dotnet package search Microsoft.Maui.Controls --exact-match
```

**Resolution:**
```xml
<!-- Verify namespace at top of XAML -->
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    <!-- Border should use Stroke -->
    <Border Stroke="#FF0000" StrokeThickness="1" />
</ContentPage>
```

---

### MVVMTK Error: ObservableProperty Not Generated

**Full Error:** `Field 'xxx' needs a getter` or property not accessible

**Cause:** Missing `partial` keyword on class

**Fix:**
```csharp
// WRONG
public class AppreciatorViewModel : BaseViewModel
{
    [ObservableProperty]
    public decimal InvestedAmount { get; set; }
}

// CORRECT
public partial class AppreciatorViewModel : BaseViewModel
{
    [ObservableProperty]
    public partial decimal InvestedAmount { get; set; }
}
```

---

### Binding Failed: No Matching Binding Context

**Error:** Binding shows {Binding XYZ} instead of value

**Causes:**
1. BindingContext not set
2. Property name typo
3. Property doesn't exist on ViewModel
4. Property not public

**Debug:**
```csharp
// In code-behind
public AppreciatorPage()
{
    InitializeComponent();
    var vm = new AppreciatorViewModel();
    BindingContext = vm;

    // Verify properties exist at runtime
    Debug.WriteLine(vm.GetType().GetProperty("InvestedAmount"));
}
```

---

### Layout Issues

#### Results Don't Appear After Calculate

**Symptoms:** Calculate runs but nothing displays

**Causes & Fixes:**

1. **HasResults not set to true**
   ```csharp
   // In ViewModel.Calculate()
   HasResults = true;  // Make sure this happens
   ```

2. **Result Border IsVisible binding wrong**
   ```xaml
   <!-- CORRECT -->
   <Border IsVisible="{Binding HasResults}">
       <Label Text="{Binding CalculationResults}" />
   </Border>
   ```

3. **CalculationResults is null**
   ```csharp
   // Add debug check
   Debug.WriteLine($"Results: {CalculationResults}");
   if (CalculationResults == null)
       ErrorMessage = "Calculation failed silently";
   ```

---

#### ActivityIndicator Doesn't Animate

**Symptoms:** Loading indicator visible but not animating

**Causes & Fixes:**

1. **IsRunning not bound**
   ```xaml
   <!-- CORRECT -->
   <ActivityIndicator IsRunning="{Binding IsCalculating}" />
   ```

2. **IsCalculating never set to true**
   ```csharp
   // In Calculate() method
   IsCalculating = true;  // Set before calc
   // ... calculation ...
   IsCalculating = false; // Clear after
   ```

3. **Different Grid row z-order issue**
   ```xaml
   <!-- Ensure Grid.Row is clear -->
   <Grid Grid.Row="2" IsVisible="{Binding IsCalculating}">
       <ActivityIndicator ... />
   </Grid>
   ```

---

#### Entry Fields Show But Input Doesn't Work

**Symptoms:** Can't type in Entry controls

**Causes & Fixes:**

1. **Entry disabled by IsEnabled binding**
   ```xaml
   <!-- Check if entry has IsEnabled binding -->
   <!-- Remove if not needed or fix binding -->
   ```

2. **Keyboard type wrong**
   ```xaml
   <!-- For numeric fields, MUST use Numeric keyboard -->
   <Entry Keyboard="Numeric" ... />
   ```

3. **Text binding syntax error**
   ```xaml
   <!-- CORRECT -->
   <Entry Text="{Binding InvestedAmount}" />

   <!-- WRONG - missing binding marker -->
   <Entry Text="InvestedAmount" />
   ```

---

### Runtime Errors

#### NullReferenceException on Calculate

**Error:** `Object reference not set to an instance of an object`

**Likely Cause:** AppreciatorInputs instantiation fails

**Fix:**
```csharp
// Add null checks
try
{
    var appreciator = new Appreciator(inputs);
    if (appreciator == null)
        throw new InvalidOperationException("Appreciator creation failed");

    var results = appreciator.Calculate();
    if (results == null)
        ErrorMessage = "Calculation returned null";
}
catch (Exception ex)
{
    ErrorMessage = $"Error: {ex.GetBaseException().Message}";
}
```

---

#### InvalidOperationException: "Cross-thread Operation"

**Error:** `Invalid cross-thread access`

**Cause:** UI property updated from background thread

**Fix:** Ensure UI updates use MainThread marshalling
```csharp
// WRONG
await Task.Run(() =>
{
    var results = appreciator.Calculate();
    CalculationResults = results;  // ❌ Wrong thread
});

// CORRECT
await Task.Run(() =>
{
    var results = appreciator.Calculate();
    MainThread.BeginInvokeOnMainThread(() =>
    {
        CalculationResults = results;  // ✅ UI thread
    });
});
```

---

#### TaskCanceledException

**Error:** `A task was canceled`

**Cause:** CancellationToken signaled

**Fix:** Handle gracefully
```csharp
catch (OperationCanceledException)
{
    // Expected when user clicks Cancel
    ErrorMessage = "Calculation cancelled by user";
}
```

---

### Theme & Styling Issues

#### Controls Invisible in Dark Mode

**Symptoms:** Can't see text or controls

**Causes:**
1. TextColor not bound to theme
2. BackgroundColor too dark
3. Border stroke not visible

**Fix:**
```xaml
<!-- Add theme bindings -->
<Label TextColor="{AppThemeBinding Light=#000000, Dark=#FFFFFF}" />
<Border Stroke="{AppThemeBinding Light=#000000, Dark=#FFFFFF}" 
        BackgroundColor="{AppThemeBinding Light=#FFFFFF, Dark=#2A2A2A}" />
```

---

#### Borders Not Showing

**Symptoms:** Border elements invisible

**Causes:**
1. StrokeThickness = 0 (default might be thin)
2. Stroke color = background color
3. Border empty (no children to define size)

**Fix:**
```xaml
<!-- Explicit thickness and color -->
<Border Stroke="#FF0000"
        StrokeThickness="2"
        BackgroundColor="#F0F0F0"
        Padding="10">
    <Label Text="Content must exist" />
</Border>
```

---

### Performance Issues

#### Calculation Takes Too Long

**Symptoms:** Freezes for several seconds

**Causes:**
1. Large date ranges
2. Inefficient calculation algorithm
3. UI not responsive during calculation

**Check:**
```csharp
// Measure calculation time
var stopwatch = Stopwatch.StartNew();
var results = appreciator.Calculate();
stopwatch.Stop();
Debug.WriteLine($"Calculation took {stopwatch.ElapsedMilliseconds}ms");
```

**Expected:** <500ms for typical inputs

---

#### UI Becomes Unresponsive

**Symptoms:** Can't click buttons during calculation

**Cause:** Calculation on UI thread

**Verification:**
```csharp
// Ensure this is in Task.Run()
await Task.Run(() =>
{
    // CPU-intensive work here
    var results = appreciator.Calculate();
});
```

---

## Integration Testing Checklist

### Before Reporting a Bug

- [ ] Clean solution and rebuild
- [ ] Run on actual device/emulator (not just simulation)
- [ ] Clear app cache and data
- [ ] Restart device
- [ ] Test with default input values
- [ ] Check debug output for exceptions
- [ ] Review error message carefully
- [ ] Try in Release build (not just Debug)

### Information to Collect

When reporting issues, gather:
1. **Exception Details**
   ```
   Exception Type:
   Exception Message:
   Stack Trace:
   ```

2. **Environment**
   ```
   .NET Version:
   MAUI Version:
   Target Platform (Windows/iOS/Android):
   OS Version:
   ```

3. **Steps to Reproduce**
   ```
   1. Enter values: ...
   2. Click: ...
   3. Expected: ...
   4. Actual: ...
   ```

---

## Debug Techniques

### Enable Verbose Logging

```csharp
// In MauiProgram.cs
builder.Services.AddLogging(logging =>
{
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Debug);
});
```

### Inspect ViewModel Properties

```csharp
// In page code-behind
public partial class AppreciatorPage : ContentPage
{
    public AppreciatorPage()
    {
        InitializeComponent();
        var vm = new AppreciatorViewModel();
        BindingContext = vm;

        #if DEBUG
        this.Loaded += (s, e) =>
        {
            Debug.WriteLine($"InvestedAmount: {vm.InvestedAmount}");
            Debug.WriteLine($"InitialAge: {vm.InitialAge}");
            // ... check all properties
        };
        #endif
    }
}
```

### Test Bindings

```xaml
<!-- Temporarily add Debug.WriteLine equivalent -->
<Label Text="{Binding InvestedAmount, StringFormat='Amount: {0}'}" />
```

---

## Common Workarounds

### "Converter Not Found" Error

**Temporary Fix (Not Recommended):**
```xaml
<!-- Remove converter temporarily -->
<Border IsVisible="True">  <!-- Hard-code instead of binding -->
    ...
</Border>
```

**Proper Fix:**
Register in App.xaml:
```xaml
<ResourceDictionary>
    <local:InvertedBoolConverter x:Key="InvertedBoolConverter" />
    <local:StringNotNullOrEmptyBoolConverter x:Key="StringNotNullOrEmptyBoolConverter" />
</ResourceDictionary>
```

---

### "Cannot Find BindingContext"

**Temporary Fix:**
```csharp
// In code-behind
BindingContext = new AppreciatorViewModel();
```

**Verify It's Set:**
```csharp
if (BindingContext == null)
    Debug.WriteLine("ERROR: BindingContext is null!");
```

---

## Advanced Diagnostics

### Enable XAML Compilation Diagnostics

```bash
# In .csproj file
<PropertyGroup>
    <XamlDebugOutput>True</XamlDebugOutput>
</PropertyGroup>
```

### Capture Network/HTTP Issues

If calculator requires external resources:
```csharp
// Add handler for HTTP errors
HttpClient.DefaultRequestHeaders.Add("User-Agent", "SEP-IRA-Calculator");
```

### Memory Leak Detection

```csharp
// Check for proper disposal
public partial class AppreciatorViewModel : BaseViewModel, IDisposable
{
    public void Dispose()
    {
        _cancellationTokenSource?.Dispose();
    }
}
```

---

## Support Resources

- **MAUI Documentation:** https://learn.microsoft.com/dotnet/maui
- **MVVM Toolkit Docs:** https://learn.microsoft.com/windows/communitytoolkit/mvvm/
- **xUnit Testing:** https://xunit.net/
- **GitHub Issues:** Check existing issues in repository
- **Stack Overflow:** Tag: [maui] [csharp] [xaml]

---

## Emergency Fallback Plan

If MAUI integration fails completely:

1. **Revert to WinForms App**
   - Use `src\sep.ira\calculator.app\` project
   - Proven, working implementation
   - No XAML compilation issues

2. **Console App Testing**
   - Use `src\sep.ira\calculator.console\` project
   - Verify calculator logic works
   - Then debug MAUI integration

3. **Minimal Reproduction**
   - Create new MAUI project from scratch
   - Add one control at a time
   - Identify exact breaking point

---

**Last Updated:** 2026-01-18  
**Accuracy:** High (based on common MAUI & .NET issues)  
**Maintenance:** Should be updated as new issues discovered
