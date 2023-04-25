namespace FluxoCaixa.Domain.Models.Identidade;
public class IdentitySettings
{
	public const string IdentidadeChaveVariable = "IDENTIDADE_CHAVE";

	public int ExpiracaoHoras { get; set; } = 24;
	public string Emissor { get; set; }
	public string ValidoEm { get; set; }
}
