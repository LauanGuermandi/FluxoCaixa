using System.Text.Json;
using FluxoCaixa.Core.HealthChecks;
using FluxoCaixa.Infrastructure.Data.Configurations;
using HealthChecks.UI.Core;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

namespace FluxoCaixa.Api.Configurations;

public static class HealthCheckConfiguration
{
	private const string HealthCheckMessageBusConnection = "HEALTH_CHECK_MESSAGE_BUS_CONNECTION";

	public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services)
	{
		ArgumentNullException.ThrowIfNull(services, nameof(services));

		var serviceProvider = services.BuildServiceProvider();

		var identidadeConnectionString = Environment.GetEnvironmentVariable(DataContextsConfigurations.IdentidadeConnectionStringVariable);
		var fluxoCaixaConnectionString = Environment.GetEnvironmentVariable(DataContextsConfigurations.FluxoCaixaConnectionStringVariable);

		var messageBusConnectionString = Environment.GetEnvironmentVariable(HealthCheckMessageBusConnection);

		services
			.AddHealthChecks()
			.AddCheck("DEPENDENCY_INJECTION", new DependencyInjectionHealthCheck(serviceProvider))
			.AddSqlServer(identidadeConnectionString, name: "IDENTIDADE_DATABASE")
			.AddSqlServer(fluxoCaixaConnectionString, name: "FLUXOCAIXA_DATABASE")
			.AddRabbitMQ(messageBusConnectionString, new SslOption(), name: "RABBITMQ");

		return services;
	}

	public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app)
	{
		var options = new HealthCheckOptions
		{
			AllowCachingResponses = false
		};

		options.ResultStatusCodes[HealthStatus.Unhealthy] = StatusCodes.Status500InternalServerError;
		options.ResponseWriter = async (context, report) =>
		{
			var healthReport = UIHealthReport.CreateFrom(report);
			var response = JsonSerializer.Serialize(healthReport);
			await context.Response.WriteAsync(response);
		};

		return app.UseHealthChecks("/healthz", options);
	}

}
