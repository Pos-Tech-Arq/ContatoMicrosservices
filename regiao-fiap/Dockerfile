FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Regiao.Api/Regiao.Api.csproj", "Regiao.Api/"]
COPY ["src/Regiao.Domain/Regiao.Domain.csproj", "Regiao.Domain/"]
COPY ["src/Regiao.Infra/Regiao.Infra.csproj", "Regiao.Infra/"]
COPY ["src/Regiao.Worker/Regiao.Worker.csproj", "Regiao.Worker/"]
COPY ["src/Regiao.AntiCorruption/Regiao.AntiCorruption.csproj", "Regiao.AntiCorruption/"]

RUN dotnet restore "Regiao.Api/Regiao.Api.csproj"
RUN dotnet restore "Regiao.Domain/Regiao.Domain.csproj"
RUN dotnet restore "Regiao.Infra/Regiao.Infra.csproj"
RUN dotnet restore "Regiao.Worker/Regiao.Worker.csproj"
RUN dotnet restore "Regiao.AntiCorruption/Regiao.AntiCorruption.csproj"

COPY . .
WORKDIR "/src/src/Regiao.Api"

RUN dotnet build "Regiao.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Regiao.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Regiao.Api.dll"]