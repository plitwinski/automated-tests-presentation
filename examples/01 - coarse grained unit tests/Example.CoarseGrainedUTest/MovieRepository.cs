using Example.CoarseGrainedUTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Example.CoarseGrainedUTest
{
    public class MovieRepository : IMovieRepository
    {
        private const string MoviesLocation = @"C:\Cineams\";

        private readonly IFileReader fileReader;

        public MovieRepository(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(string cinema)
        {
            var cinemaPath = Path.Combine(MoviesLocation, cinema);
            var rawMovies = await fileReader.GetFileDataAsync(cinemaPath);

            var converToMovie = new Func<string, Movie>(data => {
                var movie = data.Split(';');
                return new Movie(int.Parse(movie[0]), movie[1], movie[2]);
            });

            return rawMovies.Select(converToMovie);
        }
    }
}
