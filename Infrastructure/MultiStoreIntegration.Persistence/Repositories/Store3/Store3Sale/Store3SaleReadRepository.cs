using MongoDB.Driver;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Sale;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store3.Store3Sale
{
    public class Store3SaleReadRepository : Store3ReadRepository<Store3SaleDocument>, Store3ISaleReadRepository
    {
        private readonly IMongoDatabase _database;
        public Store3SaleReadRepository(Store3MongoContext context) : base(context.Database, "Sales")
        {
            _database = context.Database;
        }
    }
}
