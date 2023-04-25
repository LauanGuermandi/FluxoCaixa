using FluxoCaixa.Domain.Services;
using Identidade.Services;

namespace FluxoCaixa.Api.Configurations;

public static class DependencyInjectionConfiguration
{
	public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
		=> services.AddScoped<IIdentidadeService, IdentidadeService>();
}
