using Application.Repositories;
using Application.Repositories.Cores;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Infastructure.Repositories.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdminContext _context;
        private bool _disposed = false;
        private readonly IServiceProvider _provider;


        // Define private fields for repositories
        private IStaffRepository _staffRepository;
        private ICategoryRepository _categoryRepository;
        private IOrganizationRepository _organizationRepository;

        // Lazy initialization properties for repositories
        public IStaffRepository StaffRepository => _staffRepository ??= _provider.GetService<IStaffRepository>();
        public ICategoryRepository CategoryRepository => _categoryRepository ??= _provider.GetService<ICategoryRepository>();
        public IOrganizationRepository OrganizationRepository => _organizationRepository ??= _provider.GetService<IOrganizationRepository>();

        public UnitOfWork(AdminContext context, IServiceProvider provider)
        {
            _context = context;
            _provider = provider;
        }



        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task<IDbContextTransaction> OpenTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task RollBackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task SaveChangeAsyncTransactional()
        {
            using (await OpenTransactionAsync())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await CommitAsync();
                }
                catch (Exception)
                {
                    await RollBackTransactionAsync();
                    // throw new TransactionalException("dont save all operations");
                    throw;
                }
            }


        }

        public async Task SaveChangeAsync()
        {

            await _context.SaveChangesAsync();

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }

    }
}
