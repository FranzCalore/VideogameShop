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

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
