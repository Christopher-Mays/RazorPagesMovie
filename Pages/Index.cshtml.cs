using Microsoft.AspNetCore.Mvc.RazorPages;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Microsoft.AspNetCore.Mvc;


namespace RazorPagesMovie.Pages;

public class IndexModel : PageModel
{
   public List<SearchMovie> Movies { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    public bool IsFirstPage => PageNumber <= 1;
    public bool IsLastPage { get; set; } = false;

    public async Task OnGetAsync()
    {
        var client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");

        // Get popular movies (or use DiscoverMovieAsync if preferred)
        var results = await client.GetMoviePopularListAsync(page: PageNumber);
        Movies = results.Results;

        // Check if this is the last page
        IsLastPage = PageNumber >= results.TotalPages;
    }
}
