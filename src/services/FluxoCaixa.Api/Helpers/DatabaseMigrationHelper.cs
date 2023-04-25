using FluxoCaixa.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Api.Helpers;

public static class DatabaseMigrationHelpers
{
	public static async Task RunMigrations(WebApplication app)
	{
		var serviceProvider = app.Services.CreateScope().ServiceProvider;
		using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
		var dataContext = serviceScope.ServiceProvider.GetRequiredService<IdentidadeContext>();
		var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

		await dataContext.Database.MigrateAsync();

		if (dataContext.Users.Any()) return;

		await SeedUsers(userManager);
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
}
