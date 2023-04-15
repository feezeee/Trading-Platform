using Images.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace Images.Api.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IImageService _imageService; 

        public ImageController(ILogger<ImageController> logger, IImageService imageService)
        {
            _logger = logger;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, CancellationToken token = default)
        {
            try
            {
                var path = await _imageService.UploadFileAsync(file.OpenReadStream(), file.FileName, token);
                return Ok(new
                {
                    image_url = path,
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
