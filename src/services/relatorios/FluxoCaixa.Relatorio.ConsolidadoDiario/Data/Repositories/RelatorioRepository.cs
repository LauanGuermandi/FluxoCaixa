using System.Globalization;
using Dapper;
using FluxoCaixa.Domain.Aggregates.RelatorioAggregation;
using FluxoCaixa.Relatorio.ConsolidadoDiario.Data.Context;
using FluxoCaixa.Relatorio.ConsolidadoDiario.Models;

namespace FluxoCaixa.Relatorio.ConsolidadoDiario.Data.Repositories;

public class RelatorioRepository : IRelatorioRepository
{
	private readonly FluxoPagamentoContext _context;

	public RelatorioRepository(FluxoPagamentoContext context)
		=> _context = context;

	public async Task<RelatorioConsolidadoDiario> ObterRelatorioConsolidadoDiario(DateOnly data)
	{
		var query = $@" SELECT
							CASE
								WHEN TIPOLANCAMENTO = 1 THEN 'Crédito'
								ELSE 'Débito'
							END as 'TipoLancamento',
							Valor
						FROM LANCAMENTOS
						WHERE DATALANCAMENTO = '{data.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}'";

		using var connection = _context.CreateConnection();
		var lancamentos = await connection.QueryAsync<LancamentoUnico>(query);

		return new RelatorioConsolidadoDiario()
		{
			Lancamentos = lancamentos?.ToList()
		};
	}

	public async Task AlterarStatusRelatorio(Guid idRelatorio, RelatorioStatus status)
	{
		var query = $@"UPDATE RELATORIOS SET STATUS = {(int)status}";
		using var connection = _context.CreateConnection();
		await connection.QueryAsync(query);
	}

}
