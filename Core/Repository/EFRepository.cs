﻿using Application.Repositories.Cores;
using Core.BaseEntities;
using Core.Pipelines.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Threading;

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


        public async Task CreateAsync(TEntity entity)
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
           
        }

        public async Task CreateAsync(IEnumerable<TEntity> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }

        public bool Delete(TEntity entity)
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
            return  true;
           
        }

        public async Task DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> entities = query.Where(predicate);

            foreach (var entity in entities)
                Delete(entity);
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

      



        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            return query.FirstOrDefault(predicate);
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

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index=1,int size=10,
            bool enableTracking = false)
        {
            IQueryable<TEntity> queryable = query;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null) queryable = orderBy(queryable);
            //if (pageRequest != null) queryable.Skip((index - 1) * size ).Take(size);
            return await queryable.ToListAsync();
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return query.Where(predicate);
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>>? predicate = null, int index = 1, int size = 10)
        {
            IQueryable<TEntity> queryable = query;
            if (predicate != null) queryable = queryable.Where(predicate);
            return queryable;
        }

        public bool Update(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _dbset.Attach(entity);
            }
            entry.State = EntityState.Modified;
            return true;
        }

        public async Task<TEntity> FindAsync(TPrimaryKey id)
        {
            return await _dbset.FindAsync(id);
        }

        public Task<TEntity> GetSingle(TPrimaryKey id)
        {
            return query.SingleOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public async  Task<TEntity?>  Delete(TPrimaryKey id)
        {
            var entity = await FindAsync(id);
            if (entity == null) return default; // not found; assume already deleted.
            Delete(entity);
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

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public Task SaveChange()
        {
           return Task.Run(() => _context.SaveChanges());
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollBackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async  Task CreateSavepointAsync()
        {
            await _context.SaveChangesAsync();
            await _context.Database.CurrentTransaction.CreateSavepointAsync(SavePointNames.savepointone);
        }

      
    }
}
