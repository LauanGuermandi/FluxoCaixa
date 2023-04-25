using FluxoCaixa.Domain.Aggregates.CaixaAggregation;

namespace FluxoCaixa.Domain.Dtos;
public class LancamentoDto
{
	public decimal Valor { get; set; }
	public TipoLancamento TipoLancamento { get; set; }
}
