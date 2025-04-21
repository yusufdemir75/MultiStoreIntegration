using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiStoreIntegration.Persistence.Contexts;
using Microsoft.Extensions.Configuration;

namespace MultiStoreIntegration.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Store1DbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Store1Db")));

            services.AddDbContext<Store2DbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Store2Db")));

            services.AddSingleton<Store3MongoContext>();
        }
    }
}
