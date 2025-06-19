using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace RazorPagesMovie.Pages
{
    public class SearchModel : PageModel
    {
        public List<Genre> Genres { get; set; } = new();
        public SearchContainer<SearchMovie>? Results { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Query { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? GenreId { get; set; }

        public int? SelectedGenreId => GenreId;

        public async Task OnGetAsync()
        {
            var client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");

            // Load all genres
            Genres = await client.GetMovieGenresAsync();

            // Case 1: Text search (with optional genre filtering)
            if (!string.IsNullOrWhiteSpace(Query))
            {
            Results = await client.SearchMovieAsync(Query);

            if (GenreId.HasValue)
            {
                Results.Results = Results.Results
                .Where(m => m.GenreIds != null && m.GenreIds.Contains(GenreId.Value))
                .ToList();
            }
            }
            // Case 2: Genre-only search using Discover
            else if (GenreId.HasValue)
            {
            // Use Discover API to get movies by genre
            var discoverResults = await client.DiscoverMoviesAsync()
                .IncludeWithAllOfGenre(new[] { GenreId.Value })
                .Query();
            Results = discoverResults;
            }
        }
    }
}
