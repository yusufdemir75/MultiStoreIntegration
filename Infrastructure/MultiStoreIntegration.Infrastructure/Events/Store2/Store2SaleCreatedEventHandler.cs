using MediatR;
using MultiStoreIntegration.Domain.Events;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;
using MongoDB.Driver;
using MultiStoreIntegration.Domain.Events.Store2;

namespace MultiStoreIntegration.Application.Features.Events
{
    public class Store2SaleCreatedEventHandler : INotificationHandler<Store2SaleCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;

        public Store2SaleCreatedEventHandler(WarehouseMongoDbContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
        }

        public async Task Handle(Store2SaleCreatedEvent notification, CancellationToken cancellationToken)
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
                UpdatedDate = DateTime.UtcNow
            };

            // 1. Satışı Store1Sales tablosuna ekle
            var salesCollection = _warehouseContext.Database.GetCollection<SaleDocument>("Store2Sales");
            await salesCollection.InsertOneAsync(saleDocument, cancellationToken: cancellationToken);

            // 2. Stoktan ilgili ürünün stoğunu bul
            var stockCollection = _warehouseContext.Database.GetCollection<StockDocument>("Store2Stocks");

            var stock = await stockCollection.Find(x => x.RelationalId == sale.ProductId).FirstOrDefaultAsync(cancellationToken);

            // 3. Stok bulunduysa miktarı azalt
            if (stock != null)
            {
                stock.Quantity -= sale.Quantity;

                // 4. Güncellenmiş stok bilgisini Mongo'ya yaz
                await stockCollection.ReplaceOneAsync(
                    filter: x => x.Id == stock.Id,
                    replacement: stock,
                    cancellationToken: cancellationToken
                );
            }
            else
            {
                // Stok bulunamazsa log yazmak faydalı olabilir
                Console.WriteLine($"Stok bulunamadı: ProductId={sale.ProductId}");
            }
        }
    }
}
