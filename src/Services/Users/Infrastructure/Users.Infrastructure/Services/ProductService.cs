using Products.Api.Models.Products.Response;
using RestSharp;
using System.Text.Json;
using Users.Application.Contracts;
using Users.Application.Options;

namespace Users.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsApiOptions _productsApiOptions;

        public ProductService(ProductsApiOptions productsApiOptions)
        {
            _productsApiOptions = productsApiOptions;
        }

        public Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var client = new RestClient($"{_productsApiOptions.ProductsApiHost}{_productsApiOptions.DeleteProduct}/{id}");
            var request = new RestRequest("", RestSharp.Method.Delete);
            return client.DeleteAsync(request, token);
        }

        public async Task<List<GetProductResponse>> GetByUserIdAsync(Guid userId, CancellationToken token = default)
        {
            var client = new RestClient($"{_productsApiOptions.ProductsApiHost}{_productsApiOptions.GetProducts}");
            var request = new RestRequest("", RestSharp.Method.Get);
            request.AddParameter("userId", userId);
            var response = await client.GetAsync(request, token);

            return JsonSerializer.Deserialize<List<GetProductResponse>>(response.Content ?? "", new JsonSerializerOptions
            {
                MaxDepth = 0
            }) ?? new List<GetProductResponse>();
        }
    }
}
