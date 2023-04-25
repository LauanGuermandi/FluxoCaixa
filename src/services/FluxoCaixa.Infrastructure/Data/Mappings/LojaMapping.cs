using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Infrastructure.Data.Mappings;
public class LojaMapping : IEntityTypeConfiguration<Loja>
{
	public void Configure(EntityTypeBuilder<Loja> builder)
	{
		builder.ToTable("LOJAS");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Nome)
			.HasColumnType("varchar(255)")
			.IsRequired();

		builder.Property(x => x.Cnpj)
			.HasColumnName("CNPJ")
			.HasConversion(x => x.Valor, o => new Cnpj(o))
			.HasColumnType("varchar(14)")
			.IsRequired();

		builder.HasOne(x => x.Caixa)
			.WithOne(x => x.Loja)
			.HasForeignKey<Loja>(x => x.IdCaixa);

		builder.HasMany(x => x.Usuarios)
			.WithOne(x => x.Loja)
			.HasForeignKey(x => x.IdLoja);
	}
}
