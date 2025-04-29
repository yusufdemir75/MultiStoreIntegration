using MultiStoreIntegration.Application.Repositories.Store1.Store1Sale;
using MultiStoreIntegration.Domain.Entities;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store1.Store1Sale
{
    public class Store1SaleWriteRepository : Store1WriteRepository<Sale>, Store1ISaleWriteRepository
    {
        public Store1SaleWriteRepository(Store1DbContext context) : base(context)
        {
        }
    }
}
