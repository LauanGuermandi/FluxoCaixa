using FluxoCaixa.Core.Logging;
using FluxoCaixa.Core.WebApi.Controllers;
using FluxoCaixa.Domain.Models.Identidade;
using FluxoCaixa.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluxoCaixa.Api.Controllers;

public class IdentidadeController : MainController
{
	private readonly IIdentidadeService _identidadeService;
	private readonly ILoggerService<IdentidadeController> _logger;

	public IdentidadeController(IIdentidadeService identidadeService, ILoggerService<IdentidadeController> logger)
	{
		_identidadeService = identidadeService;
		_logger = logger;
	}

	[AllowAnonymous]
	[HttpPost("autenticar")]
	public async Task<IActionResult> EfetuarLogin([FromBody] UsuarioLogin usuarioLogin)
	{
		var resultado = await _identidadeService.EfetuarLogin(usuarioLogin.Email, usuarioLogin.Senha);
		if (resultado.Sucesso)
		{
			var usuarioRespostaLogin = await _identidadeService.GerarJwt(usuarioLogin.Email);
			return CustomResponse(usuarioRespostaLogin);
		}

		if (resultado.EstaBloqueado)
		{
			_logger.LogInformation($"Tentativa de login de usuario bloqueado: {0}", usuarioLogin.Email);
			AddErrorToStack("Usuário bloqueado.");
			return CustomResponse();
		}

		AddErrorToStack("Usuário ou senha inválidos");
		return CustomResponse();
	}
}
