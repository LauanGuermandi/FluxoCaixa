using FluxoCaixa.Domain.Dtos;

namespace FluxoCaixa.Domain.Services;

public interface IRelatorioService
{
	Task SolicitarRelatorioConsolidadoDiario(SolicitacaoRelatorioConsolidadoDiario solicitacaoRelatorioConsolidadoDiario);
}
