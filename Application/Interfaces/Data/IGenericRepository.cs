using System.Linq.Expressions;

namespace Application.Interfaces.Data
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByPrimaryKey<TPrimaryKey>(TPrimaryKey id);

        Task<bool> DoesExists(Expression<Func<T, bool>> filter);

        Task<T> GetSingleBySpec(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetListBySpec(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetWithInclude(Expression<Func<T, object>>[] children);

        Task<T> GetWithIncludeAndFilter(
            Expression<Func<T, object>>[] children,
            Expression<Func<T, bool>> filter
        );

        Task<IEnumerable<T>> GetListWithIncludeAndFilter(
            Expression<Func<T, object>>[] children,
            Expression<Func<T, bool>> filter
        );
    }
}
