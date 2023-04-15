using Products.Models.Products;

namespace Products.Application.Contracts
{
    public interface IProductService
    {
        public Task<List<GetProductDto>> GetAllAsync(CancellationToken token = default);

        public Task<GetProductDto?> GetByIdAsync(Guid id, CancellationToken token = default);

        public Task<GetProductDto> CreateAsync(CreateProductDto product, CancellationToken token = default);

        public Task<GetProductDto> UpdateAsync(UpdateProductDto product, CancellationToken token = default);

        public Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
