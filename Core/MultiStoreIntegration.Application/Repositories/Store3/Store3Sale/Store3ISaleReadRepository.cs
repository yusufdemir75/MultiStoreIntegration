using MultiStoreIntegration.Domain.MongoDocuments;
using MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Repositories.Store3.Store3Sale
{
    public interface Store3ISaleReadRepository: Store3IReadRepository<Store3SaleDocument>
    {
    }
}
