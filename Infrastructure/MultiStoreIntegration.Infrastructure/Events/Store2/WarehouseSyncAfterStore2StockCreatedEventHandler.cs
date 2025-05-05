using MediatR;
using MultiStoreIntegration.Domain.Events.Store2;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;
using MongoDB.Driver;

namespace MultiStoreIntegration.Application.Features.Events
{
    public class WarehouseSyncAfterStockCreatedEventHandler : INotificationHandler<Store2StockCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;

        public WarehouseSyncAfterStockCreatedEventHandler(WarehouseMongoDbContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public async Task Handle(Store2StockCreatedEvent notification, CancellationToken cancellationToken)
        {

            var stock = notification.Stock;
            var stockDocument = new StockDocument
            {
                RelationalId = stock.Id,
                ProductCode = stock.ProductCode,
                Category = stock.Category,
                ProductName = stock.ProductName,
                Size = stock.Size,
                Color = stock.Color,
                Quantity = stock.Quantity,
                UnitPrice = (int)stock.UnitPrice,
                CreatedDate = stock.CreatedDate,
                UpdatedDate = DateTime.UtcNow
            };

            var collection = _warehouseContext.Database.GetCollection<StockDocument>("Store2Stocks");
            await collection.InsertOneAsync(stockDocument, cancellationToken: cancellationToken);
        }
    }
}
