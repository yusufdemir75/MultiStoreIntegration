using Microsoft.Extensions.DependencyInjection;
using MultiStoreIntegration.Application.Abstractions.Services;
using MultiStoreIntegration.Infrastructure.Services;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using MultiStoreIntegration.Persistence.Contexts;


namespace MultiStoreIntegration.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379"; 
            });
            services.AddSingleton<RedisContext>();

            services.AddScoped<ICacheService, RedisCacheService>();
        }
    }
}
