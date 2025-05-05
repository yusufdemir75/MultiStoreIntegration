using MediatR;
using MongoDB.Driver;
using MultiStoreIntegration.Domain.Events.Store1;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;

namespace MultiStoreIntegration.Infrastructure.Events.Store1
{
    public class Store1ReturnCreatedEventHandler : INotificationHandler<Store1ReturnCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _wareHouseContext;

        public Store1ReturnCreatedEventHandler(WarehouseMongoDbContext warehouseContext)
        {
            _wareHouseContext = warehouseContext;
        }

        
        public async Task Handle(Store1ReturnCreatedEvent notification,  CancellationToken cancellationToken)
        {
            var Return = notification._Return;


            //Sale'de Değişiklik yapılırsa kullanılacak
           /* var saleCollection = _wareHouseContext.Database.GetCollection<SaleDocument>("Store1Sales");
            var sale = await saleCollection.Find(x => x.RelationalId == Return.SaleId).FirstOrDefaultAsync(cancellationToken);*/


            var Document = new Domain.MongoDocuments.ReturnDocument
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

            var collection = _wareHouseContext.Database.GetCollection<Domain.MongoDocuments.ReturnDocument>("Store1Returns");
            await collection.InsertOneAsync(Document, cancellationToken: cancellationToken);
        }

        
    }
}
