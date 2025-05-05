using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiStoreIntegration.Domain.MongoDocuments
{
    public class ReturnDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public Guid RelationalId { get; set; }

        public Guid ProductId { get; set; }

        public Guid SaleId { get; set; }

        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }

        public int Quantity { get; set; }
        public string? ReturnReason { get; set; }
        public float RefundAmount { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
