using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GettingStarted.Models;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

namespace GettingStarted.Data
{
    public class GettingStartedMovieContext : DbContext, IDataProtectionKeyContext
    {
        public GettingStartedMovieContext(DbContextOptions<GettingStartedMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = default!;
    }
}
