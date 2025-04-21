using MongoDB.Driver;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Persistence.Migrations.Mongo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Migrations.Mongo.Migrations
{
    public class AddInitialIndexes : IMongoMigration
    {
        public string Version => "20240421_AddInitialIndexes";

        public async Task UpAsync(IMongoDatabase database)
        {
            var collection = database.GetCollection<SaleDocument>("Sales");

            var indexKeys = Builders<SaleDocument>.IndexKeys.Ascending(x => x.CustomerPhone);
            var indexModel = new CreateIndexModel<SaleDocument>(indexKeys);

            await collection.Indexes.CreateOneAsync(indexModel);
        }
    }
}
