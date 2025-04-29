using MultiStoreIntegration.Domain.MongoDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Repositories.Store3.Store3Stock
{
    public interface Store3IStockWriteRepository : Store3IWriteRepository<StockDocument>
    {
    }
}
