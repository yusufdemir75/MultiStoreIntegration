using MongoDB.Bson;
using MongoDB.Driver;
using MultiStoreIntegration.Application.Repositories.Store3;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturnDocument = MultiStoreIntegration.Domain.MongoDocuments.ReturnDocument;

namespace MultiStoreIntegration.Persistence.Repositories.Store3
{
    public class Store3WriteRepository<T> : Store3IWriteRepository<T> where T : class
    {
        private readonly IMongoDatabase _database;

        public Store3WriteRepository(Store3MongoContext context)
        {
            _database = context.Database;
        }

        public IMongoCollection<T> Collection
        {
            get
            {
                if (typeof(T) == typeof(Store3StockDocument))
                {
                    return _database.GetCollection<T>("Stocks"); 
                }

                else if (typeof(T) == typeof(Store3SaleDocument))
                {
                    return _database.GetCollection<T>("Sales");
                }
                else if (typeof(T) == typeof(Store3ReturnDocument))
                {
                    return _database.GetCollection<T>("Returns");
                }
                

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
                var idProperty = model.GetType().GetProperty("Id");
                if (idProperty == null)
                {
                    Console.WriteLine("Model does not contain an Id property.");
                    return false;
                }

                var idValue = idProperty.GetValue(model);
                if (idValue == null)
                {
                    Console.WriteLine("Id property is null.");
                    return false;
                }

                var objectId = idValue is ObjectId ? (ObjectId)idValue : ObjectId.Parse(idValue.ToString());

                var filter = Builders<T>.Filter.Eq("_id", objectId);  

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
