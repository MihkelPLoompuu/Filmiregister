using Filmiregister.Data;
using Filmiregister.Domain;
using Filmiregister.Dto;
using Filmiregister.Models;
using Filmiregister.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Filmiregister.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly FilmContext _context;

        public FileServices
            (
                IHostEnvironment webHost,
                FilmContext context
            )
        {
            _webHost = webHost;
            _context = context;
        }
        public void UploadFilesToDatabase(FilmDto dto, Film domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var image in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            ID = Guid.NewGuid(),
                            ImageTitle = image.FileName,
                            FilmID = domain.FilmID,
                        };

                        image.CopyTo(target);
                        files.ImageData = target.ToArray();
                        _context.FileToDatabase.Add(files);

                    }
                }
            }
        }
        public async Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto)
        {
            var imageID = await _context.FileToDatabase
                .FirstOrDefaultAsync(x => x.ID == dto.ID);
            var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\" + imageID.ImageData;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.FileToDatabase.Remove(imageID);
            await _context.SaveChangesAsync();

            return null;

        }
    }
}
