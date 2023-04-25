using FluxoCaixa.Core.DomainObjects;

namespace FluxoCaixa.Domain.Aggregates.CaixaAggregation;
public class Lancamento : Entity
{
	public TipoLancamento TipoLancamento { get; set; }
	public decimal Valor { get; set; }
	public DateOnly DataLancamento { get; set; }
	public TimeOnly HoraLancamento { get; set; }

	public Caixa Caixa { get; set; }
	public Guid IdCaixa { get; set; }

	private Lancamento() { }

	public Lancamento(TipoLancamento tipoLancamento, decimal valor)
	{
		TipoLancamento = tipoLancamento;
		Valor = valor;

		AdicionarDataHora();
		ValidarConstrucao();
	}

	public void AssociarAoCaixa(Caixa caixa)
		=> Caixa = caixa;

	private void ValidarConstrucao()
	{
		Validations.NullThrowsException(TipoLancamento, "A propriedade 'TipoLancamento' deve conter um valor válido.");
		Validations.LessThanThrowsException(Valor, 0, "O valor do lançamento deve ser maior que zero(0).");
		Validations.TrueThrowsException(DataLancamento < DateOnly.FromDateTime(DateTime.Now), "A data do lancamento não pode ser anterior a data de hoje.");
		Validations.TrueThrowsException(DataLancamento > DateOnly.FromDateTime(DateTime.Now), "A data do lancamento não pode ser poterior a data de hoje.");
		Validations.NullThrowsException(HoraLancamento, "A propriedade 'HoraLancamento' deve conter um valor válido.");
	}

	private void AdicionarDataHora()
	{
		var data = DateTime.Now;
		DataLancamento = DateOnly.FromDateTime(data);
		HoraLancamento = TimeOnly.FromDateTime(data);
	}
}
