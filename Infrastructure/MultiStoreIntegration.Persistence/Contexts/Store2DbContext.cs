using Microsoft.EntityFrameworkCore;
using MultiStoreIntegration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Persistence.Contexts
{
    public class Store2DbContext : DbContext
    {
        public Store2DbContext(DbContextOptions<Store2DbContext> options) : base(options) { }

        public DbSet<Stock> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Return> Returns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Return -> Product ilişkisi
            modelBuilder.Entity<Return>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Returns)
                .HasForeignKey(r => r.ProductId);

            // Return -> Sale ilişkisi
            modelBuilder.Entity<Return>()
                .HasOne(r => r.Sales)
                .WithMany(s => s.Returns)
                .HasForeignKey(r => r.SaleId);

            // Sale -> Product ilişkisi
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductId);
        }
    }
}
