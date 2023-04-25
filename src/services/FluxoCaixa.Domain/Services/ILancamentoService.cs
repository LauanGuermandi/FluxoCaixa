using FluxoCaixa.Domain.Dtos;

namespace FluxoCaixa.Domain.Services;
public interface ILancamentoService
{
	Task AdicionarLancamento(string email, LancamentoDto lancamentoDto);
}
