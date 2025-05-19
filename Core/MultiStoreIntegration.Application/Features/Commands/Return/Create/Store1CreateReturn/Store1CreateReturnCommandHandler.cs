using MediatR;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Return;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Sale;
using MultiStoreIntegration.Application.Features.Commands.Return.Create.CreateReturn;
using MultiStoreIntegration.Domain.Events;
using MultiStoreIntegration.Domain.Events.Store1;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Sale;
using MultiStoreIntegration.Application.Features.Commands.Return.Create.Store2CreateReturn;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Stock;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;

namespace MultiStoreIntegration.Application.Features.Commands.Store1Returns
{
    public class Store1CreateReturnCommandHandler : IRequestHandler<Store1GetAllCommandRequest, Store1CreateReturnCommandResponse>
    {
        private readonly Store1IReturnWriteRepository _store1ReturnWriteRepository;
        private readonly Store1ISaleReadRepository _store1SaleReadRepository;
        private readonly Store1ISaleWriteRepository _store1SaleWriteRepository;
        private readonly Store1IStockReadRepository _store1IStockReadRepository;

        private readonly IMediator _mediator;

        public Store1CreateReturnCommandHandler(
            Store1IReturnWriteRepository store1ReturnWriteRepository,
            Store1ISaleWriteRepository store1SaleWriteRepository,
            Store1ISaleReadRepository store1SaleReadRepository,
            Store1IStockReadRepository store1StockReadRepository,
            IMediator mediator)
        {
            _store1ReturnWriteRepository = store1ReturnWriteRepository;
            _store1SaleReadRepository = store1SaleReadRepository;
            _mediator = mediator;
            _store1SaleWriteRepository = store1SaleWriteRepository;
            _store1IStockReadRepository = store1StockReadRepository;
        }

        public async Task<Store1CreateReturnCommandResponse> Handle(Store1GetAllCommandRequest request, CancellationToken cancellationToken)
        {

            var sale = await _store1SaleReadRepository.GetByIdAsync(request.SaleId);
            var stock = await _store1IStockReadRepository.GetByIdAsync(sale.ProductId);
            if (sale == null)
            {
                return new Store1CreateReturnCommandResponse
                {
                    Success = false,
                    Message = "Satış bilgisi bulunamadı."
                };
            }

            float unitPrice = (int)sale.TotalPrice / sale.Quantity;
            float returnTotalPrice = unitPrice * request.Quantity;

            var Return = new Domain.Entities.Return
            {
                Id = Guid.NewGuid(),
                ProductId = sale.ProductId,
                SaleId = request.SaleId,
                CustomerName = sale.CustomerName,
                CustomerPhone = sale.CustomerPhone,
                Quantity = request.Quantity,
                ReturnReason = request.ReturnReason,
                RefundAmount = (int)returnTotalPrice,
                CreatedDate = DateTime.UtcNow
            };

            sale.Quantity -= request.Quantity;
            sale.TotalPrice = sale.Quantity * stock.UnitPrice;


            await _store1SaleWriteRepository.SaveAsync();
            await _store1ReturnWriteRepository.AddAsync(Return);
            await _store1ReturnWriteRepository.SaveAsync();
            await _mediator.Publish(new Store1ReturnCreatedEvent(Return), cancellationToken);

            //mongodbde de işlem yapmak için oranın satış tablosuna veri eklemek gerekiyor

            return new Store1CreateReturnCommandResponse
            {
                Success = true,
                Message = "İade işlemi başarıyla tamamlandı."
            };
        }
    }
}
