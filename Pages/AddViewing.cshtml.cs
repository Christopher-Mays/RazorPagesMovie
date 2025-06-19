using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace RazorPagesMovie.Pages
{
    public class AddViewingModel : PageModel
    {
        private readonly TMDbClient _client;

        public AddViewingModel()
        {
            _client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public List<SearchMovie> SearchResults { get; set; } = new();

        [BindProperty]
        public string? SelectedMovieTitle { get; set; }

        [BindProperty]
        public DateTime ViewingDate { get; set; }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                var results = await _client.SearchMovieAsync(SearchQuery);
                SearchResults = results.Results.Take(5).ToList(); // Limit to 5 results
            }
        }

        public IActionResult OnPost()
        {
            TempData["Message"] = $"Viewing for \"{SelectedMovieTitle}\" scheduled on {ViewingDate}.";
            return RedirectToPage("/Index");
        }
    }
}
