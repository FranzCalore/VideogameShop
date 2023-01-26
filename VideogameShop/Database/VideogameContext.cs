using Microsoft.EntityFrameworkCore;
using VideogameShop.Models;

namespace VideogameShop.Database
{
    public class VideogameContext:DbContext
    {
        public DbSet<Videogioco> Videogiochi { get; set; }
        public DbSet<Tipologia> Tipologie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = VideoGameDB; Integrated Security = True; Pooling = False;TrustServerCertificate=True");
        }
    }
}
