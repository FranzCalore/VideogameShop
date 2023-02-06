using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideogameShop.Database;
using VideogameShop.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideogameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        // GET: api/<HomeController>
        [HttpGet]
        public IActionResult Get(string? search)
        {
            using VideogameContext db = new();
            List<Videogioco> ListaVideogiochi = new();
            if(search is null || search == "")
            {
                ListaVideogiochi = db.Videogiochi.ToList();
            }
            else
            {
                search = search.ToLower();
                ListaVideogiochi = db.Videogiochi.Include(V=>V.Tipologia)
                                                 .Include(V=>V.ListaConsole)
                                                 .Where(V => V.Nome.ToLower().Contains(search) ||                                         
                                                        V.Tipologia.TipologiaNome.ToLower().Contains(search) ||
                                                        V.ListaConsole.Any(c=>c.Name.ToLower().Contains(search)))
                                                 .ToList();
            }
            return Ok(ListaVideogiochi);
        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using VideogameContext db = new();
            Videogioco videogioco = db.Videogiochi.Where(V => V.Id == id).Include(Videogioco => Videogioco.Tipologia).FirstOrDefault();
            if (videogioco is null)
            {
                return NotFound("Mario, sembra che il tuo videogioco sia in un altro castello!");
            }
            return Ok(videogioco);
        }

        [HttpPut("{id}")]
        public IActionResult Like(int id, [FromBody]Videogioco videogioco)
        {
            using VideogameContext db = new();
            Videogioco videogame = db.Videogiochi.Where(vi => vi.Id == videogioco.Id).FirstOrDefault();
            videogame.NumeroLike = videogioco.NumeroLike;
            db.SaveChanges();
            return Ok(videogioco.NumeroLike);
        }

    }
}
