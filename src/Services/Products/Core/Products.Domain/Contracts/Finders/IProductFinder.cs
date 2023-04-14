using Products.Domain.Entities;

namespace Products.Domain.Contracts.Finders
{
    public interface IProductFinder
    {
        public Task<List<ProductEntity>> GetAllAsync(CancellationToken token = default);
    }
}
