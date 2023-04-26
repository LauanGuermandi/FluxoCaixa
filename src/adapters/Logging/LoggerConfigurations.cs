using FluxoCaixa.Core.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Logging;
public static class LoggerConfigurations
{
	public static void AddLoggerConfiguration(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
	{
		switch (serviceLifetime)
		{
			case ServiceLifetime.Transient:
			case ServiceLifetime.Scoped:
				services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
				break;
			case ServiceLifetime.Singleton:
				services.AddSingleton(typeof(ILoggerService<>), typeof(LoggerService<>));
				break;
		}
	}
}
