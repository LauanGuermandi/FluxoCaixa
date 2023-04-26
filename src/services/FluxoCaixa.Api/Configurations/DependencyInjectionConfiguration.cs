using FluxoCaixa.Api.Services;
using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Domain.Aggregates.RelatorioAggregation;
using FluxoCaixa.Domain.Aggregates.UsuarioAggregation;
using FluxoCaixa.Domain.Services;
using FluxoCaixa.Infrastructure.Data.Repositories;
using Identidade;

namespace FluxoCaixa.Api.Configurations;

public static class DependencyInjectionConfiguration
{
	public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
	{
		// Services
		services.AddScoped<IIdentidadeService, IdentidadeService>();
		services.AddScoped<ILancamentoService, LancamentoService>();
		services.AddScoped<IRelatorioService, RelatorioService>();

		// Repositories
		services.AddScoped<IUsuarioRepository, UsuarioRepository>();
		services.AddScoped<ICaixaRepository, CaixaRepository>();
		services.AddScoped<IRelatorioRepository, RelatorioRepository>();
	}
}
