using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideogameShop.Database;
using VideogameShop.Models;

namespace VideogameShop.Controllers
{
    public class IdentityController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Gestisci()
        {
            VideogameContext db = new();
            var Users = db.Users.ToList();
            List<UtenteRuoloView> ListaUtentiRuoli = new();
            foreach(var User in Users)
            {
                UtenteRuoloView UtenteconRuolo = new();
                var Ruoli = db.Roles.ToList();
                UtenteconRuolo.Ruoli = Ruoli;
                var RuoloUtente = db.UserRoles.Where(ur => ur.UserId == User.Id).FirstOrDefault();
                if(RuoloUtente is not null)
                {
                    string IdRuoloUtente = RuoloUtente.RoleId;
                    var Ruoloperutente = db.Roles.Where(ro => ro.Id == IdRuoloUtente).FirstOrDefault();
                    UtenteconRuolo.RuoloUtente = Ruoloperutente;
                }
                else
                {
                    UtenteconRuolo.RuoloUtente = null;
                }
                
                UtenteconRuolo.Utente = User;               
                ListaUtentiRuoli.Add(UtenteconRuolo);
            }
            return View(ListaUtentiRuoli);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GestisciUtente(string id)
        {
            VideogameContext db = new();
            var User = db.Users.Where(u=>u.Id == id).FirstOrDefault();
            UtenteRuoloView UtenteRuoloView = new();
            UtenteRuoloView.Utente = User;
            var Ruoli = db.Roles.ToList();
            UtenteRuoloView.Ruoli = Ruoli;
            var RuoloUtente = db.UserRoles.Where(ur => ur.UserId == User.Id).FirstOrDefault();
            if (RuoloUtente is not null)
            {
                string IdRuoloUtente = RuoloUtente.RoleId;
                var Ruoloperutente = db.Roles.Where(ro => ro.Id == IdRuoloUtente).FirstOrDefault();
                UtenteRuoloView.RuoloUtente = Ruoloperutente;
            }
            else
            {
                UtenteRuoloView.RuoloUtente = null;
            }
            return View(UtenteRuoloView);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult GestisciUtente(UtenteRuoloView formData)
        {
            using VideogameContext db = new();
            if (!ModelState.IsValid || formData.RuoloUtente.Id=="")
            {
                var Ruoli = db.Roles.ToList();
                formData.Ruoli = Ruoli;
                return View(formData);
            }
            string UsernameNormalizzato = formData.Utente.UserName.ToUpper();
            var Utente= db.Users.Where(u => u.Id == formData.Utente.Id).FirstOrDefault();
            var NomeRuolo = db.Roles.Where(r => r.Id == formData.RuoloUtente.Id).FirstOrDefault().Name; 
            Utente.UserName = formData.Utente.UserName;
            Utente.NormalizedUserName = UsernameNormalizzato;
            var utenteruolo = db.UserRoles.Where(ur => ur.UserId == formData.Utente.Id).FirstOrDefault();
            if(utenteruolo is null)
            {
                IdentityUserRole<string> identityUserRole = new();
                identityUserRole.UserId = formData.Utente.Id;
                identityUserRole.RoleId = formData.RuoloUtente.Id;
                db.UserRoles.Add(identityUserRole);                
            }
            else
            {
                if (User.Identity.Name == Utente.UserName && NomeRuolo == "Cliente")
                {
                    return Unauthorized("Non puoi modificare il tuo ruolo");
                }
                var posizioneutente= db.UserRoles.Where(ur => ur.UserId == formData.Utente.Id).FirstOrDefault();
                db.UserRoles.Remove(posizioneutente);
                db.SaveChanges();
                IdentityUserRole<string> identityUserRole = new();
                identityUserRole.UserId = formData.Utente.Id;
                identityUserRole.RoleId = formData.RuoloUtente.Id;
                db.UserRoles.Add(identityUserRole);
            }
            db.SaveChanges();
            return RedirectToAction("Gestisci");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Elimina(string username)
        {
            using VideogameContext db = new();
            var Utente = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            db.Remove(Utente);
            db.SaveChanges();

            return RedirectToAction("Gestisci");
        }
        public IActionResult MieiOrdini(string username)
        {
            VideogameContext db = new();
            List<Carrello> MieiOrdini = db.Carrelli.Where(c=>c.NomeUtente==username).Include(c=>c.ProdottiAcquistati).ToList();
            foreach(Carrello carrello in MieiOrdini)
            {
                foreach(Acquisto acquisto in carrello.ProdottiAcquistati)
                {
                    Videogioco videogioco = db.Videogiochi.Where(v=>v.Id==acquisto.VideogiocoId).FirstOrDefault();
                    acquisto.Videogioco=videogioco;
                }
            }
            return View(MieiOrdini);
        }

        public IActionResult DettagliOrdine(int id)
        {
            using VideogameContext db = new();
            Carrello ordine = db.Carrelli.Where(c=>c.Id==id).Include(c=>c.ProdottiAcquistati).FirstOrDefault();
            foreach (Acquisto acquisto in ordine.ProdottiAcquistati)
            {
                Videogioco videogioco = db.Videogiochi.Where(v => v.Id == acquisto.VideogiocoId).FirstOrDefault();
                acquisto.Videogioco = videogioco;
            }
            return View(ordine);
        }
    }
}
