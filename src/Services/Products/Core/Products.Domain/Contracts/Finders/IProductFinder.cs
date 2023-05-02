using Products.Domain.Entities;

namespace Products.Domain.Contracts.Finders
{
    public interface IProductFinder
    {
        public Task<List<ProductEntity>> GetAllAsync(
            Guid? userId = null,
            decimal? fromPrice = null,
            decimal? toPrice = null,
            bool? priceIsSet = null,
            bool? imagesAreSet = null, 
            CancellationToken token = default);

        public Task<List<ProductEntity>> GetAllPaginationAsync(int pageNumber, int pageSize, CancellationToken token = default);

        public Task<int> GetCountAsync(CancellationToken token = default);

        public Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    }
}
