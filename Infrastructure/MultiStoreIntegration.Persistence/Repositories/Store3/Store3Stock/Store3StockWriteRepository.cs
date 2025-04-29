using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Repositories.Store3;
using MultiStoreIntegration.Persistence;

public class Store3StockWriteRepository : Store3WriteRepository<StockDocument>, Store3IStockWriteRepository
{
    public Store3StockWriteRepository(Store3MongoContext context) : base(context)
    {
    }
}
