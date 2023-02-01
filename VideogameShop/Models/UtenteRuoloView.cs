using Microsoft.AspNetCore.Identity;

namespace VideogameShop.Models
{
    public class UtenteRuoloView
    {
        public IdentityUser Utente { get; set; }
        public IdentityRole? RuoloUtente { get; set; }

        public List<IdentityRole>? Ruoli { get; set; }

    }
}
