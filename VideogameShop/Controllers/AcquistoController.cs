using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideogameShop.Database;
using VideogameShop.Models;


namespace VideogameShop.Controllers
{
    public class AcquistoController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Compra(int id, VideogiocoTipologiaView dataForm)
        {
            using (VideogameContext db = new VideogameContext())
            {
                if (!ModelState.IsValid)
                {
                    List<Tipologia> tipologie = db.Tipologie.ToList<Tipologia>();
                    dataForm.Tipologie = tipologie;
                    return View("Dettagli", dataForm);
                }

                Acquisto acquisto = new Acquisto();
                acquisto.DataAcquisto = DateTime.Now;
                acquisto.VideogiocoId = id;
                acquisto.Quantita = dataForm.Acquisto.Quantita;
                db.Acquisti.Add(acquisto);
                db.SaveChanges();
                
            }

            return View("Successo");
        }
    }
}
