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

        // GET: HomeController/Details/5
        public ActionResult Dettagli(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Compra(Acquisto dataForm)
        {
            using (VideogameContext db = new VideogameContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("Dettagli", dataForm);
                }

                dataForm.DataAcquisto = DateTime.Now;
                db.Acquisti.Add(dataForm);
                db.SaveChanges();

            }

            return View("Successo");
        }
    }
}
