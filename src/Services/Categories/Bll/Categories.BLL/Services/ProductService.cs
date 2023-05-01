using System.Text.Json;
using Categories.BLL.Contracts.Services;
using Categories.BLL.Options;
using Microsoft.Extensions.Options;
using Products.Api.Models.Products.Request;
using Products.Api.Models.Products.Response;
using RestSharp;

namespace Categories.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsApiOptions _productsApiOptions;


        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ProductService(IOptions<ProductsApiOptions> productsApiOptionsParam)
        {
            _productsApiOptions = productsApiOptionsParam.Value;
        }

        public async Task<List<GetProductResponse>> GetAllAsync(CancellationToken token = default)
        {
            var client = new RestClient($"{_productsApiOptions.ProductsApiHost}{_productsApiOptions.GetProducts}");
            var request = new RestRequest("", RestSharp.Method.Get);
            var response = await client.GetAsync(request, token);

            return JsonSerializer.Deserialize<List<GetProductResponse>>(response.Content ?? "", new JsonSerializerOptions
            {
                MaxDepth = 0
            }) ?? new List<GetProductResponse>();
        }

        public async Task<List<GetProductResponse>> GetWithCategoryAsync(Guid categoryId, CancellationToken token = default)
        {
            var products = await GetAllAsync(token);
            return products.Where(t => t.CategoryIdList.Any(t => t == categoryId)).ToList();
        }

        public async Task<GetProductResponse> UpdateProductAsync(PutProductRequest putProduct, CancellationToken token = default)
        {
            var client = new RestClient($"{_productsApiOptions.ProductsApiHost}{_productsApiOptions.UpdateProduct}");
            var request = new RestRequest("", RestSharp.Method.Put);

            var json = JsonSerializer.Serialize<PutProductRequest>(putProduct, new JsonSerializerOptions
            {
                MaxDepth = 0
            });
            request.AddJsonBody(json);

            var response = await client.PutAsync(request, token);

            return JsonSerializer.Deserialize<GetProductResponse>(response.Content ?? "", new JsonSerializerOptions
            {
                MaxDepth = 0
            })!;
        }
    }
}
