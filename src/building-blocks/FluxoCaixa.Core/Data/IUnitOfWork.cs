namespace FluxoCaixa.Core.Data;
public interface IUnitOfWork
{
	Task<bool> Commit();
}

