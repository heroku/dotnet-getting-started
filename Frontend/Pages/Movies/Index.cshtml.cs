using GettingStarted.Data;
using GettingStarted.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GettingStarted.Frontend.Pages.Movies
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
