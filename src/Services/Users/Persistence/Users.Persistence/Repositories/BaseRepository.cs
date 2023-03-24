using Users.Domain.Contracts.Repositories;

namespace Users.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly UserContext _userContext;

        public BaseRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public void Create(T entity)
        {
            _userContext.Add(entity);
        }

        public void Update(T entity)
        {
            _userContext.Update(entity);
        }

        public void Delete(T entity)
        {
            _userContext.Remove(entity);
        }
    }
}
