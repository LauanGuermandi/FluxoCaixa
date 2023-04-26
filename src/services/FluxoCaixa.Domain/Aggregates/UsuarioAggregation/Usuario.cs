using FluxoCaixa.Core.DomainObjects;
using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Domain.ValueObjects;

namespace FluxoCaixa.Domain.Aggregates.UsuarioAggregation;
public class Usuario : Entity, IAggregateRoot
{
	public string Nome { get; set; }
	public Email Email { get; set; }

	public Loja Loja { get; set; }
	public Guid IdLoja { get; set; }

	private Usuario() { }

	public Usuario(string nome, string email)
	{
		Nome = nome;
		Email = new Email(email);

		ValidarConstrucao();
	}

	private void ValidarConstrucao()
	{
		Validations.EmptyThrowsException(Nome, "A propriedade 'Nome' deve conter um valor válido.");
		Validations.EmptyThrowsException(Email.Valor, "A propriedade 'Email' deve conter um valor válido.");
		Validations.TrueThrowsException(!Email.EhValido(), "A propriedade 'Email' deve conter um valor válido.");
	}
}
