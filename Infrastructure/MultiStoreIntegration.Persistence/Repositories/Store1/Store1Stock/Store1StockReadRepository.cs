using MultiStoreIntegration.Application.Repositories.Store1.Store1Stock;
using MultiStoreIntegration.Domain.Entities;
using MultiStoreIntegration.Persistence.Contexts;
using MultiStoreIntegration.Persistence.Repositories.Store1;
using MultiStoreIntegration.Persistence.Repositories.Store2.Store2Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store1.Store1Stock
{
    public class Store1StockReadRepository : Store1ReadRepository<Stock>, Store1IStockReadRepository
    {
        public Store1StockReadRepository(Store1DbContext context) : base(context)
        {
        }
    }
}
