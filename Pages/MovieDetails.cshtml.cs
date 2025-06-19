using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace RazorPagesMovie.Pages
{
    public class MovieDetailsModel : PageModel
    {
        public Movie Movie { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");
            Movie = await client.GetMovieAsync(id, TMDbLib.Objects.Movies.MovieMethods.Credits | TMDbLib.Objects.Movies.MovieMethods.Videos);

            if (Movie == null)
                return NotFound();

            return Page();
        }
    }
}
