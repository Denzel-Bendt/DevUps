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

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pacman _V2.dll"]

# GOSS installation stage (simplified)
FROM alpine:latest AS goss-installer
RUN apk add --no-cache curl && \
    curl -fsSL https://github.com/aelsabbahy/goss/releases/latest/download/goss-linux-amd64 -o /usr/local/bin/goss && \
    chmod +x /usr/local/bin/goss

# Test stage
FROM runtime AS test
WORKDIR /tests
# Copy goss binary first
COPY --from=goss-installer /usr/local/bin/goss /usr/local/bin/goss
# Then copy test files
COPY Tests/infra-tests/goss.yaml .
# Verify goss is available and test file exists
RUN goss --version && ls -la && \
    goss validate
