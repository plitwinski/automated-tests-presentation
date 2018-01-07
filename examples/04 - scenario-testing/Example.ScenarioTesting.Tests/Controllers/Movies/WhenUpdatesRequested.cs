using Example.InMemoryDependencies.Core;
using Example.InMemoryDependencies.DataAccess;
using Example.InMemoryDependencies.Messages;
using Example.InMemoryDependencies.Models;
using Example.ScenarioTesting.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Example.ScenarioTesting.Tests.Controllers.Movies
{
    public class WhenUpdatesRequested : ScenarioTestingBase
    {
        private const string Cinema1 = "Cinema1";
        private const string Cinema2 = "Cinema2";
        private const string Movie1 = "Movie1";
        private const string Director1 = "Director1";
        private const int MovieId = 123;
        private CinemaUpdate[] _cinemaUpdates;
        private HttpClient _client;
        private Mock<IQueueClient> _queueClientMock;

        public override async Task Given()
        {
            _queueClientMock = new Mock<IQueueClient>();
            var context = await SetupDatabaseAsync();

            var server = ServerFactory.CreateServer(new Dictionary<Type, object>()
            {
                { typeof(IQueueClient), _queueClientMock.Object },
                { typeof(MoviesContext), context }
            });
            
            _client = server.CreateClient();
        }


        public override async Task When()
        {
            var result = await _client.GetAsync("api/movieUpdates");
            _cinemaUpdates = JsonConvert.DeserializeObject<CinemaUpdate[]>(await result.Content.ReadAsStringAsync());
        }

        [Test]
        public void ThenCinema1IsReturned()
        {
            Assert.That(_cinemaUpdates[0].Name, Is.EqualTo(Cinema1));
        }

        [Test]
        public void ThenCinema1MoviesCollectionIsNotEmpty()
        {
            Assert.IsNotEmpty(_cinemaUpdates[0].AddedMovies);
        }

        [Test]
        public void ThenCinema2IsReturned()
        {
            Assert.That(_cinemaUpdates[1].Name, Is.EqualTo(Cinema2));
        }

        [Test]
        public void ThenCinema2MoviesCollectionIsNotEmpty()
        {
            Assert.IsNotEmpty(_cinemaUpdates[1].AddedMovies);
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
                Director = Director1,
                Title = Movie1
            });
            await context.SaveChangesAsync();
            return context;
        }
    }
}
