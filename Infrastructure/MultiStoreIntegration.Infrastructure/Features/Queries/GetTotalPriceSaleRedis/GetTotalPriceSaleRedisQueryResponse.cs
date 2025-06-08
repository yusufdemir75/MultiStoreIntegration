
using MultiStoreIntegration.Application.DTOs.DailyTotalSale;

namespace MultiStoreIntegration.Infrastructure.Features.Queries.GetTotalPriceSaleRedis
{
    public class GetTotalPriceSaleRedisQueryResponse
    {
        public List<DailyTotalSaleDto> DailySales { get; set; } = new();

    }
}
