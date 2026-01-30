namespace ReceptekWebAPI.Models
{
    public class ReceptResponseDto
    {
        public Guid Id { get; set; }
        public string Nev { get; set; } = string.Empty;
        public string? Leiras { get; set; } = string.Empty;
        public string? Hozzavalok { get; set; } = string.Empty;
        public int ElkeszitesiIdo { get; set; }
        public string? NehezsegiSzint { get; set; } = string.Empty;
        public int Likes { get; set; }

        public string FeltoltoUsername { get; set; } = string.Empty;

        public List<string> Cimkek { get; set; } = [];
        public bool MentveVan { get; set; }
    }
}
