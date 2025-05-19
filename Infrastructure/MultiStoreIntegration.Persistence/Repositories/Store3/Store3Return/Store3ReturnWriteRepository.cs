using MultiStoreIntegration.Application.Repositories.Store3.Store3Return;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store3.Store3Return
{
    internal class Store3ReturnWriteRepository : Store3WriteRepository<Store3ReturnDocument>, Store3IReturnWriteRepository
    {
        public Store3ReturnWriteRepository(Store3MongoContext context) : base(context)
        {
        }

    }
}
