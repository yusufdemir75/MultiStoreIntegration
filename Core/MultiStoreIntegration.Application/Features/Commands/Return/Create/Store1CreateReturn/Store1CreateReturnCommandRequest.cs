using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Return.Create.CreateReturn
{
    public class Store1GetAllCommandRequest : IRequest<Store1CreateReturnCommandResponse>
    {
        public Guid SaleId { get; set; }
        public int Quantity { get; set; }
        public string? ReturnReason { get; set; }
    }
}
