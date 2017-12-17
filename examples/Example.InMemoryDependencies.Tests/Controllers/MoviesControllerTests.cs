using Example.InMemoryDependencies.DataAccess;
using Example.InMemoryDependencies.Models;
using Example.InMemoryDependencies.Tests.Factories;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task WhenUpdatesArrived_ThenMoviesEndpointReturnsData()
        {
            var server = ServerFactory.CreateServer();
            var context = SetupDatabase(server);
            await context.SaveChangesAsync();

            var client = server.CreateClient();

            var result = await client.GetAsync("api/movieUpdates");

            var content = await result.Content.ReadAsStringAsync();

            var cinemaUpdates = JsonConvert.DeserializeObject<CinemaUpdate[]>(content);

            Assert.That(cinemaUpdates[0].Name, Is.EqualTo(Cinema1));
            Assert.That(cinemaUpdates[1].Name, Is.EqualTo(Cinema2));
        }

        private static MoviesContext SetupDatabase(Microsoft.AspNetCore.TestHost.TestServer server)
        {
            var context = server.Host.Services.GetService(typeof(MoviesContext)) as MoviesContext;

            context.Movies.Add(new MovieEntity
            {
                Id = MovieId,
                Director = Director1,
                Title = Movie1
            });
            return context;
        }
    }
}
