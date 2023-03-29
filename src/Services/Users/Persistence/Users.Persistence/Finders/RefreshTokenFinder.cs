using Microsoft.EntityFrameworkCore;
using Users.Domain.Contracts.Finders;
using Users.Domain.Entities;

namespace Users.Persistence.Finders
{
    public class RefreshTokenFinder : IRefreshTokenFinder
    {
        protected readonly UserContext _userContext;

        public RefreshTokenFinder(UserContext userContext)
        {
            _userContext = userContext;
        }

        protected IQueryable<RefreshTokenEntity> AsQueryable()
        {
            var data = _userContext.RefreshTokens;
            return data.AsQueryable();
        }

        public Task<RefreshTokenEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var role = AsQueryable()
                .TagWith("Get refresh token by id")
                .FirstOrDefaultAsync(q => q.Id == id, token);
            return role;
        }

        public Task<List<RefreshTokenEntity>> GetByUserIdAsync(Guid userId, CancellationToken token = default)
        {
            var roles = AsQueryable()
                .TagWith("Get refresh token by user id")
                .Where(t => t.UserId == userId)
                .ToListAsync(token);
            return roles;
        }

        public Task<RefreshTokenEntity?> GetByUserIdAndRefreshTokenAsync(Guid userId, string refreshToken, CancellationToken token = default)
        {
            var roles = AsQueryable()
                .TagWith("Get refresh token by user id and refresh token")
                .FirstOrDefaultAsync(t => t.UserId == userId && t.RefreshToken.ToLower() == refreshToken.ToLower(), token);
            return roles;
        }
    }
}
