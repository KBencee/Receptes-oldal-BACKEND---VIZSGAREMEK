using System.ComponentModel.DataAnnotations;

namespace ReceptekWebAPI.Models
{
    public class CimkeDto
    {
        public string CimkeNev { get; set; }
    }

    public class ReceptCimkeDto
    {
        public Guid ReceptId { get; set; }
        public int CimkeId { get; set; }
    }
}
