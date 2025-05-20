using MongoDB.Driver;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;

namespace MultiStoreIntegration.Persistence.Repositories.Store3.Store3Stock
{
    public class Store3StockReadRepository : Store3ReadRepository<Store3StockDocument>, Store3IStockReadRepository
    {
        public Store3StockReadRepository(Store3MongoContext context)
            : base(context.Database, "Stocks") // "Stocks" MongoDB collection adı
        {
        }

        // Kategoriye göre stok miktarlarını döner
        public async Task<List<(string Category, int TotalQuantity)>> GetTotalStockPerCategoryAsync()
        {
            var aggregate = await Collection.Aggregate()
                .Match(Builders<Store3StockDocument>.Filter.Ne(x => x.Category, null))
                .Group(x => x.Category, g => new
                {
                    Category = g.Key,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .ToListAsync();

            return aggregate
                .Select(x => (x.Category, x.TotalQuantity))
                .ToList();
        }
    }
}
