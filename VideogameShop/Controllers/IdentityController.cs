using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideogameShop.Database;
using VideogameShop.Models;

namespace VideogameShop.Controllers
{
    [Authorize(Roles ="Admin")]
    public class IdentityController : Controller
    {
        [HttpGet]
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
                var poszioneutente= db.UserRoles.Where(ur => ur.UserId == formData.Utente.Id).FirstOrDefault();
                db.UserRoles.Remove(poszioneutente);
                db.SaveChanges();
                IdentityUserRole<string> identityUserRole = new();
                identityUserRole.UserId = formData.Utente.Id;
                identityUserRole.RoleId = formData.RuoloUtente.Id;
                db.UserRoles.Add(identityUserRole);
            }
            db.SaveChanges();
            return RedirectToAction("Gestisci");
        }
    }
}
