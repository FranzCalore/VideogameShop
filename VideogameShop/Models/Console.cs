namespace VideogameShop.Models
{
    public class Console
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Videogioco>? ListaGiochi { get; set; }

        public Console() { }
    }
}
