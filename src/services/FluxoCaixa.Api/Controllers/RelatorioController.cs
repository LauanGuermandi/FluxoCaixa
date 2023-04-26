using AutoMapper;
using FluxoCaixa.Core.WebApi.Controllers;
using FluxoCaixa.Domain.Dtos;
using FluxoCaixa.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Api.Controllers;

public class RelatorioController : MainController
{

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

		await _relatorioService.SolicitarRelatorioConsolidadoDiario(solicitacaoRelatorioConsolidadoDiario);
		return CustomResponse();
	}
}
