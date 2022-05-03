#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IHonesto.Scraper/IHonesto.Scraper.csproj", "IHonesto.Scraper/"]
RUN dotnet restore "IHonesto.Scraper/IHonesto.Scraper.csproj"
COPY . .
WORKDIR "/src/IHonesto.Scraper"
RUN dotnet build "IHonesto.Scraper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IHonesto.Scraper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IHonesto.Scraper.dll"]