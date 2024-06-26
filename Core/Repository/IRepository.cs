﻿using Core.BaseEntities;
using Core.Repository.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Repositories.Cores
{
    public interface IRepository< TEntity, TPrimaryKey> 
        where TEntity : BaseEntity<TPrimaryKey>
    {

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
                      IIncludableQueryable<TEntity, object>>? include = null,
                      bool enableTracking = false);

        Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
           int index = 0, int size = 10,
           bool enableTracking = false,
           CancellationToken cancellationToken = default);


        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>>? predicate = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate=null);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        bool DeleteWhere(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> DeleteAsync(TPrimaryKey id);

        Task<TEntity> FindAsync(TPrimaryKey id);

        Task<TEntity> GetFirst(TPrimaryKey id);

        Task<TEntity> GetFirstIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetSingle(TPrimaryKey id);

        Task CommitAsync();
        Task RollBackAsync();

        Task RollbackToSavePointAsync(string name="savepointone");
        Task CreateSavepointAsync(string name= "savepointone");
        Task OpenTransactionAsync();
        Task<int> SaveAsync(CancellationToken cancellationToken =default);


    }
}
