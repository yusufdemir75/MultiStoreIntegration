using MediatR;
using MultiStoreIntegration.Application.DTOs.SaleDtos.Store2SaleDto;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Sale;

namespace MultiStoreIntegration.Application.Features.Queries.Sale.GetAllSale.Store2GetAllSale
{
    public class Store2GetAllSaleQueryHandler : IRequestHandler<Store2GetAllSaleQueryRequest, Store2GetAllSaleQueryResponse>
    {
        private readonly Store2ISaleReadRepository _store2SaleReadRepository;


        public Store2GetAllSaleQueryHandler(Store2ISaleReadRepository store2ISaleReadRepository)
        {
            _store2SaleReadRepository = store2ISaleReadRepository;
        }


        public async Task<Store2GetAllSaleQueryResponse> Handle(Store2GetAllSaleQueryRequest request, CancellationToken cancellationToken)
        {
            var sales = await _store2SaleReadRepository.GetAllAsync();

            if (sales == null || !sales.Any())
            {
                return new Store2GetAllSaleQueryResponse
                {
                    Success = false,
                    Message = "Stok verisi bulunamadı.",
                    Store2Sales = new List<Store2SaleDto>()
                };
            }

            var saleDtos = sales.Select(sale => new Store2SaleDto
            {
                Id = sale.Id,
                Quantity = sale.Quantity,
                CustomerName = sale.CustomerName,
                CustomerPhone = sale.CustomerPhone,
                PaymentMethod = sale.PaymentMethod,
                TotalPrice = sale.TotalPrice,
            }).ToList();

            return new Store2GetAllSaleQueryResponse
            {
                Success = true,
                Message = "Stok verileri başarıyla getirildi.",
                Store2Sales = saleDtos
            };
        }
    }
}
