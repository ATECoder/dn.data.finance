# SEP IRA Calculator - Build Output Optimization Plan
## English Localization & NuGet Package Cleanup

**Objective:** Reduce compiled binary size by removing non-English resources and unnecessary NuGet packages to ship only what's needed  
**Target:** Windows-only build (MAUI app)  
**Implementation:** Visual Studio Project Editor + Package Manager Console

---

## Understanding the Problem

### What's Currently Included:
The MAUI project pulls in packages for **all platforms** (Android, iOS, macOS, Windows), even though only the Windows build is needed. This results in:
- **Android** satellite assemblies and Xamarin.AndroidX packages (~30-50 MB)
- **iOS** platform-specific packages (~20-30 MB)
- **macCatalyst** platform-specific packages (~10-15 MB)
- **Non-English** satellite resource assemblies (~15-20 MB)

### Solution Strategy:
1. Configure the project for **Windows-only** targeting
2. Set **English (en) as the only language** for satellite assemblies
3. Remove **all unused NuGet package references** from the .csproj file
4. This ensures the Release build only includes Windows + English resources

---

## Phase 1: Target Framework Configuration (Highest Impact)

**File:** `src\sep.ira\calculator.maui\SepIraCalculatorMauiApp.csproj`

### Step 1A: Change from Multi-Platform to Windows-Only

Find this section in the .csproj file:
```xml
<TargetFrameworks>net10.0-android;net10.0-ios;net10.0-maccatalyst;net10.0-windows10.0.19041.0</TargetFrameworks>
```

Replace with:
```xml
<TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>
```

**Impact:** Removes all non-Windows platform dependencies (~60-80 MB)

### Step 1B: Set English as Neutral Language

In the main `<PropertyGroup>`, add:
```xml
<NeutralLanguage>en</NeutralLanguage>
```

This prevents the build system from creating satellite assemblies for other languages.

**Impact:** Removes non-English resource assemblies (~15-20 MB)

---

## Phase 2: Target Framework Configuration (Highest Impact)

### Step 1A: Configure MAUI App for English Only

**File:** `src\sep.ira\calculator.maui\SepIraCalculatorMauiApp.csproj`

Add to the first `<PropertyGroup>`:
```xml
<SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows'">10.0.19041.0</SupportedOSPlatformVersion>
<UseMaui>true</UseMaui>
<SingleProject>true</SingleProject>

<!-- English-only localization -->
<NeutralLanguage>en</NeutralLanguage>
```

**Impact:** Removes satellite assemblies for all languages except English (~15-20 MB reduction)

### Step 1B: Remove Platform-Specific Localizations

**File:** `src\sep.ira\calculator.maui\Platforms\Android\AndroidManifest.xml`

Verify the `android:label` is set to English:
```xml
<application android:label="@string/app_name" ...>
```

**File:** `src\sep.ira\calculator.maui\Platforms\iOS\Info.plist`

Verify `CFBundleDisplayName` and `CFBundleName` are in English (no localized variants).

**File:** `src\sep.ira\calculator.maui\Platforms\Windows\Package.appxmanifest`

Check that only English language is declared:
```xml
<Resource Language="en-us" />
```

### Step 1C: Disable Non-Windows Platforms (Optional - High Impact)

**File:** `src\sep.ira\calculator.maui\SepIraCalculatorMauiApp.csproj`

Current TargetFramework (if multi-platform):
```xml
<TargetFrameworks>net10.0-android;net10.0-ios;net10.0-maccatalyst;net10.0-windows10.0.19041.0</TargetFrameworks>
```

**Recommendation for Windows-only:**
```xml
<TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>
```

**Impact:** Eliminates Android, iOS, macOS dependencies (~30-40 MB reduction)  
**Decision:** Keep only if targeting Windows exclusively.

---

## Phase 2: NuGet Package Audit & Cleanup

### Step 2A: Inventory Current NuGet Dependencies

