using Images.Core.Contracts.Infrastructure;
using Images.Infrastructure.Configurations;
using Images.Core.Exceptions;
using Microsoft.Extensions.Options;

namespace Images.Infrastructure.ImagesUploader
{
    public class ImageUploader : IImageUploader
    {
        private readonly NextCloudSettings _nextCloudSettings;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ImageUploader(IOptions<NextCloudSettings> nextCloudConfigurationOptions)
        {
            _nextCloudSettings = nextCloudConfigurationOptions.Value;
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string fileName, CancellationToken token = default)
        {
            try
            {
                var filePath = $"{Guid.NewGuid()}{Guid.NewGuid()}{fileName}";

                string savePath = Path.Combine("images", filePath);

                byte[] fileBytes;
                
                using (var memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream, token);
                    fileBytes = memoryStream.ToArray();
                }

                if (!Directory.Exists("images"))
                {
                    Directory.CreateDirectory("images");
                }
                await File.WriteAllBytesAsync(savePath, fileBytes, token);

                return savePath;
            }
            catch (Exception e)
            {
                throw new ImageUploadException("Some problem with next cloud service", e);
            }
        }
    }
}
