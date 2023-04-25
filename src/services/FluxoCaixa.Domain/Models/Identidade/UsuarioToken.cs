namespace FluxoCaixa.Domain.Models.Identidade;
public class UsuarioToken
{
	public Guid Id { get; set; }
	public string Email { get; set; }
	public IEnumerable<UsuarioClaim> Claims { get; set; }
}