**MAUI Project** (`SepIraCalculatorMauiApp.csproj`):
Run in Visual Studio Package Manager Console:
```powershell
Get-Project "SepIraCalculatorMauiApp" | Get-Package
```

**Expected Dependencies (MAUI):**
```
CommunityToolkit.Mvvm        [KEEP]  - ViewModel framework
Microsoft.Maui              [KEEP]  - Core MAUI framework
Microsoft.Maui.Controls     [KEEP]  - MAUI controls
Microsoft.Maui.Essentials   [KEEP]  - Platform APIs (GPS, battery, etc.)
```

**Unnecessary Packages to Remove:**
- Any preview/pre-release versions
- Duplicate package references
- Transitive dependencies no longer needed

### Step 2B: Calculator Project Dependencies

**Calculator Project** (`cc.isr.Finance.Sep.Ira.Calculator.csproj`):
```powershell
Get-Project "cc.isr.Finance.Sep.Ira.Calculator" | Get-Package
```

**Expected:** Should be minimal (likely no external packages)  
**Action:** Remove any unused dependencies

### Step 2C: Forms Project Dependencies (if included)

**Forms Project** (`SepIraCalculatorFormsApp.csproj`):
```powershell
Get-Project "SepIraCalculatorFormsApp" | Get-Package
```

**Recommended Cleanup:**
- Remove obsolete Xamarin.Forms packages if migrating to MAUI
- Remove redundant platform-specific packages

---

## Phase 3: Resource File Optimization

### Step 3A: Remove Unnecessary Fonts

