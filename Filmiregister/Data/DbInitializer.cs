using Filmiregister.Models;

namespace Filmiregister.Data
{
    public class DbInitializer
    {
        public static void Initialize(FilmContext context)
        {
            if(context.Films.Any())
            {
                return; 
            }
            var films = new Film[]
            {
                new Film
                {
                    FilmID = 1,
                    FilmTitle = "TestFilm",
                    FilmCategory = new List<string> { "Action", "Drama" },
                    FilmDescription = "TestFilmDescription",
                    FilmImage = "No image",
                    FilmRating = 4.5,
                    PublicationDate = new DateOnly(2023, 10, 1),
                    FilmDuration = new TimeOnly(2, 30)
                }
            };
        }
    }
}
