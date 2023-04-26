using FluxoCaixa.Core.DomainObjects;
using FluxoCaixa.Domain.Aggregates.UsuarioAggregation;
using FluxoCaixa.Domain.ValueObjects;

namespace FluxoCaixa.Domain.Aggregates.CaixaAggregation;
public class Loja : Entity
{
	public string Nome { get; set; }
	public Cnpj Cnpj { get; set; }
	public ICollection<Usuario> Usuarios { get; set; }

	public Caixa Caixa { get; set; }
	public Guid IdCaixa { get; set; }

	private Loja() { }

	public Loja(string nome, string cnpj)
	{
		Nome = nome;
		Cnpj = new Cnpj(cnpj);

		ValidarConstrucao();
	}

	public void AdicionarUsuario(Usuario usuario)
	{
		if (Usuarios is null)
			Usuarios = new List<Usuario>();

		Usuarios.Add(usuario);
	}

	private void ValidarConstrucao()
	{
		Validations.EmptyThrowsException(Nome, "A propriedade 'Nome' deve conter um valor válido.");
		Validations.EmptyThrowsException(Cnpj.Valor, "A propriedade 'Cnpj' deve conter um valor válido.");
		Validations.TrueThrowsException(!Cnpj.EhValido(), "A propriedade 'Cnpj' deve conter um valor válido.");
	}
}
