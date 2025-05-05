using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store2CreateSale
{
    public class Store2CreateSaleCommandRequest : IRequest<Store2CreateSaleCommandResponse>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string PaymentMethod { get; set; }
    }
}
