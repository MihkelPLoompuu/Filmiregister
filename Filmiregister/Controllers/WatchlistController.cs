using Filmiregister.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filmiregister.Controllers
{
    public class WatchlistController : Controller
    {
        private static List<Film> _watchlist = new List<Film>();
        public IActionResult Index()
        {
            return View(_watchlist);
        }
        [HttpGet]
        public IActionResult Add(Guid id, string title)
        {
            var movie = new Film { FilmID = id, FilmTitle = title };

            if (!_watchlist.Any(m => m.FilmID == id))
            {
                _watchlist.Add(movie);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Remove(Guid id)
        {
            var movie = _watchlist.FirstOrDefault(m => m.FilmID == id);
            if (movie != null)
            {
                _watchlist.Remove(movie);
            }

            return RedirectToAction("Index");
        }
    }
}
