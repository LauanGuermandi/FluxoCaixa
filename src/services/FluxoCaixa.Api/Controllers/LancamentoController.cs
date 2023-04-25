using FluxoCaixa.Core.WebApi.Controllers;
using FluxoCaixa.Domain.Dtos;
using FluxoCaixa.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Api.Controllers;

public class LancamentoController : MainController
{
	private readonly ILancamentoService _lancamentoService;

	public LancamentoController(ILancamentoService lancamentoService)
		=> _lancamentoService = lancamentoService;

	[HttpPost("adicionar-lancamento")]
	public async Task<IActionResult> AdicionarLancamento(LancamentoDto lancamentoDto)
	{
		var email = GetAuthenticatedUserEmail();
		if (string.IsNullOrEmpty(email))
		{
			AddErrorToStack("Erro ao obter usuário autenticado.");
			return CustomResponse();
		}

		await _lancamentoService.AdicionarLancamento(email, lancamentoDto);
		return CustomResponse();
	}
}
