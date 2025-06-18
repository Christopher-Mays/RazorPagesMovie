using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace RazorPagesMovie.Pages;

public class SearchModel : PageModel
{
    public List<SearchMovie> SearchResults { get; set; } = new();
    
    [BindProperty(SupportsGet = true)]
    public string Query { get; set; } = "";

    public async Task OnGetAsync()
    {
        if (!string.IsNullOrEmpty(Query))
        {
            var client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");
            var results = await client.SearchMovieAsync(Query);
            SearchResults = results.Results;
        }
    }
}
