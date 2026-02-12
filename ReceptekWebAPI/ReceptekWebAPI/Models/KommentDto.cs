using System.ComponentModel.DataAnnotations;

namespace ReceptekWebAPI.Models
{
    public class KommentDto
    {
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "A komment 1 és 1000 karakter között lehet")]
        public string Szoveg { get; set; } = null!;
    }
}
