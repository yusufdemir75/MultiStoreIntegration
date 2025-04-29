using MongoDB.Driver;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;

namespace MultiStoreIntegration.Persistence.Repositories.Store3.Store3Stock
{
    public class Store3StockReadRepository : Store3ReadRepository<StockDocument>, Store3IStockReadRepository
    {
        private readonly IMongoDatabase _database;

        // Store3MongoContext üzerinden veritabanına erişim sağlıyoruz
        public Store3StockReadRepository(Store3MongoContext context) : base(context.Database) // Store3MongoContext üzerinden _database alıyoruz
        {
            _database = context.Database; // Database'yi burada alıyoruz
        }

        // Diğer metodlar burada yer alacak
    }
}
