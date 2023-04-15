using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Models.Products.Request;
using Products.Api.Models.Products.Response;
using Products.Application.Contracts;
using Products.Exceptions.RecordException;
using Products.Models.Products;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductResponse?>> GetByIdAsync([FromRoute(Name = "id")] Guid id, CancellationToken token = default)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id, token);
                return _mapper.Map<GetProductResponse?>(product);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PostProductRequest product, CancellationToken token = default)
        {
            try
            {
                var createProduct = _mapper.Map<CreateProductDto>(product);
                var productCreated = await _productService.CreateAsync(createProduct, token);
                var getProduct = _mapper.Map<GetProductResponse>(productCreated);

                return CreatedAtAction(nameof(GetByIdAsync), new {id = productCreated.Id}, getProduct);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] PutProductRequest product,
            CancellationToken token = default)
        {
            try
            {
                var updateProduct = _mapper.Map<UpdateProductDto>(product);
                var productUpdated = await _productService.UpdateAsync(updateProduct, token);
                return Ok(_mapper.Map<GetProductResponse>(productUpdated));
            }
            catch (RecordNotFoundException e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken token = default)
        {
            try
            {
                await _productService.DeleteAsync(id, token);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(500);
            }
        }
    }
}
