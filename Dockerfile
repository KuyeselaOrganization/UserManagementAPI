# Use official .NET runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Use official .NET SDK as build environment
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project file and restore dependencies
COPY ["UserManagementAPI.csproj", "./"]
RUN dotnet restore

# Copy everything else and build the application
COPY . .

# ✅ Ensure appsettings.json and appsettings.Development.json exist
RUN test -f appsettings.json || cp appsettings.json.example appsettings.json
RUN test -f appsettings.Development.json || cp appsettings.Development.json.example appsettings.Development.json

# Build the application
RUN dotnet publish -c Release -o /app/publish

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# ✅ Ensure configuration files exist at runtime
RUN test -f appsettings.json || cp appsettings.json.example appsettings.json
RUN test -f appsettings.Development.json || cp appsettings.Development.json.example appsettings.Development.json

ENTRYPOINT ["dotnet", "UserManagementAPI.dll"]
