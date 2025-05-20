using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Queries.Sale.GetAllSale.Store3GetAllSale
{
    public class Store3GetAllSaleQueryHandler : IRequestHandler<Store3GetAllSaleQueryRequest, Store3GetAllSaleQueryResponse>
    {
        public Task<Store3GetAllSaleQueryResponse> Handle(Store3GetAllSaleQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
