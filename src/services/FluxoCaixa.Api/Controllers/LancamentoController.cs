using FluxoCaixa.Core.Logging;
using FluxoCaixa.Core.WebApi.Controllers;
using FluxoCaixa.Domain.Dtos;
using FluxoCaixa.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Api.Controllers;

public class LancamentoController : MainController
{
	private readonly ILancamentoService _lancamentoService;
	private readonly ILoggerService<LancamentoController> _logger;

	public LancamentoController(ILancamentoService lancamentoService, ILoggerService<LancamentoController> logger)
	{
		_lancamentoService = lancamentoService;
		_logger = logger;
	}

	[HttpPost("adicionar-lancamento")]
	public async Task<IActionResult> AdicionarLancamento([FromBody] LancamentoDto lancamentoDto)
	{
		var email = GetAuthenticatedUserEmail();
		if (string.IsNullOrEmpty(email))
		{
			_logger.LogInformation("Erro ao obter dados do usuário autenticado.");
			AddErrorToStack("Erro ao obter dados do usuário autenticado.");
			return CustomResponse();
		}

		await _lancamentoService.AdicionarLancamento(email, lancamentoDto);
		return CustomResponse();
	}
}
