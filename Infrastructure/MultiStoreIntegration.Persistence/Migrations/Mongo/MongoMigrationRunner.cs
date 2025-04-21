using MongoDB.Bson;
using MongoDB.Driver;
using MultiStoreIntegration.Persistence.Contexts;
using MultiStoreIntegration.Persistence.Migrations.Mongo.Interfaces;
using MultiStoreIntegration.Persistence.Migrations.Mongo.Migrations;

namespace MultiStoreIntegration.Persistence.Migrations.Mongo
{
    public class MongoMigrationRunner
    {
        private readonly IMongoDatabase _database;
        private readonly List<IMongoMigration> _migrations;

        public MongoMigrationRunner(Store3MongoContext context)
        {
            _database = context.GetDatabase();
            _migrations = new List<IMongoMigration>
            {
                new CreateInitialCollections()
                // buraya diğer migration sınıfları da eklenecek
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
