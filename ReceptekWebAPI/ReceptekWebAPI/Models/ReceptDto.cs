using System.ComponentModel.DataAnnotations;

namespace ReceptekWebAPI.Models
{
    public class ReceptDto
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Nev { get; set; } = null!;
        [StringLength(2000)]
        public string? Leiras { get; set; }
        [StringLength(2000)]
        public string? Hozzavalok { get; set; }
        [Range(1, 1440)]
        public int ElkeszitesiIdo { get; set; }
        [StringLength(50)]
        public string? NehezsegiSzint { get; set; }
        public ICollection<int>? CimkeIds { get; set; }
    }
}
