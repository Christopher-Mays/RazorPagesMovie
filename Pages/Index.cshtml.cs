using Microsoft.AspNetCore.Mvc.RazorPages;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace RazorPagesMovie.Pages;

public class IndexModel : PageModel
{
    public string MovieTitle { get; set; } = "";
    public string PosterUrl { get; set; } = "";

    public async Task OnGetAsync()
    {
        var client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");
        var movie = await client.GetMovieAsync(550); // Fight Club

        MovieTitle = movie.Title ?? "Unknown Title";
        PosterUrl = string.IsNullOrEmpty(movie.PosterPath)
            ? ""
            : $"https://image.tmdb.org/t/p/w500{movie.PosterPath}";
    }
}
