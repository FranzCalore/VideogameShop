using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideogameShop.Models;

namespace VideogameShop.Models
{
    [Table("Tipologia")]
    public class Tipologia
    {
        public int TipologiaId { get; set; }
        public string TipologiaNome { get; set; }
        public List<Videogioco>? Videogiochi { get; set; }

        public Tipologia() { }

        public Tipologia(int tipologiaId, string tipologiaNome)
        {
            TipologiaId = tipologiaId;
            TipologiaNome = tipologiaNome;
        }
    }
}
