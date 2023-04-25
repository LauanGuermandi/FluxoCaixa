using FluxoCaixa.Core.Exceptions;
using FluxoCaixa.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FluxoCaixa.Infrastructure.Data.Context;
public class IdentidadeContextFactory : IDesignTimeDbContextFactory<IdentidadeContext>
{
	public IdentidadeContext CreateDbContext(string[] args)
	{
		var builder = new DbContextOptionsBuilder<IdentidadeContext>();
		var connectionString = Environment.GetEnvironmentVariable(DataContextsCofigurations.IdentidadeConnectionStringVariable);
		if (string.IsNullOrEmpty(connectionString))
		{
			throw new RequiredConfigurationException($"Erro ao obter a string de conexão da variável {DataContextsCofigurations.IdentidadeConnectionStringVariable}.");
		}
		builder.UseSqlServer(connectionString);
		return new IdentidadeContext(builder.Options);
	}
}
