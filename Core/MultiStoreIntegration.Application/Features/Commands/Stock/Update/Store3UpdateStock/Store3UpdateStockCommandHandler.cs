using MediatR;
using MongoDB.Driver;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Stock.Update.Store3UpdateStock
{
    public class Store3UpdateStockCommandHandler : IRequestHandler<Store3UpdateStockCommandRequest, Store3UpdateStockCommandResponse>
    {
        private readonly Store3IStockReadRepository _stockReadRepository;
        private readonly Store3IStockWriteRepository _stockWriteRepository;

        public Store3UpdateStockCommandHandler(Store3IStockReadRepository stockReadRepository, Store3IStockWriteRepository stockWriteRepository)
        {
            _stockReadRepository = stockReadRepository;
            _stockWriteRepository = stockWriteRepository;
        }

        public async Task<Store3UpdateStockCommandResponse> Handle(Store3UpdateStockCommandRequest request, CancellationToken cancellationToken)
        {
            // Ürünü ProductCode'a göre bul
            var existingStock = await _stockReadRepository.GetSingleAsync(x => x.ProductCode == request.ProductCode);

            if (existingStock == null)
            {
                return new Store3UpdateStockCommandResponse
                {
                    Success = false,
                    Message = "Güncellenecek stok bulunamadı."
                };
            }

            // Alanları güncelle
            existingStock.Category = request.Category;
            existingStock.ProductName = request.ProductName;
            existingStock.Size = request.Size;
            existingStock.Color = request.Color;
            existingStock.Quantity = request.Quantity;
            existingStock.UnitPrice = request.UnitPrice;
            existingStock.UpdatedDate = DateTime.UtcNow;

            // Güncelleme işlemini yap
            var result = await _stockWriteRepository.UpdateAsync(existingStock);

            return new Store3UpdateStockCommandResponse
            {
                Success = result,
                Message = result ? "Stok başarıyla güncellendi." : "Stok güncellenemedi."
            };
        }
    }
}
