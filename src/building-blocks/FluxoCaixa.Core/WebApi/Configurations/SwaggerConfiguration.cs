using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FluxoCaixa.Core.WebApi.Configurations;
public static class SwaggerConfiguration
{
	public static IServiceCollection AddSwagger(this IServiceCollection services, string appName, string version = "v1")
	{
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc(version, new OpenApiInfo { Title = $"{appName} - {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}", Version = version });

			c.MapType<DateOnly>(() => new OpenApiSchema
			{
				Type = "string",
				Format = "date"
			});

			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Adicione um token válido.",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
		});

		return services;
	}

	public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app)
	{
		app.UseSwagger();
		app.UseSwaggerUI();
		return app;
	}
}

