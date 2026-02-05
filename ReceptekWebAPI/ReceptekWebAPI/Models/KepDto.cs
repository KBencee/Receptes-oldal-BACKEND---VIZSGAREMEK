namespace ReceptekWebAPI.Models
{
    public class KepResponseDto
    {
        public string? FileId { get; set; }
        public IFormFile File { get; set; }
        public string? Url { get; set; }
    }
}
