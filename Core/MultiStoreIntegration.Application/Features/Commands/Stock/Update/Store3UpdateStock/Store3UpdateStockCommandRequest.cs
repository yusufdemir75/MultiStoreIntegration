using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Stock.Update.Store3UpdateStock
{
    public class Store3UpdateStockCommandRequest : IRequest<Store3UpdateStockCommandResponse>
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
