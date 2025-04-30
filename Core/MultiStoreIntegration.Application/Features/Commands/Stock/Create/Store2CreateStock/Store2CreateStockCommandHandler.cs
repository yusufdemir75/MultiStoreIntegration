using MediatR;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store1CreateStock;
using MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store3CreateStock;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Stock;
using MultiStoreIntegration.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store2CreateStock
{
    public class Store2CreateStockCommandHandler : IRequestHandler<Store2CreateStockCommandRequest, Store2CreateStockCommandResponse>
    {
        private readonly Store2IStockWriteRepository _store2StockWriteRepository;
        private readonly Store2IStockReadRepository _store2StockReadRepository;
        private readonly IMediator _mediator;

        public Store2CreateStockCommandHandler(Store2IStockWriteRepository store2StockWriteRepository, Store2IStockReadRepository store2StockReadRepository, IMediator mediator)
        {
            _store2StockWriteRepository = store2StockWriteRepository;
            _store2StockReadRepository = store2StockReadRepository;
            _mediator = mediator;
        }


        public async Task<Store2CreateStockCommandResponse> Handle(Store2CreateStockCommandRequest request, CancellationToken cancellationToken)
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
            await _store2StockWriteRepository.AddAsync(stock);
            await _store2StockWriteRepository.SaveAsync();

            // Event yayılıyor
            await _mediator.Publish(new Store2StockCreatedEvent(stock));

            // Başarılı dönüş
            return new Store2CreateStockCommandResponse
            {
                Success = true,
                Message = "Mağaza verisi oluşturuldu"
            };
        }
    }
}
