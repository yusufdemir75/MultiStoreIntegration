using MongoDB.Driver;
using MultiStoreIntegration.Persistence.Migrations.Mongo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Migrations.Mongo.Migrations
{
    public class Store4CreateInitialCollections : Store4IMongoMigration
    {
        public string Version => "20250603_CreateInitialCollections";

        public async Task UpAsync(IMongoDatabase database)
        {
            var collectionNames = await database.ListCollectionNames().ToListAsync();

            if (!collectionNames.Contains("Stocks"))
                await database.CreateCollectionAsync("Stocks");

            if (!collectionNames.Contains("Sales"))
                await database.CreateCollectionAsync("Sales");

            if (!collectionNames.Contains("Returns"))
                await database.CreateCollectionAsync("Returns");
        }
    }
}
