using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace RazorPagesMovie.Pages
{
    public class MovieDetailsModel : PageModel
{
    private readonly TMDbClient _client;

    public MovieDetailsModel()
    {
        _client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");
    }

    

    public TMDbLib.Objects.Movies.Movie? Movie { get; set; }

    public int MovieId { get; set; } // 

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");
        MovieId = id; 
        Movie = await _client.GetMovieAsync(id);

        if (Movie == null)
        {
            return NotFound();
        }

        return Page();
    }
}
}
