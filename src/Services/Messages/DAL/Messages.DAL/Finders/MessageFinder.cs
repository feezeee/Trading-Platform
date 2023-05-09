using Messages.BLL.Contracts.Finders;
using Messages.BLL.Entities;
using Messages.DAL.Context;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Messages.DAL.Finders
{
    public class MessageFinder : IMessageFinder
    {
        private readonly IMessageContext _messageContext;

        public MessageFinder(IMessageContext messageContext)
        {
            _messageContext = messageContext;
        }

        protected IMongoQueryable<MessageEntity> AsQueryable()
        {
            return _messageContext.Messages.AsQueryable();
        }

        public Task<List<MessageEntity>> GetAllAsync(CancellationToken token = default)
        {
            return AsQueryable().ToListAsync(token);
        }

        public Task<List<MessageEntity>> GetAllForChatIdAsync(Guid chatId, CancellationToken token = default)
        {
            return AsQueryable()
                .Where(t => t.ChatId == chatId)
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync(token);
        }
    }
}
