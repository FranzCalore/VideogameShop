using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideogameShop.Database;
using VideogameShop.Models;

namespace VideogameShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AdminController/Edit/5
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
