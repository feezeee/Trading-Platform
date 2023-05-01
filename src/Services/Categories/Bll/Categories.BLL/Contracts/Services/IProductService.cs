using Products.Api.Models.Products.Request;
using Products.Api.Models.Products.Response;

namespace Categories.BLL.Contracts.Services
{
    public interface IProductService
    {
        public Task<List<GetProductResponse>> GetAllAsync(CancellationToken token = default);

        public Task<List<GetProductResponse>> GetWithCategoryAsync(Guid categoryId, CancellationToken token = default);

        public Task<GetProductResponse> UpdateProductAsync(PutProductRequest putProduct,
            CancellationToken token = default);
    }
}
