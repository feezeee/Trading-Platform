using Products.Models.Pagintaion;
using Products.Models.Products;

namespace Products.Application.Contracts
{
    public interface IProductService
    {
        public Task<List<GetProductDto>> GetAllAsync(
            Guid? userId = null,
            decimal? fromPrice = null,
            decimal? toPrice = null,
            bool? priceIsSet = null,
            bool? imagesAreSet = null, 
            CancellationToken token = default);

        public Task<GetPaginationDto<GetProductDto>> GetAllPaginationAsync(int pageNumber, int pageSize,
            CancellationToken token = default);

        public Task<GetProductDto?> GetByIdAsync(Guid id, CancellationToken token = default);

        public Task<GetProductDto> CreateAsync(CreateProductDto product, CancellationToken token = default);

        public Task<GetProductDto> UpdateAsync(UpdateProductDto product, CancellationToken token = default);

        public Task DeleteAsync(Guid id, CancellationToken token = default);
    }
}
