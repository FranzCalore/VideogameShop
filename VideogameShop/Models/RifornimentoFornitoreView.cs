using Microsoft.AspNetCore.Mvc.Rendering;

namespace VideogameShop.Models
{
    public class RifornimentoFornitoreView
    {
        public Rifornimento RifornimentoVideogioco { get; set; }
        public List<Fornitore>? ListaFornitori { get; set; }
    }
}
