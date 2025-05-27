using MediatR;
using MultiStoreIntegration.Application.DTOs.ReturnDtos.Store3ReturnDto;
using MultiStoreIntegration.Application.DTOs.StockDtos.Store3StockDto;
using MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store3GetAllStock;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Return.Store3GetAllReturn
{
    public class Store3GetAllReturnQueryHandler : IRequestHandler<Store3GetAllReturnQueryRequest, Store3GetAllReturnQueryResponse>
    {
        private readonly Store3IReturnReadRepository _ReturnReadRepository;

        public Store3GetAllReturnQueryHandler(Store3IReturnReadRepository ReturnReadRepository)
        {
            _ReturnReadRepository = ReturnReadRepository;
        }

        public async Task<Store3GetAllReturnQueryResponse> Handle(Store3GetAllReturnQueryRequest request, CancellationToken cancellationToken)
        {
            var returns = await _ReturnReadRepository.GetAllAsync();

            var returnDtos = returns.Select(Return => new Store3ReturnDto
            {
                Id = Return.Id.ToString(),
                Quantity= Return.Quantity,
                ReturnReason = Return.ReturnReason,
                CustomerName = Return.CustomerName,
                CustomerPhone = Return.CustomerPhone
            }).ToList();

            return new Store3GetAllReturnQueryResponse
            {
                Success = true,
                Message = "Tüm iade verileri başarıyla getirildi.",
                store3Returns = returnDtos
            };
        }
    }
}
