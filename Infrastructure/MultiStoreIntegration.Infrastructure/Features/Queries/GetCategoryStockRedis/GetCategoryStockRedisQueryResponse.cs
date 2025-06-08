using MultiStoreIntegration.Application.DTOs.StoreCategoryStock;

namespace MultiStoreIntegration.Infrastructure.Features.Queries.GetCategoryStockRedis
{
    public class GetCategoryStockRedisQueryResponse
    {
        public List<StoreCategoryStockDto> StoreCategoryStocks { get; set; } = new();

    }
}
