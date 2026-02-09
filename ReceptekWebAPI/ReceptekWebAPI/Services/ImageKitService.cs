using Imagekit.Sdk;

namespace ReceptekWebAPI.Services
{
    public class ImageKitService : IImageKitService
    {
        private readonly ImagekitClient _client;

        public ImageKitService(ImagekitClient client)
        {
            _client = client;
        }

        public async Task<(string Url, string FileId)> UploadAsync(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var request = new FileCreateRequest
            {
                file = ms.ToArray(),
                fileName = $"{Guid.NewGuid()}_{file.FileName}",
                folder = "/receptek",
                useUniqueFileName = true
            };

            var result = await Task.Run(() => _client.Upload(request));

            if (result == null || string.IsNullOrEmpty(result.url))
                throw new Exception("ImageKit feltöltés sikertelen");

            return (result.url, result.fileId);
        }

        public async Task DeleteAsync(string fileId)
        {
            if (!string.IsNullOrWhiteSpace(fileId))
            {
                await Task.Run(() => _client.DeleteFile(fileId));
            }
        }
    }
}