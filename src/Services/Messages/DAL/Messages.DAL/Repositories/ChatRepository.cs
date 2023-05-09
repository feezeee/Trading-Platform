using Messages.BLL.Contracts.Repositories;
using Messages.BLL.Entities;
using Messages.DAL.Context;
using MongoDB.Driver;

namespace Messages.DAL.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMessageContext _messageContext;

        public ChatRepository(IMessageContext messageContext)
        {
            _messageContext = messageContext;
        }

        public Task CreateAsync(ChatEntity chat, CancellationToken token = default)
        {
            return _messageContext.Chats.InsertOneAsync(chat, null, token);
        }

        public Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var filter = Builders<ChatEntity>.Filter.Eq(t => t.Id, id);
            return _messageContext.Chats.DeleteOneAsync(filter, token);
        }

        public Task UpdateAsync(ChatEntity chat, CancellationToken token = default)
        {
            var filter = Builders<ChatEntity>.Filter.Eq(t => t.Id, chat.Id);
            return _messageContext.Chats.ReplaceOneAsync(filter, chat, null as ReplaceOptions, token);
        }
    }
}
