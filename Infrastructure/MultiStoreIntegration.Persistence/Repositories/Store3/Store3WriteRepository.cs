using MongoDB.Driver;
using MultiStoreIntegration.Application.Repositories.Store3;
using MultiStoreIntegration.Domain.MongoDocuments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturnDocument = MultiStoreIntegration.Domain.MongoDocuments.ReturnDocument;

namespace MultiStoreIntegration.Persistence.Repositories.Store3
{
    public class Store3WriteRepository<T> : Store3IWriteRepository<T> where T : class
    {
        private readonly IMongoDatabase _database;

        // Store3MongoContext üzerinden IMongoDatabase alıyoruz
        public Store3WriteRepository(Store3MongoContext context)
        {
            _database = context.Database;
        }

        public IMongoCollection<T> Collection
        {
            get
            {
                // "StockDocument" yerine "products" koleksiyonunu kullanmak istiyoruz.
                if (typeof(T) == typeof(StockDocument))
                {
                    return _database.GetCollection<T>("Products"); // Burada 'products' koleksiyonuna yönlendiriyoruz.
                }

                else if (typeof(T) == typeof(SaleDocument))
                {
                    return _database.GetCollection<T>("Sales");
                }
                else if (typeof(T) == typeof(ReturnDocument))
                {
                    return _database.GetCollection<T>("Returns");
                }
                // Diğer türler için varsayılan koleksiyon adını döndürüyoruz
                return _database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
            }
        }
        public async Task<bool> AddAsync(T model)
        {
            try
            {
                await Collection.InsertOneAsync(model);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting document: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            try
            {
                await Collection.InsertManyAsync(datas);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting documents: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveAsync(string id)
        {
            try
            {
                var result = await Collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting document: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T model)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("Id", model.GetType().GetProperty("Id").GetValue(model).ToString());
                var result = await Collection.ReplaceOneAsync(filter, model);

                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating document: {ex.Message}");
                return false;
            }
        }
    }
}
