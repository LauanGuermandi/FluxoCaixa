using System.Net.Mail;

namespace FluxoCaixa.Domain.ValueObjects;
public class Email
{
	protected Email()
	{
	}

	public string Valor { get; set; }

	public Email(string email)
		=> Valor = email;

	public bool EhValido()
	{
		try
		{
			_ = new MailAddress(Valor);
		}
		catch
		{
			return false;
		}

		return true;
	}
}
