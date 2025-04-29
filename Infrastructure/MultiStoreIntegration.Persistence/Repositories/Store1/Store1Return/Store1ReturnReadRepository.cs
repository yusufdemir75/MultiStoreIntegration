using MultiStoreIntegration.Application.Repositories.Store1.Store1Return;
using MultiStoreIntegration.Domain.Entities;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store1.Store1Return
{
    public class Store1ReturnReadRepository : Store1ReadRepository<Return>, Store1IReturnReadRepository
    {
        public Store1ReturnReadRepository(Store1DbContext context) : base(context)
        {
        }
    }
}
