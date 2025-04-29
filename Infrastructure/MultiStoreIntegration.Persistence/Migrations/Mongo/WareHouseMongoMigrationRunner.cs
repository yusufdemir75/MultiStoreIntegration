using MongoDB.Bson;
using MongoDB.Driver;
using MultiStoreIntegration.Persistence.Contexts;
using MultiStoreIntegration.Persistence.Migrations.Mongo.Interfaces;
using MultiStoreIntegration.Persistence.Migrations.Mongo.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Migrations.Mongo
{
    public class WareHouseMongoMigrationRunner
    {
        private readonly IMongoDatabase _database;
        private readonly List<IWareHouseMongoMigration> _migrations;

        public WareHouseMongoMigrationRunner(WarehouseMongoDbContext warehouseMongoDbContext)
        {
            _database = warehouseMongoDbContext.Database;
            _migrations = new List<IWareHouseMongoMigration>
            {
                new WareHouseCreateInitialCollections()
                // Buraya diğer migration sınıfları da eklenebilir
            };
        }

        public async Task RunMigrationsAsync()
        {
            var appliedMigrationsCollection = _database.GetCollection<BsonDocument>("__applied_migrations");

            foreach (var migration in _migrations)
            {
                var alreadyApplied = await appliedMigrationsCollection
                    .Find(Builders<BsonDocument>.Filter.Eq("version", migration.Version))
                    .AnyAsync();

                if (!alreadyApplied)
                {
                    await migration.UpAsync(_database);

                    var doc = new BsonDocument
                    {
                        { "version", migration.Version },
                        { "appliedAt", DateTime.UtcNow }
                    };
                    await appliedMigrationsCollection.InsertOneAsync(doc);
                }
            }
        }

    }
}
