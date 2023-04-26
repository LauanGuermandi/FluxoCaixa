using FluxoCaixa.Domain.Services;
using FluxoCaixa.Relatorio.ConsolidadoDiario;
using FluxoCaixa.Relatorio.ConsolidadoDiario.Data.Context;
using FluxoCaixa.Relatorio.ConsolidadoDiario.Data.Repositories;
using FluxoCaixa.Relatorio.ConsolidadoDiario.Services;
using Logging;
using MessageBus;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
		services.AddLoggerConfiguration(ServiceLifetime.Singleton);
		services.AddMessageBusConfiguration();
		services.AddSingleton<FluxoPagamentoContext>();

		services.AddSingleton<IRelatorioConsolidadoDiarioService, RelatorioConsolidadoDiarioService>();
		services.AddSingleton<IRelatorioRepository, RelatorioRepository>();

		services.AddHostedService<Worker>();
	})
	.UseSerilog((hostingContext, services, loggerConfiguration)
		=> loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)) // Configuração de logging com o serilog
	.Build();

await host.RunAsync();
