using MediatR;
using MultiStoreIntegration.Application.DTOs.StockDtos.Store2StockDto;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Stock;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetCategoryStock.Store2GetCategoryStock
{
    public class Store2GetCategoryStockQueryHandler : IRequestHandler<Store2GetCategoryStockQueryRequest, Store2GetCategoryStockQueryResponse>
    {
        private readonly Store2IStockReadRepository _stockReadRepository;

        public Store2GetCategoryStockQueryHandler(Store2IStockReadRepository stockReadRepository)
        {
            _stockReadRepository = stockReadRepository;
        }

        public async Task<Store2GetCategoryStockQueryResponse> Handle(Store2GetCategoryStockQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _stockReadRepository.GetTotalStockPerCategoryAsync();

            var response = new Store2GetCategoryStockQueryResponse
            {
                Success = true,
                Message = "Kategori bazlı stoklar getirildi",
                CategoryStocks = data.Select(d => new Store2CategoryStockDto
                {
                    Category = d.Category,
                    TotalQuantity = d.TotalQuantity
                }).ToList()
            };

            return response;
        }
    }
}
