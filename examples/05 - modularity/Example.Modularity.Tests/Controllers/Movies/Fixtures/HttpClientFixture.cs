using Example.InMemoryDependencies.Core;
using Example.Modularity.Tests.Builders;
using Moq;
using System.Net.Http;

namespace Example.Modularity.Tests.Controllers.Movies.Fixtures
{
    public class HttpClientFixture
    {
        private readonly ServerBuilder serverBuilder;

        public Mock<IQueueClient> QueueClientMock { get; private set; }

        public HttpClientFixture()
        {
            serverBuilder = new ServerBuilder();
        }

        public HttpClientFixture AddMovies()
        {
            var context = new MoviesContextBuilder("databaseNameFixture")
                .AddEntities(Data.Movie())
                .Build();
            serverBuilder.AddMock(context);
            return this;
        }

        public HttpClientFixture AddQueueClient()
        {
            QueueClientMock = new Mock<IQueueClient>();
            serverBuilder.AddMock(QueueClientMock.Object);
            return this;
        }

        public HttpClient Create()
        {
            return serverBuilder.Build().CreateClient();
        }
    }
}
