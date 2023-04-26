using FluxoCaixa.Domain.Aggregates.RelatorioAggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Infrastructure.Data.Mappings;

public class RelatorioMetadadosMapping : IEntityTypeConfiguration<RelatorioMetadados>
{
	public void Configure(EntityTypeBuilder<RelatorioMetadados> builder)
	{
		builder.ToTable("RELATORIO_METADADOS");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasColumnName("ID");

		builder.Property(x => x.Valor)
			.HasColumnName("VALOR")
			.HasColumnType("text")
			.IsRequired();
	}
}
