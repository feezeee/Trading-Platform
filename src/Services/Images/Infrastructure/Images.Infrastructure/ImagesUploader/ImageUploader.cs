using Images.Core.Contracts.Infrastructure;
using Images.Infrastructure.Configurations;
using Images.Core.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

namespace Images.Infrastructure.ImagesUploader
{
    public class ImageUploader : IImageUploader
    {
        private readonly NextCloudConfiguration _nextCloudConfiguration;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ImageUploader(NextCloudConfiguration nextCloudConfiguration)
        {
            _nextCloudConfiguration = nextCloudConfiguration;
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string fileName, CancellationToken token = default)
        {
            try
            {
                var filePath = $"{Guid.NewGuid()}{Guid.NewGuid()}{fileName}";

                var restOptions = new RestClientOptions(_nextCloudConfiguration.Url)
                {
                    Authenticator = new HttpBasicAuthenticator(_nextCloudConfiguration.UserName, _nextCloudConfiguration.Password)
                };

                var client = new RestClient(restOptions);

                
                
                var request = new RestRequest("remote.php/dav/files/{username}/{filePath}", Method.Put);

                request.AddHeader("Content-Type", "application/octet-stream");
                request.AddParameter("username", _nextCloudConfiguration.UserName, ParameterType.UrlSegment);
                request.AddParameter("filePath", filePath, ParameterType.UrlSegment);

                byte[] fileBytes;

                if (fileStream is MemoryStream)
                    fileBytes = ((MemoryStream)fileStream).ToArray();

                using (var memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }


                request.AddParameter("application/octet-stream", fileBytes, ParameterType.RequestBody);

                var response = await client.ExecuteAsync(request, token);

                // Получаем ответ от сервера Nextcloud
                if (response.IsSuccessful)
                {
                    return $"{_nextCloudConfiguration.Url}/remote.php/dav/files/{_nextCloudConfiguration.UserName}/{filePath}";
                }
                else
                {
                    throw new ImageUploadException("Some problem with next cloud service");
                }
            }
            catch (Exception e)
            {
                throw new ImageUploadException("Some problem with next cloud service");
            }
        }
    }
}
