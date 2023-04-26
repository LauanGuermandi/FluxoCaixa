using FluxoCaixa.Core.DomainObjects;

namespace FluxoCaixa.Domain.Aggregates.RelatorioAggregation;

public class RelatorioMetadados : Entity
{
	public string Valor { get; set; }

	public Guid IdRelatorio { get; set; }
	public Relatorio Relatorio { get; set; }

	private RelatorioMetadados() { }

	public RelatorioMetadados(string valor)
		=> Valor = valor;
}
