using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Return;
using MongoDB.Driver;


namespace MultiStoreIntegration.Persistence.Repositories.Store3.Store3Return
{
    public class Store3ReturnReadRepository: Store3ReadRepository<Store3ReturnDocument>, Store3IReturnReadRepository
    {
        private readonly IMongoDatabase _database;
        public Store3ReturnReadRepository(Store3MongoContext context) : base(context.Database, "Returns")
        {
            _database = context.Database;
        }
    }
}
