namespace ReceptekWebAPI.Models
{
    public class KommentResponseDto
    {
        public Guid Id { get; set; }
        public string Szoveg { get; set; } = string.Empty;
        public DateTime IrtaEkkor { get; set; } 
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public bool SajatKomment { get; set; }
    }
}
