using FluxoCaixa.Core.Exceptions;
using FluxoCaixa.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FluxoCaixa.Infrastructure.Data.Context.FluxoCaixa;
public class FluxoCaixaContextFactory : IDesignTimeDbContextFactory<FluxoCaixaContext>
{
	public FluxoCaixaContext CreateDbContext(string[] args)
	{
		var builder = new DbContextOptionsBuilder<FluxoCaixaContext>();
		var connectionString = Environment.GetEnvironmentVariable(DataContextsConfigurations.FluxoCaixaConnectionStringVariable);
		if (string.IsNullOrEmpty(connectionString))
			throw new RequiredConfigurationException($"Erro ao obter a string de conexão da variável {DataContextsConfigurations.FluxoCaixaConnectionStringVariable}.");
		builder.UseSqlServer(connectionString);
		return new FluxoCaixaContext(builder.Options);
	}
}
