using MediatR;
using MultiStoreIntegration.Application.DTOs.StockDtos.Store3StockDto;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetCategoryStock.Store3GetCategoryStock
{
    public class Store3GetCategoryStockQueryHandler : IRequestHandler<Store3GetCategoryStockQueryRequest, Store3GetCategoryStockQueryResponse>
    {
        private readonly Store3IStockReadRepository _stockReadRepository;

        public Store3GetCategoryStockQueryHandler(Store3IStockReadRepository stockReadRepository)
        {
            _stockReadRepository = stockReadRepository;
        }

        public async Task<Store3GetCategoryStockQueryResponse> Handle(Store3GetCategoryStockQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _stockReadRepository.GetTotalStockPerCategoryAsync();

            var response = new Store3GetCategoryStockQueryResponse
            {
                Success = true,
                Message = "Kategori bazlı stoklar MongoDB'den getirildi",
                CategoryStocks = data.Select(d => new Store3CategoryStockDto
                {
                    Category = d.Category,
                    TotalQuantity = d.TotalQuantity
                }).ToList()
            };

            return response;
        }
    }
}
