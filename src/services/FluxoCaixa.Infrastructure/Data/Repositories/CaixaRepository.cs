using FluxoCaixa.Core.Data;
using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Infrastructure.Data.Context.FluxoCaixa;

namespace FluxoCaixa.Infrastructure.Data.Repositories;
public class CaixaRepository : ICaixaRepository
{
	private readonly FluxoCaixaContext _context;

	public CaixaRepository(FluxoCaixaContext context)
		=> _context = context;

	public IUnitOfWork UnitOfWork
			=> _context;

	public async Task AtualizarCaixa(Caixa caixa)
	{
		await _context.Lancamentos.AddRangeAsync(caixa.Lancamentos);
		_context.Caixas.Update(caixa);
	}
}
