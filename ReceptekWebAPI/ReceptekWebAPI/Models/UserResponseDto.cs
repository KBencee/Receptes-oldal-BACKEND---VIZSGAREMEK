namespace ReceptekWebAPI.Models
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Role { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
