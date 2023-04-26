using FluxoCaixa.Core.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Logging;
public static class LoggerConfigurations
{
	public static void AddLoggerConfiguration(this IServiceCollection services)
		=> services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
}
