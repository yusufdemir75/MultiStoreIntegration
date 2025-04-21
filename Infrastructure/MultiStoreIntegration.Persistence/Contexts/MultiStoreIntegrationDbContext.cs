using Microsoft.EntityFrameworkCore;
using MultiStoreIntegration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Contexts
{
    public class MultiStoreIntegrationDbContext : DbContext
    {
        public MultiStoreIntegrationDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Stock> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Return> Returns { get; set; }
    }
}
