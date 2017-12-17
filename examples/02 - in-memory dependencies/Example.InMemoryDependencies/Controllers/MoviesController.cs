using Example.InMemoryDependencies.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Example.InMemoryDependencies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        [Route("api/movieUpdates")]
        public async Task<IActionResult> GetMoviesUpdates()
        {
            var updates = await movieService.CheckForChangesAsync();
            return Ok(updates);
        }
    }
}
