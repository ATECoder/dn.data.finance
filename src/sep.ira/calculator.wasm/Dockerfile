# Multi-stage build for SEP IRA Calculator WebAssembly

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files
COPY ["src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj", "src/sep.ira/calculator.wasm/"]
COPY ["src/sep.ira/calculator/cc.isr.Finance.Sep.Ira.Calculator.csproj", "src/sep.ira/calculator/"]
COPY ["src/items/keyPair.snk", "src/items/"]

# Restore dependencies
RUN dotnet restore "src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj"

# Copy source
COPY . .

# Build release
RUN dotnet build "src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj" \
    --configuration Release \
    --no-restore

# Publish
RUN dotnet publish "src/sep.ira/calculator.wasm/SepIraCalculatorWebAssembly.csproj" \
    --configuration Release \
    --no-build \
    --output /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Environment variables
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost/index.html || exit 1

# Start application
ENTRYPOINT ["dotnet", "Sep.Ira.Calculator.WebAssembly.dll"]
