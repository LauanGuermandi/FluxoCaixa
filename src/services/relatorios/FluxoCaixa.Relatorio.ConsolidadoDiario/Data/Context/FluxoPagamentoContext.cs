using System.Data;
using FluxoCaixa.Core.Exceptions;
using Microsoft.Data.SqlClient;

namespace FluxoCaixa.Relatorio.ConsolidadoDiario.Data.Context;

public class FluxoPagamentoContext
{
	public const string FluxoCaixaConnectionStringVariable = "FLUXOCAIXA_CONNECTION_STRING";

	private readonly string _connectionString;

	public FluxoPagamentoContext()
	{
		_connectionString = Environment.GetEnvironmentVariable(FluxoCaixaConnectionStringVariable);
		if (string.IsNullOrEmpty(_connectionString))
			throw new RequiredConfigurationException($"Erro ao obter a string de conexão da variável {FluxoCaixaConnectionStringVariable}.");
	}

	public IDbConnection CreateConnection()
		=> new SqlConnection(_connectionString);
}
