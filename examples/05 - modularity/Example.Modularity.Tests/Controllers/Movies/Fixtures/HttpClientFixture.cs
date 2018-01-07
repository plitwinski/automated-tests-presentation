using Example.InMemoryDependencies.Core;
using Example.Modularity.Tests.Factories;
using Moq;
using System.Net.Http;

namespace Example.Modularity.Tests.Controllers.Movies.Fixtures
{
    public class HttpClientFixture
    {
        private readonly ServerFactory serverFactory;

        public Mock<IQueueClient> QueueClientMock { get; private set; }

        public HttpClientFixture()
        {
            serverFactory = new ServerFactory();
        }

        public HttpClientFixture AddMovies()
        {
            var context = new MoviesContextFactory("databaseNameFixture")
                .AddEntities(Data.Movie())
                .Create();
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
            return serverFactory.Create().CreateClient();
        }
    }
}
