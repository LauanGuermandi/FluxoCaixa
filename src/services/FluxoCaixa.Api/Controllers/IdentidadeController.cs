using FluxoCaixa.Core.WebApi.Controllers;
using FluxoCaixa.Domain.Models.Identidade;
using FluxoCaixa.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Api.Controllers;

public class IdentidadeController : MainController
{
	private readonly IIdentidadeService _identidadeService;

	public IdentidadeController(IIdentidadeService identidadeService)
		=> _identidadeService = identidadeService;

	[AllowAnonymous]
	[HttpPost("autenticar")]
	public async Task<IActionResult> EfetuarLogin(UsuarioLogin usuarioLogin)
	{
		var resultado = await _identidadeService.EfetuarLogin(usuarioLogin.Email, usuarioLogin.Senha);
		if (resultado.Sucesso)
		{
			var usuarioRespostaLogin = await _identidadeService.GerarJwt(usuarioLogin.Email);
			return CustomResponse(usuarioRespostaLogin);
		}

		if (resultado.EstaBloqueado)
		{
			AddErrorToStack("Usuário bloqueado.");
			return CustomResponse();
		}

		AddErrorToStack("Usuário ou senha inválidos");
		return CustomResponse();
	}
}
