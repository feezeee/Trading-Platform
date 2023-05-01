using Categories.BLL.Contracts.Finders;
using Categories.BLL.Contracts.Repositories;
using Categories.BLL.Contracts.UnitOfWork;
using Categories.DAL.Context;

namespace Categories.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CategoryContext _categoryContext;
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public UnitOfWork(ICategoryRepository categoryRepository, ICategoryFinder categoryFinder, CategoryContext categoryContext)
        {
            CategoryRepository = categoryRepository;
            CategoryFinder = categoryFinder;
            _categoryContext = categoryContext;
        }

        public ICategoryRepository CategoryRepository { get; }

        public ICategoryFinder CategoryFinder { get; }

        public Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            return _categoryContext.SaveChangesAsync(token);
        }
    }
}
