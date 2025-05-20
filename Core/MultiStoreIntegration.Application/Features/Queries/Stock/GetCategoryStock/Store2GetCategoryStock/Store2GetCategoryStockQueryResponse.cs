using MultiStoreIntegration.Application.DTOs.StockDtos.Store2StockDto;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetCategoryStock.Store2GetCategoryStock
{
    public class Store2GetCategoryStockQueryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Store2CategoryStockDto> CategoryStocks { get; set; }
    }
}
