using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideogameShop.Database;
using VideogameShop.Models;

namespace VideogameShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Play()
        {
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Dettagli(int id)
        {
            using VideogameContext db = new();
            VideogiocoTipologiaView view = new();
            view.Videogioco = db.Videogiochi.Where(v => v.Id == id).FirstOrDefault();
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Compra(VideogiocoTipologiaView dataForm)
        {
            using (VideogameContext db = new VideogameContext())
            {
                Videogioco videogioco = db.Videogiochi.Where(v => v.Id == dataForm.Acquisto.VideogiocoId).FirstOrDefault();
                dataForm.Videogioco = videogioco;
                if (!ModelState.IsValid)
                {
                    return View("Dettagli", dataForm);
                }
                
                if (dataForm.Acquisto.Quantita <= dataForm.Videogioco.QuantitaDisponibile)
                {

                    Acquisto acquisto = dataForm.Acquisto;
                    videogioco.QuantitaDisponibile = videogioco.QuantitaDisponibile - acquisto.Quantita;
                    acquisto.DataAcquisto = DateTime.Now;
                    db.Acquisti.Add(acquisto);
                    db.SaveChanges();
                    return View("Successo");
                }
                return View("Dettagli", dataForm);
                

            }

            
        }
    }
}
