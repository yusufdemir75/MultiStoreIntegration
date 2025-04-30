using MediatR;


namespace MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store1CreateStock
{
    public class Store1CreateStockCommandRequest : IRequest<Store1CreateStockCommandResponse>
    {
        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
