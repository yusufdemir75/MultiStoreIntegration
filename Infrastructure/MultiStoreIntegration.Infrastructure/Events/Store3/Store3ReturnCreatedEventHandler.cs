using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
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
    public class Store3ReturnCreatedEventHandler : INotificationHandler<Store3ReturnCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;
        private readonly ILogger<Store3SaleCreatedEventHandler> _logger;

        public Store3ReturnCreatedEventHandler(
            WarehouseMongoDbContext warehouseContext,
            ILogger<Store3SaleCreatedEventHandler> logger)
        {
            _warehouseContext = warehouseContext;
            _logger = logger;
        }

        public async Task Handle(Store3ReturnCreatedEvent notification, CancellationToken cancellationToken)
        {
            var Return = notification.Store3ReturnDocument;

            var ReturnDocument = new Store3ReturnDocument
            {
                CreatedDate = Return.CreatedDate,
                SaleId = Return.SaleId,
                CustomerName = Return.CustomerName,
                CustomerPhone = Return.CustomerPhone,
                Id = Return.Id,
                ProductId = Return.ProductId,
                Quantity = Return.Quantity,
                RefundAmount = Return.RefundAmount,
                ReturnReason = Return.ReturnReason,
                UpdatedDate = Return.UpdatedDate,

            };

            var saleCollection = _warehouseContext.Database.GetCollection<Store3SaleDocument>("Store3Sales"); 
            var sale = await saleCollection.Find(x =>  x.Id == Return.SaleId).FirstOrDefaultAsync(cancellationToken);
            if (sale == null) {
                _logger.LogWarning("Satış Bulunamadı : SaleId={Id}", sale?.Id);
                return;
            }

            var stockCollection = _warehouseContext.Database.GetCollection<Store3StockDocument>("Store3Stocks");
            var stock = await stockCollection.Find(x => x.Id == sale.ProductId).FirstOrDefaultAsync(cancellationToken);


            if( sale!= null && stock!= null)
            {
                sale.Quantity -= ReturnDocument.Quantity;
                sale.TotalPrice = sale.Quantity * stock.UnitPrice;

                await saleCollection.ReplaceOneAsync(
                    filter: x => x.Id == sale.Id,
                    replacement: sale,
                    cancellationToken: cancellationToken
                    );

            }
            else
            {
                _logger.LogWarning("Satış Bulunamadı : SaleId={Id}", sale?.Id);

            }



            var returnsCollection = _warehouseContext.Database.GetCollection<Store3ReturnDocument>("Store3Returns");
            await returnsCollection.InsertOneAsync(ReturnDocument, cancellationToken: cancellationToken);

            _logger.LogInformation("İade kaydedildi: SaleId={ProductId}, Quantity={Quantity}", sale?.Id, Return.Quantity);
        }
    }
}
