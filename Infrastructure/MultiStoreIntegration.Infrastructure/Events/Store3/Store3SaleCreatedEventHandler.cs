using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MultiStoreIntegration.Domain.Events.Store3;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Infrastructure.Events.Store3
{
    public class Store3SaleCreatedEventHandler : INotificationHandler<Store3SaleCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;
        private readonly ILogger<Store3SaleCreatedEventHandler> _logger;

        public Store3SaleCreatedEventHandler(
            WarehouseMongoDbContext warehouseContext,
            ILogger<Store3SaleCreatedEventHandler> logger)
        {
            _warehouseContext = warehouseContext;
            _logger = logger;
        }


        public async Task Handle(Store3SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            var sale = notification.Store3SaleDocument;

            var saleDocument = new Store3SaleDocument
            {
                Id=sale.Id,
                ProductId = sale.ProductId,
                Quantity = sale.Quantity,
                TotalPrice = sale.TotalPrice,
                CustomerName = sale.CustomerName,
                CustomerPhone = sale.CustomerPhone,
                PaymentMethod = sale.PaymentMethod,
                CreatedDate = sale.CreatedDate,
                UpdatedDate = DateTime.UtcNow
            };

            var stockCollection = _warehouseContext.Database.GetCollection<Store3StockDocument>("Store3Stocks");

            var stock = await stockCollection.Find(x => x.Id == sale.ProductId).FirstOrDefaultAsync(cancellationToken);

            if (stock == null)
            {
                _logger.LogWarning("Stok bulunamadı: ProductId={ProductId}", sale.ProductId);
                return;
            }

            if (stock.Quantity < sale.Quantity)
            {
                _logger.LogWarning("Stoktaki ürün miktarı yetersiz. Mevcut: {Available}, Talep edilen: {Requested}",
                    stock.Quantity, sale.Quantity);
                return;
            }

            stock.Quantity -= sale.Quantity;

            try
            {
                await stockCollection.ReplaceOneAsync(
                    filter: x => x.Id == stock.Id,
                    replacement: stock,
                    cancellationToken: cancellationToken
                );

                _logger.LogInformation("Stok güncellendi: ProductId={ProductId}, Yeni miktar={NewQuantity}", stock.Id, stock.Quantity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stok güncelleme hatası: ProductId={ProductId}", stock.Id);
            }

            var salesCollection = _warehouseContext.Database.GetCollection<Store3SaleDocument>("Store3Sales");
            await salesCollection.InsertOneAsync(saleDocument, cancellationToken: cancellationToken);

            _logger.LogInformation("Satış kaydedildi: ProductId={ProductId}, Quantity={Quantity}", sale.ProductId, sale.Quantity);
        }



    }
}
