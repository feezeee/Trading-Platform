using AutoMapper;
using Products.Application.Contracts;
using Products.Domain.Contracts.Finders;
using Products.Domain.Contracts.Repositories;
using Products.Domain.Entities;
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
                var products = await _productFinder.GetAllAsync(token);
                return _mapper.Map<List<GetProductDto>>(products);
            }
            catch (AutoMapperConfigurationException)
            {
                throw;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new MyDbException("Some problem with db", e);
            }
        }

        public async Task<GetProductDto?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var product = await _productFinder.GetByIdAsync(id, token);
                return _mapper.Map<GetProductDto?>(product);
            }
            catch (AutoMapperConfigurationException)
            {
                throw;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new MyDbException("Some problem with db", e);
            }
        }

        public async Task<GetProductDto> CreateAsync(CreateProductDto product, CancellationToken token = default)
        {
            try
            {
                var createProduct = _mapper.Map<ProductEntity>(product);
                createProduct.Id = Guid.NewGuid();
                await _productRepository.CreateAsync(createProduct, token);
                return _mapper.Map<GetProductDto>(createProduct);
            }
            catch (AutoMapperConfigurationException)
            {
                throw;
            }
            catch (AutoMapperMappingException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new MyDbException("Some problem with db", e);
            }
        }
    }
}
