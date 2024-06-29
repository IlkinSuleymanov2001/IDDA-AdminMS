using Application.Repositories.Cores;
using Core.BaseEntities;
using Core.Repository.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Repository
{
    public class EFRepository<TContext ,TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TContext : DbContext

    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _dbset;
        private IQueryable<TEntity> query => _context.Set<TEntity>();

        public EFRepository(TContext context)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
            
        }


        public IQueryable<TEntity> GetListIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var queryable = query;
            queryable = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return queryable;
        }

        public async Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await query.AllAsync(predicate); ;
        }



        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await query.AnyAsync(predicate);
        }


        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate=null)
        {
            if (predicate != null) return await query.CountAsync(predicate);
            return await query.CountAsync();
        }


        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                await _dbset.AddAsync(entity);
            }
            return entity;

        }

        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities)
        {
            await _dbset.AddRangeAsync(entities);
            return entities;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {

            var entry = _context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                _dbset.Attach(entity);
                _dbset.Remove(entity);
            }
            return Task.FromResult(entity);
           
        }

        public  bool DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> entities = query.Where(predicate);
            if (entities.Count() == 0) return false;
            _dbset.RemoveRange(entities);
            return true;
        }


        public async Task<TEntity> GetFirst(TPrimaryKey id)
        {
            return await query.FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }


        public async Task<TEntity> GetFirstIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var querable = query;
            querable = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await querable.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false)
        {
            IQueryable<TEntity> queryable = query;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index=0,int size=10,
            bool enableTracking = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = query;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
                return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);

            return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
        }


        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> queryable = query;
            if (predicate != null) queryable = queryable.Where(predicate);
            return queryable;
        }

        public  Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _dbset.Attach(entity);
            }
            entry.State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public async Task<TEntity> FindAsync(TPrimaryKey id)
        {
            return await _dbset.FindAsync(id);
        }

        public Task<TEntity> GetSingle(TPrimaryKey id)
        {
            return query.SingleOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public async  Task<TEntity?>  DeleteAsync(TPrimaryKey id)
        {
            var entity = await FindAsync(id);
            if (entity == null) return default; // not found; assume already deleted.
            await DeleteAsync(entity);
            return entity;
        }

        private static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken =default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
            await _context.DisposeAsync();
        }

        public async Task RollBackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
            await _context.DisposeAsync();
        }

        public async  Task CreateSavepointAsync(string name = "savepointone")
        {
            await _context.Database.CurrentTransaction.CreateSavepointAsync(name);
        }

        public async Task OpenTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task RollbackToSavePointAsync(string name = "savepointone")
        {
            await _context.Database.CurrentTransaction.RollbackToSavepointAsync(name);
            await _context.DisposeAsync();
        }
    }
}
