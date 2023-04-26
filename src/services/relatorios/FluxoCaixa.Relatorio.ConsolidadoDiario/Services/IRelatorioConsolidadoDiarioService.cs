using FluxoCaixa.Domain.Dtos;

namespace FluxoCaixa.Domain.Services;

public interface IRelatorioConsolidadoDiarioService
{
	Task GerarRelatorioConsolidadoDiario(SolicitacaoRelatorioConsolidadoDiario solicitacaoRelatorioConsolidadoDiario);
}
