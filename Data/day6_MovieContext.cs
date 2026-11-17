using Microsoft.EntityFrameworkCore;

public class day6_MovieContext(DbContextOptions<day6_MovieContext> options) : DbContext(options)
{
    public DbSet<day6_Movie.Models.Gunre> Gunre { get; set; } = default!;
    public DbSet<day6_Movie.Models.Movie> Movie { get; set; } = default!;
}
