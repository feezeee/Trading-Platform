using Users.Domain.Contracts.Repositories;
using Users.Domain.Entities;

namespace Users.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(UserContext userContext) : base(userContext)
        {
        }
    }
}
