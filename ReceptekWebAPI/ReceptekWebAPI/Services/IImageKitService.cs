namespace ReceptekWebAPI.Services
{
    public interface IImageKitService
    {
        Task<(string Url, string FileId)> UploadAsync(IFormFile file);
        Task DeleteAsync(string fileId);
    }
}
