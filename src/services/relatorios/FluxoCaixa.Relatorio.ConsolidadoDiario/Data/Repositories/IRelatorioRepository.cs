using FluxoCaixa.Domain.Aggregates.RelatorioAggregation;
using FluxoCaixa.Relatorio.ConsolidadoDiario.Models;

namespace FluxoCaixa.Relatorio.ConsolidadoDiario.Data.Repositories;

public interface IRelatorioRepository
{
	Task<RelatorioConsolidadoDiario> ObterRelatorioConsolidadoDiario(DateOnly data);

	Task AlterarStatusRelatorio(Guid idRelatorio, RelatorioStatus status);
}
