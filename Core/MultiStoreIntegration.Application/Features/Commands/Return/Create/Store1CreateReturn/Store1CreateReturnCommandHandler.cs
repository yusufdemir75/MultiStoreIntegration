using MediatR;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Return;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Sale;
using MultiStoreIntegration.Application.Features.Commands.Return.Create.CreateReturn;
using MultiStoreIntegration.Domain.Events;
using MultiStoreIntegration.Domain.Events.Store1;

namespace MultiStoreIntegration.Application.Features.Commands.Store1Returns
{
    public class Store1CreateReturnCommandHandler : IRequestHandler<Store1CreateReturnCommandRequest, Store1CreateReturnCommandResponse>
    {
        private readonly Store1IReturnWriteRepository _store1ReturnWriteRepository;
        private readonly Store1ISaleReadRepository _store1SaleReadRepository;
        private readonly IMediator _mediator;

        public Store1CreateReturnCommandHandler(
            Store1IReturnWriteRepository store1ReturnWriteRepository,
            Store1ISaleReadRepository store1SaleReadRepository,
            IMediator mediator)
        {
            _store1ReturnWriteRepository = store1ReturnWriteRepository;
            _store1SaleReadRepository = store1SaleReadRepository;
            _mediator = mediator;
        }

        public async Task<Store1CreateReturnCommandResponse> Handle(Store1CreateReturnCommandRequest request, CancellationToken cancellationToken)
        {
            var sale = await _store1SaleReadRepository.GetByIdAsync(request.SaleId);
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

            await _store1ReturnWriteRepository.AddAsync(Return);
            await _store1ReturnWriteRepository.SaveAsync();
            await _mediator.Publish(new Store1ReturnCreatedEvent(Return), cancellationToken);



            return new Store1CreateReturnCommandResponse
            {
                Success = true,
                Message = "İade işlemi başarıyla tamamlandı."
            };
        }
    }
}
