using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Categories.API.Models.Category;
using Categories.API.Models.Pagination.Response;
using Categories.BLL.Contracts.UnitOfWork;
using Categories.BLL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Categories.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(ILogger<CategoryController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCategoryResponse>>> GetAllAsync(CancellationToken token = default)
        {
            try
            {
                var categories = await _unitOfWork.CategoryFinder.GetAllAsync(token);
                return Ok(_mapper.Map<List<GetCategoryResponse>>(categories));
            }
            catch (Exception e)
            {
               _logger.LogError(e, e.Message);
               return StatusCode(500);
            }
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<GetPaginationResponse<GetCategoryResponse>>> GetAllPaginationAsync(
            [FromQuery][BindRequired][Required] int pageNumber,
            [FromQuery][BindRequired][Required] int pageSize, 
            CancellationToken token = default)
        {
            try
            {
                if (pageNumber < 1)
                {
                    return BadRequest("pageNumber less than 1");
                }

                if (pageSize < 1)
                {
                    return BadRequest("pageNumber less than 1");
                }

                var categories = await _unitOfWork.CategoryFinder.GetAllPaginationAsync(pageNumber, pageSize, token);
                var totalCount = await _unitOfWork.CategoryFinder.GetCountAsync(token);
                return Ok(new GetPaginationResponse<GetCategoryResponse>(
                    _mapper.Map<List<GetCategoryResponse>>(categories),
                    pageNumber, pageSize, totalCount));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]Guid id, CancellationToken token = default)
        {
            try
            {
                var category = await _unitOfWork.CategoryFinder.GetByIdAsync(id, token);
                return Ok(_mapper.Map<GetCategoryResponse?>(category));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryRequest categoryRequest, CancellationToken token = default)
        {
            try
            {
                var nameIsFree = await _unitOfWork.CategoryFinder.NameIsFreeAsync(categoryRequest.Name, token);

                if (!nameIsFree)
                {
                    _logger.LogError("Category name is not free");
                    return BadRequest();
                }

                var category = _mapper.Map<CategoryEntity>(categoryRequest);
                category.Id = Guid.NewGuid();

                _unitOfWork.CategoryRepository.Create(category);
                await _unitOfWork.SaveChangesAsync(token);

                var res = _mapper.Map<GetCategoryResponse>(category);

                return CreatedAtAction(nameof(GetByIdAsync), new { id = category.Id }, res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCategoryRequest categoryRequest, CancellationToken token = default)
        {
            try
            {
                var category = await _unitOfWork.CategoryFinder.GetByIdAsync(categoryRequest.Id, token);
                
                if (category is null)
                {
                    _logger.LogError("Bad category id");
                    return BadRequest();
                }

                var categoryByName = await _unitOfWork.CategoryFinder.GetByNameAsync(categoryRequest.Name, token);
                if (categoryByName is not null && categoryByName.Name.ToLower() != category.Name.ToLower())
                {
                    _logger.LogError("Category name is not free");
                    return BadRequest();
                }

                _mapper.Map(categoryRequest, category);
                _unitOfWork.CategoryRepository.Update(category);
                await _unitOfWork.SaveChangesAsync(token);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id, CancellationToken token = default)
        {
            try
            {
                var category = await _unitOfWork.CategoryFinder.GetByIdAsync(id, token);

                if (category is not null)
                {
                    _unitOfWork.CategoryRepository.Delete(category);
                    await _unitOfWork.SaveChangesAsync(token);
                }

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

    }
}