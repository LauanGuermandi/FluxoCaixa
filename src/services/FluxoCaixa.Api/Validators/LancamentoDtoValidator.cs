using FluentValidation;
using FluxoCaixa.Domain.Dtos;

namespace FluxoCaixa.Api.Validators;

public class LancamentoDtoValidator : AbstractValidator<LancamentoDto>
{
	public LancamentoDtoValidator()
	{
		RuleFor(x => x.TipoLancamento)
			.IsInEnum()
			.WithMessage("Tipo de lançamento inválido.")
			.NotEmpty()
			.WithMessage("o campo TipoLancamento deve conter um valor válido.");

		RuleFor(x => x.Valor)
			.GreaterThan(0)
			.WithMessage("O valor do lançamento deve ser maior que 0(zero).")
			.NotEmpty()
			.WithMessage("O campo Valor deve conter um valor válido.");
	}
}
