using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Sale.CreateSale
{
    public class CreateSaleCommandRequest:IRequest<CreateSaleCommandResponse>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string PaymentMethod { get; set; }
    }
}
