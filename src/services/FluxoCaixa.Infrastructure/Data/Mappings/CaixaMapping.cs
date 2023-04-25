using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Infrastructure.Data.Mappings;
public class CaixaMapping : IEntityTypeConfiguration<Caixa>
{
	public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Caixa> builder)
	{
		builder.ToTable("CAIXAS");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Saldo)
			.HasColumnType("money")
			.HasDefaultValue(0)
			.IsRequired();
	}
}
