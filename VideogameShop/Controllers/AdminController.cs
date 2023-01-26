using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using VideogameShop.Database;
using VideogameShop.Models;

namespace VideogameShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Index()
        {
            using (VideogameContext db = new VideogameContext())
            {
                List<Videogioco> ListaVideogiochi = db.Videogiochi.ToList<Videogioco>();
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
                    .FirstOrDefault();

                if (VideogiocoTrovato != null)
                {
                    return View(VideogiocoTrovato);
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

                VideogiocoCategoriaView ViewModello = new VideogiocoCategoriaView();
                ViewModello.Videogioco = new Videogioco();

                ViewModello.Tipologie = TipologieDb;

                return View("Crea", ViewModello);
            }
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crea(VideogiocoCategoriaView dataForm)
        {
            using (VideogameContext db = new VideogameContext())
            { 
                if (!ModelState.IsValid)
            {
                    List<Tipologia> tipologie = db.Tipologie.ToList<Tipologia>();

                    dataForm.Tipologie = tipologie;

                return View("Crea", dataForm);
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
                    VideogiocoCategoriaView modelloView = new()
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
        public ActionResult Modifica(int id, VideogiocoCategoriaView formData)
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
    }
}
