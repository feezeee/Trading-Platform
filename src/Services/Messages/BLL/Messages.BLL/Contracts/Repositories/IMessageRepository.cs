using Messages.BLL.Entities;

namespace Messages.BLL.Contracts.Repositories
{
    public interface IMessageRepository
    {
        public Task CreateAsync(MessageEntity message, CancellationToken token = default);
        public Task UpdateAsync(MessageEntity message, CancellationToken token = default);
        public Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
