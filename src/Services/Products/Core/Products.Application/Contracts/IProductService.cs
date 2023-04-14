using Products.Models.Products;

namespace Products.Application.Contracts
{
    public interface IProductService
    {
        public Task<List<GetProductDto>> GetAllAsync(CancellationToken token = default);
    }
}
