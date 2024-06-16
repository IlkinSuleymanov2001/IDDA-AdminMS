using Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace Application.Repositories.Cores
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {

        TEntity? Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
                      IIncludableQueryable<TEntity, object>>? include = null,
                      bool enableTracking = true);



        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                         int index = 1, int size = 10,
                         bool enableTracking = true);




        Task<List<TEntity>> GetListAsync();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);

        Task<bool> All(Expression<Func<TEntity, bool>> predicate);

        Task<int> Count(Expression<Func<TEntity, bool>>? predicate);
        Task CreateAsync(TEntity entity);

        Task CreateAsync(IEnumerable<TEntity> entities);
        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        Task DeleteWhere(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetList();

        Task<TEntity> FindAsync(TPrimaryKey id);

        Task<TEntity> GetFirst(TPrimaryKey id);

        Task<TEntity> GetFirstIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetSingle(TPrimaryKey id);
        Task<TEntity?> Delete(TPrimaryKey id);

    }
}
