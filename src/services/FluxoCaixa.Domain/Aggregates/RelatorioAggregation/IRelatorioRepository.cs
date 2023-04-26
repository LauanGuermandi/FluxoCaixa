using FluxoCaixa.Core.Data;

namespace FluxoCaixa.Domain.Aggregates.RelatorioAggregation;

public interface IRelatorioRepository : IRepository<Relatorio>
{
	Task AdicionarRelatorio(Relatorio relatorio);
	Task<Relatorio> ObterRelatorioPorId(Guid idRelatorio);
}
