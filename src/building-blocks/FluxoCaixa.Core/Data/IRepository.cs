using FluxoCaixa.Core.DomainObjects;

namespace FluxoCaixa.Core.Data;
public interface IRepository<T> where T : IAggregateRoot
{
	IUnitOfWork UnitOfWork { get; }
}
