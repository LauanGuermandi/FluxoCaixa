using EasyNetQ;
using FluxoCaixa.Core.Messaging;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace MessageBus;

public class MessageBusService : IMessageBus
{
	private IBus _bus;
	private IAdvancedBus _advancedBus;

	private readonly string _connectionString;

	public MessageBusService(string connectionString)
	{
		_connectionString = connectionString;
		TryConnect();
	}

	public bool IsConnected
		=> _bus?.Advanced.IsConnected ?? false;

	public async Task PublishAsync<T>(T message) where T : Event
	{
		TryConnect();
		await _bus.PubSub.PublishAsync(message);
	}

	public async Task SubscribeAsync<T>(string subscriptionId, Action<T> onMessage) where T : Event
	{
		TryConnect();
		await _bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
	}

	private void TryConnect()
	{
		if (IsConnected) return;

		var policy = Policy.Handle<EasyNetQException>()
			.Or<BrokerUnreachableException>()
			.RetryForever();

		policy.Execute(() =>
		{
			_bus = RabbitHutch.CreateBus(_connectionString);
			_advancedBus = _bus.Advanced;
			_advancedBus.Disconnected += OnDisconnect;
		});
	}

	private void OnDisconnect(object s, EventArgs e)
	{
		var policy = Policy.Handle<EasyNetQException>()
			.Or<BrokerUnreachableException>()
			.RetryForever();

		policy.Execute(TryConnect);
	}

	public void Dispose()
		=> _bus.Dispose();
}
