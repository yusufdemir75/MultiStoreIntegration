using MediatR;
using MultiStoreIntegration.Application.DTOs.ReturnDtos.Store2ReturnDto;
using MultiStoreIntegration.Application.Repositories.Store2.Store2Return;

namespace MultiStoreIntegration.Application.Features.Queries.Return.Store2GetAllReturn
{
    public class Store2GetAllReturnQueryHandler : IRequestHandler<Store2GetAllReturnQueryRequest, Store2GetAllReturnQueryResponse>
    {
        private readonly Store2IReturnReadRepository _store2ReturnReadRepository;

        public Store2GetAllReturnQueryHandler(Store2IReturnReadRepository store2ReturnReadRepository)
        {
            _store2ReturnReadRepository = store2ReturnReadRepository;
        }

        public async Task<Store2GetAllReturnQueryResponse> Handle(Store2GetAllReturnQueryRequest request, CancellationToken cancellationToken)
        {
            var returns = await _store2ReturnReadRepository.GetAllAsync();


            if (returns == null || !returns.Any())
            {
                return new Store2GetAllReturnQueryResponse
                {
                    Success = false,
                    Message = "İade verisi bulunamadı.",
                    Store2Returns = new List<Store2ReturnDto>()
                };
            }

            var returnDtos = returns.Select(Return => new Store2ReturnDto
            {
                Id = Return.Id,
                Quantity = Return.Quantity,
                ReturnReason = Return.ReturnReason,
                CustomerName = Return.CustomerName,
                CustomerPhone = Return.CustomerPhone
            }).ToList();

            return new Store2GetAllReturnQueryResponse
            {
                Success = true,
                Message = "İade verileri başarıyla getirildi.",
                Store2Returns = returnDtos
            };
        }
    }
}
