# Option 1: Use .NET serve (Easiest)

## Install .NET serve globally (one-time)
```
dotnet tool install -g dotnet-serve
```

### Output:
```
You can invoke the tool using the following command: dotnet-serve
Tool 'dotnet-serve' (version '1.10.194') was successfully installed.
```

## Do a complete clean

```
cd C:\my\lib\vs\data\finance\src\sep.ira\calculator.wasm

# Remove cached files
Remove-Item -Recurse -Force bin
Remove-Item -Recurse -Force obj

# Clean rebuild
dotnet clean
dotnet restore
dotnet publish -c Release
```

```
cd C:\my\lib\vs\data\finance\src\sep.ira\calculator.wasm
Remove-Item -Recurse -Force bin, obj
dotnet publish -c Release
```


## Compile
```
cd C:\my\lib\vs\data\finance\src\sep.ira\calculator.wasm
dotnet clean
dotnet publish -c Release
```

## Then serve your published files

### ..NET 10
```

cd C:\my\lib\vs\data\finance\src\sep.ira\calculator.wasm\bin\Release\net10.0\publish\wwwroot
dotnet-serve -p 8080
```

### ..NET 9
```

cd C:\my\lib\vs\data\finance\src\sep.ira\calculator.wasm\bin\Release\net9.0\publish\wwwroot
dotnet-serve -p 8080
```


### Output
```
Starting server, serving .
Listening on:
  http://localhost:8080
```

## Then open: http://localhost:8080

dotnet-serve -p 8080

## Report from COPILT once this works:

The tracking prevention warnings are just browser privacy notices about the Bootstrap CDN — they don't affect functionality. The important line is:

```
Index.razor initialized
```

