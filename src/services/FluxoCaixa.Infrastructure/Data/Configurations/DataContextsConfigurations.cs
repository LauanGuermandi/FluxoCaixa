using FluxoCaixa.Core.Exceptions;
using FluxoCaixa.Infrastructure.Data.Context.FluxoCaixa;
using FluxoCaixa.Infrastructure.Data.Context.Identidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoCaixa.Infrastructure.Data.Configurations;
public static class DataContextsConfigurations
{
	public const string IdentidadeConnectionStringVariable = "IDENTIDADE_CONNECTION_STRING";
	public const string FluxoCaixaConnectionStringVariable = "FLUXOCAIXA_CONNECTION_STRING";

	public static IServiceCollection AddIdentidadeContextConfiguration(this IServiceCollection services)
	{
		var connectionString = Environment.GetEnvironmentVariable(IdentidadeConnectionStringVariable);
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new RequiredConfigurationException($"Erro ao obter a string de conexão da variável {IdentidadeConnectionStringVariable}.");
		}

		services.AddDbContext<IdentidadeContext>(
			option => option.UseSqlServer(connectionString, options => options.EnableRetryOnFailure(3)));

		return services;
	}

	public static IServiceCollection AddFluxoCaixaContextConfiguration(this IServiceCollection services)
	{
		var connectionString = Environment.GetEnvironmentVariable(FluxoCaixaConnectionStringVariable);
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new RequiredConfigurationException($"Erro ao obter a string de conexão da variável {FluxoCaixaConnectionStringVariable}.");
		}

		services.AddDbContext<FluxoCaixaContext>(
			option => option.UseSqlServer(connectionString, options => options.EnableRetryOnFailure(3)));

		return services;
	}
}
