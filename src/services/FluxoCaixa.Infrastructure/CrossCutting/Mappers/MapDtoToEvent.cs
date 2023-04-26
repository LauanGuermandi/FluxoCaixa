using AutoMapper;
using FluxoCaixa.Domain.Dtos;

namespace FluxoCaixa.Infrastructure.CrossCutting.Mappers;
public class MapDtoToEvent : Profile
{
	public MapDtoToEvent()
		=> CreateMap<RelatorioConsolidadoDiarioDto, SolicitacaoRelatorioConsolidadoDiario>();
}
