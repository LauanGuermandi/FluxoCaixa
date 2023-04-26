using FluxoCaixa.Core.Logging;
using FluxoCaixa.Core.Messaging;
using FluxoCaixa.Domain.Dtos;
using FluxoCaixa.Domain.Services;

namespace FluxoCaixa.Relatorio.ConsolidadoDiario;

public class Worker : BackgroundService
{
	private const string SubscriptionId = "FluxoCaixa.Relatorio.ConsolidadoDiario";

	private readonly ILoggerService<Worker> _logger;
	private readonly IMessageBus _bus;
	private readonly IRelatorioConsolidadoDiarioService _relatorioConsolidadoDiarioService;

	public Worker(ILoggerService<Worker> logger, IMessageBus bus, IRelatorioConsolidadoDiarioService relatorioConsolidadoDiarioService)
	{
		_logger = logger;
		_bus = bus;
		_relatorioConsolidadoDiarioService = relatorioConsolidadoDiarioService;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Worker executando: {time}", DateTimeOffset.Now);

		await _bus.SubscribeAsync<SolicitacaoRelatorioConsolidadoDiario>(SubscriptionId, (message) =>
		{
			_relatorioConsolidadoDiarioService.GerarRelatorioConsolidadoDiario(message);
		});
	}
}
