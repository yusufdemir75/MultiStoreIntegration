// Persistence/MongoMigration/MongoMigrationInitializer.cs
using Mongo.Migration.Startup.DotNetCore;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MultiStoreIntegration.Persistence.MongoMigration
{
    public static class MongoMigrationInitializer
    {
        public static void AddMongoMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoMigrations(configuration["MongoDb:ConnectionString"], c =>
            {
                c.AddMigrationAssemblies(typeof(MongoMigrationInitializer).Assembly);
                c.Database = configuration["MongoDb:DatabaseName"];
            });
        }
    }
}
