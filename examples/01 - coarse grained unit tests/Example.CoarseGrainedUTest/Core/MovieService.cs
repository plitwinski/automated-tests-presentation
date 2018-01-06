using Example.CoarseGrainedUTest.Messages;
using Example.CoarseGrainedUTest.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.CoarseGrainedUTest.Core
{
    public class MovieService
    {
        private readonly string[] registeredCinemas = new[] { "Cinema1", "Cinema2" };
        private readonly ConcurrentDictionary<string, IList<Movie>> alreadySent = new ConcurrentDictionary<string, IList<Movie>>();
        private readonly IMovieRepository movieRepository;
        private readonly IQueueClient queueClient;

        public MovieService(IMovieRepository movieRepository, IQueueClient queueClient)
        {
            this.movieRepository = movieRepository;
            this.queueClient = queueClient;
        }

        
        public async Task CheckForChangesAsync()
        {
            foreach(var cinemaName in registeredCinemas)
            {
                var movies = await movieRepository.GetMoviesAsync(cinemaName);
                var cinemaMovies = alreadySent.GetOrAdd(cinemaName, new List<Movie>());

                var newMovies = movies.Where(p => cinemaMovies.All(x => x.Id != p.Id));

                foreach(var newMovie in newMovies)
                {
                    await queueClient.PublishMessageAsync(new MovieAddedMessage()
                    {
                        CinemaName = cinemaName,
                        MovieName = newMovie.Title
                    });
                    cinemaMovies.Add(newMovie);
                }
                alreadySent.AddOrUpdate(cinemaName, cinemaMovies, (_, __) => cinemaMovies);
            }
        }
    }
}
