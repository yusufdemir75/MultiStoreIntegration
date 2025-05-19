using MongoDB.Bson;
using MongoDB.Driver;
using MultiStoreIntegration.Application.Repositories.Store3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store3
{
    public class Store3ReadRepository<T> : Store3IReadRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public Store3ReadRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public IMongoCollection<T> Collection => _collection;

        // Get all documents
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Collection.Find(_ => true).ToListAsync();
        }

        // Implemented GetAll() method from the interface
        public IQueryable<T> GetAll()
        {
            return Collection.AsQueryable();
        }

        // Get document by ID
        public async Task<T> GetByIdAsync(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }


        // Get documents where filter is applied (GetWhere)
        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> method)
        {
            // Convert Expression<Func<T, bool>> to MongoDB FilterDefinition<T>
            var filter = ConvertExpressionToFilter(method);
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        {
            var filter = ConvertExpressionToFilter(method);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        private FilterDefinition<T> ConvertExpressionToFilter(Expression<Func<T, bool>> expression)
        {
            return Builders<T>.Filter.Where(expression);
        }

        // Get documents by ID (For specific implementation)
        public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<string> ids)
        {
            var objectIds = ids.Select(id => new MongoDB.Bson.ObjectId(id)).ToList();
            var filter = Builders<T>.Filter.In("Id", objectIds);
            return await Collection.Find(filter).ToListAsync();
        }
    }
}
