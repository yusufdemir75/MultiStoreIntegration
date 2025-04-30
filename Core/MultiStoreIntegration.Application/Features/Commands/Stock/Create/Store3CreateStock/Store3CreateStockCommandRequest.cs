using MediatR;

namespace MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store3CreateStock
{
    public class Store3CreateStockCommandRequest : IRequest<Store3CreateStockCommandResponse>
    {
        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
}
