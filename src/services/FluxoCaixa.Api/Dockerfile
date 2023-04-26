FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/FluxoCaixa.Api/FluxoCaixa.Api.csproj", "src/services/FluxoCaixa.Api/"]
COPY ["src/adapters/Identidade/Identidade.csproj", "src/adapters/Identidade/"]
COPY ["src/building-blocks/FluxoCaixa.Core/FluxoCaixa.Core.csproj", "src/building-blocks/FluxoCaixa.Core/"]
COPY ["src/services/FluxoCaixa.Domain/FluxoCaixa.Domain.csproj", "src/services/FluxoCaixa.Domain/"]
COPY ["src/adapters/Logging/Logging.csproj", "src/adapters/Logging/"]
COPY ["src/adapters/MessageBus/MessageBus.csproj", "src/adapters/MessageBus/"]
COPY ["src/services/FluxoCaixa.Infrastructure/FluxoCaixa.Infrastructure.csproj", "src/services/FluxoCaixa.Infrastructure/"]
RUN dotnet restore "src/services/FluxoCaixa.Api/FluxoCaixa.Api.csproj"
COPY . .
WORKDIR "/src/src/services/FluxoCaixa.Api"
RUN dotnet build "FluxoCaixa.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluxoCaixa.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluxoCaixa.Api.dll"]