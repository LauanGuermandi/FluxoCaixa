using FluxoCaixa.Domain.Aggregates.UsuarioAggregation;
using FluxoCaixa.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Infrastructure.Data.Mappings;
public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
	public void Configure(EntityTypeBuilder<Usuario> builder)
	{
		builder.ToTable("USUARIOS");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Nome)
			.HasColumnType("varchar(255)")
			.IsRequired();

		builder.Property(x => x.Email)
			.HasColumnName("EMAIL")
			.HasConversion(x => x.Valor, o => new Email(o))
			.HasColumnType("varchar(255)")
			.IsRequired();
	}
}
