using Filmiregister.Domain;
using Filmiregister.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmiregister.Data
{
    public class FilmContext : DbContext
    {
        public FilmContext(DbContextOptions<FilmContext> options) : base(options) { }

            public DbSet<Film> Films { get; set; }
            public DbSet<FileToDatabase> FileToDatabase { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>().ToTable("Films");
        }
    }
}

