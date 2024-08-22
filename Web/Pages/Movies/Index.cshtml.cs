using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Data;

namespace Web.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly GettingStartedMovieContext _context;

        public IndexModel(GettingStartedMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
