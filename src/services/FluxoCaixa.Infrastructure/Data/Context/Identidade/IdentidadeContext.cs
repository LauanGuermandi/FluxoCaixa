using FluxoCaixa.Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Infrastructure.Data.Context.Identidade
{
	public class IdentidadeContext : IdentityDbContext
	{
		public IdentidadeContext(DbContextOptions<IdentidadeContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.UseDatabasePropertiesAsUppperCase();

			builder.Entity<IdentityUser>(entity =>
			{
				entity.ToTable(name: "USERS");
			});

			builder.Entity<IdentityRole>(entity =>
			{
				entity.ToTable(name: "ROLES");
			});
			builder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.ToTable("USER_ROLES");
			});

			builder.Entity<IdentityUserClaim<string>>(entity =>
			{
				entity.ToTable("USER_CLAIMS");
			});

			builder.Entity<IdentityUserLogin<string>>(entity =>
			{
				entity.ToTable("USER_LOGINS");
			});

			builder.Entity<IdentityRoleClaim<string>>(entity =>
			{
				entity.ToTable("ROLE_CLAIMS");

			});

			builder.Entity<IdentityUserToken<string>>(entity =>
			{
				entity.ToTable("USER_TOKENS");
			});
		}
	}
}
