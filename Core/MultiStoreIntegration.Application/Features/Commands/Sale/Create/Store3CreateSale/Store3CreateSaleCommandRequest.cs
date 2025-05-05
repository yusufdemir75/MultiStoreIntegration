using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Features.Commands.Sale.Create.Store3CreateSale
{
    public class Store3CreateSaleCommandRequest: IRequest<Store3CreateSaleCommandResponse>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ProductId { get; set; }
        public int Quantity { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
