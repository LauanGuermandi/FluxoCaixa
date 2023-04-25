using AutoMapper;
using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Domain.Dtos;

namespace FluxoCaixa.Infrastructure.CrossCutting.Mappers;
public class MapDtoToEntity : Profile
{
	public MapDtoToEntity()
		=> CreateMap<LancamentoDto, Lancamento>()
			.ConstructUsing(x => new Lancamento(x.TipoLancamento, x.Valor));
}
