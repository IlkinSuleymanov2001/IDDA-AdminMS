using Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Repositories.Cores
{
    public interface IUnitOfWork : IDisposable
    {

        // custom repository
        IStaffRepository StaffRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IOrganizationRepository OrganizationRepository { get; }


        //using just generic repository 
        IRepository<TEntity,TPrimaryKey> GetRepository<TEntity,TPrimaryKey>() where TEntity: BaseEntity<TPrimaryKey>;

        Task SaveChangeAsyncTransactional();
        Task SaveChangeAsync();
        Task CommitAsync();
        Task<IDbContextTransaction> OpenTransactionAsync();
        Task RollBackTransactionAsync();


    }
}
