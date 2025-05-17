namespace Filmiregister.Models.Films
{
    public class FilmIndexViewModel
    {
        public Guid FilmID { get; set; }
        public string FilmTitle { get; set; }
        public List<string> FilmCategory { get; set; }
        public string FilmDescription { get; set; }
        public List<FilmImageViewModel> FilmImage { get; set; } = new List<FilmImageViewModel>();
        public double FilmRating { get; set; }
        public DateOnly PublicationDate { get; set; }
        public string FilmDuration { get; set; }
    }
}
