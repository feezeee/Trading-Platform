using Products.Domain.Entities;

namespace Products.Domain.Contracts.Repositories
{
    public interface IProductRepository
    {
        public Task CreateAsync(ProductEntity product, CancellationToken token = default);

        public Task UpdateAsync(ProductEntity product, CancellationToken token = default);

        public Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
