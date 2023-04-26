using FluxoCaixa.Core.Logging;
using Microsoft.AspNetCore.Http;

namespace FluxoCaixa.Core.WebApi.Middlewares;
public class GlobalExceptionMiddleware
{
	private readonly RequestDelegate _next;

	public GlobalExceptionMiddleware(RequestDelegate next)
		=> _next = next;

	public async Task Invoke(HttpContext httpContext, ILoggerService<HttpContext> logger)
	{
		try
		{
			await _next(httpContext);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, ex.Message);
			throw;
		}
	}
}
