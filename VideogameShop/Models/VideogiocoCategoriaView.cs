using Microsoft.AspNetCore.Mvc.Rendering;

namespace VideogameShop.Models
{
    public class VideogiocoCategoriaView
    {
        public Videogioco Videogioco { get; set; }

        public List<Tipologia>? Tipologie { get; set; }

    }
}
