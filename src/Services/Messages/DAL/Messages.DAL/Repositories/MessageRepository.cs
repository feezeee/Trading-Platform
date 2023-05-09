using Messages.BLL.Contracts.Repositories;
using Messages.BLL.Entities;
using Messages.DAL.Context;
using MongoDB.Driver;

namespace Messages.DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMessageContext _messageContext;

        public MessageRepository(IMessageContext messageContext)
        {
            _messageContext = messageContext;
        }

        public Task CreateAsync(MessageEntity message, CancellationToken token = default)
        {
            return _messageContext.Messages.InsertOneAsync(message, null, token);
        }

        public Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var filter = Builders<MessageEntity>.Filter.Eq(f => f.Id, id);

            return _messageContext.Messages.DeleteOneAsync(filter, token);
        }

        public Task UpdateAsync(MessageEntity message, CancellationToken token = default)
        {
            var filter = Builders<MessageEntity>.Filter.Eq(f => f.Id, message.Id);

            return _messageContext.Messages.ReplaceOneAsync(filter, message, null as ReplaceOptions, token);

        }
    }
}
