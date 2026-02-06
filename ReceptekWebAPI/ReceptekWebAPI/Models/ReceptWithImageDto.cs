using System.ComponentModel.DataAnnotations;

namespace ReceptekWebAPI.Models
{
    public class ReceptWithImageDto
    {
        [Required, StringLength(200)]
        public string Nev { get; set; } = null!;
        public string? Leiras { get; set; }
        public string? Hozzavalok { get; set; }
        public int ElkeszitesiIdo { get; set; }
        public string? NehezsegiSzint { get; set; }
        public List<int>? CimkeIds { get; set; }

        public IFormFile Kep { get; set; }
    }
}
