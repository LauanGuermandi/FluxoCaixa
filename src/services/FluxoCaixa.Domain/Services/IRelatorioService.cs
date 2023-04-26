using FluxoCaixa.Domain.Aggregates.RelatorioAggregation;
using FluxoCaixa.Domain.Dtos;

namespace FluxoCaixa.Domain.Services;

public interface IRelatorioService
{
	Task<Relatorio> ObterRelatorio(Guid idRelatorio);
	Task<Relatorio> SolicitarRelatorioConsolidadoDiario(SolicitacaoRelatorioConsolidadoDiario solicitacaoRelatorioConsolidadoDiario);
}
