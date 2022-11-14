FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Pau.Http/Pau.Http.csproj", "Pau.Http/"]
RUN dotnet restore "Pau.Http/Pau.Http.csproj"
COPY . .
WORKDIR "/src/Pau.Http"
RUN dotnet build "Pau.Http.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pau.Http.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pau.Http.dll"]
