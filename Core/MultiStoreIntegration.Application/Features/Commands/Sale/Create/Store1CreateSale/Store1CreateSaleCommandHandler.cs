using MediatR;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Sale;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;
using MultiStoreIntegration.Domain.Events;
using MultiStoreIntegration.Infrastructure.Exceptions;
using MultiStoreIntegration.Infrastructure.Validations;


namespace MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store1CreateSale
{
    public class Store1CreateSaleCommandHandler : IRequestHandler<Store1CreateSaleCommandRequest, Store1CreateSaleCommandResponse>
    {
        private readonly Store1ISaleWriteRepository _store1SaleWriteRepository;
        private readonly Store1IStockReadRepository _store1StockReadRepository;
        private readonly Store1IStockWriteRepository _store1StockWriteRepository;
        private readonly IMediator _mediator;

        public Store1CreateSaleCommandHandler(Store1ISaleWriteRepository store1SaleWriteRepository, Store1IStockReadRepository store1StockReadRepository, Store1IStockWriteRepository store1StockWriteRepository, IMediator mediator)
        {
            _store1SaleWriteRepository = store1SaleWriteRepository;
            _store1StockReadRepository = store1StockReadRepository;
            _store1StockWriteRepository = store1StockWriteRepository;
            _mediator = mediator;
        }

        public async Task<Store1CreateSaleCommandResponse> Handle(Store1CreateSaleCommandRequest request, CancellationToken cancellationToken)
        {  // Validasyon
            var validator = new Store1CreateSaleValidation();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new Store1CreateSaleException(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            // Stok kontrolü
            var stock = await _store1StockReadRepository.GetByIdAsync(request.ProductId);
            if (stock == null)
                throw new Store1CreateSaleException("Stokta böyle bir ürün bulunamadı.");

            if (stock.Quantity < request.Quantity)
                throw new Store1CreateSaleException("Stoktaki ürün miktarı yetersiz.");

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

            await _store1SaleWriteRepository.AddAsync(sale);
            await _store1StockWriteRepository.SaveAsync();
            await _store1SaleWriteRepository.SaveAsync();
            await _mediator.Publish(new Store1SaleCreatedEvent(sale), cancellationToken);


            return new Store1CreateSaleCommandResponse
            {
                Success = true,
                Message = "Satış başarıyla kaydedildi."
            };
        }
    }
}
