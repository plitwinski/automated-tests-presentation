using Example.InMemoryDependencies.Core;
using Example.Modularity.Tests.Builders;
using Moq;
using System.Net.Http;

namespace Example.Modularity.Tests.Controllers.Movies.Fixtures
{
    public class HttpClientFixture
    {
        private readonly ServerBuilder serverFactory;

        public Mock<IQueueClient> QueueClientMock { get; private set; }

        public HttpClientFixture()
        {
            serverFactory = new ServerBuilder();
        }

        public HttpClientFixture AddMovies()
        {
            var context = new MoviesContextBuilder("databaseNameFixture")
                .AddEntities(Data.Movie())
                .Build();
            serverFactory.AddMock(context);
            return this;
        }

        public HttpClientFixture AddQueueClient()
        {
            QueueClientMock = new Mock<IQueueClient>();
            serverFactory.AddMock(QueueClientMock.Object);
            return this;
        }

        public HttpClient Create()
        {
            return serverFactory.Build().CreateClient();
        }
    }
}
