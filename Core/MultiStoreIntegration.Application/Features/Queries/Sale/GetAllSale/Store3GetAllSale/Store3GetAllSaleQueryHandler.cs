using MediatR;
using MultiStoreIntegration.Application.DTOs.SaleDtos.Store3SaleDto;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Sale;

namespace MultiStoreIntegration.Application.Features.Queries.Sale.GetAllSale.Store3GetAllSale
{
    public class Store3GetAllSaleQueryHandler : IRequestHandler<Store3GetAllSaleQueryRequest, Store3GetAllSaleQueryResponse>
    {
        private readonly Store3ISaleReadRepository _saleReadRepository;

        public Store3GetAllSaleQueryHandler(Store3ISaleReadRepository saleReadRepository)
        {
            _saleReadRepository = saleReadRepository;
        }

        public async Task<Store3GetAllSaleQueryResponse> Handle(Store3GetAllSaleQueryRequest request, CancellationToken cancellationToken)
        {
            var sales = await _saleReadRepository.GetAllAsync();

            var saleDtos = sales.Select(sale => new Store3SaleDto
            {
                Id=sale.Id.ToString(),
                CustomerName=sale.CustomerName,
                CustomerPhone=sale.CustomerPhone,
                PaymentMethod=sale.PaymentMethod,
                Quantity=sale.Quantity,
                TotalPrice = sale.TotalPrice,
                CreatedDate = sale.CreatedDate,
                UpdatedDate = sale.UpdatedDate, 
                Size = sale.Size,
                Category = sale.Category,
                Color = sale.Color,
                ProductName = sale.ProductName,
            }).ToList();

            return new Store3GetAllSaleQueryResponse
            {
                Success = true,
                Message = "Tüm satış verileri başarıyla getirildi.",
                Store3Sales = saleDtos
            };
        }
    }
}
