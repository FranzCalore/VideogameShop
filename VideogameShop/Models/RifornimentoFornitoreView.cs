using VideogameShop.Models;

namespace VideogameShop.Models
{
    public class RifornimentoFornitoreView
    {
        public Rifornimento RifornimentoVideogioco { get; set; }
        public List<Fornitore>? ListaFornitori { get; set; }
    }
}
