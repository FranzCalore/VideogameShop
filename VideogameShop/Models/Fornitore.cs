using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideogameShop.Models;

namespace VideogameShop.Models
{
    [Table("Fornitore")]
    public class Fornitore
    {
        public int FornitoreId { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required(ErrorMessage = "Il nome del fornitore è obbligatorio!")]
        [StringLength(150, ErrorMessage = "Il nome del fornitore è troppo lungo! (>150 caratteri)")]
        public string FornitoreNome { get; set; }

        public List<Rifornimento>? Rifornimenti { get; set; }

        public Fornitore() { }

        public Fornitore (int fornitoreId, string fornitoreNome, string fornitoreIndirizzo, string fornitoreNumero)
        {
            FornitoreId = fornitoreId;
            FornitoreNome = fornitoreNome;
        }
    }
}
