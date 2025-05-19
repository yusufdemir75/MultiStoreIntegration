using Amazon.Runtime.Internal;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MultiStoreIntegration.Domain.Events.Store2;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Infrastructure.Events.Store3;
using MultiStoreIntegration.Persistence.Contexts;
using System.Threading;
using System.Threading.Tasks;
using ReturnDocument = MultiStoreIntegration.Domain.MongoDocuments.ReturnDocument;

namespace MultiStoreIntegration.Infrastructure.Events.Store2
{
    public class Store2ReturnCreatedEventHandler : INotificationHandler<Store2ReturnCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;
        private readonly ILogger<Store3SaleCreatedEventHandler> _logger;

        public Store2ReturnCreatedEventHandler(WarehouseMongoDbContext warehouseContext, ILogger<Store3SaleCreatedEventHandler> logger)
        {
            _warehouseContext = warehouseContext;
            _logger=logger;
        }

        public async Task Handle(Store2ReturnCreatedEvent notification, CancellationToken cancellationToken)
        {
            var Return = notification._Return;

            var returnDocument = new ReturnDocument
            {
                RelationalId = Return.Id,
                ProductId = Return.ProductId,
                SaleId = Return.SaleId,
                CustomerName = Return.CustomerName,
                CustomerPhone = Return.CustomerPhone,
                Quantity = Return.Quantity,
                ReturnReason = Return.ReturnReason,
                RefundAmount = Return.RefundAmount,
                CreatedDate = Return.CreatedDate,
                UpdatedDate = DateTime.UtcNow
            };

            var salesCollection = _warehouseContext.Database.GetCollection<SaleDocument>("Store2Sales");
            
            var sale = await salesCollection.Find(x=> x.RelationalId == Return.SaleId).FirstOrDefaultAsync(cancellationToken);


            var stockCollection = _warehouseContext.Database.GetCollection<StockDocument>("Store2Stocks");

            var stock = await stockCollection.Find(x => x.RelationalId == sale.ProductId).FirstOrDefaultAsync(cancellationToken);
            if (sale != null && stock != null)
            {
                sale.Quantity -= returnDocument.Quantity;
                sale.TotalPrice = sale.Quantity * stock.UnitPrice;

                await salesCollection.ReplaceOneAsync(
                    filter: x => x.Id == sale.Id,
                    replacement: sale,
                    cancellationToken: cancellationToken
                    );

            }
            else
            {
                _logger.LogWarning("Satış Bulunamadı : SaleId={Id}", sale?.Id);

            }

            var collection = _warehouseContext.Database.GetCollection<ReturnDocument>("Store2Returns");
            await collection.InsertOneAsync(returnDocument, cancellationToken: cancellationToken);
            _logger.LogInformation("İade kaydedildi: SaleId={ProductId}, Quantity={Quantity}", sale?.Id, Return.Quantity);

        }
    }
}
