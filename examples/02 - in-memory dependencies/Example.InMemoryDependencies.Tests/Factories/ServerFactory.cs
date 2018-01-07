using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Example.InMemoryDependencies.Tests.Factories
{
    public static class ServerFactory
    {
        public static TestServer CreateServer(IDictionary<Type, object> mockedServices)
        {
            var webHostBuilder = new WebHostBuilder()
                                    .ConfigureServices(p => p.AddTransient(_ => mockedServices))
                                    .UseStartup<TestStartup>();

            return new TestServer(webHostBuilder);
        }
    }
}
