using Domain.Entities.BaseEntities;
using Domain.HelperClasses;
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




        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate=null);

        Task CreateAsync(TEntity entity);

        Task CreateAsync(IEnumerable<TEntity> entities);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        Task DeleteWhere(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> Delete(TPrimaryKey id);

        Task<TEntity> FindAsync(TPrimaryKey id);

        Task<TEntity> GetFirst(TPrimaryKey id);

        Task<TEntity> GetFirstIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetSingle(TPrimaryKey id);

        Task<int> SaveChangeAsync(CancellationToken cancellationToken=default);
        Task SaveChange();


    }
}
