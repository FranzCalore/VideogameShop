using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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

        // GET: HomeController/Details/5
        public ActionResult Dettagli(int id)
        {
            using VideogameContext db = new();
            VideogiocoTipologiaView view = new();
            view.Videogioco = db.Videogiochi.Where(v => v.Id == id).FirstOrDefault();
            return View(view);
        }

        [HttpGet]
        [Authorize]
        [Route("/Carrello")]
        public IActionResult VistaCarrello()
        {
            return View("Carrello");
        }

        [HttpPost]
        [Route("/Carrello")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AggiungiCarrello(VideogiocoTipologiaView dataForm)
        {           
            using (VideogameContext db = new VideogameContext())
            {
                if(Carrello.CarrelloCorrente.ProdottiAcquistati.Count() == 0)
                {
                    Carrello.CarrelloCorrente = new Carrello();                   
                }
                Carrello.CarrelloCorrente.NomeUtente = User.Identity.Name;
                Videogioco videogioco = db.Videogiochi.Where(v => v.Id == dataForm.Acquisto.VideogiocoId).FirstOrDefault();
                dataForm.Videogioco = videogioco;
                if (!ModelState.IsValid)
                {
                    return View("Dettagli", dataForm);
                }
                
                if (dataForm.Acquisto.Quantita <= dataForm.Videogioco.QuantitaDisponibile)
                {

                    Acquisto acquisto = dataForm.Acquisto;
                    acquisto.Videogioco = dataForm.Videogioco;
                    videogioco.QuantitaDisponibile = videogioco.QuantitaDisponibile - acquisto.Quantita;
                    acquisto.DataAcquisto = DateTime.Now;
                    foreach(Acquisto prodottoCarrello in Carrello.CarrelloCorrente.ProdottiAcquistati)
                    {
                        if(prodottoCarrello.Videogioco.Nome == acquisto.Videogioco.Nome)
                        {
                            prodottoCarrello.Quantita += acquisto.Quantita;
                            return View("Carrello");
                        }
                    }
                    Carrello.CarrelloCorrente.ProdottiAcquistati.Add(acquisto);
                   /* db.Acquisti.Add(acquisto);
                    db.SaveChanges();*/
                    return View("Carrello");
                }
                return View("Dettagli", dataForm);
                


            }

            
        }
        public IActionResult RimuoviElemento(int id)
        {
            Carrello.CarrelloCorrente.ProdottiAcquistati.Remove(Carrello.CarrelloCorrente.ProdottiAcquistati.Find(a=>a.VideogiocoId== id));
            return View("Carrello");
        }

        public IActionResult ConfermaAcquisto()
        {
            VideogameContext db = new();
            Carrello carrello = Carrello.CarrelloCorrente;
            carrello.DataOra = DateTime.Now;
            Carrello.CarrelloCorrente = new();
            double prezzoTotale = 0;
            foreach(Acquisto acquisto in carrello.ProdottiAcquistati)
            {
                prezzoTotale += (acquisto.Quantita * acquisto.Videogioco.Prezzo);
                acquisto.Videogioco = null;
            }

            carrello.PrezzoTotale = prezzoTotale;
            db.Carrelli.Add(carrello);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
