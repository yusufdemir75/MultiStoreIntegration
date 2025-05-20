using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MultiStoreIntegration.Persistence.Contexts
{
    public class WarehouseMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public WarehouseMongoDbContext([FromKeyedServices("WarehouseMongoClient")] IMongoClient mongoClient, IConfiguration configuration)
        {
            var databaseName = configuration["WareHouse:DatabaseName"];
            _database = mongoClient.GetDatabase(databaseName); 
        }

        public IMongoDatabase Database => _database;
    }
}
