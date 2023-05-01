using Categories.BLL.Contracts.Repositories;
using Categories.BLL.Entities;
using Categories.DAL.Context;

namespace Categories.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryContext _categoryContext;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public CategoryRepository(CategoryContext categoryContext)
        {
            _categoryContext = categoryContext;
        }

        public void Create(CategoryEntity entity)
        {
            _categoryContext.Add(entity);
        }

        public void Update(CategoryEntity entity)
        {
            _categoryContext.Update(entity);
        }

        public void Delete(CategoryEntity entity)
        {
            _categoryContext.Remove(entity);
        }
    }
}
