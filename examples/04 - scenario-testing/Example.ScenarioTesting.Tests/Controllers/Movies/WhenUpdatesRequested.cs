using Example.InMemoryDependencies;
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
using System.Net.Http;
using System.Threading.Tasks;

namespace Example.ScenarioTesting.Tests.Controllers.Movies
{
    public class WhenUpdatesRequested : ScenarioTestingBase
    {
        private const string MovieTitle = "MovieTitle";
        private const string DirectorName = "DirectorName";
        private const int MovieId = 123;

        private static readonly CinemaUpdate[] ExpectedResult = new[]
        {
            new CinemaUpdate("Cinema1", new[] { new Movie(MovieId, DirectorName, MovieTitle) }),
            new CinemaUpdate("Cinema2", new[] { new Movie(MovieId, DirectorName, MovieTitle) })
        };

        private CinemaUpdate[] _cinemaUpdates;
        private HttpClient _client;
        private Mock<IQueueClient> _queueClientMock;

        public override async Task Given()
        {
            var context = await SetupDatabaseAsync();
            _queueClientMock = new Mock<IQueueClient>();
            _client = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
                {
                    services.AddTransient(_ => context);
                    services.AddTransient(_ => _queueClientMock.Object);

                })
                )
                .CreateClient();
        }


        public override async Task When()
        {
            var result = await _client.GetAsync("api/movieUpdates");
            _cinemaUpdates = JsonConvert.DeserializeObject<CinemaUpdate[]>(await result.Content.ReadAsStringAsync());
        }

        [Test]
        public void ThenCinemaUpdatesReturned()
        {
            _cinemaUpdates.Should()
                .BeEquivalentTo(ExpectedResult);
        }

        [Test]
        public void ThenTwoEventsWerePublished()
        {
            _queueClientMock.Verify(p => p.PublishMessageAsync(It.IsAny<MovieAddedMessage>()), Times.Exactly(2));
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
