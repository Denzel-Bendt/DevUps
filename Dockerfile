# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Pacman _V2.sln"
RUN dotnet build "Pacman _V2.sln" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "../Pacman _V2/Pacman _V2.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pacman _V2.dll"]
