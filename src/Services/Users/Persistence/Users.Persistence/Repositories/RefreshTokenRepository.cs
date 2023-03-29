using Users.Domain.Contracts.Repositories;
using Users.Domain.Entities;

namespace Users.Persistence.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshTokenEntity>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(UserContext userContext) : base(userContext)
        {
        }
    }
}
