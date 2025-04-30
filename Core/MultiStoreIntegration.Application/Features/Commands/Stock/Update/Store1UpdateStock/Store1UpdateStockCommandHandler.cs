using MediatR;
using MultiStoreIntegration.Application.Repositories;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;
using MultiStoreIntegration.Domain.Entities;

namespace MultiStoreIntegration.Application.Features.Commands.Stock.Update.Store1UpdateStock
{
    public class Store1UpdateStockCommandHandler : IRequestHandler<Store1UpdateStockCommandRequest, Store1UpdateStockCommandResponse>
    {
        private readonly Store1IStockWriteRepository _stockWriteRepository;
        private readonly Store1IStockReadRepository _stockReadRepository;

        public Store1UpdateStockCommandHandler(Store1IStockWriteRepository stockWriteRepository, Store1IStockReadRepository stockReadRepository)
        {
            _stockWriteRepository = stockWriteRepository;
            _stockReadRepository = stockReadRepository;
        }

        public async Task<Store1UpdateStockCommandResponse> Handle(Store1UpdateStockCommandRequest request, CancellationToken cancellationToken)
        {
            var stock = await _stockReadRepository.GetByIdAsync(request.Id);

            if (stock == null)
            {
                return new Store1UpdateStockCommandResponse
                {
                    Success = false,
                    Message = "Stok kaydı bulunamadı."
                };
            }

            if (!string.IsNullOrWhiteSpace(request.ProductCode))
                stock.ProductCode = request.ProductCode;

            if (!string.IsNullOrWhiteSpace(request.Category))
                stock.Category = request.Category;

            if (!string.IsNullOrWhiteSpace(request.ProductName))
                stock.ProductName = request.ProductName;

            if (!string.IsNullOrWhiteSpace(request.Size))
                stock.Size = request.Size;

            if (!string.IsNullOrWhiteSpace(request.Color))
                stock.Color = request.Color;

            if (request.Quantity != default(int)) 
                stock.Quantity = request.Quantity;

            if (request.UnitPrice != default(float)) 
                stock.UnitPrice = request.UnitPrice;

            stock.UpdatedDate = DateTime.UtcNow;

            await _stockWriteRepository.SaveAsync();

            return new Store1UpdateStockCommandResponse
            {
                Success = true,
                Message = "Stok başarıyla güncellendi."
            };
        }
    }
}
