using Filmiregister.Domain;
using Filmiregister.Dto;
using Filmiregister.Models;
using System.Diagnostics.Metrics;

namespace Filmiregister.ServiceInterface
{
    public interface IFilmsServices
    {
        Task<Film> DetailsAsync(Guid id);
        Task<Film> Delete(Guid id);
        Task<Film> Create(FilmDto dto);
    }
}
