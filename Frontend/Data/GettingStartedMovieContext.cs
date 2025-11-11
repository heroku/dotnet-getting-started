using GettingStarted.Models;
using Microsoft.EntityFrameworkCore;

namespace GettingStarted.Data
{
    public class GettingStartedMovieContext : DbContext
    {
        public GettingStartedMovieContext(DbContextOptions<GettingStartedMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}
