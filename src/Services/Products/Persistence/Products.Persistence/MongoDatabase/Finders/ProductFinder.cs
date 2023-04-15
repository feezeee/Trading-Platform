using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Products.Domain.Contracts.Finders;
using Products.Domain.Entities;
using Products.Persistence.MongoDatabase.Data;

namespace Products.Persistence.MongoDatabase.Finders
{
    public class ProductFinder : IProductFinder
    {
        private readonly IProductContext _orderContext;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ProductFinder(IProductContext orderContext)
        {
            _orderContext = orderContext;
        }

        protected IMongoQueryable<ProductEntity> AsQueryable()
        {
            return _orderContext.Products.AsQueryable();
        }

        public Task<List<ProductEntity>> GetAllAsync(CancellationToken token = default)
        {
            return AsQueryable().ToListAsync(token);
        }

        public Task<List<ProductEntity>> GetAllPaginationAsync(int pageNumber, int pageSize, CancellationToken token = default)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "pageNumber less than 1");
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize less than 1");
            }

            return AsQueryable().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(token);
        }

        public Task<int> GetCountAsync(CancellationToken token = default)
        {
            return AsQueryable().CountAsync(token);
        }

        public Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return AsQueryable().FirstOrDefaultAsync(t => t.Id == id, token)!;
        }
    }
}
