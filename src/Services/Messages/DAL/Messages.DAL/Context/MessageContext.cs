using Messages.BLL.Entities;
using Messages.DAL.Configurations;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Messages.DAL.Context
{
    public class MessageContext : IMessageContext
    {
        public MessageContext(MongoDbConfiguration mongoDbOptions)
        {
            var client = new MongoClient(mongoDbOptions.ConnectionString);
            var database = client.GetDatabase(mongoDbOptions.DatabaseName);

            BsonClassMap.RegisterClassMap<ChatEntity>(cm => cm.SetUp());
            BsonClassMap.RegisterClassMap<MessageEntity>(cm => cm.SetUp());

            Chats = database.GetCollection<ChatEntity>(mongoDbOptions.ChatCollectionName);
            Messages = database.GetCollection<MessageEntity>(mongoDbOptions.MessageCollectionName)
        }

        public IMongoCollection<ChatEntity> Chats { get; }

        public IMongoCollection<MessageEntity> Messages { get; }
    }
}
