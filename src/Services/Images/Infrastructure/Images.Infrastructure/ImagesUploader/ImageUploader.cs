using Images.Core.Contracts.Infrastructure;
using Images.Core.Exceptions;

namespace Images.Infrastructure.ImagesUploader
{
    public class ImageUploader : IImageUploader
    {
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

                return $"/{savePath}";
            }
            catch (Exception e)
            {
                throw new ImageUploadException("Some problem with next cloud service", e);
            }
        }
    }
}
