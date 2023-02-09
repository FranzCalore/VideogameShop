using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideogameShop.Models
{
    public class MessaggioPrivato
    {
        public int Id { get; set; }

        public string Titolo { get; set; }
        public IdentityUser Mittente { get; set; }

        public IdentityUser Destinatario { get; set; }

        public DateTime? DataOra { get; set; }

        [Column(TypeName = "text")]
        public string TestoMessaggio { get; set; }

        public bool MessaggioLetto { get; set; }
    }
}
