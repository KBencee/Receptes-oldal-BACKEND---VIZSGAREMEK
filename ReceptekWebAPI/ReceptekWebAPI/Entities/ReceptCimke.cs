namespace ReceptekWebAPI.Entities
{
    public class ReceptCimke
    {
        public Guid ReceptId { get; set; }
        public Recept? Recept { get; set; }

        public int CimkeId { get; set; }
        public Cimke? Cimke { get; set; }
    }
}
