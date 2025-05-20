using MultiStoreIntegration.Application.DTOs.StockDtos.Store3StockDto;
using System.Collections.Generic;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetCategoryStock.Store3GetCategoryStock
{
    public class Store3GetCategoryStockQueryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Store3CategoryStockDto> CategoryStocks { get; set; }
    }
}
