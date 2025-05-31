using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using MultiStoreIntegration.Application.Abstractions.Services;
using MultiStoreIntegration.Persistence.Contexts;

namespace MultiStoreIntegration.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly StackExchange.Redis.IDatabase _database;

        public RedisCacheService(RedisContext redisContext)
        {
            _database = redisContext.Database;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value!);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var jsonData = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, jsonData, expiry ?? TimeSpan.FromMinutes(30));
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }

}
