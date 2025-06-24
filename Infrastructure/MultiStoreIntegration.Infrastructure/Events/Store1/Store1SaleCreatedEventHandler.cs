using MediatR;
using MultiStoreIntegration.Domain.Events;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;
using MongoDB.Driver;

namespace MultiStoreIntegration.Application.Features.Events
{
    public class Store1SaleCreatedEventHandler : INotificationHandler<Store1SaleCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;

        public Store1SaleCreatedEventHandler(WarehouseMongoDbContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public async Task Handle(Store1SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            var sale = notification.Sale;

            var saleDocument = new SaleDocument
            {
                RelationalId = sale.Id,
                ProductId = sale.ProductId,
                Quantity = sale.Quantity,
                TotalPrice = sale.TotalPrice,
                CustomerName = sale.CustomerName,
                CustomerPhone = sale.CustomerPhone,
                PaymentMethod = sale.PaymentMethod,
                CreatedDate = sale.CreatedDate,
                Size = sale.Size,
                Category = sale.Category,
                Color = sale.Color,
                ProductName = sale.ProductName,
                UpdatedDate = DateTime.UtcNow
            };

            var salesCollection = _warehouseContext.Database.GetCollection<SaleDocument>("Store1Sales");
            await salesCollection.InsertOneAsync(saleDocument, cancellationToken: cancellationToken);

            var stockCollection = _warehouseContext.Database.GetCollection<StockDocument>("Store1Stocks");

            var stock = await stockCollection.Find(x => x.RelationalId == sale.ProductId).FirstOrDefaultAsync(cancellationToken);

            if (stock != null)
            {
                stock.Quantity -= sale.Quantity;

                await stockCollection.ReplaceOneAsync(
                    filter: x => x.Id == stock.Id,
                    replacement: stock,
                    cancellationToken: cancellationToken
                );
            }
            else
            {
                Console.WriteLine($"Stok bulunamadı: ProductId={sale.ProductId}");
            }
        }
    }
}
