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
							LJ.NOME as 'Loja',
							CASE
								WHEN L.TIPOLANCAMENTO = 1 THEN 'Credito'
								ELSE 'Debito'
							END as 'TipoLancamento',
							L.VALOR,
							CAST(L.HORALANCAMENTO AS VARCHAR) as 'HoraLancamento'
						FROM LANCAMENTOS as L
						LEFT JOIN CAIXAS as C
							ON C.ID = L.IDCAIXA
						LEFT JOIN LOJAS as LJ
							ON LJ.IDCAIXA = C.ID
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

	public async Task AlterarCaminhoRelatorio(Guid idRelatorio, string caminhoArquivo)
	{
		var query = $@"UPDATE RELATORIOS SET CAMINHOARQUIVO = '{caminhoArquivo}'";
		using var connection = _context.CreateConnection();
		await connection.QueryAsync(query);
	}
}
