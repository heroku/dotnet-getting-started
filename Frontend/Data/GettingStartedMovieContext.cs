using Microsoft.EntityFrameworkCore;
using GettingStarted.Models;

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
