using MultiStoreIntegration.Application.DTOs.StockDtos.Store2StockDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store2GetAllStock
{
    public class Store2GetAllStockQueryResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public List<Store2StockDto> Store2Stocks { get; set; }
    }
}
