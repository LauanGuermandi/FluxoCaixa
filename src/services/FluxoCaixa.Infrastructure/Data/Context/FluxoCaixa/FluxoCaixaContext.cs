using FluxoCaixa.Core.Data;
using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Domain.Aggregates.RelatorioAggregation;
using FluxoCaixa.Domain.Aggregates.UsuarioAggregation;
using FluxoCaixa.Domain.ValueObjects;
using FluxoCaixa.Infrastructure.Data.Extensions;
using FluxoCaixa.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Infrastructure.Data.Context.FluxoCaixa;
public class FluxoCaixaContext : DbContext, IUnitOfWork
{
	public DbSet<Usuario> Usuarios { get; set; }
	public DbSet<Caixa> Caixas { get; set; }
	public DbSet<Lancamento> Lancamentos { get; set; }
	public DbSet<Relatorio> Relatorios { get; set; }

	public FluxoCaixaContext(DbContextOptions<FluxoCaixaContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Ignore<Cnpj>();
		modelBuilder.Ignore<Email>();

		modelBuilder.UseDatabasePropertiesAsUppperCase();

		modelBuilder.ApplyConfiguration(new CaixaMapping());
		modelBuilder.ApplyConfiguration(new LojaMapping());
		modelBuilder.ApplyConfiguration(new LancamentoMapping());
		modelBuilder.ApplyConfiguration(new UsuarioMapping());
		modelBuilder.ApplyConfiguration(new RelatorioMapping());
		modelBuilder.ApplyConfiguration(new RelatorioMetadadosMapping());
	}

	public async Task<bool> Commit()
		=> await base.SaveChangesAsync() > 0;
}
