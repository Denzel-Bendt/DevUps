# Build stage (use SDK)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only project files first (better layer caching)
COPY ["Pacman _V2/Pacman _V2.csproj", "Pacman _V2/"]
COPY ["Pacman _V2.sln", "./"]
RUN dotnet restore "Pacman _V2.sln"

# Copy everything else and build
COPY . .
RUN dotnet build "Pacman _V2/Pacman _V2.csproj" -c Release --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish "Pacman _V2/Pacman _V2.csproj" -c Release -o /app/publish --no-build

# Runtime stage (use RUNTIME, not ASP.NET)
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pacman _V2.dll"]

# GOSS test stage
FROM alpine:latest AS goss
RUN apk add --no-cache curl
RUN curl -fsSL https://goss.rocks/install | sh

FROM runtime AS test
WORKDIR /tests  # <-- Changed: Set dedicated working directory
COPY Tests/infra-tests/goss.yaml ./  # <-- Changed: Copy to working directory
COPY --from=goss /usr/local/bin/goss /usr/local/bin/goss  # <-- Added: Ensure goss is available
RUN goss validate  # <-- Now looks for ./goss.yaml in /tests
