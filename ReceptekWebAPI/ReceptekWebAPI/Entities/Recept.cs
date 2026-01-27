namespace ReceptekWebAPI.Entities
{
    public class Recept
    {
        public Guid Id { get; set; }
        public string Nev { get; set; }
        public string Leiras { get; set; }
        public string Hozzavalok { get; set; }
        public string ElkeszitesiIdo { get; set; }
        public string NehezsegiSzint { get; set; }
        public int Likes { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public ICollection<ReceptCimke> ReceptCimkek { get; set; } = new List<ReceptCimke>();
    }
}
