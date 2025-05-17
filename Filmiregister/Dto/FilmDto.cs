namespace Filmiregister.Dto
{
    public class FilmDto
    {
        public Guid FilmID { get; set; }
        public string FilmTitle { get; set; }
        public List<string> FilmCategory { get; set; }
        public string FilmDescription { get; set; }
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToDatabaseDto> FilmImage { get; set; } = new List<FileToDatabaseDto>();
        public double FilmRating { get; set; }
        public DateOnly PublicationDate { get; set; }
        public string FilmDuration { get; set; }
        //public List<Comments> CommentsOnMovies { get; set; }
    }
}
