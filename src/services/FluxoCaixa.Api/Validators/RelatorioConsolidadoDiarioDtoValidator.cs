using System.Globalization;
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

	protected bool EhDataValida(string data)
	{
		try
		{
			var cultureInfo = new CultureInfo("pt-BR");
			if (DateTime.Parse(data, cultureInfo, DateTimeStyles.NoCurrentDateDefault) <= DateTime.Now)
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
