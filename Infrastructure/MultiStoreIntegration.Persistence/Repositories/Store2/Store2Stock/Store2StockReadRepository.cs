using MultiStoreIntegration.Application.Repositories.Store2.Store2Stock;
using MultiStoreIntegration.Domain.Entities;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store2.Store2Stock
{
    public class Store2StockReadRepository : Store2ReadRepository<Stock>, Store2IStockReadRepository
    {
        public Store2StockReadRepository(Store2DbContext context) : base(context)
        {
        }
    }
}
