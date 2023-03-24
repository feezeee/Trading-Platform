using Users.Domain.Contracts;

namespace Users.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly UserContext _userContext;

        public UnitOfWork(UserContext userContext)
        {
            _userContext = userContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            return _userContext.SaveChangesAsync(token);
        }
    }
}
