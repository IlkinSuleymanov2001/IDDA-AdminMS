using Application.Repositories.Cores;
using Core.BaseEntities;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infastructure.Repositories.Context
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext  _context;


        public IRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>() where TEntity : BaseEntity<TPrimaryKey>
        {
            return new EFRepository<TContext, TEntity,TPrimaryKey>(_context);
        }
        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task<IDbContextTransaction> OpenTransactionAsync()
        {
            return  await _context.Database.BeginTransactionAsync();
        }

        public async Task RollBackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }


        public async Task SaveChangeAsync()
        {

            await _context.SaveChangesAsync();

        }

    }
}
