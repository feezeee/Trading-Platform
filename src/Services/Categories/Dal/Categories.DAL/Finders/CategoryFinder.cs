using Categories.BLL.Contracts.Finders;
using Categories.BLL.Entities;
using Categories.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Categories.DAL.Finders
{
    public class CategoryFinder : ICategoryFinder
    {
        private readonly CategoryContext _categoryContext;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public CategoryFinder(CategoryContext categoryContext)
        {
            _categoryContext = categoryContext;
        }

        protected IQueryable<CategoryEntity> AsQueryable()
        {
            return _categoryContext.CategoryEntities.AsQueryable();
        }

        public Task<List<CategoryEntity>> GetAllAsync(CancellationToken token = default)
        {
            return AsQueryable().ToListAsync(token);
        }

        public Task<List<CategoryEntity>> GetAllPaginationAsync(int pageNumber, int pageSize, CancellationToken token = default)
        {
            return AsQueryable()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
        }

        public Task<int> GetCountAsync(CancellationToken token = default)
        {
            return AsQueryable().CountAsync(token);
        }

        public Task<CategoryEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return AsQueryable().FirstOrDefaultAsync(t => t.Id == id, token);
        }

        public Task<CategoryEntity?> GetByNameAsync(string name, CancellationToken token = default)
        {
            return AsQueryable().FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower(), token);
        }

        public Task<bool> NameIsFreeAsync(string name, CancellationToken token = default)
        {
            return AsQueryable().AllAsync(t => t.Name.ToLower() != name.ToLower(), token);
        }
    }
}
