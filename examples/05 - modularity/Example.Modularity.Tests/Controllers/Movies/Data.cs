using Example.InMemoryDependencies.DataAccess;
using Example.InMemoryDependencies.Models;

namespace Example.Modularity.Tests.Controllers.Movies
{
    internal static class Data
    {
        private const string MovieTitle = "MovieTitle";
        private const string DirectorName = "DirectorName";
        private const int MovieId = 123;

        public static readonly CinemaUpdate[] ExpectedResult = new[]
        {
            new CinemaUpdate("Cinema1", new[] { new Movie(MovieId, DirectorName, MovieTitle) }),
            new CinemaUpdate("Cinema2", new[] { new Movie(MovieId, DirectorName, MovieTitle) })
        };

        public static MovieEntity Movie() => new MovieEntity()
        {
            Id = MovieId,
            Director = DirectorName,
            Title = MovieTitle
        };
    }
}
