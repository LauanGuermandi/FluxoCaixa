using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Infrastructure.Data.Comparers;
using FluxoCaixa.Infrastructure.Data.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Infrastructure.Data.Mappings;
public class LancamentoMapping : IEntityTypeConfiguration<Lancamento>
{
	public void Configure(EntityTypeBuilder<Lancamento> builder)
	{
		builder.ToTable("LANCAMENTOS");

		builder.HasKey(x => x.Id);

		builder.HasIndex(p => p.DataLancamento);
		builder.HasIndex(p => p.TipoLancamento);

		builder.Property(x => x.Valor)
			.HasColumnType("money")
			.IsRequired();

		builder.Property(x => x.TipoLancamento)
			.HasColumnType("tinyint")
			.HasConversion<int>()
			.IsRequired();

		builder.Property(x => x.DataLancamento)
			.HasColumnName("DATALANCAMENTO")
			.HasConversion<DateOnlyConverter, DateOnlyComparer>()
			.HasColumnType("date")
			.IsRequired();

		builder.Property(x => x.HoraLancamento)
			.HasColumnName("HORALANCAMENTO")
			.HasConversion<TimeOnlyConverter, TimeOnlyComparer>()
			.HasColumnType("time")
			.IsRequired();

		builder.HasOne(x => x.Caixa)
			.WithMany(x => x.Lancamentos)
			.HasForeignKey(x => x.IdCaixa);
	}
}
