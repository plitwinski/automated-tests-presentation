using System;
using Example.InMemoryDependencies.Core;
using Example.InMemoryDependencies.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Example.InMemoryDependencies.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup()
        {
        }

        protected override void RegisterDatabase(IServiceCollection services)
        {
            services.AddDbContext<MoviesContext>(options => options.UseInMemoryDatabase());
        }

        protected override void RegisterCoreServices(IServiceCollection services)
        {
            base.RegisterCoreServices(services);
            services.AddTransient(_ => Mock.Of<IQueueClient>());
        }
    }
}
