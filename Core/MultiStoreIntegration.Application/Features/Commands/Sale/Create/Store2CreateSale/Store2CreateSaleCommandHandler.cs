using MediatR;
using MultiStoreIntegration.Application.Common.Exceptions;
using MultiStoreIntegration.Application.Common.Validations;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Sale;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Stock;
using MultiStoreIntegration.Domain.Events.Store2;
using MultiStoreIntegration.Infrastructure.Exceptions;


namespace MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store2CreateSale
{
    public class Store2CreateSaleCommandHandler : IRequestHandler<Store2CreateSaleCommandRequest, Store2CreateSaleCommandResponse>
    {
        private readonly Store2ISaleWriteRepository _store2SaleWriteRepository;
        private readonly Store2IStockReadRepository _store2StockReadRepository;
        private readonly Store2IStockWriteRepository _store2StockWriteRepository;
        private readonly IMediator _mediator;

        public Store2CreateSaleCommandHandler(Store2ISaleWriteRepository store2SaleWriteRepository, Store2IStockReadRepository store2StockReadRepository, Store2IStockWriteRepository store2StockWriteRepository, IMediator mediator)
        {
            _store2SaleWriteRepository = store2SaleWriteRepository;
            _store2StockReadRepository = store2StockReadRepository;
            _store2StockWriteRepository = store2StockWriteRepository;
            _mediator = mediator;
        }

        public async Task<Store2CreateSaleCommandResponse> Handle(Store2CreateSaleCommandRequest request, CancellationToken cancellationToken)
        {  // Validasyon
            var validator = new Store2CreateSaleValidation();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new Store2CreateSaleException(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            // Stok kontrolü
            var stock = await _store2StockReadRepository.GetByIdAsync(request.ProductId);
            if (stock == null)
                throw new Store2CreateSaleException("Stokta böyle bir ürün bulunamadı.");

            if (stock.Quantity < request.Quantity)
                throw new Store2CreateSaleException("Stoktaki ürün miktarı yetersiz.");

            // Stok güncelle
            stock.Quantity -= request.Quantity;
            stock.UpdatedDate = DateTime.UtcNow;

            // Satış oluştur
            var sale = new Domain.Entities.Sale
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                TotalPrice = request.Quantity * stock.UnitPrice,
                CustomerName = request.CustomerName,
                CustomerPhone = request.CustomerPhone,
                PaymentMethod = request.PaymentMethod,
                CreatedDate = DateTime.UtcNow
            };

            await _store2SaleWriteRepository.AddAsync(sale);
            await _store2StockWriteRepository.SaveAsync();
            await _store2SaleWriteRepository.SaveAsync();
            await _mediator.Publish(new Store2SaleCreatedEvent(sale), cancellationToken);


            return new Store2CreateSaleCommandResponse
            {
                Success = true,
                Message = "Satış başarıyla kaydedildi."
            };
        }
    }
}
