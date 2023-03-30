using Microsoft.EntityFrameworkCore;
using Users.Domain.Contracts.Finders;
using Users.Domain.Entities;

namespace Users.Persistence.Finders
{
    public class UserFinder : IUserFinder
    {
        protected readonly UserContext _userContext;

        public UserFinder(UserContext userContext)
        {
            _userContext = userContext;
        }

        protected IQueryable<UserEntity> AsQueryable()
        {
            var data = _userContext.Users;
            return data
                .Include(t => t.RefreshTokens)
                .Include(t => t.Roles)
                .AsQueryable();
        }

        public Task<UserEntity?> GetByNicknameAndPasswordAsync(string nickName, string password, CancellationToken token = default)
        {
            var user = AsQueryable()
                .TagWith("Get user by email and password")
                .FirstOrDefaultAsync(t => t.Nickname.ToLower() == nickName.ToLower() && t.Password == password, token);
            return user;
        }

        public Task<List<UserEntity>> GetAllAsync(CancellationToken token = default)
        {
            var users = AsQueryable()
                .TagWith("Get users")
                .ToListAsync(token);
            return users;
        }

        public Task<bool> HasAnyByNicknameAsync(string nickname, CancellationToken token = default)
        {
            var existAny = AsQueryable()
                .TagWith("Has any user by nickname")
                .AnyAsync(t => t.Nickname.ToLower() == nickname.ToLower(), token);
            return existAny;
        }
    }
}
