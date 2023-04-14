using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Models.Products.Response;
using Products.Application.Contracts;

namespace Products.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ILogger<ProductController> logger, IMapper mapper)
        {
            _productService = productService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProductResponse>>> GetAllAsync(CancellationToken token = default)
        {
            try
            {
                var products = await _productService.GetAllAsync(token);
                return _mapper.Map<List<GetProductResponse>>(products);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(500);
            }
        }
    }
}
