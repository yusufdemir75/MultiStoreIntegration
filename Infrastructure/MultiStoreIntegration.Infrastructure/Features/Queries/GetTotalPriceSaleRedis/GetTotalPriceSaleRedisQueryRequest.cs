using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Infrastructure.Features.Queries.GetTotalPriceSaleRedis
{
    public class GetTotalPriceSaleRedisQueryRequest : IRequest<GetTotalPriceSaleRedisQueryResponse>
    {
    }
}
