namespace FluxoCaixa.Core.Messaging;

public interface IMessageBus
{
	bool IsConnected { get; }
	Task PublishAsync<T>(T message) where T : Event;
	Task SubscribeAsync<T>(string subscriptionId, Action<T> onMessage) where T : Event;
}

