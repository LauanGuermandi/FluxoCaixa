FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/relatorios/FluxoCaixa.Relatorio.ConsolidadoDiario/FluxoCaixa.Relatorio.ConsolidadoDiario.csproj", "src/services/relatorios/FluxoCaixa.Relatorio.ConsolidadoDiario/"]
COPY ["src/adapters/Logging/Logging.csproj", "src/adapters/Logging/"]
COPY ["src/building-blocks/FluxoCaixa.Core/FluxoCaixa.Core.csproj", "src/building-blocks/FluxoCaixa.Core/"]
COPY ["src/adapters/MessageBus/MessageBus.csproj", "src/adapters/MessageBus/"]
COPY ["src/services/FluxoCaixa.Domain/FluxoCaixa.Domain.csproj", "src/services/FluxoCaixa.Domain/"]
RUN dotnet restore "src/services/relatorios/FluxoCaixa.Relatorio.ConsolidadoDiario/FluxoCaixa.Relatorio.ConsolidadoDiario.csproj"
COPY . .
WORKDIR "/src/src/services/relatorios/FluxoCaixa.Relatorio.ConsolidadoDiario"
RUN dotnet build "FluxoCaixa.Relatorio.ConsolidadoDiario.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoCaixa.Relatorio.ConsolidadoDiario.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixa.Relatorio.ConsolidadoDiario.dll"]