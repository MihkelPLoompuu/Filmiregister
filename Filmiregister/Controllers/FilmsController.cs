using Filmiregister.Data;
using Filmiregister.Domain;
using Filmiregister.Dto;
using Filmiregister.Models;
using Filmiregister.Models.Films;
using Filmiregister.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Filmiregister.Controllers
{
    public class FilmsController : Controller
    {
        private readonly FilmContext _context;
        private readonly IFilmsServices _filmsServices;
        private readonly IFileServices _fileServices;

        public FilmsController(FilmContext context, IFilmsServices filmsServices, IFileServices fileServices)
        {
            _context = context;
            _filmsServices = filmsServices;
            _fileServices = fileServices;
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
        [HttpGet]
        public IActionResult Create()
        {
            FilmCreateViewModel vm = new();
            return View("Create", vm);
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FilmCreateViewModel vm)
        {
            var dto = new FilmDto()
            {
                FilmTitle = vm.FilmTitle,
                FilmDescription = vm.FilmDescription,
                FilmDuration = vm.FilmDuration,
                FilmCategory = vm.FilmCategory,
                FilmRating = vm.FilmRating,
                PublicationDate = vm.PublicationDate,
                Files = vm.Files,
                FilmImage = vm.FilmImage
                .Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    FilmID = x.FilmID,
                }).ToArray()
            };
            var result = await _filmsServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction("index");
            }

            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var film = await _filmsServices.DetailsAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            var images = await _context.FileToDatabase
                .Where(t => t.FilmID == id)
                .Select(y => new FilmImageViewModel
                {
                    FilmID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new FilmDeleteViewModel();
            vm.FilmID = film.FilmID;
            vm.FilmTitle = film.FilmTitle;
            vm.FilmDuration = film.FilmDuration;
            vm.FilmDescription = film.FilmDescription;
            vm.FilmCategory = film.FilmCategory;
            vm.FilmRating = film.FilmRating;
            vm.PublicationDate = film.PublicationDate;
            vm.FilmImage.AddRange(images);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null) { return NotFound(); }
            
            var film = await _filmsServices.DetailsAsync(id);

            if (film == null) { return NotFound(); };

            var images = await _context.FileToDatabase
                .Where(x => x.FilmID == id)
                .Select(y => new FilmImageViewModel
                {
                    FilmID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();
            var vm = new FilmDeleteViewModel();

            vm.FilmID = film.FilmID;
            vm.FilmTitle = film.FilmTitle;
            vm.FilmDuration = film.FilmDuration;
            vm.FilmDescription = film.FilmDescription;
            vm.FilmCategory = film.FilmCategory;
            vm.FilmRating = film.FilmRating;
            vm.PublicationDate = film.PublicationDate;
            vm.FilmImage.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            if (id == Guid.Empty) { return BadRequest("Invalid ID"); }
            var filmToDelete = await _filmsServices.Delete(id);

            if (filmToDelete == null) { return RedirectToAction("Index"); }

            return RedirectToAction("Index");
        }
    }
}
