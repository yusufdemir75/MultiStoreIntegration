using MongoDB.Driver;

namespace MultiStoreIntegration.Application.Repositories.Store3
{
    public interface Store3IRepository<T> where T : class
    {
        IMongoCollection<T> Collection { get; }
    }
}
