using System.Diagnostics;
using Filmiregister.Data;
using Filmiregister.Domain;
using Filmiregister.Models;
using Filmiregister.Models.Films;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filmiregister.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FilmContext _context;

        public HomeController(ILogger<HomeController> logger, FilmContext filmContext)
        {
            _logger = logger;
            _context = filmContext;
        }

        public IActionResult Index()
        {
            var resultingInventory = _context.Films
                .OrderByDescending(y => y.FilmRating)
                .Select(x => new FilmIndexViewModel
                {
                    FilmID = x.FilmID,
                    FilmTitle = x.FilmTitle,
                    FilmDescription = x.FilmDescription,
                    FilmDuration = x.FilmDuration,
                    FilmCategory = x.FilmCategory,
                    FilmRating = x.FilmRating,
                    PublicationDate = x.PublicationDate,
                    FilmImage = (List<FilmImageViewModel>)_context.FileToDatabase
                       .Where(t => t.FilmID == x.FilmID)
                       .Select(z => new FilmImageViewModel
                       {
                           FilmID = z.ID,
                           ImageID = z.ID,
                           ImageData = z.ImageData,
                           ImageTitle = z.ImageTitle,
                           Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(z.ImageData))
                       })
                });
            return View(resultingInventory);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
