using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;

namespace MultiStoreIntegration.Persistence
{
    public class Store3MongoContext
    {
        private readonly IMongoDatabase _database;

        public Store3MongoContext([FromKeyedServices("Store3MongoClient")] IMongoClient mongoClient, IConfiguration configuration)
        {
            var databaseName = configuration["MongoDb:DatabaseName"]; 
            _database = mongoClient.GetDatabase(databaseName);
        }

        
        public IMongoDatabase Database => _database;
    }
}
