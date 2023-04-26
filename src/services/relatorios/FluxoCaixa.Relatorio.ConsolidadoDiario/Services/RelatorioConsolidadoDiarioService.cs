using FluxoCaixa.Core.Logging;
using FluxoCaixa.Domain.Dtos;
using FluxoCaixa.Domain.Services;
using FluxoCaixa.Relatorio.ConsolidadoDiario.Data.Repositories;
using RelatorioAggregation = FluxoCaixa.Domain.Aggregates.RelatorioAggregation;

namespace FluxoCaixa.Relatorio.ConsolidadoDiario.Services;
public class RelatorioConsolidadoDiarioService : IRelatorioConsolidadoDiarioService
{
	private const string CaminhoRelatorios = "./Relatorios";

	private readonly IRelatorioRepository _relatorioRepository;
	private readonly ILoggerService<RelatorioConsolidadoDiarioService> _logger;


	public RelatorioConsolidadoDiarioService(IRelatorioRepository relatorioRepository, ILoggerService<RelatorioConsolidadoDiarioService> logger)
	{
		_relatorioRepository = relatorioRepository;
		_logger = logger;

		CriarDiretorioDeRelatorios();
	}

	public async Task GerarRelatorioConsolidadoDiario(SolicitacaoRelatorioConsolidadoDiario solicitacaoRelatorioConsolidadoDiario)
	{
		try
		{
			await AlterarStatusRelatorioAsync(solicitacaoRelatorioConsolidadoDiario.IdRelatorio, RelatorioAggregation.RelatorioStatus.Processando);
			_logger.LogInformation($"O relatório com ID '{solicitacaoRelatorioConsolidadoDiario.IdRelatorio}' está sendo gerado.");

			var dadosRelatorioConsolidadoDiario = await _relatorioRepository.ObterRelatorioConsolidadoDiario(solicitacaoRelatorioConsolidadoDiario.Data);

			var nomeArquivo = solicitacaoRelatorioConsolidadoDiario.Data.ToString().Replace("/", "-");

			var dadosRelatorio = dadosRelatorioConsolidadoDiario.Lancamentos
									.Select(x => $"{x.Loja},{x.TipoLancamento},{x.Valor},{x.HoraLancamento}").ToList();
			dadosRelatorio.Insert(0, "Loja,TipoLancamento,Valor");

			// Relatorios gerados no disco local para facilitar a implementação devido ao tempo.
			SalvarRelatorioEmArquivo(nomeArquivo, dadosRelatorio.ToArray());

			await AlterarStatusRelatorioAsync(solicitacaoRelatorioConsolidadoDiario.IdRelatorio, RelatorioAggregation.RelatorioStatus.Finalizado);
			_logger.LogInformation($"O relatório com ID '{solicitacaoRelatorioConsolidadoDiario.IdRelatorio}' foi finalizado.");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"Ocorreu um erro ao processar o relatório de ID '{solicitacaoRelatorioConsolidadoDiario.IdRelatorio}'.");
			await AlterarStatusRelatorioAsync(solicitacaoRelatorioConsolidadoDiario.IdRelatorio, RelatorioAggregation.RelatorioStatus.Erro);
		}
	}

	private static void CriarDiretorioDeRelatorios()
	{
		if (!Directory.Exists(CaminhoRelatorios))
			Directory.CreateDirectory(CaminhoRelatorios);
	}

	private static void SalvarRelatorioEmArquivo(string nomeArquivo, string[] dadosRelatorio)
		=> File.WriteAllLines($"{CaminhoRelatorios}/{nomeArquivo}.csv", dadosRelatorio);

	private async Task AlterarStatusRelatorioAsync(Guid idRelatorio, RelatorioAggregation.RelatorioStatus status)
		=> await _relatorioRepository.AlterarStatusRelatorio(idRelatorio, status);
}
