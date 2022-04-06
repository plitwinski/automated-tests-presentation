using Example.InMemoryDependencies.Core;
using Example.InMemoryDependencies.DataAccess;
using Example.InMemoryDependencies.Messages;
using Example.InMemoryDependencies.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Example.InMemoryDependencies.Tests.Controllers
{
    public class MoviesControllerTests
    {
        private const string MovieTitle = "MovieTitle";
        private const string DirectorName = "DirectorName";
        private const int MovieId = 123;

        private static readonly CinemaUpdate[] ExpectedResult = new[]
        {
            new CinemaUpdate("Cinema1", new[] { new Movie(MovieId, DirectorName, MovieTitle) }),
            new CinemaUpdate("Cinema2", new[] { new Movie(MovieId, DirectorName, MovieTitle) })
        };

        [Test]
        public async Task WhenUpdatesRequested_ThenMoviesEndpointReturnsDataAndMovieAddedMessageWasPublishedTwice()
        {
            var queueClientMock = new Mock<IQueueClient>();
            var context = await SetupDatabaseAsync();

            var client = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
                    {
                        services.AddTransient(_ => context);
                        services.AddTransient(_ => queueClientMock.Object);

                    })
                )
                .CreateClient();

            var result = await client.GetAsync("api/movieUpdates");
            var cinemaUpdates = JsonConvert.DeserializeObject<CinemaUpdate[]>(await result.Content.ReadAsStringAsync());

            cinemaUpdates.Should()
                .BeEquivalentTo(ExpectedResult);

            queueClientMock.Verify(p => p.PublishMessageAsync(It.IsAny<MovieAddedMessage>()), Times.Exactly(2));
        }

        private static async Task<MoviesContext> SetupDatabaseAsync()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesContext>();
            optionsBuilder.UseInMemoryDatabase("databaseName");

            var context = new MoviesContext(optionsBuilder.Options);
            context.Movies.Add(new MovieEntity
            {
                Id = MovieId,
                Director = DirectorName,
                Title = MovieTitle
            });
            await context.SaveChangesAsync();
            return context;
        }
    }
}
