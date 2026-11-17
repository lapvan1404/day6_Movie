using System.ComponentModel.DataAnnotations;

namespace day6_Movie.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int GunreId { get; set; }
    }
}
