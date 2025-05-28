using MultiStoreIntegration.Application.Repositories.Store1.Store1User;
using MultiStoreIntegration.Domain.Entities;
using MultiStoreIntegration.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Repositories.Store1.Store1User
{
    public class Store1UserWriteRepository : Store1WriteRepository<User>, Store1IUserWriteRepository
    {

        public Store1UserWriteRepository(Store1DbContext context) : base(context)
        {
        }

        
    }
}
