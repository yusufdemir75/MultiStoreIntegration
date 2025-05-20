using MultiStoreIntegration.Application.DTOs.StockDtos.Store3StockDto;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store3GetAllStock
{
    public class Store3GetAllStockQueryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<Store3StockDto> Stocks { get; set; } = new();
    }
}