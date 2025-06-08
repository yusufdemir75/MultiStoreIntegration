using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Contexts
{
    public class Store4MongoContext
    {
        private readonly IMongoDatabase _database;

        public Store4MongoContext([FromKeyedServices("Store4MongoClient")] IMongoClient mongoClient, IConfiguration configuration)
        {
            var databaseName = configuration["Store4Mongo:DatabaseName"];
            _database = mongoClient.GetDatabase(databaseName);
        }


        public IMongoDatabase Database => _database;
    }
}
