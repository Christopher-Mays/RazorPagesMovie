using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using TMDbLib.Client;

namespace RazorPagesMovie.Pages
{
    public class AddMovieModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public AddMovieModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public List<Movie> MyMovies { get; set; } = new();

        public async Task OnGetAsync()
        {
            MyMovies = await _context.Movie.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var client = new TMDbClient("b0847ec3dcc4d1a3f7b158f328aaba78");
            var tmdbMovie = await client.GetMovieAsync(id);

            if (tmdbMovie == null)
            {
                return NotFound();
            }

            // Prevent duplicate movies
            bool alreadyExists = await _context.Movie.AnyAsync(m => m.Title == tmdbMovie.Title && m.ReleaseDate == tmdbMovie.ReleaseDate);
            if (alreadyExists)
            {
                return RedirectToPage("/AddMovie"); // Skip adding
            }

            var newMovie = new Movie
            {
                Title = tmdbMovie.Title ?? "Untitled",
                ReleaseDate = tmdbMovie.ReleaseDate ?? DateTime.Now,
                Genre = tmdbMovie.Genres?.FirstOrDefault()?.Name ?? "Unknown",
                Rating = tmdbMovie.Adult ? "R" : "PG",
                Price = 9.99M, // You can adjust this default
                PosterPath = tmdbMovie.PosterPath
            };

            _context.Movie.Add(newMovie);
            await _context.SaveChangesAsync();

            return RedirectToPage("/AddMovie");
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(); // refreshes the list
        }

    }
}
