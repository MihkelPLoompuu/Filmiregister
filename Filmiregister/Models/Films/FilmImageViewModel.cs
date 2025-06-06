﻿namespace Filmiregister.Models.Films
{
    public class FilmImageViewModel
    {
        public Guid ImageID { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? FilmID { get; set; }
    }
}
