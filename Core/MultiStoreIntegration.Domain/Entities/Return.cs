using MultiStoreIntegration.Domain.Entities.common;

namespace MultiStoreIntegration.Domain.Entities
{
    public class Return :BaseEntity
    {
        // Foreign key to Stock
        public Guid ProductId { get; set; }
        public Stock? Product { get; set; }

        // Foreign key to Sale
        public Guid SaleId { get; set; }
        public Sale? Sales { get; set; }

        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public int Quantity { get; set; }
        public string? ReturnReason { get; set; }
        public int RefundAmount { get; set; }

       
    }
}
