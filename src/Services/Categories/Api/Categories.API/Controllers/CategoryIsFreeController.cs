using Categories.BLL.Contracts.Finders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Categories.BLL.Contracts.UnitOfWork;

namespace Categories.API.Controllers
{
    [ApiController]
    [Route("api/category-name-is-free")]
    public class CategoryIsFreeController : ControllerBase
    {
        private readonly ILogger<CategoryIsFreeController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public CategoryIsFreeController(ILogger<CategoryIsFreeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Determines whether [is free asynchronous] [the specified category name].
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> IsFreeAsync(
            [FromQuery][BindRequired][Required] string categoryName, CancellationToken token = default)
        {
            try
            {
                var isFree = await _unitOfWork.CategoryFinder.NameIsFreeAsync(categoryName, token);
                return Ok(new
                {
                    is_free = isFree
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
    }
}
