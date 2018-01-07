using Example.InMemoryDependencies.Core;
using Example.InMemoryDependencies.DataAccess;
using Example.InMemoryDependencies.Messages;
using Example.InMemoryDependencies.Models;
using Example.InMemoryDependencies.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.InMemoryDependencies.Tests.Controllers
{
    public class MoviesControllerTests
    {
        private const string Cinema1 = "Cinema1";
        private const string Cinema2 = "Cinema2";
        private const string Movie1 = "Movie1";
        private const string Director1 = "Director1";
        private const int MovieId = 123;

        [Test]
        public async Task WhenUpdatesRequested_ThenMoviesEndpointReturnsDataAndMovieAddedMessageWasPublishedTwice()
        {
            var queueClientMock = new Mock<IQueueClient>();
            var context = await SetupDatabaseAsync();
            var server = ServerFactory.CreateServer(new Dictionary<Type, object>()
            {
                { typeof(MoviesContext), context },
                { typeof(IQueueClient), queueClientMock.Object }
            });
            var client = server.CreateClient();

            var result = await client.GetAsync("api/movieUpdates");
            var cinemaUpdates = JsonConvert.DeserializeObject<CinemaUpdate[]>(await result.Content.ReadAsStringAsync());

            Assert.That(cinemaUpdates[0].Name, Is.EqualTo(Cinema1));
            Assert.That(cinemaUpdates[1].Name, Is.EqualTo(Cinema2));
            Assert.IsNotEmpty(cinemaUpdates[0].AddedMovies);
            Assert.IsNotEmpty(cinemaUpdates[1].AddedMovies);
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
                Director = Director1,
                Title = Movie1
            });
            await context.SaveChangesAsync();
            return context;
        }
    }
}
