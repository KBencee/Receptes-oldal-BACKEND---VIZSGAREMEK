using System.ComponentModel.DataAnnotations;

namespace ReceptekWebAPI.Entities
{
    public class FeltoltottKep
    {
        [Key]
        public Guid Id { get; set; }
        public string FileId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public Guid ReceptId { get; set; }
        public Recept Recept { get; set; } = null!;
    }
}
