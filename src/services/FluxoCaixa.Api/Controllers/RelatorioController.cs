using AutoMapper;
using FluxoCaixa.Core.WebApi.Controllers;
using FluxoCaixa.Domain.Aggregates.RelatorioAggregation;
using FluxoCaixa.Domain.Dtos;
using FluxoCaixa.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Api.Controllers;

public class RelatorioController : MainController
{
	private const string DefaultRelatorioMimeType = "text/csv";

	private readonly IRelatorioService _relatorioService;
	private readonly IMapper _mapper;

	public RelatorioController(IRelatorioService relatorioService, IMapper mapper)
	{
		_relatorioService = relatorioService;
		_mapper = mapper;
	}

	[HttpPost("consolidado-diario")]
	public async Task<IActionResult> SolicitarRelatorioConsolidadoDiario([FromBody] RelatorioConsolidadoDiarioDto relatorioConsolidadoDiario)
	{
		var solicitacaoRelatorioConsolidadoDiario = _mapper.Map<SolicitacaoRelatorioConsolidadoDiario>(relatorioConsolidadoDiario);
		if (solicitacaoRelatorioConsolidadoDiario is null)
		{
			solicitacaoRelatorioConsolidadoDiario = new SolicitacaoRelatorioConsolidadoDiario()
			{
				Data = DateOnly.FromDateTime(DateTime.Now)
			};
		}

		var relatorio = await _relatorioService.SolicitarRelatorioConsolidadoDiario(solicitacaoRelatorioConsolidadoDiario);
		var response = new RelatorioDto()
		{
			Id = relatorio.Id,
			Status = Enum.GetName(relatorio.Status)
		};
		return CustomResponse(response);
	}

	[HttpGet("{idRelatorio}")]
	public async Task<IActionResult> ObterRelatorio([FromRoute] Guid idRelatorio)
	{
		var relatorio = await _relatorioService.ObterRelatorio(idRelatorio);
		if (relatorio.Status != RelatorioStatus.Finalizado)
		{
			if (relatorio.Status == RelatorioStatus.Erro)
			{
				AddErrorToStack($"Erro ao processar relatório referente ao ID '{idRelatorio}'.");
				return CustomResponse();
			}

			return CustomResponse(relatorio);
		}
		else if (relatorio.Status == RelatorioStatus.Finalizado)
		{
			var arquivoBytes = ObterBytesArquivo(relatorio);
			return File(arquivoBytes, DefaultRelatorioMimeType, $"Relatorio-{idRelatorio}.csv");
		}

		return NotFound();
	}

	private static byte[] ObterBytesArquivo(Relatorio relatorio)
		=> System.IO.File.ReadAllBytes(relatorio.CaminhoArquivo);
}
