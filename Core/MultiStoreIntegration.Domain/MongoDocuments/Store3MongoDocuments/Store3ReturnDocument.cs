using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiStoreIntegration.Domain.MongoDocuments.Store3MongoDocuments
{
    public class Store3ReturnDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ProductId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId SaleId { get; set; }

        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }

        public int Quantity { get; set; }
        public string? ReturnReason { get; set; }
        public float RefundAmount { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
