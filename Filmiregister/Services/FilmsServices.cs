using Filmiregister.Data;
using Filmiregister.Domain;
using Filmiregister.Dto;
using Filmiregister.Models;
using Filmiregister.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Filmiregister.Services
{
    public class FilmsServices : IFilmsServices
    {
        private readonly FilmContext _context;
        private readonly IFileServices _fileServices;

        public FilmsServices(FilmContext context,IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }
        public async Task<Film> DetailsAsync(Guid id)
        {
            var result = await _context.Films
                .FirstOrDefaultAsync(x => x.FilmID == id);
            return result;
        }
        public async Task<Film> Create(FilmDto dto)
        {
            Film film = new Film();

            film.FilmID = Guid.NewGuid();

            //set by user
            film.FilmTitle = dto.FilmTitle;
            film.FilmDuration = dto.FilmDuration;
            film.FilmDescription = dto.FilmDescription;
            film.FilmCategory = dto.FilmCategory;
            film.FilmRating = dto.FilmRating;
            film.PublicationDate = dto.PublicationDate;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, film);
            }

            await _context.Films.AddAsync(film);
            await _context.SaveChangesAsync();

            return film;
        }
        public async Task<Film> Delete(Guid id)
        {
            var result = await _context.Films
                .FirstOrDefaultAsync(x => x.FilmID == id);
            _context.Films.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
