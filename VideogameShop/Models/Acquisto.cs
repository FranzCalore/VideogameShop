using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideogameShop.Models;

namespace VideogameShop.Models
{
    [Table("Acquisto")]
    public class Acquisto
    {
        public int AcquistoId { get; set; }

        public DateTime DataAcquisto { get; set; }
        public int VideogiocoId { get; set; }

        public Videogioco? Videogioco { get; set; }

        public int Quantita { get; set; }

        public List<Carrello>? Carrelli { get; set; }

        public Acquisto() { }

        public Acquisto(int acquistoId, DateTime dataAcquisto, int videogiocoId, int quantita)
        {
            AcquistoId = acquistoId;
            DataAcquisto = dataAcquisto;
            VideogiocoId = videogiocoId;
            Quantita = quantita;
        }
    }
}
