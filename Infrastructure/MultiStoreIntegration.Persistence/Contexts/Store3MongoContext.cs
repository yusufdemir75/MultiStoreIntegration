using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MultiStoreIntegration.Domain.MongoDocuments;

namespace MultiStoreIntegration.Persistence.Contexts
{
    public class Store3MongoContext
    {
        private readonly IMongoDatabase _database;

        public Store3MongoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDb:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDb:DatabaseName"]);
        }

        // MongoDB koleksiyonları için Document sınıflarını kullan
        public IMongoCollection<StockDocument> Products => _database.GetCollection<StockDocument>("Products");
        public IMongoCollection<SaleDocument> Sales => _database.GetCollection<SaleDocument>("Sales");
        public IMongoCollection<Domain.MongoDocuments.ReturnDocument> Returns => _database.GetCollection<Domain.MongoDocuments.ReturnDocument>("Returns");
        public IMongoDatabase GetDatabase() => _database;

    }
}
