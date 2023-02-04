using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideogameShop.Database;
using VideogameShop.Models;

namespace VideogameShop.Controllers
{
    public class MessaggiController : Controller
    {
        [Route("/CasellaMessaggi")]
        public IActionResult Index()
        {
            VideogameContext db = new();
            List<MessaggioPrivato> ListaMessaggi = db.Messaggi.Where(m=>m.Destinatario.UserName==User.Identity.Name).Include(u=>u.Mittente).ToList();
            return View(ListaMessaggi);
        }

        [HttpGet]
        public IActionResult NuovoMessaggio()
        {
            MessaggioPrivato messaggio = new();
            return View(messaggio);
        }

        [HttpPost]
        public IActionResult NuovoMessaggio(MessaggioPrivato formdata)
        {
            using (VideogameContext db = new VideogameContext())
            {
                if (!ModelState.IsValid)
                {
                    return View(formdata);
                }
                formdata.Mittente = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                formdata.Destinatario = db.Users.Where(u => u.UserName == formdata.Destinatario.UserName).FirstOrDefault();
                formdata.DataOra = DateTime.Now;
                db.Messaggi.Add(formdata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }

}
