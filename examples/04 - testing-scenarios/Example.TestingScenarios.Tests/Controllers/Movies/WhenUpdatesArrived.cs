using Example.InMemoryDependencies.DataAccess;
using Example.InMemoryDependencies.Models;
using Example.TestingScenarios.Tests.Factories;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Example.TestingScenarios.Tests.Controllers.Movies
{
    public class WhenUpdatesArrived : TestingScenarioBase
    {
        private const string Cinema1 = "Cinema1";
        private const string Cinema2 = "Cinema2";
        private const string Movie1 = "Movie1";
        private const string Director1 = "Director1";
        private const int MovieId = 123;
        private CinemaUpdate[] _cinemaUpdates;
        private HttpClient _client;

        public override async Task Given()
        {
            var server = ServerFactory.CreateServer();
            var context = SetupDatabase(server);
            await context.SaveChangesAsync();

            _client = server.CreateClient();
        }


        public override async Task When()
        {
            var result = await _client.GetAsync("api/movieUpdates");

            var content = await result.Content.ReadAsStringAsync();

            _cinemaUpdates = JsonConvert.DeserializeObject<CinemaUpdate[]>(content);
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
