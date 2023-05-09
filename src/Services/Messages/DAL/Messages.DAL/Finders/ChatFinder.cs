using Messages.BLL.Contracts.Finders;
using Messages.BLL.Entities;
using Messages.DAL.Context;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Messages.DAL.Finders
{
    public class ChatFinder : IChatFinder
    {
        private readonly IMessageContext _messageContext;

        public ChatFinder(IMessageContext messageContext)
        {
            _messageContext = messageContext;
        }

        protected IMongoQueryable<ChatEntity> AsQueryable()
        {
            return _messageContext.Chats.AsQueryable();
        }

        public Task<List<ChatEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return AsQueryable().ToListAsync(cancellationToken);
        }

        public Task<List<ChatEntity>> GetAllForUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return AsQueryable().Where(t => t.Users.Any(uId => uId == userId)).ToListAsync(cancellationToken);
        }

        public async Task<ChatEntity?> GetChatBetweenUsersAsync(List<Guid> userIdList,
            CancellationToken cancellationToken = default)
        {
            return await AsQueryable().Where(t => userIdList.All(q => t.Users.Contains(q)))
                .FirstOrDefaultAsync(cancellationToken) ?? null;
        }
    }
}
