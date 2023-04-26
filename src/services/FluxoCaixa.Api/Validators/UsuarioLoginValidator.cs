using FluentValidation;
using FluxoCaixa.Domain.Models.Identidade;
using FluxoCaixa.Domain.ValueObjects;

namespace FluxoCaixa.Api.Validators;

public class UsuarioLoginValidator : AbstractValidator<UsuarioLogin>
{
	public UsuarioLoginValidator()
	{
		RuleFor(x => new Email(x.Email).EhValido())
			.Equal(true)
			.WithMessage("O campo Email deve conter um valor válido.");

		RuleFor(x => x.Senha)
			.NotEmpty()
			.WithMessage("O campo senha deve conter um valor válido.");
	}
}
