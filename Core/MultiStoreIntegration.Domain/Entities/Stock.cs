using MultiStoreIntegration.Domain.Entities.common;

namespace MultiStoreIntegration.Domain.Entities
{
    public class Stock:BaseEntity
    {
        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }

        // Navigation properties
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Return> Returns { get; set; }
    }
}
