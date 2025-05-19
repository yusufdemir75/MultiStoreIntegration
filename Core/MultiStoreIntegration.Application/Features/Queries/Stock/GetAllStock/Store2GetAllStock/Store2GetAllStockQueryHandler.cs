using MediatR;
using MultiStoreIntegration.Application.DTOs;
using MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store1GetAllStock;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store2GetAllStock
{
    public class Store2GetAllStockQueryHandler : IRequestHandler<Store2GetAllStockQueryRequest, Store2GetAllStockQueryResponse>
    {
        private readonly Store2IStockReadRepository _store2StockReadRepository;

        public Store2GetAllStockQueryHandler(Store2IStockReadRepository store2StockReadRepository)
        {
            _store2StockReadRepository = store2StockReadRepository;
        }
        public async Task<Store2GetAllStockQueryResponse> Handle(Store2GetAllStockQueryRequest request, CancellationToken cancellationToken)
        {
            var stocks = await _store2StockReadRepository.GetAllAsync();

            if (stocks == null || !stocks.Any())
            {
                return new Store2GetAllStockQueryResponse
                {
                    Success = false,
                    Message = "Stok verisi bulunamadı.",
                    Store2Stocks = new List<Store2StockDto>()
                };
            }

            var stockDtos = stocks.Select(stock => new Store2StockDto
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

            return new Store2GetAllStockQueryResponse
            {
                Success = true,
                Message = "Stok verileri başarıyla getirildi.",
                Store2Stocks = stockDtos
            };
        }
    }
}
