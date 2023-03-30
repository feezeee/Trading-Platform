using Microsoft.EntityFrameworkCore;
using Users.Domain.Contracts.Finders;
using Users.Domain.Entities;

namespace Users.Persistence.Finders
{
    public class RoleFinder : IRoleFinder
    {
        protected readonly UserContext _userContext;

        public RoleFinder(UserContext userContext)
        {
            _userContext = userContext;
        }

        protected IQueryable<RoleEntity> AsQueryable()
        {
            var data = _userContext.Roles;
            return data.AsQueryable();
        }

        public Task<List<RoleEntity>> GetAllAsync(CancellationToken token = default)
        {
            return AsQueryable()
                .TagWith("Get all roles")
                .ToListAsync(token);
        }

        public Task<List<RoleEntity>> GetAllPaginationAsync(int pageNumber, int pageSize, CancellationToken token = default)
        {
            return AsQueryable()
                .TagWith("Get all pagination roles")
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
        }

        public Task<RoleEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var role = AsQueryable()
                .TagWith("Get role by id")
                .FirstOrDefaultAsync(q => q.Id == id, token);
            return role;
        }

        public Task<RoleEntity?> GetByNameAsync(string name, CancellationToken token = default)
        {
            var role = AsQueryable()
                .TagWith("Get role by name")
                .FirstOrDefaultAsync(q => q.Name.ToLower() == name.ToLower(), token);
            return role;
        }

        public Task<bool> HasAnyByNameAsync(string name, CancellationToken token = default)
        {
            var role = AsQueryable()
                .TagWith("Has any role by name")
                .AnyAsync(q => q.Name.ToLower() == name.ToLower(), token);
            return role;
        }

        public Task<bool> HasAnyByIdAsync(Guid id, CancellationToken token = default)
        {
            var role = AsQueryable()
                .TagWith("Has any role by id")
                .AnyAsync(q => q.Id == id, token);
            return role;
        }
    }
}
