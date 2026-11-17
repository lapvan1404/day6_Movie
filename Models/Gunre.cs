using System.ComponentModel.DataAnnotations;

namespace day6_Movie.Models
{
    public class Gunre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
