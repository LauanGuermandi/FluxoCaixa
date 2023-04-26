namespace FluxoCaixa.Core.Messaging;

public abstract class Event
{
	public string MessageType { get; protected set; }
	public DateTime Timestamp { get; private set; }

	protected Event()
	{
		MessageType = GetType().Name;
		Timestamp = DateTime.Now;
	}
}