**Current Fonts in** `src\sep.ira\calculator.maui\Resources\Fonts\`:
- `Cousine-Bold.ttf` (~100 KB) - *Font variant*
- `Cousine-BoldItalic.ttf` (~100 KB) - *Font variant*
- `Cousine-Italic.ttf` (~100 KB) - *Font variant*
- `Cousine-Regular.ttf` (~100 KB) - *Base font*
- `OFL.txt` (~3 KB) - *License file*
- `OpenSans-Regular.ttf` (~60 KB) - *Keep for UI text*
- `OpenSans-Semibold.ttf` (~60 KB) - *Keep for UI text*

**Action Required:**
1. Review `AppreciatorPage.xaml` for font usage
2. Check if `Cousine-*` variants are used (search: `FontFamily="Cousine"`)
3. If only `Cousine-Regular` is used, delete other variants
4. If `Cousine` font is only for display purposes, consider using only OpenSans

**Potential Impact:** ~300 KB reduction if all Cousine variants removed

### Step 3B: Remove Sample Images

**Current Images in** `src\sep.ira\calculator.maui\Resources\Images\`:
- (Search for files with `dotnet_bot` or sample image names)

**Action:** Delete any sample/placeholder images not used in the application

### Step 3C: Clean Up App Icons

**Current Icons in** `src\sep.ira\calculator.maui\Resources\AppIcon\`:
- `appicon.svg` (~2 KB) - *Primary icon*
- `appiconfg.svg` (~2 KB) - *Likely duplicate or foreground*

**Action:** Keep only the primary `appicon.svg`; delete `appiconfg.svg` if it's a duplicate

**Potential Impact:** ~2 KB reduction

### Step 3D: Splash Screen

**Current Splash in** `src\sep.ira\calculator.maui\Resources\Splash\`:
- `splash.svg` (~5 KB)

**Decision:** 
- Keep if showing on app startup
- Delete if startup splash is disabled or minimal

---

## Phase 4: Platform-Specific Cleanup

### Step 4A: Android Platform (Optional)

If not targeting Android, delete entire folder:
```
src\sep.ira\calculator.maui\Platforms\Android\
```

**Impact:** Removes all Android-specific resources (~10-15 MB)

### Step 4B: iOS Platform (Optional)

If not targeting iOS, delete entire folder:
```
src\sep.ira\calculator.maui\Platforms\iOS\
```

**Impact:** Removes all iOS-specific resources (~5 MB)

### Step 4C: macCatalyst Platform (Optional)

If not targeting macCatalyst, delete entire folder:
```
src\sep.ira\calculator.maui\Platforms\MacCatalyst\
```

**Impact:** Removes all macCatalyst-specific resources (~5 MB)

---

## Phase 5: Implementation Sequence (Visual Studio)

### For Minimal Changes (Keep all platforms):

1. **Edit** `SepIraCalculatorMauiApp.csproj`
   - Add `<NeutralLanguage>en</NeutralLanguage>`

2. **Audit & Remove NuGet packages:**
   - Right-click project → Manage NuGet Packages
   - Remove any preview/unnecessary versions
   - Keep only essential packages

3. **Delete unused resources:**
   - Remove Cousine font variants if only Regular is used
   - Remove sample images (if any)
   - Remove duplicate app icon `appiconfg.svg`

4. **Rebuild and test:**
   ```
   Build → Rebuild Solution
   ```

### For Maximum Optimization (Windows-only):

1. **Edit** `SepIraCalculatorMauiApp.csproj`
   ```xml
   <!-- Replace TargetFrameworks with: -->
   <TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>
   <NeutralLanguage>en</NeutralLanguage>
   ```

2. **Delete platform folders** (in Solution Explorer):
   - Right-click `Platforms\Android` → Delete
   - Right-click `Platforms\iOS` → Delete
   - Right-click `Platforms\MacCatalyst` → Delete

3. **Update** `MauiProgram.cs` - Remove platform-specific initialization if any

4. **Clean & Rebuild:**
   ```
   Build → Clean Solution
   Build → Rebuild Solution
   ```

---

## Estimated Size Reduction

| Optimization | Estimated Reduction | Difficulty |
|--------------|-------------------|-----------|
| English-only localization | 15-20 MB | Easy |
| Remove Cousine fonts | 300-400 KB | Easy |
| Remove duplicate icon | 2 KB | Easy |
| Remove sample images | 50+ KB | Easy |
| Remove non-Windows platforms | 30-40 MB | Medium |
| **TOTAL (All optimizations)** | **45-60 MB** | - |

---

## Package Manager Console Commands

### List all packages:
```powershell
Get-Project "SepIraCalculatorMauiApp" | Get-Package | Sort-Object Id
```

### Remove a package:
```powershell
Uninstall-Package "PackageName" -Project "SepIraCalculatorMauiApp"
```

### Update all packages to latest:
```powershell
Update-Package -Project "SepIraCalculatorMauiApp"
```

---

## Verification Checklist

After applying optimizations:

- [ ] Solution builds without errors
- [ ] MAUI app launches and functions correctly
- [ ] All UI controls render properly
- [ ] Fonts display correctly (especially Courier in results)
- [ ] No missing resource errors in Output window
- [ ] App runs with expected performance
- [ ] No runtime exceptions for localization

---

## Rollback Instructions

If any optimization causes issues:

1. **Undo platform deletion:**
   - Right-click project → Add Existing Folder
   - Navigate to version control backup

2. **Restore packages:**
   ```powershell
   Update-Package -Reinstall -Project "SepIraCalculatorMauiApp"
   ```

3. **Restore files:**
   - Use Visual Studio's Undo or Git restore

---

## Next Steps

1. **Execute Phase 1:** Add English-only localization
2. **Execute Phase 2:** Audit and remove unnecessary NuGet packages
3. **Execute Phase 3:** Remove redundant font variants and images
4. **Execute Phase 4 (Optional):** Remove non-Windows platforms if Windows-only
5. **Test thoroughly** to ensure no functionality loss
6. **Measure output size** before and after

---

**Estimated time to implement: 30-45 minutes**  
**Risk level: Low (all changes are reversible)**
