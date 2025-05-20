using MediatR;
using MultiStoreIntegration.Application.DTOs;
using MultiStoreIntegration.Application.DTOs.StockDtos.Store1StockDto;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetCategoryStock.Store1GetCategoryStock
{
    public class Store1GetCategoryStockQueryHandler : IRequestHandler<Store1GetCategoryStockQueryRequest, Store1GetCategoryStockQueryResponse>
    {
        private readonly Store1IStockReadRepository _stockReadRepository;

        public Store1GetCategoryStockQueryHandler(Store1IStockReadRepository stockReadRepository)
        {
            _stockReadRepository = stockReadRepository;
        }

        public async Task<Store1GetCategoryStockQueryResponse> Handle(Store1GetCategoryStockQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _stockReadRepository.GetTotalStockPerCategoryAsync();

            var response = new Store1GetCategoryStockQueryResponse
            {
                Success = true,
                Message = "Kategori bazlı stoklar getirildi",
                CategoryStocks = data.Select(d => new Store1CategoryStockDto
                {
                    Category = d.Category,
                    TotalQuantity = d.TotalQuantity
                }).ToList()
            };

            return response;
        }
    }
}
