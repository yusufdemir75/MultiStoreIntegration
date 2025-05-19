using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store3CreateSale
{
    public class Store3CreateSaleCommandRequest: IRequest<Store3CreateSaleCommandResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
