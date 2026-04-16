using System.Linq.Expressions;

namespace Shatbly.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<List<T>> GetAsync(Expression<Func<T, bool>>? expression = null,
             Expression<Func<T, object>>[]? includes = null, bool tracking = true);

        Task<T?> GetOneAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>[]? includes = null, bool tracking = true);

        Task<int> CommitAsync();

    }
}
