using FluxoCaixa.Domain.Models.Identidade;

namespace FluxoCaixa.Domain.Services;
public interface IIdentidadeService
{
	Task<UsuarioRespostaLogin> GerarJwt(string email);
	Task<EfetuarLoginResultado> EfetuarLogin(string email, string senha);
}
