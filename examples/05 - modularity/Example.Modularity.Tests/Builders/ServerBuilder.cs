using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;

namespace Example.Modularity.Tests.Builders
{
    public class ServerBuilder
    {
        private readonly IDictionary<Type, object> mockedServices;

        public ServerBuilder()
        {
            mockedServices = new Dictionary<Type, object>();
        }

        public ServerBuilder AddMock<TMock>(TMock mockedObject) where TMock : class
        {
            mockedServices.Add(typeof(TMock), mockedObject);
            return this;
        }

        public ServerBuilder AddMock<TMock>() where TMock : class
        {
            mockedServices.Add(typeof(TMock), Mock.Of<TMock>());
            return this;
        }


        public TestServer Build()
        {
            var webHostBuilder = new WebHostBuilder()
                                    .ConfigureServices(p => p.AddSingleton(mockedServices))
                                    .UseStartup<TestStartup>();

            return new TestServer(webHostBuilder);
        }
    }
}
