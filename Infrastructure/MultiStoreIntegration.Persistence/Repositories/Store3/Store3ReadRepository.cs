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
        private readonly IMongoDatabase _database;

        public Store3ReadRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<T> Collection => _database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());

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
        public async Task<T> GetByIdAsync(string id)
        {
            return await Collection.Find(x => x.GetType().GetProperty("Id").GetValue(x).ToString() == id).FirstOrDefaultAsync();
        }

        // Get documents where filter is applied (GetWhere)
        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> method)
        {
            // Convert Expression<Func<T, bool>> to MongoDB FilterDefinition<T>
            var filter = ConvertExpressionToFilter(method);
            return await Collection.Find(filter).ToListAsync();
        }

        // Get single document by filter
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        {
            // Convert Expression<Func<T, bool>> to MongoDB FilterDefinition<T>
            var filter = ConvertExpressionToFilter(method);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        // Helper method to convert expression to MongoDB FilterDefinition<T>
        private FilterDefinition<T> ConvertExpressionToFilter(Expression<Func<T, bool>> expression)
        {
            // MongoDB does not directly support LINQ expressions, so we use Builders<T>.Filter to convert.
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
