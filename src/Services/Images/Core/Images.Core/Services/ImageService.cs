using Images.Core.Contracts;
using Images.Core.Contracts.Infrastructure;
using Images.Core.Exceptions;

namespace Images.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageUploader _imageUploader;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ImageService(IImageUploader imageUploader)
        {
            _imageUploader = imageUploader;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken token = default)
        {
            try
            {
                return await _imageUploader.UploadImageAsync(fileStream, fileName, token);
            }
            catch (Exception e)
            {
                throw new ImageUploadException("Some problem with image", e);
            }
        }
    }
}
