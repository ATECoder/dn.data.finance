# 🚀 Build Windows Executable - Quick Start

## One Command to Create Shareable Windows Executable

```powershell
./build-windows-executable.ps1
```

**Result**: `SEPIraCalculator-Windows-x64.zip` (~200 MB)

---

## What You Get

✅ Self-contained executable (no .NET installation needed)  
✅ Works completely offline  
✅ Single ZIP file to share  
✅ Windows 10, 11, Server compatible  
✅ Professional distribution package  

---

## For Your Users

### Download & Run (3 simple steps)

1. **Download** the ZIP file
2. **Extract** to desired folder
   - Right-click → "Extract All"
   - Or use File Explorer
3. **Run** `Sep.Ira.Calculator.WebAssembly.exe`
   - Double-click to launch
   - App opens in browser window

**That's it!** No installation, no .NET required.

---

## File Details

- **Filename**: `SEPIraCalculator-Windows-x64.zip`
- **Size**: ~200 MB (includes .NET 10 runtime)
- **Compatibility**: Windows 10, 11, Server 2019+
- **Installation**: None - just extract and run
- **Offline**: Yes - works completely offline
- **Updates**: Create new ZIP for new version

---

## Sharing the File

### Option A: Direct File Share
```
Send the ZIP file via:
- Email (if < 25 MB limit, split if needed)
- File sharing service (Google Drive, OneDrive, Dropbox)
- USB drive
- Internal network share
```

### Option B: Cloud Storage
```
1. Upload ZIP to cloud:
   - Google Drive
   - OneDrive
   - Dropbox
   - AWS S3

2. Share the download link:
   - Users click link
   - Download starts
   - Extract and run
```

### Option C: GitHub Release
```powershell
# If you push to GitHub:
1. Create a Release
2. Attach the ZIP file
3. Users download from Releases page
```

---

## Advanced Options

| Need | Command | Result |
|------|---------|--------|
| ZIP for distribution | `./build-windows-executable.ps1` | ZIP file (~200 MB) |
| Professional installer | See: `WINDOWS_EXECUTABLE_GUIDE.md` Option 3 | Inno Setup .exe |
| Desktop app icon | See: `WINDOWS_EXECUTABLE_GUIDE.md` Option 1 | Electron app |
| Store deployment | See: `WINDOWS_EXECUTABLE_GUIDE.md` Option 4 | MSIX package |

See `WINDOWS_EXECUTABLE_GUIDE.md` for all options.

---

## Troubleshooting

### ZIP extraction fails
- Extract tool issue → Use Windows built-in "Extract All"
- Corruption → Re-download the ZIP

### EXE won't start
- Missing dependencies → Ensure .NET isn't corrupted (very rare)
- Antivirus block → Add exception to antivirus
- Wrong architecture → Ensure using `win-x64` (64-bit)

### App performance
- First run slower → Loading .NET runtime for first time
- Subsequent runs → Much faster

### Update app
- Create new ZIP with latest code
- Users extract to new folder or overwrite

---

## File Structure Users See

```
SEPIraCalculator-Windows-x64/
├── Sep.Ira.Calculator.WebAssembly.exe  ← Run this!
├── wwwroot/
│   ├── index.html
│   ├── app.css
│   └── _framework/
├── [Other runtime files]
└── dotnet.exe
```

Users only need to run: `Sep.Ira.Calculator.WebAssembly.exe`

---

## Delivery Checklist

- [ ] Run: `./build-windows-executable.ps1`
- [ ] Verify ZIP file created: `SEPIraCalculator-Windows-x64.zip`
- [ ] Test extraction locally
- [ ] Test running the EXE
- [ ] Upload to file sharing service
- [ ] Share link with users
- [ ] Provide extraction instructions (below)

---

## Share This With Users

**Send them this simple instruction:**

```
Here's the SEP IRA Calculator for Windows!

DOWNLOAD & RUN:
1. Download: SEPIraCalculator-Windows-x64.zip
2. Right-click ZIP → "Extract All"
3. Choose a folder to extract to
4. Find and double-click: Sep.Ira.Calculator.WebAssembly.exe
5. App opens in your browser - done!

NO INSTALLATION NEEDED
Just extract and run - that's it!

TIPS:
• First run takes a few seconds to start
• Works completely offline
• Create desktop shortcut if you like
```

---

## Size & Performance

- ZIP size: ~200 MB
- Extracted size: ~350-400 MB (includes .NET runtime)
- RAM usage: ~100-150 MB when running
- Startup time: ~2-5 seconds (first run), <1 second (subsequent)
- Network: None required (works offline)

---

## Keeping Users Updated

### When you release a new version:

1. **Build new executable**:
   ```powershell
   ./build-windows-executable.ps1
   ```

2. **Upload new ZIP**:
   - Replace old ZIP on file sharing service
   - Or: Create version-numbered ZIP: `SEPIraCalculator-v1.1.0-Windows-x64.zip`

3. **Notify users**:
   - Email: "New version available - download fresh ZIP"
   - Users extract to new folder
   - Run the new EXE

---

## Alternative: Professional Installer

Want an `.exe` installer that installs to Start Menu?

See: `WINDOWS_EXECUTABLE_GUIDE.md` → Option 3 (Inno Setup)

---

**Ready? Run:**
```powershell
./build-windows-executable.ps1
```

**Questions?** See `WINDOWS_EXECUTABLE_GUIDE.md` for detailed options.
