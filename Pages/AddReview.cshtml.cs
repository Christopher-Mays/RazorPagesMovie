using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace RazorPagesMovie.Pages
{
    public class AddReviewModel : PageModel
    {
        [BindProperty]
        public int MovieId { get; set; }

        [BindProperty]
        [Required, StringLength(1000)]
        public string ReviewText { get; set; } = "";

        [BindProperty]
        [Range(1, 10)]
        public int Rating { get; set; }

        public string MovieTitle { get; set; } = "";

        public async Task OnGetAsync(int id)
        {
            MovieId = id;
            var client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");
            var movie = await client.GetMovieAsync(id);
            MovieTitle = movie.Title;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");
                var movie = await client.GetMovieAsync(MovieId);
                MovieTitle = movie.Title;
                return Page();
            }

            // Save logic here – to database, in-memory list, or file
            Console.WriteLine($"Review for Movie {MovieId}: {ReviewText} ({Rating}/10)");

            return RedirectToPage("/MovieDetails", new { id = MovieId });
        }
    }
}
