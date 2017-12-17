using System.Collections.Generic;

namespace Example.InMemoryDependencies.Models
{
    public class CinemaUpdate
    {
        public IEnumerable<Movie> AddedMovies { get; }
        public string Name { get; }

        public CinemaUpdate(string name, IEnumerable<Movie> addedMovies)
        {
            Name = name;
            AddedMovies = addedMovies;
        }
    }
}
