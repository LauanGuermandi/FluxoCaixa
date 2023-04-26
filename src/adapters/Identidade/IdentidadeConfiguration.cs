using System.Text;
using FluxoCaixa.Core.Exceptions;
using FluxoCaixa.Domain.Models.Identidade;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identidade;

public static class IdentidadeConfiguration
{
	public static IServiceCollection AddIdentidadeConfiguration<T>(this IServiceCollection services, IdentitySettings identitySettings) where T : IdentityDbContext
	{
		services.AddDefaultIdentity<IdentityUser>()
			.AddRoles<IdentityRole>()
			.AddErrorDescriber<MessagesExtension>()
			.AddEntityFrameworkStores<T>()
			.AddDefaultTokenProviders();

		var identidadeChave = Environment.GetEnvironmentVariable(IdentitySettings.IdentidadeChaveVariable);
		if (string.IsNullOrEmpty(identidadeChave))
			throw new RequiredConfigurationException("Erro ao obter chave referente ao contexto de identidade.");

		var key = Encoding.ASCII.GetBytes(identidadeChave);

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(bearerOptions =>
		{
			bearerOptions.SaveToken = true;
			bearerOptions.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidAudience = identitySettings.ValidoEm,
				ValidIssuer = identitySettings.Emissor
			};
		});

		return services;
	}

	public static void UseCustomAuthentication(this IApplicationBuilder app)
	{
		app.UseAuthentication();
		app.UseAuthorization();
	}
}
