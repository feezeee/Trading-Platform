namespace Images.Core.Contracts.Infrastructure
{
    public interface IImageUploader
    {
        public Task<string> UploadImageAsync(Stream fileStream, string fileName,
            CancellationToken token = default);
    }
}
