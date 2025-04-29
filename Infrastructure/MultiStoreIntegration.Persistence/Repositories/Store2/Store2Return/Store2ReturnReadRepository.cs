using MultiStoreIntegration.Application.Repositories.Store2.Store2Return;
using MultiStoreIntegration.Domain.Entities;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store2.Store2Return
{
    public class Store2ReturnReadRepository : Store2ReadRepository<Return>, Store2IReturnReadRepository
    {
        public Store2ReturnReadRepository(Store2DbContext context) : base(context)
        {
        }
    }
}
