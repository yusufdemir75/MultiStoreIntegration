using MediatR;

using MultiStoreIntegration.Application.Repositories.Store2.Store2Return;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Sale;
using MultiStoreIntegration.Domain.Events.Store2;

namespace MultiStoreIntegration.Application.Features.Commands.Return.Create.Store2CreateReturn
{
    public class Store2CreateReturnCommandHandler : IRequestHandler<Store2CreateReturnCommandRequest, Store2CreateReturnCommandResponse>
    {
        private readonly Store2IReturnWriteRepository _store2ReturnWriteRepository;
        private readonly Store2ISaleReadRepository _store2SaleReadRepository;
        private readonly IMediator _mediator;

        public Store2CreateReturnCommandHandler(Store2IReturnWriteRepository store2ReturnWriteRepository,
            Store2ISaleReadRepository store2SaleReadRepository,
            IMediator mediator)
        {
            _mediator = mediator;
            _store2ReturnWriteRepository = store2ReturnWriteRepository;
            _store2SaleReadRepository = store2SaleReadRepository;
        }
        public async Task<Store2CreateReturnCommandResponse> Handle(Store2CreateReturnCommandRequest request, CancellationToken cancellationToken)
        {
            var sale = await _store2SaleReadRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
            {
                return new Store2CreateReturnCommandResponse
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

            await _store2ReturnWriteRepository.AddAsync(Return);
            await _store2ReturnWriteRepository.SaveAsync();
            await _mediator.Publish(new Store2ReturnCreatedEvent(Return), cancellationToken);



            return new Store2CreateReturnCommandResponse
            {
                Success = true,
                Message = "İade işlemi başarıyla tamamlandı."
            };
        }
    }
}
