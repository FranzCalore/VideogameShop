using System.Text.Json.Serialization;

namespace VideogameShop.Models
{
    public class Console
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public List<Videogioco>? ListaGiochi { get; set; }

        public Console() { }
    }
}
