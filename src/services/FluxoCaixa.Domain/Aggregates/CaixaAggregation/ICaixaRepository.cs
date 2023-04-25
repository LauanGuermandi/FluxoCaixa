using FluxoCaixa.Core.Data;

namespace FluxoCaixa.Domain.Aggregates.CaixaAggregation;
public interface ICaixaRepository : IRepository<Caixa>
{
	Task AtualizarCaixa(Caixa caixa);
}
