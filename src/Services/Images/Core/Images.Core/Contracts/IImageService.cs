namespace Images.Core.Contracts
{
    public interface IImageService
    {
        public Task<string> UploadFileAsync(Stream fileStream, string fileName,
            CancellationToken token = default);
    }
}
