using Microsoft.EntityFrameworkCore;
using Users.Domain.Contracts.Findres;
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
            return data.AsQueryable();
        }

        public Task<UserEntity?> GetUserAsync(string email, string password, CancellationToken token = default)
        {
            var user = AsQueryable()
                .TagWith("Get user by email and password")
                .FirstOrDefaultAsync(t => t.Email.ToLower() == email.ToLower() && t.Password == password);
            return user;
        }

        public Task<List<UserEntity>> GetUsersAsync(CancellationToken token = default)
        {
            var users = AsQueryable()
                .TagWith("Get users")
                .ToListAsync(token);
            return users;
        }
    }
}
