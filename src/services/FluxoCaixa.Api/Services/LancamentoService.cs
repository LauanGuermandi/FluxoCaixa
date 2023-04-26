using AutoMapper;
using FluxoCaixa.Core.Exceptions;
using FluxoCaixa.Domain.Aggregates.CaixaAggregation;
using FluxoCaixa.Domain.Aggregates.UsuarioAggregation;
using FluxoCaixa.Domain.Dtos;
using FluxoCaixa.Domain.Services;

namespace FluxoCaixa.Api.Services;

public class LancamentoService : ILancamentoService
{
	private readonly ICaixaRepository _caixaRepository;
	private readonly IUsuarioRepository _usuarioRepository;
	private readonly IMapper _mapper;

	public LancamentoService(ICaixaRepository caixaRepository, IUsuarioRepository usuarioRepository, IMapper mapper)
	{
		_caixaRepository = caixaRepository;
		_usuarioRepository = usuarioRepository;
		_mapper = mapper;
	}

	public async Task AdicionarLancamento(string email, LancamentoDto lancamentoDto)
	{
		var caixa = await _usuarioRepository.ObterCaixaPorUsuarioEmail(email);
		if (caixa == null)
		{
			throw new DomainException("Não foi possivel encontrar um Caixa/Usuário assossicado a este e-mail.");
		}

		var lancamento = _mapper.Map<Lancamento>(lancamentoDto);
		if (lancamento is null)
		{
			throw new UnexpectedError("Erro ao mapear Lancamento(DTO para Entidade).");
		}

		caixa.AdicionarLancamento(lancamento);
		await _caixaRepository.AtualizarCaixa(caixa);
		await _caixaRepository.UnitOfWork.Commit();
	}
}
