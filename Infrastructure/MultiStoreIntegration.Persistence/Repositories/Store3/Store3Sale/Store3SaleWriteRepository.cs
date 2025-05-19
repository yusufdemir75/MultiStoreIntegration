using MultiStoreIntegration.Application.Repositories.Store3.Store3Sale;
using MultiStoreIntegration.Application.Repositories.Store3.Store3Stock;
using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store3.Store3Sale
{
    public class Store3SaleWriteRepository : Store3WriteRepository<Store3SaleDocument>, Store3ISaleWriteRepository
    {
        public Store3SaleWriteRepository(Store3MongoContext context) : base(context)
        {
        }
    }
}
