using MediatR;
using MultiStoreIntegration.Domain.Events.Store3;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Infrastructure.Events.Store3
{
    public class Store3StockCreatedEventHandler : INotificationHandler<Store3StockCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;

        public Store3StockCreatedEventHandler(WarehouseMongoDbContext warehousecontext)
        {
            _warehouseContext = warehousecontext;
        }
        public async Task Handle(Store3StockCreatedEvent notification, CancellationToken cancellationToken)
        {
            var store3document = notification.Stor3StockDocument;
            var stockDocument = new Store3StockDocument
            {
                Id = store3document.Id,
                ProductCode = store3document.ProductCode,
                Category = store3document.Category,
                ProductName = store3document.ProductName,
                Size = store3document.Size,
                Color = store3document.Color,
                Quantity = store3document.Quantity,
                UnitPrice = (int)store3document.UnitPrice,
                CreatedDate = store3document.CreatedDate,
                UpdatedDate = DateTime.UtcNow
            };

            var collection = _warehouseContext.Database.GetCollection<Store3StockDocument>("Store3Stocks");
            await collection.InsertOneAsync(stockDocument, cancellationToken: cancellationToken);
        }
    }
}
