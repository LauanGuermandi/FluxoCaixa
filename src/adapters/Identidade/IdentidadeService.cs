using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluxoCaixa.Core.Exceptions;
using FluxoCaixa.Domain.Models.Identidade;
using FluxoCaixa.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identidade;
public class IdentidadeService : IIdentidadeService
{
	private readonly SignInManager<IdentityUser> _signInManager;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly IdentitySettings _identitySettings;

	public IdentidadeService(
		SignInManager<IdentityUser> signInManager,
		UserManager<IdentityUser> userManager,
		IOptions<IdentitySettings> identitySettings)
	{
		_signInManager = signInManager;
		_userManager = userManager;
		_identitySettings = identitySettings.Value;
	}

	public async Task<UsuarioRespostaLogin> GerarJwt(string email)
	{
		var user = await _userManager.FindByEmailAsync(email);
		var claims = await _userManager.GetClaimsAsync(user);

		var identityClaims = await ObterClaimsUsuario(claims, user);
		var encodedToken = CodificarToken(identityClaims);

		return ObterRespostaToken(encodedToken, user, claims);
	}

	public async Task<EfetuarLoginResultado> EfetuarLogin(string email, string senha)
	{
		var result = await _signInManager.PasswordSignInAsync(email, senha, false, false);

		if (result == null)
			throw new UnexpectedError("Erro inesperado ao efetuar login.");

		return new EfetuarLoginResultado()
		{
			Sucesso = result.Succeeded,
			EstaBloqueado = result.IsLockedOut
		};
	}

	private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
	{
		var userRoles = await _userManager.GetRolesAsync(user);

		claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
		claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
		claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
		claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
		claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""));

		foreach (var userRole in userRoles)
			claims.Add(new Claim(ClaimTypes.Role, userRole));

		var identityClaims = new ClaimsIdentity();
		identityClaims.AddClaims(claims);

		return identityClaims;
	}

	private string CodificarToken(ClaimsIdentity identityClaims)
	{
		var tokenHandler = new JwtSecurityTokenHandler();

		var envKey = Environment.GetEnvironmentVariable(IdentitySettings.IdentidadeChaveVariable);
		if (envKey is null)
			throw new RequiredConfigurationException($"A chave de autenticação {IdentitySettings.IdentidadeChaveVariable} deve conter um valor válido.");

		var key = Encoding.ASCII.GetBytes(envKey);
		var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
		{
			Issuer = _identitySettings.Emissor,
			Audience = _identitySettings.ValidoEm,
			Subject = identityClaims,
			Expires = DateTime.UtcNow.AddHours(_identitySettings.ExpiracaoHoras),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		});

		return tokenHandler.WriteToken(token);
	}

	private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
		=> new()
		{
			AccessToken = encodedToken,
			ExpiresIn = TimeSpan.FromHours(_identitySettings.ExpiracaoHoras).TotalSeconds,
			UsuarioToken = new UsuarioToken
			{
				Id = Guid.Parse(user.Id),
				Email = user.Email,
				Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
			}
		};

	private static long ToUnixEpochDate(DateTime date)
		=> (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}
