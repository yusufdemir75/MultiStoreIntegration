using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Migrations.Mongo.Interfaces
{
    public interface IMongoMigration
    {
        string Version { get; }
        Task UpAsync(IMongoDatabase database);
    }
}
