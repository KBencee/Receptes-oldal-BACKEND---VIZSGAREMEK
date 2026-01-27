namespace ReceptekWebAPI.Entities
{
    public class Cimke
    {
        public int CimkeId { get; set; }
        public string CimkeNev { get; set; }

        public ICollection<ReceptCimke> ReceptCimkek { get; set; } = new List<ReceptCimke>();
    }
}
