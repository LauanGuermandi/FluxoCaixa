using FluxoCaixa.Core.Exceptions;
using FluxoCaixa.Core.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBus
{
	public static class MessageBusConfiguration
	{
		public const string MessageBusConnectionVariable = "MESSAGE_BUS_CONNECTION_STRING";

		public static void AddMessageBusConfiguration(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services, nameof(services));

			var connection = Environment.GetEnvironmentVariable(MessageBusConnectionVariable);
			if (string.IsNullOrEmpty(connection))
				throw new RequiredConfigurationException("Erro ao obter configuração do Message Bus.");

			services.AddSingleton<IMessageBus>(new MessageBusService(connection));
		}
	}
}
