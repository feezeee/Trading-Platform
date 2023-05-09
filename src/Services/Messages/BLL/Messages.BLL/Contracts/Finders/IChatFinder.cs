using Messages.BLL.Entities;

namespace Messages.BLL.Contracts.Finders
{
    public interface IChatFinder
    {
        public Task<List<ChatEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        public Task<List<ChatEntity>> GetAllForUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
