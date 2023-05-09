using Messages.BLL.Entities;
using MongoDB.Driver;

namespace Messages.DAL.Context
{
    public interface IMessageContext
    {
        IMongoCollection<ChatEntity> Chats { get; }
        IMongoCollection<MessageEntity> Messages { get; }
    }
}
