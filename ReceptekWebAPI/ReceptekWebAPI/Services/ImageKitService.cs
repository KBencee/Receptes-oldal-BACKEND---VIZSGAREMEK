using Imagekit.Sdk;
using ReceptekWebAPI.Models;

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
                fileName = file.FileName,
                folder = "/receptek",
                useUniqueFileName = true
            };

            var result = _client.Upload(request);

            if (result == null)
                throw new Exception("ImageKit upload failed");

            return (result.url, result.fileId);
        }

        public async Task DeleteAsync(string fileId)
        {
            if (!string.IsNullOrWhiteSpace(fileId))
            {
                _client.DeleteFile(fileId);
            }
        }
    }
}
