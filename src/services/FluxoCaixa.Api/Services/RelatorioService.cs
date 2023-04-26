using System.Text.Json;
using FluxoCaixa.Core.Converters;
using FluxoCaixa.Core.Messaging;
using FluxoCaixa.Domain.Aggregates.RelatorioAggregation;
using FluxoCaixa.Domain.Dtos;
using FluxoCaixa.Domain.Services;

namespace FluxoCaixa.Api.Services;

public class RelatorioService : IRelatorioService
{
	private readonly IRelatorioRepository _relatorioRepository;
	private readonly IMessageBus _bus;

	public RelatorioService(IRelatorioRepository relatorioRepository, IMessageBus bus)
	{
		_relatorioRepository = relatorioRepository;
		_bus = bus;
	}

	public async Task<Relatorio> SolicitarRelatorioConsolidadoDiario(SolicitacaoRelatorioConsolidadoDiario solicitacaoRelatorioConsolidadoDiario)
	{
		var relatorioMetadados = ConverterParaJson(solicitacaoRelatorioConsolidadoDiario);
		var relatorio = new Relatorio(relatorioMetadados);
		await _relatorioRepository.AdicionarRelatorio(relatorio);
		await _relatorioRepository.UnitOfWork.Commit();

		solicitacaoRelatorioConsolidadoDiario.IdRelatorio = relatorio.Id;
		await _bus.PublishAsync(solicitacaoRelatorioConsolidadoDiario);

		return relatorio;
	}

	public async Task<Relatorio> ObterRelatorio(Guid idRelatorio)
		=> await _relatorioRepository.ObterRelatorioPorId(idRelatorio);

	private static string ConverterParaJson(SolicitacaoRelatorioConsolidadoDiario solicitacaoRelatorioConsolidadoDiario)
	{
		var jsonSerializerOptions = new JsonSerializerOptions();
		jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());

		return JsonSerializer.Serialize(solicitacaoRelatorioConsolidadoDiario, jsonSerializerOptions);
	}
}
