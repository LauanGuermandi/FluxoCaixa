using System.Globalization;
using AutoMapper;
using FluxoCaixa.Domain.Dtos;

namespace FluxoCaixa.Infrastructure.CrossCutting.Mappers;
public class MapDtoToEvent : Profile
{
	public MapDtoToEvent()
		=> CreateMap<RelatorioConsolidadoDiarioDto, SolicitacaoRelatorioConsolidadoDiario>()
			.ForMember(x => x.Data, o => o.MapFrom(x => MapStringDateToDateOnly(x.Data)));

	private DateOnly MapStringDateToDateOnly(string date)
		=> DateOnly.FromDateTime(DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture));
}
