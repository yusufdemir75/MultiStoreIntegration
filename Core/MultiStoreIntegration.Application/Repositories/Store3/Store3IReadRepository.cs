using MongoDB.Bson;
using System.Linq.Expressions;

namespace MultiStoreIntegration.Application.Repositories.Store3
{
    public interface Store3IReadRepository<T> : Store3IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> method);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method);
        Task<T> GetByIdAsync(ObjectId id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
