using Messages.BLL.Entities;

namespace Messages.BLL.Contracts.Finders
{
    public interface IMessageFinder
    {
        public Task<List<MessageEntity>> GetAllAsync(CancellationToken token = default);
        public Task<List<MessageEntity>> GetAllForChatIdAsync(Guid chatId, CancellationToken token = default);
    }
}
