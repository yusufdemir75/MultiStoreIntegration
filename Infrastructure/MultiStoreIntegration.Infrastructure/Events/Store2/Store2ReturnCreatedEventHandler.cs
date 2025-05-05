using MediatR;
using MultiStoreIntegration.Domain.Events.Store2;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Infrastructure.Events.Store2
{
    public class Store2ReturnCreatedEventHandler : INotificationHandler<Store2ReturnCreatedEvent>
    {
        private readonly WarehouseMongoDbContext _warehouseContext;

        public Store2ReturnCreatedEventHandler(WarehouseMongoDbContext warehouseContext)
        {
            _warehouseContext = warehouseContext;
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
                UpdatedDate = DateTime.UtcNow // MongoDB'de ayrı güncelleme tarihi tutmak için
            };

            var collection = _warehouseContext.Database.GetCollection<ReturnDocument>("Store2Returns");
            await collection.InsertOneAsync(returnDocument, cancellationToken: cancellationToken);
        }
    }
}
