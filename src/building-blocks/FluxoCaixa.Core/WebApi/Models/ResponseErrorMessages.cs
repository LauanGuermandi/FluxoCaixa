namespace FluxoCaixa.Core.WebApi.Models;
public class ResponseErrorMessages
{
	public ResponseErrorMessages()
		=> Messages = new List<string>();

	public List<string> Messages { get; set; }
}
