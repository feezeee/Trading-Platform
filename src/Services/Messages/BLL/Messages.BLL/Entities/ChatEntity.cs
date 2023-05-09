using MongoDB.Bson.Serialization.Attributes;

namespace Messages.BLL.Entities
{
    public class ChatEntity
    {
        [BsonId]
        public Guid Id { get; set; }

        public List<Guid> Users { get; set; } = new List<Guid>();

        [BsonIgnore]
        public List<MessageEntity> Messages { get; set; } = new List<MessageEntity>();
    }
}
