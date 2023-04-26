using FluxoCaixa.Core.DomainObjects;

namespace FluxoCaixa.Domain.Aggregates.RelatorioAggregation;

public class Relatorio : Entity, IAggregateRoot
{
	public RelatorioStatus Status { get; set; } = RelatorioStatus.Pendente;
	public string CaminhoArquivo { get; set; }

	public RelatorioMetadados Metadados { get; set; }

	private Relatorio() { }

	public Relatorio(string metadados)
		=> Metadados = new RelatorioMetadados(metadados);
}
