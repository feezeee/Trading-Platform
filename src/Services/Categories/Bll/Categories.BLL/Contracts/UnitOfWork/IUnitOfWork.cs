using Categories.BLL.Contracts.Finders;
using Categories.BLL.Contracts.Repositories;

namespace Categories.BLL.Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }

        public ICategoryFinder CategoryFinder { get; }

        public Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
