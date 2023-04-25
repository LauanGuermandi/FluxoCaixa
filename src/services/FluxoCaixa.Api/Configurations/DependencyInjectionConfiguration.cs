using FluxoCaixa.Api.Services;
using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Domain.Aggregates.LojaAggregation;
using FluxoCaixa.Domain.Services;
using FluxoCaixa.Infrastructure.Data.Repositories;
using Identidade.Services;

namespace FluxoCaixa.Api.Configurations;

public static class DependencyInjectionConfiguration
{
	public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
	{
		services.AddScoped<IIdentidadeService, IdentidadeService>();
		services.AddScoped<ILancamentoService, LancamentoService>();

		services.AddScoped<IUsuarioRepository, UsuarioRepository>();
		services.AddScoped<ICaixaRepository, CaixaRepository>();
	}
}
