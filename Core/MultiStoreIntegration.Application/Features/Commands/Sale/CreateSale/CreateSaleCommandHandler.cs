using MediatR;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Sale.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommandRequest, CreateSaleCommandResponse>
    {
        private readonly Store1ISaleWriteRepository _store1SaleWriteRepository;

        public CreateSaleCommandHandler(Store1ISaleWriteRepository store1SaleWriteRepository)
        {
            _store1SaleWriteRepository = store1SaleWriteRepository;
        }

        public async Task<CreateSaleCommandResponse> Handle(CreateSaleCommandRequest request, CancellationToken cancellationToken)
        {
            var sale = new Domain.Entities.Sale
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                TotalPrice = request.TotalPrice,
                CustomerName = request.CustomerName,
                CustomerPhone = request.CustomerPhone,
                PaymentMethod = request.PaymentMethod,
                CreatedDate = DateTime.UtcNow
            };

            await _store1SaleWriteRepository.AddAsync(sale);
            await _store1SaleWriteRepository.SaveAsync();

            return new CreateSaleCommandResponse
            {
                Success = true,
                Message = "Satış kaydı başarıyla oluşturuldu"
            };
        }
    }
}
