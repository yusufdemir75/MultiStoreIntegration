using MediatR;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;
using MultiStoreIntegration.Domain.Entities;
using MultiStoreIntegration.Domain.Events.Store2;
using MultiStoreIntegration.Domain.Events.Store3;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;


namespace MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store3CreateStock
{
    public class Store3CreateStockCommandHandler : IRequestHandler<Store3CreateStockCommandRequest, Store3CreateStockCommandResponse>
    {
        private readonly Store3IStockWriteRepository _writeRepository;
        private readonly IMediator _mediator;

        public Store3CreateStockCommandHandler(Store3IStockWriteRepository writeRepository, IMediator mediator)
        {
            _writeRepository = writeRepository;
            _mediator = mediator;
        }

        public async Task<Store3CreateStockCommandResponse> Handle(Store3CreateStockCommandRequest request, CancellationToken cancellationToken)
        {
            var newStock = new Store3StockDocument
            {
                ProductCode = request.ProductCode,
                Category = request.Category,
                ProductName = request.ProductName,
                Size = request.Size,
                Color = request.Color,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            // Stok verisini MongoDB'ye ekleyelim
            var success = await _writeRepository.AddAsync(newStock);

            await _mediator.Publish(new Store3StockCreatedEvent(newStock));


            // Sonuç döndürelim
            return new Store3CreateStockCommandResponse
            {
                Success = success,
                Message = success ? "Stock successfully created in Store3 MongoDB." : "Failed to create stock in Store3 MongoDB."
            };
        }
    }
}
