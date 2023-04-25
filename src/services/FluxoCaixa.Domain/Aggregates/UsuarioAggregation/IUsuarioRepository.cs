﻿using FluxoCaixa.Core.Data;
using FluxoCaixa.Domain.Aggregates.CaixaAggregation;

namespace FluxoCaixa.Domain.Aggregates.LojaAggregation;
public interface IUsuarioRepository : IRepository<Usuario>
{
	Task<Caixa> ObterCaixaPorUsuarioEmail(string email);
}
