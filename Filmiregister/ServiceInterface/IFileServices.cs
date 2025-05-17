using Filmiregister.Domain;
using Filmiregister.Dto;
using Filmiregister.Models;
using System.Diagnostics.Metrics;

namespace Filmiregister.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(FilmDto dto, Film domain);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
    }
}
