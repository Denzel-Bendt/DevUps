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
FROM ubuntu:20.04 as goss
RUN apt-get update && \
    apt-get install -y curl && \
    curl -L https://github.com/aelsabbahy/goss/releases/download/v0.3.16/goss-linux-amd64 -o /usr/local/bin/goss && \
    chmod +x /usr/local/bin/goss

FROM runtime AS test
WORKDIR /Tests  # <-- Changed: Set dedicated working directory
COPY Tests/infra-tests/goss.yaml ./  # <-- Changed: Copy to working directory
COPY --from=goss /usr/local/bin/goss /usr/local/bin/goss  # <-- Added: Ensure goss is available
RUN goss validate  # <-- Now looks for ./goss.yaml in /tests
