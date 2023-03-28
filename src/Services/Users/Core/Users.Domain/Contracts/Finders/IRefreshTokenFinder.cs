using Users.Domain.Entities;

namespace Users.Domain.Contracts.Finders
{
    public interface IRefreshTokenFinder
    {
        public Task<RefreshTokenEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
        public Task<List<RefreshTokenEntity>> GetByUserIdAsync(Guid userId, CancellationToken token = default);
        public Task<RefreshTokenEntity?> GetByUserIdAndRefreshTokenAsync(Guid userId, string refreshToken, CancellationToken token = default);
    }
}
