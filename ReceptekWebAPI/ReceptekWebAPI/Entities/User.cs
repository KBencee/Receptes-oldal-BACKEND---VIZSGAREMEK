namespace ReceptekWebAPI.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? ProfileImageFileId { get; set; }

        public ICollection<Recept> Receptek { get; set; } = new List<Recept>();
        public ICollection<MentettRecept> MentettReceptek { get; set; } = new List<MentettRecept>();
    }
}
