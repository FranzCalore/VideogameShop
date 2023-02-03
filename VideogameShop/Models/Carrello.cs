using Microsoft.Identity.Client;

namespace VideogameShop.Models
{
    public class Carrello
    {
        public int Id { get; set; }

        public string NomeUtente { get; set; }

        public List<Acquisto>? ProdottiAcquistati { get; set; } = new();

        public DateTime? DataOra { get; set; }

        public double? PrezzoTotale { get; set; }

        public static Carrello CarrelloCorrente { get; set; } = new();


        public Carrello() {        }
    }

}
