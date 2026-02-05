using Imagekit.Sdk;
using ReceptekWebAPI.Models;

namespace ReceptekWebAPI.Services
{
    public class ImageKitService
    {
        private readonly ImagekitClient _imagekit;

        public ImageKitService(ImagekitClient imagekit)
        {
            _imagekit = imagekit;
        }

        public Result Upload(byte[] file, string fileName)
        {
            var request = new FileCreateRequest
            {
                file = file,
                fileName = fileName,
                folder = "/receptek",
                useUniqueFileName = true
            };

            return _imagekit.Upload(request);
        }

        public ResultDelete Delete(string fileId)
        {
            if (string.IsNullOrWhiteSpace(fileId))
                return null;

            return _imagekit.DeleteFile(fileId);
        }
    }
}
