using FluxoCaixa.Core.DomainObjects;
using FluxoCaixa.Core.Exceptions;

namespace FluxoCaixa.Domain.Aggregates.CaixaAggregation;
public class Caixa : Entity, IAggregateRoot
{
	public decimal Saldo { get; set; }
	public ICollection<Lancamento> Lancamentos { get; set; }
	public Loja Loja { get; set; }

	private Caixa() { }

	public Caixa(Loja loja)
	{
		Loja = loja;

		ValidarConstrucao();
	}

	public void AdicionarLancamento(Lancamento lancamento)
	{
		ProcessarLancamento(lancamento);

		if (Lancamentos is null)
			Lancamentos = new List<Lancamento>();

		lancamento.AssociarAoCaixa(this);
		Lancamentos.Add(lancamento);
	}

	private void ProcessarLancamento(Lancamento lancamento)
	{
		switch (lancamento.TipoLancamento)
		{
			case TipoLancamento.Debito:
				DebitarValor(lancamento.Valor);
				break;
			case TipoLancamento.Credito:
				CreditarValor(lancamento.Valor);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(lancamento.TipoLancamento), "Operação de caixa não prevista ou inválida.");
		}
	}

	private void DebitarValor(decimal valor)
	{
		if ((Saldo - valor) < 0)
		{
			throw new DomainException("Não existe valor suficiente no saldo do caixa para registrar este débido.");
		}

		Saldo -= valor;
	}

	private void CreditarValor(decimal valor)
		=> Saldo += valor;

	private void ValidarConstrucao()
		=> Validations.NullThrowsException(Loja, "A propriedade 'Loja' deve conter um valor válido.");
}
