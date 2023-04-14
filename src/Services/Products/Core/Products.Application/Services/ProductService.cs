using AutoMapper;
using Products.Application.Contracts;
using Products.Domain.Contracts.Finders;
using Products.Domain.Contracts.Repositories;
using Products.Models.Products;
using Products.Exceptions.DbExceptions;

namespace Products.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductFinder _productFinder;


        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ProductService(IMapper mapper, IProductRepository productRepository, IProductFinder productFinder)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productFinder = productFinder;
        }

        public async Task<List<GetProductDto>> GetAllAsync(CancellationToken token = default)
        {
            try
            {
                var orders = await _productFinder.GetAllAsync(token);
                return _mapper.Map<List<GetProductDto>>(orders);
            }
            catch (Exception e)
            {
                throw new MyDbException("Some problem with db", e);
            }
        }
    }
}
