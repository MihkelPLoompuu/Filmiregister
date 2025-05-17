using Filmiregister.Models.Films;

namespace Filmiregister.Domain
{
    public class Film
    {
        public Guid FilmID { get; set; }
        public string FilmTitle { get; set; }
        public List<string> FilmCategory { get; set; }
        public string FilmDescription { get; set; }
        public double FilmRating { get; set; }
        public DateOnly PublicationDate { get; set; }
        public string FilmDuration { get; set; }
    }
}
