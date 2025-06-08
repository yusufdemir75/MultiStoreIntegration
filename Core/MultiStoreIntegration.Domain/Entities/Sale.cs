using MultiStoreIntegration.Domain.Entities.common;

namespace MultiStoreIntegration.Domain.Entities
{
    public class Sale : BaseEntity
    {
        // Foreign key
        public Guid ProductId { get; set; }
        public Stock? Product { get; set; }

        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string? Category { get; set; }
        public string? ProductName { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentMethod { get; set; }

        // Navigation
        public ICollection<Return>? Returns { get; set; }

    }
}
