namespace VideogameShop.Models
{
    public class Rifornimento
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public Videogioco Videogioco { get; set; }

        public int Quantita { get; set; }

        public string NomeFornitore { get; set; }

        public double Prezzo { get; set; }
    }
}
