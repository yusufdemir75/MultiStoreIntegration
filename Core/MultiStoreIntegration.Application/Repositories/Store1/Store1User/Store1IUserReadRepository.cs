using MultiStoreIntegration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Repositories.Store1.Store1User
{
    public interface Store1IUserReadRepository : Store1IReadRepository<User>
    {
        Task<User> GetByEmailAsync(string email, bool tracking = true);
    }
}
