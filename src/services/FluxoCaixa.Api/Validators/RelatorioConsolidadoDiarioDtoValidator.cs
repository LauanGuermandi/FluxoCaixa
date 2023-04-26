using FluentValidation;
using FluxoCaixa.Domain.Dtos;

namespace FluxoCaixa.Api.Validators;

public class RelatorioConsolidadoDiarioDtoValidator : AbstractValidator<RelatorioConsolidadoDiarioDto>
{
	public RelatorioConsolidadoDiarioDtoValidator()
		=> RuleFor(x => x.Data)
			.Must(x => EhDataValida(x))
			.WithMessage("A data para o relatório não pode ser posterior a data atual.")
			.NotEmpty()
			.WithMessage("A data deve conter um valor válido.");

	protected bool EhDataValida(DateOnly data)
	{
		try
		{
			if (data.ToDateTime(TimeOnly.MinValue) <= DateTime.Now)
			{
				return true;
			}
		}
		catch (Exception)
		{
			return false;
		}

		return false;
	}
}
