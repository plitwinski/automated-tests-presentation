using Example.InMemoryDependencies.Core;
using Example.InMemoryDependencies.DataAccess;
using Example.InMemoryDependencies.Models;
using Example.Modularity.Tests.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Modularity.Tests.Controllers.Movies.Fixtures
{
    public class WebApplicationFixture<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private readonly Dictionary<Type, object> _typesToMock = new Dictionary<Type, object>();

        public Mock<IQueueClient> QueueClientMock { get; private set; }


        public WebApplicationFixture<TStartup> AddMovies(params MovieEntity[] movies)
        {
            var context = new MoviesContextBuilder("databaseNameFixture")
                .AddEntities(movies)
                .Build();

            _typesToMock.Add(context.GetType(), context);

            return this;
        }

        public WebApplicationFixture<TStartup> AddQueueClient()
        {
            QueueClientMock = new Mock<IQueueClient>();
            _typesToMock.Add(typeof(IQueueClient), QueueClientMock.Object);
            return this;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            { 
                foreach(var mock in _typesToMock)
                {
                    services.AddTransient(mock.Key, _ => mock.Value);
                }
            });
        }
    }
}
