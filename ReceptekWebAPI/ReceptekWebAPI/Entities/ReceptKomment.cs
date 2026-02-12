namespace ReceptekWebAPI.Entities
{
    public class ReceptKomment
    {
        public Guid Id { get; set; }
        public string Szoveg { get; set; } = string.Empty;
        public DateTime IrtaEkkor { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid ReceptId { get; set; }
        public Recept Recept { get; set; } = null;
    }
}
