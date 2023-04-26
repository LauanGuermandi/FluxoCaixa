using EasyNetQ;
using FluxoCaixa.Core.Messaging;

namespace FluxoCaixa.Domain.Dtos;

[Queue("Relatorio.ConsolidadoDiario", ExchangeName = "Relatorios")]
public class SolicitacaoRelatorioConsolidadoDiario : Event
{
	public Guid IdRelatorio { get; set; }
	public DateOnly Data { get; set; }
}
