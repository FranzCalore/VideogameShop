using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using VideogameShop.Database;
using VideogameShop.Models;
using VideogameShop.Utils;

namespace VideogameShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Index()
        {
            using (VideogameContext db = new VideogameContext())
            {
                List<Videogioco> ListaVideogiochi = db.Videogiochi.Include(v=>v.Tipologia).ToList<Videogioco>();
                ListaVideogiochi=ListaVideogiochi.OrderBy(V => V.QuantitaDisponibile).ToList();              
                return View("Index", ListaVideogiochi);
            }
        }

        // GET: AdminController/Dettagli/5
        public ActionResult Dettagli(int id)
        {
            using (VideogameContext db = new VideogameContext())
            {
                // LINQ: syntax methos
                Videogioco VideogiocoTrovato = db.Videogiochi
                    .Where(DbVideogioco => DbVideogioco.Id == id)
                    .Include(videogioco => videogioco.Tipologia)
                    .Include(videogioco => videogioco.ListaConsole)
                    .FirstOrDefault();

                if (VideogiocoTrovato != null)
                {
                    VideogiocoTipologiaView VideogiocoView = new();
                    VideogiocoView.Videogioco = VideogiocoTrovato;
                    return View(VideogiocoView);
                }

                return NotFound("Mario, sembra che il tuo videogioco sia in un altro castello!");
            }
        }

        // GET: AdminController/Crea
        public ActionResult Crea()
        {
            using (VideogameContext db = new VideogameContext())
            {
                List<Tipologia> TipologieDb = db.Tipologie.ToList<Tipologia>();
                List<Models.Console> ConsoleDb = db.Consoles.ToList();

                VideogiocoTipologiaView ViewModello = new VideogiocoTipologiaView();
                ViewModello.Videogioco = new Videogioco();

                ViewModello.ListaConsole = SelectItemManager.ConverterListConsole();

                ViewModello.Tipologie = TipologieDb;

                return View("Crea", ViewModello);
            }
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crea(VideogiocoTipologiaView dataForm)
        {
            using (VideogameContext db = new VideogameContext())
            { 
                if (!ModelState.IsValid)
            {
                    List<Tipologia> tipologie = db.Tipologie.ToList<Tipologia>();

                    dataForm.Tipologie = tipologie;
                    dataForm.ListaConsole = SelectItemManager.ConverterListConsole();

                return View("Crea", dataForm);
            }
                if(dataForm.ListaIdConsole is not null)
                {
                    dataForm.Videogioco.ListaConsole = new();
                    foreach(string StringaIdConsole in dataForm.ListaIdConsole)
                    {
                        int IdConsole = int.Parse(StringaIdConsole);
                        Models.Console? Console = db.Consoles.Where(C => C.Id == IdConsole).FirstOrDefault();
                        dataForm.Videogioco.ListaConsole.Add(Console);
                    }
                }
                db.Videogiochi.Add(dataForm.Videogioco);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Modifica(int id)
        {
            using (VideogameContext db = new VideogameContext())
            {
                Videogioco? videogioco = db.Videogiochi.Where(v => v.Id == id).FirstOrDefault();
                if (videogioco != null)
                {
                    List<Tipologia> tipologie = db.Tipologie.ToList();
                    VideogiocoTipologiaView modelloView = new()
                    {
                        Videogioco = videogioco,
                        Tipologie = tipologie
                    };
                    return View("Modifica", modelloView);
                }
                else
                {
                    return NotFound("Mario, sembra che il tuo videogioco sia in un altro castello!");
                }

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modifica(int id, VideogiocoTipologiaView formData)
        {
            if (!ModelState.IsValid)
            {
                using VideogameContext db = new();
                List<Tipologia> tipologie = db.Tipologie.ToList();
                formData.Tipologie = tipologie;
                return View("Modifica", formData);
            }

            using (VideogameContext db = new VideogameContext())
            {
                Videogioco? videogioco = db.Videogiochi.Where(p => p.Id == id).FirstOrDefault();
                if (videogioco != null)
                {
                    videogioco.Nome = formData.Videogioco.Nome;
                    videogioco.Descrizione = formData.Videogioco.Descrizione;
                    videogioco.Foto = formData.Videogioco.Foto;
                    videogioco.Prezzo = formData.Videogioco.Prezzo;
                    videogioco.TipologiaId = formData.Videogioco.TipologiaId;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index");
    }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Elimina(int id)
        {
            VideogameContext db = new VideogameContext();
            Videogioco videogioco = (from v in db.Videogiochi where v.Id == id select v).FirstOrDefault();
            if (videogioco is null)
            {
                return NotFound("Mario, sembra che il tuo videogioco sia in un altro castello!");
            }
            else
            {
                db.Remove(videogioco);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rifornisci(VideogiocoTipologiaView dataForm)
        {
            using (VideogameContext db = new VideogameContext())
            {
                Videogioco videogioco = db.Videogiochi.Where(v => v.Id == dataForm.Videogioco.Id).Include(v=>v.Tipologia).FirstOrDefault();
                dataForm.Videogioco = videogioco;
                if (!ModelState.IsValid)
                {
                    return View("Dettagli", dataForm);
                }

                    Rifornimento rifornimento = dataForm.Rifornimento;
                    videogioco.QuantitaDisponibile = videogioco.QuantitaDisponibile + rifornimento.Quantita;
                    rifornimento.Data = DateTime.Now;
                    rifornimento.VideogiocoId=videogioco.Id;
                    db.Rifornimenti.Add(rifornimento);
                    db.SaveChanges();
                    return RedirectToAction("Index");


            }


        }
    }
}
