using MongoDB.Driver;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Migrations.Mongo.Interfaces;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Migrations.Mongo.Migrations
{
    public class WareHouseCreateInitialCollections : IWareHouseMongoMigration
    {
        public string Version => "20240422_CreateInitialCollections";

        public async Task UpAsync(IMongoDatabase database)
        {
            var collectionNames = await database.ListCollectionNames().ToListAsync();

            if (!collectionNames.Contains("Store1Stocks"))
                await database.CreateCollectionAsync("Store1Stocks");

            if (!collectionNames.Contains("Store1Sales"))
                await database.CreateCollectionAsync("Store1Sales");

            if (!collectionNames.Contains("Store1Returns"))
                await database.CreateCollectionAsync("Store1Returns");

            if (!collectionNames.Contains("Store2Stocks"))
                await database.CreateCollectionAsync("Store2Stocks");

            if (!collectionNames.Contains("Store2Sales"))
                await database.CreateCollectionAsync("Store2Sales");

            if (!collectionNames.Contains("Store2Returns"))
                await database.CreateCollectionAsync("Store2Returns");

            if (!collectionNames.Contains("Store3Stocks"))
                await database.CreateCollectionAsync("Store3Stocks");

            if (!collectionNames.Contains("Store3Sales"))
                await database.CreateCollectionAsync("Store3Sales");

            if (!collectionNames.Contains("Store3Returns"))
                await database.CreateCollectionAsync("Store3Returns");
        }
    }
}
