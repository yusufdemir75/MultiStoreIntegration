using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Return.Create.Store3CreateReturn
{
    public class Store3CreateReturnCommandRequest: IRequest<Store3CreateReturnCommandResponse>
    {
        public string SaleId { get; set; }
        public int Quantity { get; set; }
        public string? ReturnReason { get; set; }
    }
}
