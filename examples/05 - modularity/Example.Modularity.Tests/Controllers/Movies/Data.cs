using Example.InMemoryDependencies.DataAccess;

namespace Example.Modularity.Tests.Controllers.Movies
{
    internal static class Data
    {
        public static MovieEntity Movie() => new MovieEntity()
        {
            Id = 123,
            Director = "Director1",
            Title = "Movie1"
        };
    }
}
