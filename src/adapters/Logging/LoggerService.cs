using FluxoCaixa.Core.Logging;
using Microsoft.Extensions.Logging;

namespace Logging;

public class LoggerService<T> : ILoggerService<T>
{
	private readonly ILogger<T> _logger;

	public LoggerService(ILogger<T> logger)
		=> _logger = logger;

	public void LogError(Exception ex, string message, params object[] args)
		=> _logger.LogError(ex, message, args);

	public void LogInformation(string message, params object[] args)
		=> _logger.LogInformation(message, args);
}
