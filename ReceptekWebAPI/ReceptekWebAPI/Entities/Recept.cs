namespace ReceptekWebAPI.Entities
{
    public class Recept
    {
        public Guid Id { get; set; }
        public string Nev { get; set; } = string.Empty;
        public string Leiras { get; set; } = string.Empty;
        public string Hozzavalok { get; set; } = string.Empty;
        public int ElkeszitesiIdo { get; set; }
        public string NehezsegiSzint { get; set; } = string.Empty;
        public int Likes { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public DateTime FeltoltveEkkor { get; set; }

        public ICollection<ReceptCimke> ReceptCimkek { get; set; } = new List<ReceptCimke>();
        public ICollection<MentettRecept> MentettReceptek { get; set; } = new List<MentettRecept>();

        public FeltoltottKep? Kep { get; set; }
    }
}
