namespace ReceptekWebAPI.Models
{
    public class ReceptDto
    {
        public string Nev { get; set; } = null!;
        public string? Leiras { get; set; }
        public string? Hozzavalok { get; set; }
        public string? ElkeszitesiIdo { get; set; }
        public string? NehezsegiSzint { get; set; }
        public ICollection<int>? CimkeIds { get; set; }
    }
}
