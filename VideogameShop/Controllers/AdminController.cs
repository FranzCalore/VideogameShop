using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                List<Videogioco> ListaVideogiochi = db.Videogiochi.Include(v => v.Tipologia).ToList<Videogioco>();
                ListaVideogiochi = ListaVideogiochi.OrderBy(V => V.QuantitaDisponibile).ToList();
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
                    /*VideogiocoTipologiaView VideogiocoView = new();
                    VideogiocoView.Videogioco = VideogiocoTrovato;
                    return View(VideogiocoView);*/

                    RifornimentoFornitoreView VideogiocoView = new();
                    VideogiocoView.RifornimentoVideogioco = new();
                    VideogiocoView.RifornimentoVideogioco.Videogioco = VideogiocoTrovato;
                    VideogiocoView.ListaFornitori = db.Fornitori.ToList();
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
                if (dataForm.ListaIdConsole is not null)
                {
                    dataForm.Videogioco.ListaConsole = new();
                    foreach (string StringaIdConsole in dataForm.ListaIdConsole)
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
                Videogioco? videogioco = db.Videogiochi.Where(v => v.Id == id).Include(vi => vi.ListaConsole).FirstOrDefault();
                if (videogioco != null)
                {
                    List<Tipologia> tipologie = db.Tipologie.ToList();
                    VideogiocoTipologiaView modelloView = new()
                    {
                        Videogioco = videogioco,
                        Tipologie = tipologie
                    };
                    List<Models.Console> ConsoleDb = db.Consoles.ToList();
                    List<SelectListItem> ListItems = new();
                    foreach (Models.Console Console in ConsoleDb)
                    {
                        bool Selected = videogioco.ListaConsole.Any(con => con.Id == Console.Id);
                        SelectListItem Option = new()
                        {
                            Text = Console.Name,
                            Value = Console.Id.ToString(),
                            Selected = Selected

                        };
                        ListItems.Add(Option);
                    }
                    modelloView.ListaConsole = ListItems;
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
                Videogioco? videogioco = db.Videogiochi.Where(p => p.Id == id).Include(c => c.ListaConsole).FirstOrDefault();
                if (videogioco != null)
                {
                    videogioco.Nome = formData.Videogioco.Nome;
                    videogioco.Descrizione = formData.Videogioco.Descrizione;
                    videogioco.Foto = formData.Videogioco.Foto;
                    videogioco.Prezzo = formData.Videogioco.Prezzo;
                    videogioco.TipologiaId = formData.Videogioco.TipologiaId;
                    videogioco.FotoOrizzontale = formData.Videogioco.FotoOrizzontale;
                    videogioco.ListaConsole.Clear();
                    if (formData.ListaIdConsole is not null)
                    {
                        foreach (string IdStringaConsole in formData.ListaIdConsole)
                        {
                            int IdConsole = int.Parse(IdStringaConsole);
                            var Console = db.Consoles.Where(c => c.Id == IdConsole).FirstOrDefault();
                            videogioco.ListaConsole.Add(Console);

                        }
                    }

                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index");
        }

        // POST: AdminController/Delete/5
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
                db.Videogiochi.Remove(videogioco);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Rifornisci(VideogiocoTipologiaView dataForm)
        public IActionResult Rifornisci(RifornimentoFornitoreView dataForm)
        {
            using (VideogameContext db = new VideogameContext())
            {
                //Videogioco videogioco = db.Videogiochi.Where(v => v.Id == dataForm.Videogioco.Id).Include(v=>v.Tipologia).FirstOrDefault();
                Videogioco videogioco = db.Videogiochi.Where(v => v.Id == dataForm.RifornimentoVideogioco.Videogioco.Id).Include(v => v.Tipologia).Include(v=>v.ListaConsole).FirstOrDefault();
                //dataForm.Videogioco = videogioco;
                dataForm.RifornimentoVideogioco.Videogioco = videogioco;
                if (!ModelState.IsValid)
                {
                    List<Fornitore> listafornitori = db.Fornitori.ToList();
                    dataForm.ListaFornitori = listafornitori;
                    return View("Dettagli", dataForm);
                }
                Fornitore fornitoreDb = new();
                if (dataForm.RifornimentoVideogioco.Fornitore is not null)
                {

                    fornitoreDb = db.Fornitori.Where(f => f.FornitoreNome == dataForm.RifornimentoVideogioco.Fornitore.FornitoreNome).FirstOrDefault();
                    if (fornitoreDb is null)
                    {
                        Fornitore fornitoreForm = new();
                        fornitoreForm.FornitoreNome = dataForm.RifornimentoVideogioco.Fornitore.FornitoreNome;
                        db.Fornitori.Add(fornitoreForm);
                        db.SaveChanges();
                    }
                    fornitoreDb = db.Fornitori.Where(f => f.FornitoreNome == dataForm.RifornimentoVideogioco.Fornitore.FornitoreNome).FirstOrDefault();
                    //Rifornimento rifornimento = dataForm.Rifornimento;
                }
                else
                {
                    fornitoreDb = db.Fornitori.Where(f => f.FornitoreId == dataForm.RifornimentoVideogioco.FornitoreId).FirstOrDefault();
                }
                Rifornimento rifornimento = dataForm.RifornimentoVideogioco;
                videogioco.QuantitaDisponibile = videogioco.QuantitaDisponibile + rifornimento.Quantita;
                rifornimento.Data = DateTime.Now;
                rifornimento.VideogiocoId = videogioco.Id;
                rifornimento.Fornitore = fornitoreDb;
                db.Rifornimenti.Add(rifornimento);
                db.SaveChanges();
                return RedirectToAction("Index");


            }


        }
    }
}
