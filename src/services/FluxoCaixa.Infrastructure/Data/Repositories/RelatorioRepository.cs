﻿using FluxoCaixa.Core.Data;
using FluxoCaixa.Domain.Aggregates.RelatorioAggregation;
using FluxoCaixa.Infrastructure.Data.Context.FluxoCaixa;

namespace FluxoCaixa.Infrastructure.Data.Repositories;

public class RelatorioRepository : IRelatorioRepository
{
	private readonly FluxoCaixaContext _context;

	public RelatorioRepository(FluxoCaixaContext context)
		=> _context = context;

	public IUnitOfWork UnitOfWork
			=> _context;

	public async Task AdicionarRelatorio(Relatorio relatorio)
		=> await _context.Relatorios.AddAsync(relatorio);
}
