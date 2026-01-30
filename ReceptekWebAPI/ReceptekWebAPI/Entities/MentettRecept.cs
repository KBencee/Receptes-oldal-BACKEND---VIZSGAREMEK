namespace ReceptekWebAPI.Entities
{
    public class MentettRecept
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid ReceptId { get; set; }
        public Recept Recept { get; set; } = null!;

        public DateTime MentveEkkor { get; set; } = DateTime.UtcNow;
    }
}
