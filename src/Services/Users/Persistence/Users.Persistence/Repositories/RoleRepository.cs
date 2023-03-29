using Users.Domain.Contracts.Repositories;
using Users.Domain.Entities;

namespace Users.Persistence.Repositories
{
    public class RoleRepository : BaseRepository<RoleEntity>, IRoleRepository
    {
        public RoleRepository(UserContext userContext) : base(userContext)
        {
        }
    }
}
