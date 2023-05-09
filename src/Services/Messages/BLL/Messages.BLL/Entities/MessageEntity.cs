using MongoDB.Bson.Serialization.Attributes;

namespace Messages.BLL.Entities
{
    public class MessageEntity
    {
        [BsonId]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Message { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public Guid ChatId { get; set; }
    }
}
