FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Pau.App/Pau.App.csproj", "Pau.App/"]
RUN dotnet restore "Pau.App/Pau.App.csproj"
COPY . .
WORKDIR "/src/Pau.App"
RUN dotnet build "Pau.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pau.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pau.App.dll"]
