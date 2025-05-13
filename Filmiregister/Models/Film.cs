namespace Filmiregister.Models
{
    public class Film
    {
        public int FilmID { get; set; }
        public string FilmTitle { get; set; }
        public List<string> FilmCategory { get; set; }
        public string FilmDescription { get; set; }
        public string FilmImage { get; set; }
        public Double FilmRating { get; set; }
        public DateOnly PublicationDate { get; set; }
        public TimeOnly FilmDuration { get; set; }
        //public List<Comments> CommentsOnMovies { get; set; }
    }
}
