using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace MultiStoreIntegration.Domain.MongoDocuments
{
    public class SaleDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public Guid ProductId { get; set; }
        public Guid RelationalId { get; set; }

        public int Quantity { get; set; }
        public float TotalPrice { get; set; }

        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentMethod { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
