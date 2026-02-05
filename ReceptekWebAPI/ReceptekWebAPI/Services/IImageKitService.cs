namespace ReceptekWebAPI.Services
{
    public interface IImageKitService
    {
        Task<(string fileId, string url)> UploadImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(string fileId);
    }
}
