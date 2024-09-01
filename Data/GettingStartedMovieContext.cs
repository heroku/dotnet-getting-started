using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GettingStarted.Models;

namespace GettingStarted.Data
{
    public class GettingStartedMovieContext : DbContext
    {
        public GettingStartedMovieContext (DbContextOptions<GettingStartedMovieContext> options)
            : base(options)
        {
        }

        public DbSet<GettingStarted.Models.Movie> Movie { get; set; } = default!;
    }
}
