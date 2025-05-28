using MultiStoreIntegration.Application.Repositories.Store1.Store1User;
using MultiStoreIntegration.Domain.Entities;
using MultiStoreIntegration.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace MultiStoreIntegration.Persistence.Repositories.Store1.Store1User
{
    public class Store1UserReadRepository : Store1ReadRepository<User>, Store1IUserReadRepository
    {

        public Store1UserReadRepository(Store1DbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string email, bool tracking = true)
        {
            return await GetWhere(u => u.Email == email, tracking).FirstOrDefaultAsync();
        }
    }
}
