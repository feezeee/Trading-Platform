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

        protected IMongoQueryable<ProductEntity> AsQueryable(
            Guid? userId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            bool? priceIsSet = null,
            bool? imagesAreSet = null)
        {
            var data = _orderContext.Products.AsQueryable();

            data = userId is null
                ? data
                : data.Where(t => t.UserId == userId);

            data = priceIsSet switch
            {
                null when minPrice != null && maxPrice != null => data.Where(t =>
                    t.Price == null || (t.Price != null && minPrice <= t.Price && t.Price <= maxPrice)),
                null when minPrice != null => data.Where(t =>
                    t.Price == null || (t.Price != null && minPrice <= t.Price)),
                null when maxPrice != null => data.Where(t =>
                    t.Price == null || (t.Price != null && t.Price <= maxPrice)),
                null => data.Where(t => t.Price == null || t.Price != null),
                true when minPrice != null && maxPrice != null => data.Where(t =>
                    (t.Price != null && minPrice <= t.Price && t.Price <= maxPrice)),
                true when minPrice != null => data.Where(t => (t.Price != null && minPrice <= t.Price)),
                true when maxPrice != null => data.Where(t => (t.Price != null && t.Price <= maxPrice)),
                true => data.Where(t => t.Price != null),
                false => data.Where(t => t.Price == null)
            };

            data = imagesAreSet is null
                ? data
                : imagesAreSet == true
                    ? data.Where(t => t.ImageUrls.Any())
                    : data.Where(t => !t.ImageUrls.Any());

            return data;
        }

        public Task<List<ProductEntity>> GetAllAsync(
            Guid? userId = null,
            decimal? fromPrice = null,
            decimal? toPrice = null,
            bool? priceIsSet = null,
            bool? imagesAreSet = null,
            CancellationToken token = default)
        {
            return AsQueryable(
                userId: userId,
                minPrice: fromPrice,
                maxPrice: toPrice,
                priceIsSet: priceIsSet,
                imagesAreSet: imagesAreSet).ToListAsync(token);
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
