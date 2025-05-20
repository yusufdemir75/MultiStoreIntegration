using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using MultiStoreIntegration.Domain.Events.Store1;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Infrastructure.Events.Store3;
using MultiStoreIntegration.Persistence.Contexts;
using ReturnDocument = MultiStoreIntegration.Domain.MongoDocuments.ReturnDocument;

namespace MultiStoreIntegration.Infrastructure.Events.Store1
{
    public class Store1ReturnCreatedEventHandler : INotificationHandler<Store1ReturnCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _wareHouseContext;
        private readonly ILogger<Store3SaleCreatedEventHandler> _logger;
        public Store1ReturnCreatedEventHandler(WarehouseMongoDbContext warehouseContext, ILogger<Store3SaleCreatedEventHandler> logger)
        {
            _wareHouseContext = warehouseContext;
            _logger = logger;
        }
        
        
        public async Task Handle(Store1ReturnCreatedEvent notification,  CancellationToken cancellationToken)
        {
            var Return = notification._Return;



            var Document = new ReturnDocument
            {
                RelationalId = Return.Id,
                ProductId = Return.ProductId,
                SaleId = Return.SaleId,
                CustomerName = Return.CustomerName,
                CustomerPhone = Return.CustomerPhone,
                Quantity = Return.Quantity,
                RefundAmount = Return.RefundAmount,
                ReturnReason = Return.ReturnReason,
                CreatedDate = Return.CreatedDate,
                UpdatedDate = DateTime.UtcNow,
               
            };

            var salesCollection = _wareHouseContext.Database.GetCollection<SaleDocument>("Store1Sales");

            var sale = await salesCollection.Find(x => x.RelationalId == Return.SaleId).FirstOrDefaultAsync(cancellationToken);


            var stockCollection = _wareHouseContext.Database.GetCollection<StockDocument>("Store1Stocks");

            var stock = await stockCollection.Find(x => x.RelationalId == sale.ProductId).FirstOrDefaultAsync(cancellationToken);
            if (sale != null && stock != null)
            {
                sale.Quantity -= Document.Quantity;
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

            var collection = _wareHouseContext.Database.GetCollection<ReturnDocument>("Store1Returns");
            await collection.InsertOneAsync(Document, cancellationToken: cancellationToken);
            _logger.LogInformation("İade kaydedildi: SaleId={ProductId}, Quantity={Quantity}", sale?.Id, Return.Quantity);
        }

        
    }
}
