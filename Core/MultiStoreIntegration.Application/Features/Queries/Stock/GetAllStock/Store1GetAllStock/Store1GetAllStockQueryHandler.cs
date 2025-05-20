using MediatR;
using MultiStoreIntegration.Application.DTOs.StockDtos.Store1StockDto;
using MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store1GetAllStock;
using MultiStoreIntegration.Application.Repositories.Store1;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;
using MultiStoreIntegration.Domain.Entities;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetAll.Store1GetAll
{
    public class Store1GetAllStockQueryHandler : IRequestHandler<Store1GetAllStockQueryRequest, Store1GetAllStockQueryResponse>
    {
        private readonly Store1IStockReadRepository _store1StockReadRepository;

        public Store1GetAllStockQueryHandler(Store1IStockReadRepository store1StockReadRepository)
        {
            _store1StockReadRepository = store1StockReadRepository;
        }

        public async Task<Store1GetAllStockQueryResponse> Handle(Store1GetAllStockQueryRequest request, CancellationToken cancellationToken)
        {
            var stocks = await _store1StockReadRepository.GetAllAsync();

            if (stocks == null || !stocks.Any())
            {
                return new Store1GetAllStockQueryResponse
                {
                    Success = false,
                    Message = "Stok verisi bulunamadı.",
                    Store1Stocks = new List<Store1StockDto>()
                };
            }

            var stockDtos = stocks.Select(stock => new Store1StockDto
            {
                Id = stock.Id,
                ProductCode = stock.ProductCode,
                Category = stock.Category,
                ProductName = stock.ProductName,
                Size = stock.Size,
                Color = stock.Color,
                Quantity = stock.Quantity,
                UnitPrice = stock.UnitPrice
            }).ToList();

            return new Store1GetAllStockQueryResponse
            {
                Success = true,
                Message = "Stok verileri başarıyla getirildi.",
                Store1Stocks = stockDtos
            };
        }
    }
}
