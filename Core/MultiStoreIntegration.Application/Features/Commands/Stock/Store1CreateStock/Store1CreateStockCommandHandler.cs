using MediatR;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;
using MultiStoreIntegration.Domain.Events;


namespace MultiStoreIntegration.Application.Features.Commands.Stock.Store1CreateStock
{
    public class Store1CreateStockCommandHandler : IRequestHandler<Store1CreateStockCommandRequest, Store3CreateStockCommandResponse>
    {
        private readonly Store1IStockWriteRepository _store1StockWriteRepository;
        private readonly Store1IStockReadRepository _store1StockReadRepository;
        private readonly IMediator _mediator;

        public Store1CreateStockCommandHandler(Store1IStockWriteRepository store1StockWriteRepository, Store1IStockReadRepository store1StockReadRepository, IMediator mediator)
        {
            _store1StockWriteRepository = store1StockWriteRepository;
            _store1StockReadRepository = store1StockReadRepository;
            _mediator = mediator;
        }

        public async Task<Store3CreateStockCommandResponse> Handle(Store1CreateStockCommandRequest request, CancellationToken cancellationToken)
        {
            // Yeni stok nesnesi oluşturuluyor
            var stock = new Domain.Entities.Stock
            {
                ProductCode = request.ProductCode,
                Category = request.Category,
                ProductName = request.ProductName,
                Size = request.Size,
                Color = request.Color,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                CreatedDate = request.CreatedDate
            };

            // Stok veri tabanına ekleniyor
            await _store1StockWriteRepository.AddAsync(stock);
            await _store1StockWriteRepository.SaveAsync();

            // Event yayılıyor
            await _mediator.Publish(new Store1StockCreatedEvent(stock));

            // Başarılı dönüş
            return new Store3CreateStockCommandResponse
            {
                Success = true,
                Message = "Mağaza verisi oluşturuldu"
            };
        }
    }
}
