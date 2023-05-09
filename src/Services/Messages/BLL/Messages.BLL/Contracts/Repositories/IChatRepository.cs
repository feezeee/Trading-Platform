using Messages.BLL.Entities;

namespace Messages.BLL.Contracts.Repositories
{
    public interface IChatRepository
    {
        public Task CreateAsync(ChatEntity chat, CancellationToken token = default);
        public Task UpdateAsync(ChatEntity chat, CancellationToken token = default);
        public Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
