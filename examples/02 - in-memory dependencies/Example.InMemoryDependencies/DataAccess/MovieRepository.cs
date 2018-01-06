using Example.InMemoryDependencies.Core;
using Example.InMemoryDependencies.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.InMemoryDependencies.DataAccess
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext context;

        public MovieRepository(MoviesContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(string cinema)
        {
            var rawMovies = await context.Movies.ToListAsync();
            return rawMovies.Select(p => new Movie(p.Id, p.Director, p.Title));
        }
    }
}
