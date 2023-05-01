using Categories.BLL.Entities;

namespace Categories.BLL.Contracts.Finders
{
    public interface ICategoryFinder
    {
        public Task<List<CategoryEntity>> GetAllAsync(CancellationToken token = default);

        public Task<List<CategoryEntity>> GetAllPaginationAsync(int pageNumber, int pageSize, CancellationToken token = default);

        public Task<int> GetCountAsync(CancellationToken token = default);

        public Task<CategoryEntity?> GetByIdAsync(Guid id, CancellationToken token = default);

        public Task<CategoryEntity?> GetByNameAsync(string name, CancellationToken token = default);

        public Task<bool> NameIsFreeAsync(string name, CancellationToken token = default);
    }
}
