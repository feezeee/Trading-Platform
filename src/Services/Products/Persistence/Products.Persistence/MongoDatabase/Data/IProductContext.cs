using MongoDB.Driver;
using Products.Domain.Entities;

namespace Products.Persistence.MongoDatabase.Data
{
    public interface IProductContext
    {
        IMongoCollection<ProductEntity> Products { get; }
    }
}
