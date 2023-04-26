using FluxoCaixa.Core.Data;
using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Domain.Aggregates.UsuarioAggregation;
using FluxoCaixa.Domain.ValueObjects;
using FluxoCaixa.Infrastructure.Data.Context.FluxoCaixa;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Infrastructure.Data.Repositories;
public class UsuarioRepository : IUsuarioRepository
{
	private readonly FluxoCaixaContext _context;

	public UsuarioRepository(FluxoCaixaContext context)
		=> _context = context;

	public IUnitOfWork UnitOfWork
			=> _context;

	public async Task<Caixa> ObterCaixaPorUsuarioEmail(string email)
		=> await _context.Usuarios
			.AsNoTrackingWithIdentityResolution()
			.Where(x => x.Email == new Email(email))
			.Include(x => x.Loja.Caixa)
			.Select(x => x.Loja.Caixa)
			.FirstOrDefaultAsync();
}
