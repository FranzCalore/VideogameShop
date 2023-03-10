using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VideogameShop.Models
{
    public class Videogioco
    {

        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        [Required(ErrorMessage = "Il nome del videogioco è obbligatorio!")]
        [StringLength(100, ErrorMessage = "Il nome della del videogioco è troppo lungo! (>100 caratteri)")]
        public string Nome { get; set; }

        [Column(TypeName = "text")]
        [Required(ErrorMessage = "La descrizione del videogioco è obbligatoria!")]
        public string Descrizione { get; set; }

        [Column(TypeName = "varchar(300)")]
        [Url(ErrorMessage = "L'url inserito è invalido!")]
        public string Foto { get; set; }

        [Column(TypeName = "varchar(300)")]
        [Url(ErrorMessage = "L'url inserito è invalido!")]
        public string? FotoOrizzontale { get; set; }

        [Required(ErrorMessage = "Il prezzo del videogioco è obbligatorio")]
        [Range(0, 10000, ErrorMessage = "Il prezzo deve essere superiore a 0!")]
        public double Prezzo { get; set; }

        [Range(0,int.MaxValue)]
        [DefaultValue(0)]
        public int QuantitaDisponibile { get; set; }

        public int TipologiaId { get; set; }
        public Tipologia? Tipologia { get; set; }

        public List<Acquisto>? Acquisti { get; set; }

        public List<Rifornimento>? ListaRifornimenti { get; set; }

        public List<Console>? ListaConsole { get; set; }

        [DefaultValue(0)]
        public int NumeroLike { get; set; }

        //Costruttori:

        public Videogioco() { }

        public Videogioco(int id, string nome, string descrizione, string foto, float prezzo, int tipologiaId, Tipologia? tipologia)
        {
            Id = id;
            Nome = nome;
            Descrizione = descrizione;
            Foto = foto;
            Prezzo = prezzo;
            TipologiaId = tipologiaId;
            Tipologia = tipologia;
        }
    }
}
