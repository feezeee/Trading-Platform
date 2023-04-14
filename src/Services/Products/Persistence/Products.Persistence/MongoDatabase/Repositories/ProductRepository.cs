using MongoDB.Driver;
using Products.Domain.Contracts.Repositories;
using Products.Domain.Entities;
using Products.Persistence.MongoDatabase.Data;

namespace Products.Persistence.MongoDatabase.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _orderContext;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ProductRepository(IProductContext orderContext)
        {
            _orderContext = orderContext;
        }

        public Task CreateAsync(ProductEntity product, CancellationToken token = default)
        {
            return _orderContext.Products.InsertOneAsync(product, null, token);
        }

        public Task UpdateAsync(ProductEntity product, CancellationToken token = default)
        {
            var filter = Builders<ProductEntity>.Filter.Eq(f => f.Id, product.Id);

            return _orderContext.Products.ReplaceOneAsync(filter, product, null as ReplaceOptions, token);
        }

        public Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            var filter = Builders<ProductEntity>.Filter.Eq(f => f.Id, id);

            return _orderContext.Products.DeleteOneAsync(filter, token);
        }
    }
}
