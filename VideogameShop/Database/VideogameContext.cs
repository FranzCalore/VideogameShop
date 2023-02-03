using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideogameShop.Models;

namespace VideogameShop.Database
{
    public class VideogameContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<Videogioco> Videogiochi { get; set; }
        public DbSet<Tipologia> Tipologie { get; set; }

        public DbSet<Acquisto> Acquisti { get; set; }

        public DbSet<Rifornimento> Rifornimenti { get; set; }

        public DbSet<Models.Console> Consoles { get; set; }

        public DbSet<Fornitore> Fornitori { get; set; }

        public DbSet<Carrello> Carrelli { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = VideoGameDB; Integrated Security = True; Pooling = False;TrustServerCertificate=True");
        }
    }
}
