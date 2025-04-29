using MultiStoreIntegration.Application.Repositories.Store2.Store2Sale;
using MultiStoreIntegration.Domain.Entities;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store2.Store2Sale
{
    public class Store2SaleReadRepository : Store2ReadRepository<Sale>, Store2ISaleReadRepository
    {
        public Store2SaleReadRepository(Store2DbContext context) : base(context)
        {
        }
    }
}
