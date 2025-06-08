
namespace MultiStoreIntegration.Application.DTOs.StoreCategoryStock
{
    public class StoreCategoryStockDto
    {
        public string StoreName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int TotalQuantity { get; set; }
    }
}
