using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Products.Domain.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; } = string.Empty;
        
        public List<string> ImageUrls { get; set; } = new List<string>();

        public List<string> PhoneNumbers { get; set; } = new List<string>();

        public List<Guid> CategoryIdList { get; set; } = new List<Guid>();

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? Price { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
        
        public Guid UserId { get; set; }
    }
}
