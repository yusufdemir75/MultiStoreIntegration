using MultiStoreIntegration.Application.DTOs.StockDtos.Store1StockDto;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetCategoryStock.Store1GetCategoryStock
{
    public class Store1GetCategoryStockQueryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Store1CategoryStockDto> CategoryStocks { get; set; }
    }
}
