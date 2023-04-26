using FluxoCaixa.Domain.Aggregates.RelatorioAggregation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Infrastructure.Data.Mappings;

public class RelatorioMapping : IEntityTypeConfiguration<Relatorio>
{
	public void Configure(EntityTypeBuilder<Relatorio> builder)
	{
		builder.ToTable("RELATORIOS");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasColumnName("ID");

		builder.Property(x => x.Status)
			.HasColumnName("STATUS")
			.HasColumnType("tinyint")
			.IsRequired();

		builder.Property(x => x.CaminhoArquivo)
			.HasColumnType("varchar(max)");

		builder.HasOne(x => x.Metadados)
			.WithOne(x => x.Relatorio)
			.HasForeignKey<RelatorioMetadados>(x => x.IdRelatorio);
	}
}
