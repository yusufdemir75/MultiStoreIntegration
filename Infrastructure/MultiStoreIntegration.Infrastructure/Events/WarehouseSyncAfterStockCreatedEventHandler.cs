using MediatR;
using MultiStoreIntegration.Domain.Events;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Events
{
    public class WarehouseSyncAfterStockCreatedEventHandler : INotificationHandler<Store1StockCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;

        public WarehouseSyncAfterStockCreatedEventHandler(WarehouseMongoDbContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public async Task Handle(Store1StockCreatedEvent notification, CancellationToken cancellationToken)
        {
            var stock = notification.Stock;

            var stockDocument = new StockDocument
            {
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

            var collection = _warehouseContext.Database.GetCollection<StockDocument>("Stocks");
            await collection.InsertOneAsync(stockDocument);
        }
    }
}
