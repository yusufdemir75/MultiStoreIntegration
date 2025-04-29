using Microsoft.EntityFrameworkCore;
using MultiStoreIntegration.Domain.Entities;


namespace MultiStoreIntegration.Persistence.Contexts
{
    public class Store1DbContext : DbContext
    {
        public Store1DbContext(DbContextOptions<Store1DbContext> options) : base(options) { }

        public DbSet<Stock> stock { get; set; }
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
