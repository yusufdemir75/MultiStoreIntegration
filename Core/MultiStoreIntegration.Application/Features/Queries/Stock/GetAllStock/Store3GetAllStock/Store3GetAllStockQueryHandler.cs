using MediatR;
using MultiStoreIntegration.Application.DTOs.StockDtos.Store3StockDto;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store3GetAllStock
{
    public class Store3GetAllStockQueryHandler : IRequestHandler<Store3GetAllStockQueryRequest, Store3GetAllStockQueryResponse>
    {
        private readonly Store3IStockReadRepository _stockReadRepository;

        public Store3GetAllStockQueryHandler(Store3IStockReadRepository stockReadRepository)
        {
            _stockReadRepository = stockReadRepository;
        }

        public async Task<Store3GetAllStockQueryResponse> Handle(Store3GetAllStockQueryRequest request, CancellationToken cancellationToken)
        {
            var stocks = await _stockReadRepository.GetAllAsync();

            var stockDtos = stocks.Select(stock => new Store3StockDto
            {
                Id = stock.Id.ToString(),
                ProductCode = stock.ProductCode,
                Category = stock.Category,
                ProductName = stock.ProductName,
                Size = stock.Size,
                Color = stock.Color,
                Quantity = stock.Quantity,
                UnitPrice = stock.UnitPrice,
                CreatedDate = stock.CreatedDate,
                UpdatedDate = stock.UpdatedDate
            }).ToList();

            return new Store3GetAllStockQueryResponse
            {
                Success = true,
                Message = "Tüm stok verileri başarıyla getirildi.",
                Stocks = stockDtos
            };
        }
    }
}
