using MediatR;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;
using MultiStoreIntegration.Domain.Events.Store1;

namespace MultiStoreIntegration.Infrastructure.Events.Store1
{
    public class WarehouseSyncAfterStore1StockCreatedEventHandler : INotificationHandler<Store1StockCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;

        public WarehouseSyncAfterStore1StockCreatedEventHandler(WarehouseMongoDbContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public async Task Handle(Store1StockCreatedEvent notification, CancellationToken cancellationToken)
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

            var collection = _warehouseContext.Database.GetCollection<StockDocument>("Store1Stocks");
            await collection.InsertOneAsync(stockDocument, cancellationToken: cancellationToken);
        }
    }
}
