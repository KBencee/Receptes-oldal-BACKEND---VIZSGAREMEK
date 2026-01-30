namespace ReceptekWebAPI.Models
{
    public class MentettReceptResponseDto
    {
        public Guid ReceptId { get; set; }
        public string Nev { get; set; } = string.Empty;
        public int Likes { get; set; }
        public DateTime MentveEkkor { get; set; }
    }
}
