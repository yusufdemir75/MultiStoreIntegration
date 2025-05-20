using MediatR;
using MultiStoreIntegration.Application.Features.Commands.Stock.Update.Store2UpdateStock;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Stock;
using MultiStoreIntegration.Domain.Events.Store1;
using MultiStoreIntegration.Domain.Events.Store2;

namespace MultiStoreIntegration.Application.Features.Commands.Stock.Update.Store1UpdateStock
{
    public class Store2UpdateStockCommandHandler : IRequestHandler<Store2UpdateStockCommandRequest, Store2UpdateStockCommandResponse>
    {
        private readonly Store2IStockWriteRepository _stockWriteRepository;

        private readonly Store2IStockReadRepository _stockReadRepository;

        private readonly IMediator _mediator;

        public Store2UpdateStockCommandHandler(Store2IStockWriteRepository stockWriteRepository , Store2IStockReadRepository stockReadRepository, IMediator mediator )
        {
            _stockReadRepository = stockReadRepository;
            _stockWriteRepository = stockWriteRepository;
            _mediator = mediator;

        }

        public async Task<Store2UpdateStockCommandResponse> Handle(Store2UpdateStockCommandRequest request, CancellationToken cancellationToken)
        {
            var stock = await _stockReadRepository.GetByIdAsync(request.Id);

            if (stock == null)
            {
                return new Store2UpdateStockCommandResponse
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
            await _mediator.Publish(new Store2StockUpdatedEvent(stock));

            return new Store2UpdateStockCommandResponse
            {
                Success = true,
                Message = "Stok başarıyla güncellendi."
            };
        }
    }
}
