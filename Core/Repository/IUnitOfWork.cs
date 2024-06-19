using Core.BaseEntities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Repositories.Cores
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<TEntity,TPrimaryKey> GetRepository<TEntity,TPrimaryKey>() where TEntity: BaseEntity<TPrimaryKey>;

        Task SaveChangeAsync();
        Task CommitAsync();
        Task<IDbContextTransaction> OpenTransactionAsync();
        Task RollBackTransactionAsync();


    }
}
