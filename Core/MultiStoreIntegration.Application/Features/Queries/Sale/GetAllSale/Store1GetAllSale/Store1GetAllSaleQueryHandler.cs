using MediatR;
using MultiStoreIntegration.Application.DTOs.SaleDtos.Store1SaleDto;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Sale;

namespace MultiStoreIntegration.Application.Features.Queries.Sale.GetAllSale.Store1GetAllSale
{
    public class Store1GetAllSaleQueryHandler : IRequestHandler<Store1GetAllSaleQueryRequest, Store1GetAllSaleQueryResponse>
    {

        private readonly Store1ISaleReadRepository _store1SaleReadRepository;


        public Store1GetAllSaleQueryHandler(Store1ISaleReadRepository store1ISaleReadRepository)
        {
            _store1SaleReadRepository = store1ISaleReadRepository;
        }


        public async Task<Store1GetAllSaleQueryResponse> Handle(Store1GetAllSaleQueryRequest request, CancellationToken cancellationToken)
        {
            var sales = await _store1SaleReadRepository.GetAllAsync();

            if (sales == null || !sales.Any())
            {
                return new Store1GetAllSaleQueryResponse
                {
                    Success = false,
                    Message = "Stok verisi bulunamadı.",
                    Store1Sales = new List<Store1SaleDto>()
                };
            }

            var saleDtos = sales.Select(sale => new Store1SaleDto
            {
                Id = sale.Id,
                Quantity = sale.Quantity,
                CustomerName = sale.CustomerName,
                CustomerPhone = sale.CustomerPhone,
                PaymentMethod = sale.PaymentMethod,
                TotalPrice = sale.TotalPrice,
            }).ToList();

            return new Store1GetAllSaleQueryResponse
            {
                Success = true,
                Message = "Stok verileri başarıyla getirildi.",
                Store1Sales = saleDtos
            };
        }
    }
}
