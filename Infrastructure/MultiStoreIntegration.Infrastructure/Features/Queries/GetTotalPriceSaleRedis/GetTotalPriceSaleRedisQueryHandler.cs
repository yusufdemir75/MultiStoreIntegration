using MediatR;
using MongoDB.Driver;
using MultiStoreIntegration.Application.Abstractions.Services;
using MultiStoreIntegration.Application.DTOs.DailyTotalSale;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Contexts;

namespace MultiStoreIntegration.Infrastructure.Features.Queries.GetTotalPriceSaleRedis
{
    public class GetTotalPriceSaleRedisQueryHandler : IRequestHandler<GetTotalPriceSaleRedisQueryRequest, GetTotalPriceSaleRedisQueryResponse>
    {
        private readonly WarehouseMongoDbContext _context;
        private readonly ICacheService _cacheService;
        private static readonly string[] StoreCollections = new[]
        {
            "Store1Sales",
            "Store2Sales"
        };

        public GetTotalPriceSaleRedisQueryHandler(WarehouseMongoDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<GetTotalPriceSaleRedisQueryResponse> Handle(GetTotalPriceSaleRedisQueryRequest request, CancellationToken cancellationToken)
        {
            const string cacheKey = "sales:total:daily:lastweek";
            var cached = await _cacheService.GetAsync<GetTotalPriceSaleRedisQueryResponse>(cacheKey);
            if (cached != null)
                return cached;

            var result = new GetTotalPriceSaleRedisQueryResponse();
            var today = DateTime.UtcNow.Date;
            var weekStart = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var weekEnd = weekStart.AddDays(7);

            var dailyStats = new Dictionary<DateTime, (int count, float total)>();

            foreach (var collectionName in StoreCollections)
            {
                var collection = _context.Database.GetCollection<SaleDocument>(collectionName);

                var sales = await collection
                    .Find(x => x.UpdatedDate >= weekStart && x.UpdatedDate < weekEnd)
                    .ToListAsync(cancellationToken);

                foreach (var sale in sales)
                {
                    var date = sale.UpdatedDate.Date;

                    if (!dailyStats.ContainsKey(date))
                        dailyStats[date] = (0, 0);

                    var current = dailyStats[date];
                    dailyStats[date] = (current.count + sale.Quantity, current.total + sale.TotalPrice);
                }
            }

            foreach (var day in Enumerable.Range(0, 7))
            {
                var date = weekStart.AddDays(day);
                dailyStats.TryGetValue(date, out var stat);

                result.DailySales.Add(new DailyTotalSaleDto
                {
                    Day = date.ToString("dddd", new System.Globalization.CultureInfo("tr-TR")), // örn: Pazartesi
                    Date = date,
                    TotalProductCount = stat.count,
                    TotalPrice = stat.total
                });
            }

            await _cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(10));
            return result;
        }
    }
}
