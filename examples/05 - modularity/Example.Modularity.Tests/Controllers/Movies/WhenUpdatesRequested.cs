using Example.InMemoryDependencies;
using Example.InMemoryDependencies.Messages;
using Example.InMemoryDependencies.Models;
using Example.Modularity.Tests.Controllers.Movies.Fixtures;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Example.Modularity.Tests.Controllers.Movies
{
    public class WhenUpdatesRequested : ScenarioTestingBase
    {
        private CinemaUpdate[] _cinemaUpdates;
        private WebApplicationFixture<Startup> _clientFixture;
        private HttpClient _client;

        public override async Task Given()
        {
            _clientFixture = new WebApplicationFixture<Startup>()
                .AddMovies(Data.Movie())
                .AddQueueClient();

            _client = _clientFixture.CreateClient();
        }


        public override async Task When()
        {
            var result = await _client.GetAsync("api/movieUpdates");
            _cinemaUpdates = JsonConvert.DeserializeObject<CinemaUpdate[]>(await result.Content.ReadAsStringAsync());
        }

        [Test]
        public void ThenCinemaUpdatesIsReturned()
        {
            _cinemaUpdates.Should()
                .BeEquivalentTo(Data.ExpectedResult);
        }

        [Test]
        public void ThenTwoEventsWerePublished()
        {
            _clientFixture.QueueClientMock.Verify(p => p.PublishMessageAsync(It.IsAny<MovieAddedMessage>()), Times.Exactly(2));
        }
    }
}
