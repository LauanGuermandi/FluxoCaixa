using FluentValidation;
using FluentValidation.AspNetCore;

namespace FluxoCaixa.Api.Configurations;

public static class ValidationConfiguration
{
	public static void AddValidationConfiguration(this IServiceCollection services)
		=> services
			.AddValidatorsFromAssembly(typeof(ValidationConfiguration).Assembly)
			.AddFluentValidationAutoValidation(conf =>
			{
				conf.DisableDataAnnotationsValidation = true;
			})
			.AddFluentValidationClientsideAdapters();
}


