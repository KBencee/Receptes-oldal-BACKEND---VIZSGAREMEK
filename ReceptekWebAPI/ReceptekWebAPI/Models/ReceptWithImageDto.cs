using System.ComponentModel.DataAnnotations;

namespace ReceptekWebAPI.Models
{
    public class ReceptWithImageDto
    {
        [Required, StringLength(200)]
        public string Nev { get; set; } = null!;
        [StringLength(2000), MinLength(1)]
        public string? Leiras { get; set; }
        [StringLength(2000), MinLength(1)]
        public string? Hozzavalok { get; set; }
        [Range(1, 1440)]
        public int ElkeszitesiIdo { get; set; }
        [StringLength(50), MinLength(1)]
        public string? NehezsegiSzint { get; set; }
        public List<int>? CimkeIds { get; set; }

        public IFormFile Kep { get; set; }
    }
}
