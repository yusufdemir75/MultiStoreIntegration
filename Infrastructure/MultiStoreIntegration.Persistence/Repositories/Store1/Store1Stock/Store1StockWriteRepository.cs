using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;
using MultiStoreIntegration.Domain.Entities;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store1.Store1Stock
{
    public class Store1StockWriteRepository : Store1WriteRepository<Stock>, Store1IStockWriteRepository
    {
        public Store1StockWriteRepository(Store1DbContext context) : base(context)
        {
        }
    }
}
