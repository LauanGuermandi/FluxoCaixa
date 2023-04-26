namespace FluxoCaixa.Core.Logging;

public interface ILoggerService<T>
{
	void LogInformation(string message, params object[] args);
	void LogError(Exception ex, string message, params object[] args);
}
