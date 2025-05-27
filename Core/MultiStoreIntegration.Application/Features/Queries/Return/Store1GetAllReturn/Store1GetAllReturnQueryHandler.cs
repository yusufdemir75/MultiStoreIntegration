using MediatR;
using MultiStoreIntegration.Application.DTOs.ReturnDtos.Store1ReturnDto;
using MultiStoreIntegration.Application.DTOs.SaleDtos.Store1SaleDto;
using MultiStoreIntegration.Application.Features.Queries.Sale.GetAllSale.Store1GetAllSale;
using MultiStoreIntegration.Application.Repositories.Store1.Store1Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Return.Store1GetAllReturn
{
    public class Store1GetAllReturnQueryHandler : IRequestHandler<Store1GetAllReturnQueryRequest, Store1GetAllReturnQueryResponse>
    {
        private readonly Store1IReturnReadRepository _store1ReturnReadRepository;

        public Store1GetAllReturnQueryHandler(Store1IReturnReadRepository store1ReturnReadRepository)
        {
            _store1ReturnReadRepository = store1ReturnReadRepository;
        }

        public async Task<Store1GetAllReturnQueryResponse> Handle(Store1GetAllReturnQueryRequest request, CancellationToken cancellationToken)
        {
            var returns = await _store1ReturnReadRepository.GetAllAsync();


            if (returns == null || !returns.Any())
            {
                return new Store1GetAllReturnQueryResponse
                {
                    Success = false,
                    Message = "İade verisi bulunamadı.",
                    Store1Returns = new List<Store1ReturnDto>()
                };
            }

            var returnDtos = returns.Select(Return => new Store1ReturnDto
            {
                Id = Return.Id,
                Quantity = Return.Quantity,
                ReturnReason= Return.ReturnReason,
                CustomerName = Return.CustomerName,
                CustomerPhone = Return.CustomerPhone
            }).ToList();

            return new Store1GetAllReturnQueryResponse
            {
                Success = true,
                Message = "İade verileri başarıyla getirildi.",
                Store1Returns = returnDtos
            };
        }
    }
}
