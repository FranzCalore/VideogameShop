using Microsoft.AspNetCore.Mvc.Rendering;

namespace VideogameShop.Models
{
    public class VideogiocoTipologiaView
    {
        public Videogioco Videogioco { get; set; }

        public List<Tipologia>? Tipologie { get; set; }

        public Acquisto? Acquisto { get; set; }

    }
}
