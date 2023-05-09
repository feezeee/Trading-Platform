using Products.Api.Models.Products.Response;

namespace Users.Application.Contracts
{
    public interface IProductService
    {
        public Task<List<GetProductResponse>> GetByUserIdAsync(Guid userId, CancellationToken token = default);
        public Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
