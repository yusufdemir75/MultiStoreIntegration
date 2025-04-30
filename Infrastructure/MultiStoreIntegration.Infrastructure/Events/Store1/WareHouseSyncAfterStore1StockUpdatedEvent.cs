using MediatR;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;
using MongoDB.Driver;
using MultiStoreIntegration.Domain.Events.Store1;

namespace MultiStoreIntegration.Infrastructure.Events.Store1
{
    public class WarehouseSyncAfterStockUpdatedEventHandler : INotificationHandler<Store1StockUpdatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;

        public WarehouseSyncAfterStockUpdatedEventHandler(WarehouseMongoDbContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public async Task Handle(Store1StockUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var stock = notification.Stock;

            var collection = _warehouseContext.Database.GetCollection<StockDocument>("Store1Stocks");

            var filter = Builders<StockDocument>.Filter.Eq(s => s.RelationalId, stock.Id);

            var update = Builders<StockDocument>.Update
                .Set(s => s.ProductCode, stock.ProductCode)
                .Set(s => s.Category, stock.Category)
                .Set(s => s.ProductName, stock.ProductName)
                .Set(s => s.Size, stock.Size)
                .Set(s => s.Color, stock.Color)
                .Set(s => s.Quantity, stock.Quantity)
                .Set(s => s.UnitPrice, (int)stock.UnitPrice)
                .Set(s => s.UpdatedDate, DateTime.UtcNow);

            await collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }
    }
}
