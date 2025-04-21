using MongoDB.Driver;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Migrations.Mongo.Interfaces;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Migrations.Mongo.Migrations
{
    public class CreateInitialCollections : IMongoMigration
    {
        public string Version => "20240421_CreateInitialCollections";

        public async Task UpAsync(IMongoDatabase database)
        {
            var collectionNames = await database.ListCollectionNames().ToListAsync();

            if (!collectionNames.Contains("Products"))
                await database.CreateCollectionAsync("Products");

            if (!collectionNames.Contains("Sales"))
                await database.CreateCollectionAsync("Sales");

            if (!collectionNames.Contains("Returns"))
                await database.CreateCollectionAsync("Returns");
        }
    }
}
