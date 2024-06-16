using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Repositories.Cores
{
    public interface IUnitOfWork : IDisposable
    {
        IStaffRepository StaffRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IOrganizationRepository OrganizationRepository { get; }


        Task SaveChangeAsyncTransactional();
        Task SaveChangeAsync();
        Task CommitAsync();
        Task<IDbContextTransaction> OpenTransactionAsync();
        Task RollBackTransactionAsync();


    }
}
