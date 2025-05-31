using MediatR;
using MongoDB.Driver;
using MultiStoreIntegration.Application.Abstractions.Services;
using MultiStoreIntegration.Application.DTOs.StoreCategoryStock;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;

namespace MultiStoreIntegration.Infrastructure.Features.Queries.GetCategoryStockRedis
{
    public class GetCategoryStockRedisQueryHandler : IRequestHandler<GetCategoryStockRedisQueryRequest, GetCategoryStockRedisQueryResponse>
    {
        private readonly WarehouseMongoDbContext _context;
        private readonly ICacheService _cacheService;
        private static readonly string[] StoreCollections = new[]
        {
        "Store1Stocks",
        "Store2Stocks",
        "Store3Stocks"
    };

        public GetCategoryStockRedisQueryHandler(WarehouseMongoDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<GetCategoryStockRedisQueryResponse> Handle(GetCategoryStockRedisQueryRequest request, CancellationToken cancellationToken)
        {
            const string cacheKey = "category:stock:totals";
            var cached = await _cacheService.GetAsync<GetCategoryStockRedisQueryResponse>(cacheKey);
            if (cached != null)
                return cached;

            var result = new GetCategoryStockRedisQueryResponse();

            foreach (var collectionName in StoreCollections)
            {
                var collection = _context.Database.GetCollection<StockDocument>(collectionName);
                var storeName = collectionName.Replace("Stocks", "");

                var group = await collection.Aggregate()
                    .Group(x => x.Category, g => new
                    {
                        Category = g.Key,
                        TotalQuantity = g.Sum(x => x.Quantity)
                    })
                    .ToListAsync(cancellationToken);

                foreach (var item in group)
                {
                    result.StoreCategoryStocks.Add(new StoreCategoryStockDto
                    {
                        StoreName = storeName,
                        Category = item.Category,
                        TotalQuantity = item.TotalQuantity
                    });
                }
            }

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(10));
            return result;
        }
    }
}

