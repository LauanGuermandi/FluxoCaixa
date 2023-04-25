namespace FluxoCaixa.Core.Exceptions;

public class UnexpectedError : Exception
{
	public UnexpectedError(string message) : base(message) { }
}
