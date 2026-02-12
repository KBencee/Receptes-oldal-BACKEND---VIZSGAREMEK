namespace ReceptekWebAPI.Entities
{
    public class Like
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid ReceptId { get; set; }
        public Recept Recept { get; set; } = null!;

        public DateTime LikeoltaEkkor { get; set; } = DateTime.UtcNow;
    }
}
