
using MultiStoreIntegration.Application.DTOs;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store1GetAllStock
{
    public class Store1GetAllStockQueryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }


        public List<Store1StockDto> Store1Stocks { get; set; }
    }
}
