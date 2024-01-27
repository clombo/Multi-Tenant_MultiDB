FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MTA.API/MTA.API.csproj", "MTA.API/"]
RUN dotnet restore "MTA.API/MTA.API.csproj"
COPY . .
WORKDIR "/src/MTA.API"
RUN dotnet build "MTA.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MTA.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MTA.API.dll"]
