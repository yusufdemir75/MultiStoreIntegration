using MediatR;
using MultiStoreIntegration.Application.Repositories.Store3;
using MultiStoreIntegration.Domain.MongoDocuments;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Stock.Store3CreateStock
{
    public class Store3CreateStockCommandHandler : IRequestHandler<Store3CreateStockCommandRequest, Store3CreateStockCommandResponse>
    {
        private readonly Store3IWriteRepository<StockDocument> _writeRepository;

        public Store3CreateStockCommandHandler(Store3IWriteRepository<StockDocument> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<Store3CreateStockCommandResponse> Handle(Store3CreateStockCommandRequest request, CancellationToken cancellationToken)
        {
            // Yeni stok oluşturma
            var newStock = new StockDocument
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

            // Sonuç döndürelim
            return new Store3CreateStockCommandResponse
            {
                Success = success,
                Message = success ? "Stock successfully created in Store3 MongoDB." : "Failed to create stock in Store3 MongoDB."
            };
        }
    }
}
