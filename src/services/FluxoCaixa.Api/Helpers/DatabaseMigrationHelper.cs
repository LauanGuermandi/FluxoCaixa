using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Domain.Aggregates.LojaAggregation;
using FluxoCaixa.Infrastructure.Data.Context.FluxoCaixa;
using FluxoCaixa.Infrastructure.Data.Context.Identidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Api.Helpers;

public static class DatabaseMigrationHelpers
{
	public static async Task RunMigrations(WebApplication app)
	{
		var serviceProvider = app.Services.CreateScope().ServiceProvider;
		using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
		var identidadeContext = serviceScope.ServiceProvider.GetRequiredService<IdentidadeContext>();
		var fluxoCaixaContext = serviceScope.ServiceProvider.GetRequiredService<FluxoCaixaContext>();
		var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

		await identidadeContext.Database.MigrateAsync();
		await fluxoCaixaContext.Database.MigrateAsync();

		if (!identidadeContext.Users.Any())
		{
			await SeedUsers(userManager);
		}

		if (!fluxoCaixaContext.Caixas.Any())
		{
			await SeedLojaCaixa(fluxoCaixaContext);
			await fluxoCaixaContext.SaveChangesAsync();
		}
	}

	private static async Task SeedUsers(UserManager<IdentityUser> userManager)
	{
		var user = new IdentityUser()
		{
			UserName = "admingeral@desafio.com.br",
			Email = "admingeral@desafio.com.br",
			EmailConfirmed = true
		};

		await userManager.CreateAsync(user, "Desafio@123");
	}

	private static async Task SeedLojaCaixa(FluxoCaixaContext fluxoCaixaContext)
	{
		var usuario = new Usuario("Admin Geral", "admingeral@desafio.com.br");

		var loja = new Loja("Loja Teste", "17355088000167");
		loja.AdicionarUsuario(usuario);

		var caixa = new Caixa(loja);
		await fluxoCaixaContext.Caixas.AddAsync(caixa);
	}
}
