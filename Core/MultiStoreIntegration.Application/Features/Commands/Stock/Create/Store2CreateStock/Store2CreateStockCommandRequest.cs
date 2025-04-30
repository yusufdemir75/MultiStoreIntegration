using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Stock.Create.Store2CreateStock
{
    public class Store2CreateStockCommandRequest : IRequest<Store2CreateStockCommandResponse>
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
