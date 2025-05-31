using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace MultiStoreIntegration.Persistence.Contexts
{
    public class RedisContext
    {
        public IDatabase Database { get; }

        public RedisContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Redis");
            var redis = ConnectionMultiplexer.Connect(connectionString);
            Database = redis.GetDatabase();
        }
    }
}
